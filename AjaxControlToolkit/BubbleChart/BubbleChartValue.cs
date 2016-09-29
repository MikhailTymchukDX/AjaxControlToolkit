using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjaxControlToolkit {

    public class BubbleChartValue {
        string _category = String.Empty;
        decimal _x;
        decimal _y;
        decimal _data;
        string _bubbleColor = String.Empty;

        /// <summary>
        /// A name of the category.
        /// </summary>
        /// <remarks>
        /// This property is required.
        /// </remarks>
        public string Category {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Provides X value for a particular BubbleChartValue.
        /// </summary>
        /// <remarks>
        /// This property is required.
        /// </remarks>
        public decimal X {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Provides Y value for a particular BubbleChartValue.
        /// </summary>
        /// <remarks>
        /// This property is required.
        /// </remarks>
        public decimal Y {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Provides Data value for a particular BubbleChartValue.
        /// </summary>
        /// <remarks>
        /// This property is required.
        /// </remarks>
        public decimal Data {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Sets the color of the bubble for a particular BubbleChartValue.
        /// </summary>
        public string BubbleColor {
            get { return _bubbleColor; }
            set { _bubbleColor = value; }
        }
    }

}

