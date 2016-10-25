Sys.Extended.UI.ZOrdering = function () {
};

Sys.Extended.UI.ZOrdering.prototype = {
    getOrderableElementAttributeName: function () {
        return 'data-act-orderable-element';
    },

    _getElementsWithAttribute: function (attribute) {
        var matchingElements = [];
        var allElements = document.getElementsByTagName('*');
        for(var i = 0, n = allElements.length; i < n; i++) {
            if(allElements[i].getAttribute(attribute) !== null)
                matchingElements.push(allElements[i]);
        }
        return matchingElements;
    },

    _findTopElement: function () {
        var actOrderableElements = this._getElementsWithAttribute(this.getOrderableElementAttributeName());
        var topZIndex = undefined;

        for(var i = 0; i < actOrderableElements.length; i++) {
            if(topZIndex == undefined)
                topZIndex = actOrderableElements[i].style.zIndex;

            if(actOrderableElements[i].style.zIndex > topZIndex)
                topZIndex = actOrderableElements[i].style.zIndex;
        }

        return topZIndex;
    },

    getTopZIndex: function (defaultZIndex) {
        var topZIndex = this._findTopElement();

        if(topZIndex == undefined)
            return defaultZIndex;
        
        return parseInt(topZIndex) + 1;
    }
};