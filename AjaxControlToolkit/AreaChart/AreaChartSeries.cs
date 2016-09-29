using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AjaxControlToolkit {

    public class AreaChartSeries {
        string _name = String.Empty;
        string _areaColor = String.Empty;

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
        /// Sets the color of area for a particular series.
        /// </summary>
        public string AreaColor {
            get { return _areaColor; }
            set { _areaColor = value; }
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

