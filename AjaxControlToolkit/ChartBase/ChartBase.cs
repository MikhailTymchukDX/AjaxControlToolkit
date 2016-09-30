using AjaxControlToolkit.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace AjaxControlToolkit {

    [RequiredScript(typeof(CommonToolkitScripts))]
    [Designer(typeof(ChartBaseDesigner))]
    public abstract class ChartBase : ScriptControlBase {
        protected string ContainerID;

        public ChartBase()
            : base(true, HtmlTextWriterTag.Div) {
        }

        protected bool IsDesignMode {
            get { return (HttpContext.Current == null); }
        }

        /// <summary>
        /// The width of the the chart.
        /// </summary>
        [ExtenderControlProperty]
        [DefaultValue(null)]
        [ClientPropertyName("chartWidth")]
        public string ChartWidth { get; set; }

        /// <summary>
        /// The height of the the chart.
        /// </summary>
        [ExtenderControlProperty]
        [DefaultValue(null)]
        [ClientPropertyName("chartHeight")]
        public string ChartHeight { get; set; }

        /// <summary>
        /// The title of the the chart.
        /// </summary>
        [ExtenderControlProperty]
        [DefaultValue("")]
        [ClientPropertyName("chartTitle")]
        public string ChartTitle { get; set; }

        /// <summary>
        /// The font color of the chart title.
        /// </summary>
        [ExtenderControlProperty]
        [DefaultValue("")]
        [ClientPropertyName("chartTitleColor")]
        public string ChartTitleColor { get; set; }

        [ExtenderControlProperty]
        [DefaultValue("PieChart")]
        [ClientPropertyName("theme")]
        public string Theme { get; set; }

        protected override void CreateChildControls() {
            var root = new HtmlGenericControl("div");
            root.ID = "_ParentDiv";
            root.Attributes.Add("style", "border-style:solid; border-width:1px;");
            Controls.Add(root);
            ContainerID = root.ClientID;
        }
    }

}
