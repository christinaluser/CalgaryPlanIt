using CalgaryPlanIt.Views;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalgaryPlanIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Trip> TripsList;
        public static List<Attraction> AttractionsList;

        public MainWindow()
        {
            InitializeComponent();
            Navigation.window = this;
            Navigation.NavigateTo(new Home());
            CreateTrips();
            CreateAttractions();
        }

        public void SwitchPage(Page newPage)
        {
            Main.Content = newPage;
        }

        /// <summary>
        /// Create a list of trips
        /// </summary>
        private void CreateTrips()
        {
            TripsList = new List<Trip>();
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(20),
                Name = "trip 1",
                NumAdults = 0,
                NumChildren = 0,
                NumTeens = 6
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(15),
                EndDate = DateTime.Now.AddDays(25),
                Name = "trip 2",
                NumAdults = 1,
                NumChildren = 3,
                NumTeens = 0
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(7),
                Name = "trip 3",
                NumAdults = 2,
                NumChildren = 1,
                NumTeens = 2
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(50),
                EndDate = DateTime.Now.AddDays(57),
                Name = "trip 4",
                NumAdults = 3,
                NumChildren = 1,
                NumTeens = 2
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(35),
                EndDate = DateTime.Now.AddDays(57),
                Name = "trip 5",
                NumAdults = 3,
                NumChildren = 1,
                NumTeens = 2,
                ItineraryItems = new List<ItineraryItem>() {
                    new ItineraryItem() { Name = "Calgary tower", PlannedStartDate = DateTime.Now.AddDays(36)},
                    new ItineraryItem() { Name = "Cactus Club Cafe", PlannedStartDate = DateTime.Now.AddDays(36)},
                    new ItineraryItem() { Name = "Cibo", PlannedStartDate = DateTime.Now.AddDays(36)},
                    new ItineraryItem() { Name = "Cactus Club Cafe pt 2", PlannedStartDate = DateTime.Now.AddDays(37)},
                    new ItineraryItem() { Name = "Cibo pt 2", PlannedStartDate = DateTime.Now.AddDays(38)},
                }
            });
        }

        /// <summary>
        /// TODO: create a big (like 10 of them?) list of attractions
        /// </summary>
        private void CreateAttractions()
        {
            AttractionsList = new List<Attraction>();
            AttractionsList.Add(new Attraction()
            {
                Name = "Banff 1-Day Tour from Calgary",
                Category = Category.Tours,
                Price = "from $146.74 CAD",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Tours | CalgaryPlanIt.Tag.Outdoor,
                Description = "From the quaint mountainside town of Banff, to the snow-dusted peaks, misty falls, and dramatic canyons around Banff " +
                              "as you explore this rugged slice of Alberta with an experienced guide. \n Starting your day with rolling by the steep " +
                              "slopes of Sulphur Mountain for the chance to take a gondola ride to the summit's sweeping viewpoints. (Banff Sulphur " +
                              "Gondola is optional) \n Enjoy some leisure time in downtown Banff and Snap photos of Johnston Canyon's limestone-carving " +
                              "creek. \n Fill your camera with misty shots of the tumbling rapids at Bow Falls, capture panoramic images of downtown " +
                              "Banff framed against a backdrop of craggy peaks from the Surprise Corner lookout, and keep your eyes peeled for the " +
                              "monolithic silhouette of glacier-carved Hoodoos.",
                Rating = 5,
                Duration = "10 hours",
                Address = "Ramada Plaza by Wyndham Calgary Downtown, 708 8 Ave SW, Calgary, AB T2P 1H2, Canada",
                Host = "Calgary Tours"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Downtown Calgary Smartphone Audio Walking Tour",
                Category = Category.Tours,
                Price = "from $13.00 CAD",
                Tags = CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.Tours | CalgaryPlanIt.Tag.TeenFriendly,
                Description = "Listen to a tour guide through your smartphone as you explore 3 exciting audio walking tours in Calgary! \n Over 75 total " +
                              "points of interest split up into three unique tours! Downtown Calgary Sightseeing Tour, Beltline District Tour, Bow River " +
                              "Trail Tour. Don't just see Calgary, hear what it has to say! Please note you do not have to complete all the tours in one " +
                              "day. The tours are now yours to use any date or time you like. The tours also come with a fun location based trivia game. " +
                              "After the commentary plays, look at your device and tap your guess at the answer. See your score as you go! Keep the " +
                              "professional tour guide experience, ditch the crowded tour groups, tight schedules and high prices. Live GPS map shows where " +
                              "you are and where to go next. As you get close, tour guide commentary will automatically play! Getting started is easy, " +
                              "just book the tour, download the app and go. Note: the number of travelers you select is the number of devices you can " +
                              "download the tour to",
                Rating = 0,
                Duration = "2-4 hours",
                Address = "City Hall, Calgary, AB T2G 0J2, Canada",
                Host = "GPS-Guided Audio Walking, Driving & Biking Tours"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Calgary Scavenger Hunt: Calgary Culture",
                Category = Category.Tours,
                Price = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible,
                Description = "Let’s Roam is the #1 app-led scavenger hunt company. Walk to all the best landmarks and hidden gems, answering trivia questions " +
                              "and solving challenges. Work with your team or compete against them, as you learn new facts and create memorable experiences. " +
                              "Let’s Roam Scavenger Hunts are great as an everyday activity, or for bachelorette parties, birthday parties, corporate team " +
                              "building events and more! Each player chooses an interactive role, with challenges varying by person. \n Try a Calgary photo " +
                              "scavenger hunt. On Calgary Culture you will be going on an enthralling tour around Calgary checking out the art, history, and " +
                              "culture along the way. You’ll see groups of friends competing to unearth fun facts, taking ridiculous pictures, and deciding " +
                              "who will be the next scavenger hunt champion! This is the ultimate small group activity. \n A few highlights of the scavenger " +
                              "hunt include: Olympic Plaza, The Bow, Stephen Avenue.",
                Rating = 4,
                Duration = "2 hours",
                Address = "228 8 Ave SE, Calgary, AB T2P 2M5, Canada",
                Host = "Let's Roam Calgary"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Glenbow Museum",
                Category = Category.MuseumsAndGalleries,
                Price = "from $10.00 CAD",
                ExternalLink = "https://www.glenbow.org/",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible,
                Description = "Glenbow is a place of confluence. A meeting point for people and communities to join a flow of ideas, knowledge, opinions and " +
                              "experiences. Our exceptional collection of art and historical objects represents the people and ideas that have shaped our region. " +
                              "That collection is complemented and contextualized by travelling exhibitions that explore historical and contemporary art, fashion, " +
                              "design and innovation from around the world. \n We are committed to expanding the accessibility and impact of art and culture in our " +
                              "community through exhibitions and programs that provide meaningful experiences for all visitors. \n As an independent, non-profit " +
                              "member-based organization, we generate over 60% of our operating revenue through fundraising, museum and program admissions, and " +
                              "memberships.",
                Rating = 4,
                Duration = "1-2 hours",
                Address = "Glenbow Museum, 130 9th Ave SE Calgary, AB T2G 0P3, Canada"
            });

        }
    }
}
