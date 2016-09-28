using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AjaxControlToolkit {

    public class BarChartSeries {
        string _name = String.Empty;
        string _barColor = string.Empty;

        /// <summary>
        /// A name of the series.
        /// </summary>
        /// <remarks>
        /// This property is required.
        /// </remarks>
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Sets the color of bar for a particular series.
        /// </summary>
        public string BarColor {
            get { return _barColor; }
            set { _barColor = value; }
        }

        /// <summary>
        /// Provides data for a particular series.
        /// </summary>
        /// <remarks>
        /// This property is required.
        /// </remarks>
        [TypeConverter(typeof(DataConverter<decimal>))]
        public decimal[] Data { get; set; }
    }

}

