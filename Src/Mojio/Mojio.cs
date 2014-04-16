using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Mojio
{
    /// <summary>
    /// Device
    /// </summary>
    [Observable]
    public partial class Mojio : GuidEntity, IOwner
    {
        /// <summary>
        /// owner id
        /// </summary>
        [Display (Name = "Owner")]
        [Observable(typeof(User))]
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// device IMEI number
        /// </summary>
        public string Imei { get; set; }

        /// <summary>
        /// most recent communication time
        /// </summary>
        public DateTime LastContactTime { get; set; }

        /// <summary>
        /// Current vehicle ID
        /// </summary>
        [Observable(typeof(Vehicle))]
        public Guid? VehicleId { get; set; }
    }
}
