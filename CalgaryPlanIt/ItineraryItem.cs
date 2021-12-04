using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    public class ItineraryItem : Attraction
    {

        public ItineraryItem() { }

        public ItineraryItem(Attraction attraction)
        {
            Name = attraction.Name;
            Address = attraction.Address;
            StartDate = attraction.StartDate;
            EndDate = attraction.EndDate;
            Price = attraction.Price;
            Description = attraction.Description;
            Rating = attraction.Rating;
            Reviews = attraction.Reviews;
            Category = attraction.Category;
            Tags = attraction.Tags;
            ExternalLink = attraction.ExternalLink;
            Duration = attraction.Duration;
            Host = attraction.Host;
            CanvasLeftValue = attraction.CanvasLeftValue;
            CanvasTopValue = attraction.CanvasTopValue;
        }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set;}

        public string GetTimeString()
        {
            return PlannedStartDate.ToString("t") + " - " + PlannedEndDate.ToString("t");
        }
    }
}
