﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    public class ItineraryItem : Attraction
    {
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set;}
    }
}
