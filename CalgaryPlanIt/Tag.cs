using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    [Flags]
    public enum Tag
    {
        None = 0,
        KidFriendly = 1,
        TeenFriendly = 2,
        AdultOnly = 4,
        PetFriendly = 8,
        Outdoor = 16,
        Indoor = 32,
        OpenToPublic = 64,
        Tours = 128,
        FoodAndDrink = 256,
        Shopping = 512,
        ConcertsAndShows = 1024,
        Nightlife = 2048,
        Museums = 4096,
        ClassesAndWorkshops = 8192,
        AmusementParks = 16384
    }
}
