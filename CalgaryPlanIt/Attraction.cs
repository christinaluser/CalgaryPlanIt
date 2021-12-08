using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    public class Attraction : IComparable<Attraction>
    {
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Price { get; set; }
        public string Description { get; set; } = "";
        public int Rating { get; set; }
        public List<Review>? Reviews { get; set; }
        public Category Category { get; set; }
        public Tag Tags { get; set; }
        public string? ExternalLink { get; set; }
        public string? Duration { get; set; }
        public string? Host { get; set; }
        //for map:
        public double CanvasTopValue { get; set; } = 0;
        public double CanvasLeftValue { get; set; } = 0;
        public List<String>? ImageSourceName { get; set; }

        public int CompareTo(Attraction attr)
        {
            // A null value means that this object is greater.
            if (attr == null || attr.Price == "")
            {
                return 1;
            }

            else
            {
                var tprice = this.Price.Split(" ")[0];
                tprice.Remove(0, 1);

                var aprice = attr.Price.Split(" ")[0];
                aprice.Remove(0, 1);

                return double.Parse(tprice).CompareTo(double.Parse(aprice));
            }
        }


    }
}
