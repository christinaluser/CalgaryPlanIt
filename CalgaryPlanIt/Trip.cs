using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    public class Trip
    {
        public string Name { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ItineraryItem>? ItineraryItems { get; set; }
        public int NumAdults { get; set; }
        public int NumTeens { get; set; }
        public int NumChildren { get; set; }
        public string Notes { get; set; } = "";
        public List<string>? SharedEmails { get; set; }

        public string TripDatesToString()
        {
            return StartDate.ToString() + " - " + EndDate.ToString();
        }
    }
}
