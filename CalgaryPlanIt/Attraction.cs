using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    public class Attraction
    {
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = "";
        public int Rating { get; set; }
        public List<Review>? Reviews { get; set; }   
        public Category Category { get; set; }
        public Tag Tags { get; set; }

        //location?
        //photos?

    }
}
