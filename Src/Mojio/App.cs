﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojio
{
    /// <summary>
    /// 
    /// </summary>
    ///         
    [Observable]
    public partial class App : GuidEntity
    {
        public override EntityType Type
        {
            get { return EntityType.App; }
        }

        /// <summary>
        /// app name
        /// </summary>
        [Display(Name = "Display Name")]
        [StringLength(32, MinimumLength=5, ErrorMessage="Name has to be between 5 and 32 characters")]
        public string Name { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string Description { get; set; }

        [DefaultSort]
        /// <summary>
        /// record creation timestamp
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// total number of downloads
        /// </summary>
        public int? Downloads { get; set; }

        /// <summary>
        /// Valid redirect uris
        /// </summary>
        public string RedirectUris { get; set; }

        public AppTypes ApplicationType { get; set; }
    }

    public enum AppTypes { 
        web,
        installed
    }
}
