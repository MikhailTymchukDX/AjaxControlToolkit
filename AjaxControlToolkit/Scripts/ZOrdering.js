Sys.Extended.UI.ZOrdering = function () {
};

Sys.Extended.UI.ZOrdering.prototype = {
    getControlTypeAttributeName: function () {
        return 'data-act-control-type';
    },

    getElementsWithAttribute: function (attribute) {
        var matchingElements = [];
        var allElements = document.getElementsByTagName('*');
        for(var i = 0, n = allElements.length; i < n; i++) {
            if(allElements[i].getAttribute(attribute) !== null)
                matchingElements.push(allElements[i]);
        }
        return matchingElements;
    },

    findTopElement: function () {
        var actOrderableElements = this.getElementsWithAttribute(this.getControlTypeAttributeName());
        var topZIndex = undefined;

        for(var i = 0; i < actOrderableElements.length; i++) {
            if(topZIndex == undefined)
                topZIndex = actOrderableElements[i].style.zIndex;

            if(actOrderableElements[i].style.zIndex > topZIndex)
                topZIndex = actOrderableElements[i].style.zIndex;
        }

        return topZIndex;
    }
};