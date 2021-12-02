using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalgaryPlanIt
{
    public enum Category
    {
        Tours,
        Promotions,
        Nearby,
        Popular,
        Events,
        MuseumsAndGalleries,
        ShoppingMalls,
        FoodAndDrink,
        NatureAndWildlife,
        Nightlife,
        Sports,
        Parks,
        Attractions,
        Seasonal
    }

    public static class CategoryExtensions
    {
        public static string ToFriendlyString(this Category category)
        {
            switch (category)
            {
                case Category.MuseumsAndGalleries:
                    return "Musuems & Galleries";
                case Category.ShoppingMalls:
                    return "Shopping Malls";
                case Category.FoodAndDrink:
                    return "Food & Drink";
                case Category.NatureAndWildlife:
                    return "Nature & Wildlife";
                default:
                    return category.ToString();
            }
        }
    }
}
