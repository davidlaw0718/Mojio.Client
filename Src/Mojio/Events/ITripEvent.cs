﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojio.Events
{
    public interface ITripEvent
    {
        Guid? TripId { get; set; }
    }
}