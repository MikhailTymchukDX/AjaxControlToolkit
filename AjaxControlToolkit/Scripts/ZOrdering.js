Sys.Extended.UI.ZOrdering = function () {
};

Sys.Extended.UI.ZOrdering.prototype = {
    getAllElementsWithAttribute: function (attribute) {
        var matchingElements = [];
        var allElements = document.getElementsByTagName('*');
        for(var i = 0, n = allElements.length; i < n; i++) {
            if(allElements[i].getAttribute(attribute) !== null)
                matchingElements.push(allElements[i]);
        }
        return matchingElements;
    },

    findTopElement: function (controlType) {
        var actElements = this.getAllElementsWithAttribute('data-act-control-type');
        var backgrounds = [];

        for(var i = 0; i < actElements.length; i++) {
            if(actElements[i].getAttribute('data-act-control-type') ==  controlType)
                backgrounds.push(actElements[i]);
        }

        var backgroundsZindex = {};

        var topZIndex = undefined;

        for(var i = 0; i < backgrounds.length; i++) {
            if(topZIndex == undefined)
                topZIndex = backgrounds[i].style.zIndex;

            if(backgrounds[i].style.zIndex > topZIndex) {
                topZIndex = backgrounds[i].style.zIndex;
            }
        }

        return topZIndex;
    }
};