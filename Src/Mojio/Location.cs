﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mojio
{
    /// <summary>
    /// Location
    /// </summary>
    public partial class Location : ICloneable
    {
        /// <summary>
        /// latitude coordinate
        /// </summary>
         public double Lat { get; set; }

        /// <summary>
        /// longitiude coordinate
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// Was the data obtained from a locked gps reading.
        /// </summary>
        public bool FromLockedGPS { get; set; }

        /// <summary>
        /// possible error area of the lat lon data.  0 is undiluted.
        /// </summary>
        public double Dilution { get; set; }

        /// <summary>
        /// Gets a value indicating whether [is valid].
        /// </summary>
        /// <value><c>true</c> if [is valid]; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                return !(double.IsNaN(Lat) || double.IsNaN(Lng));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        public Location()
        {
            Lat = double.NaN;
            Lng = double.NaN;
        }

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
