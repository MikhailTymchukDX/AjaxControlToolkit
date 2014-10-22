﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace AjaxControlToolkit {

    // 1) It performs the hookup between an Extender (server) control and the behavior it instantiates
    // 2) It manages interacting with the ScriptManager to get the right scripts loaded
    // 3) It adds some debugging features like ValidationScript and ScriptPath
    [Themeable(true)]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [ClientScriptResource(null, Constants.BaseScriptName)]
    public abstract class ExtenderControlBase : ExtenderControl, IControlResolver {
        private Dictionary<string, Control> _findControlHelperCache = new Dictionary<string, Control>();
        private string _clientState;
        private bool _enableClientState;
        private bool _isDisposed;
        private bool _renderingScript;

        // Called when the ExtenderControlBase fails to locate a control referenced by a TargetControlID.
        // In this event, user code is given an opportunity to find the control.        
        public event ResolveControlEventHandler ResolveControlID;

        public bool Enabled {
            get {
                if(_isDisposed)
                    return false;

                return GetPropertyValue("Enabled", true);
            }
            set {
                SetPropertyValue("Enabled", value);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ClientState {
            get { return _clientState; }
            set { _clientState = value; }
        }

        [ExtenderControlProperty()]
        [ClientPropertyName("id")]
        public string BehaviorID {
            get {
                string id = GetPropertyValue("BehaviorID", "");
                return (string.IsNullOrEmpty(id) ? ClientID : id);
            }
            set {
                SetPropertyValue("BehaviorID", value);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EnableClientState {
            get { return _enableClientState; }
            set { _enableClientState = value; }
        }

        // The type of the client component - e.g. "ConfirmButtonBehavior"
        protected virtual string ClientControlType {
            get {
                ClientScriptResourceAttribute attr = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];

                return attr.ComponentType;
            }
        }

        public Control ResolveControl(string controlId) {
            return FindControl(controlId);
        }

        public override Control FindControl(string id) {

            // Use FindControlHelper so that more complete searching and OnResolveControlID will be used
            return FindControlHelper(id);
        }

        protected Control TargetControl {
            get {
                return FindControlHelper(TargetControlID);
            }
        }

        // This helper automates locating a control by ID.
        // It calls FindControl on the NamingContainer, then the Page.  If that fails, it fires the resolve event.
        protected Control FindControlHelper(string id) {
            Control c = null;
            if(_findControlHelperCache.ContainsKey(id)) {
                c = _findControlHelperCache[id];
            } else {
                c = base.FindControl(id);  // Use "base." to avoid calling self in an infinite loop
                Control nc = NamingContainer;
                while((null == c) && (null != nc)) {
                    c = nc.FindControl(id);
                    nc = nc.NamingContainer;
                }
                if(null == c) {
                    // Note: props MAY be null, but we're firing the event anyway to let the user
                    // do the best they can
                    ResolveControlEventArgs args = new ResolveControlEventArgs(id);

                    OnResolveControlID(args);
                    c = args.Control;

                }
                if(null != c) {
                    _findControlHelperCache[id] = c;
                }
            }
            return c;
        }

        // Fired when the extender can not locate it's target control. This may happen if the 
        // target control is in a different naming container.
        // By handling this event, user code can locate the target and return it via the ResolveControlEventArgs.Control property.
        protected virtual void OnResolveControlID(ResolveControlEventArgs e) {
            if(ResolveControlID != null) {
                ResolveControlID(this, e);
            }
        }

        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl) {
            if(!Enabled || !targetControl.Visible)
                return null;

            EnsureValid();

            ScriptBehaviorDescriptor descriptor = new ScriptBehaviorDescriptor(ClientControlType, targetControl.ClientID);

            // render the attributes for this element
            RenderScriptAttributes(descriptor);

            // render profile bindings
            //RenderProfileBindings(descriptor);

            // render any child scripts we need to.
            RenderInnerScript(descriptor);

            return new List<ScriptDescriptor>(new ScriptDescriptor[] { descriptor });
        }

        // Called during rendering to give derived classes a chance to validate their properties
        public virtual void EnsureValid() {
            CheckIfValid(true);
        }

        protected virtual bool CheckIfValid(bool throwException) {
            bool valid = true;
            foreach(PropertyDescriptor prop in TypeDescriptor.GetProperties(this)) {
                // If the property is tagged with RequiredPropertyAttribute, but doesn't have a value, throw an exception
                if((null != prop.Attributes[typeof(RequiredPropertyAttribute)]) && ((null == prop.GetValue(this)) || !prop.ShouldSerializeValue(this))) {
                    valid = false;
                    if(throwException) {
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "{0} missing required {1} property value for {2}.", GetType().ToString(), prop.Name, ID), prop.Name);
                    }
                }
            }
            return valid;
        }

        // Walks each of the properties in the TargetProperties object and renders script for them.
        protected virtual void RenderScriptAttributes(ScriptBehaviorDescriptor descriptor) {
            try {
                _renderingScript = true;
                ComponentDescriber.DescribeComponent(this, descriptor, this, this);
            } finally {
                _renderingScript = false;
            }
        }

        // Allows generation of markup within the behavior declaration in XML script
        protected virtual void RenderInnerScript(ScriptBehaviorDescriptor descriptor) {
        }

        protected override IEnumerable<ScriptReference> GetScriptReferences() {
            if(Enabled) {
                return EnsureScripts();
            }
            return null;
        }

        // Walks the various script types and prepares to notify the ScriptManager to load them.
        // 1) Required scripts such as ASP.NET AJAX Scripts or other components
        // 2) Scripts for this Extender/Behavior
        internal IEnumerable<ScriptReference> EnsureScripts() {
            return new Localization().GetLocalizationScriptReferences()
                .Concat(ResourceHelper.GetScriptReferences(GetType()));
        }

        protected V GetPropertyValue<V>(string propertyName, V nullValue) {
            if(ViewState[propertyName] == null) {
                return nullValue;
            }
            return (V)ViewState[propertyName];
        }

        protected void SetPropertyValue<V>(string propertyName, V value) {
            ViewState[propertyName] = value;
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);
            var localeKey = new Localization().GetLocaleKey();

            if (Enabled && !String.IsNullOrEmpty(localeKey)) {
                var script = String.Format(@"Sys.Extended.UI.Localization.SetLocale(""{0}"");", localeKey);
                Page.ClientScript.RegisterStartupScript(GetType(), "f93b988bab7e44ffbcff635ee599ade2", script, true);
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            RegisterCssReferences();
        }

        void RegisterCssReferences() {
            // Add the link to the page header instead of inside the body which is not xhtml compliant
            HtmlHead header = Page.Header;

            foreach(var styleSheet in ResourceHelper.GetCssUrls(this)) {
                // It would be nice to add the required header here, but it's too late in the page
                // lifecycle to be modifying the Page.Controls collection - throw an informative
                // exception instead and let the page author make the simple change.
                if(header == null)
                    throw new NotSupportedException("This page is missing a HtmlHead control which is required for the CSS stylesheet link that is being added. Please add <head runat=\"server\" />.");

                var linkExists = false;
                foreach(var headerControl in header.Controls) {
                    var headerLink = headerControl as HtmlLink;
                    if(headerLink != null && styleSheet.Equals(headerLink.Href, StringComparison.OrdinalIgnoreCase)) {
                        linkExists = true;
                        break;
                    }
                }

                if(linkExists)
                    break;

                // Add to HEAD even if IsInAsyncPostBack, to check duplicates on the server side
                var link = new HtmlLink();
                link.Href = styleSheet;
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                header.Controls.Add(link);

                // ASP.NET AJAX doesn't currently send a new head element down during an async postback,
                // so we do the same thing on the client by registering the appropriate script for after
                // the update.
                var scriptManager = ScriptManager.GetCurrent(Page);
                if(scriptManager == null)
                    throw new InvalidOperationException("A ScriptManager is required on the page to use ASP.NET AJAX Script Components.");

                if(!scriptManager.IsInAsyncPostBack)
                    break;

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "RegisterCssReferences",
                    "if (window.__ExtendedControlCssLoaded == null || typeof window.__ExtendedControlCssLoaded == 'undefined') {" +
                    "    window.__ExtendedControlCssLoaded = new Array();" +
                    "}" +
                    "var controlCssLoaded = window.__ExtendedControlCssLoaded; " +
                    "var head = document.getElementsByTagName('HEAD')[0];" +
                    "if (head && !Array.contains(controlCssLoaded,'" + styleSheet + "')) {" +
                        "var linkElement = document.createElement('link');" +
                        "linkElement.type = 'text/css';" +
                        "linkElement.rel = 'stylesheet';" +
                        "linkElement.href = '" + styleSheet + "';" +
                        "head.appendChild(linkElement);" +
                        "controlCssLoaded.push('" + styleSheet + "');" +
                    "}"
                    , true);
            }
        }
    }

}
