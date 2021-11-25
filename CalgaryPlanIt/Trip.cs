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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ItineraryItem>? ItineraryItems { get; set; }
        public int NumAdults { get; set; }
        public int NumTeens { get; set; }
        public int NumChildren { get; set; }
        public string Notes { get; set; } = "";
        public List<string>? SharedEmails { get; set; }
        public bool IsArchived { get; set; } = false;

        public string TripDatesToString()
        {
            // default value of DateTime type is Jan 1 0001 00:00:00 so if it that just return empty
            if (StartDate == DateTime.MinValue || EndDate == DateTime.MinValue)
            {
                return string.Empty;
            }

            // set date format. only show both months and years if they are different
            string startDateFormat = "MMM dd";
            string endDateFormat = "dd, yyyy";
            if (StartDate.Year != EndDate.Year)
            {
                startDateFormat += ", yyyy";
            }
            if (StartDate.Month != EndDate.Month)
            {
                endDateFormat = "MMM " + endDateFormat;
            }

            // return start date - end date
            return StartDate.ToString(startDateFormat) + " - " + EndDate.ToString(endDateFormat);
        }
    }
}
