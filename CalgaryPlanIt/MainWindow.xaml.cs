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
        public static List<Lis> ListofLists;

        public MainWindow()
        {
            InitializeComponent();
            Navigation.window = this;
            Navigation.NavigateTo(new Home());
            CreateTrips();
            CreateAttractions();
            CreateList();
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
                Name = "bbbbb",
                NumAdults = 0,
                NumChildren = 0,
                NumTeens = 6
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(15),
                EndDate = DateTime.Now.AddDays(25),
                Name = "zzzzzz",
                NumAdults = 1,
                NumChildren = 3,
                NumTeens = 0
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(7),
                Name = "aaaaaa",
                NumAdults = 2,
                NumChildren = 1,
                NumTeens = 2
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(50),
                EndDate = DateTime.Now.AddDays(57),
                Name = "ccccc",
                NumAdults = 3,
                NumChildren = 1,
                NumTeens = 2
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddDays(3),
                Name = "aa",
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
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(-1),
                Name = "123213",
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
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(57),
                Name = "ddddd",
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
                Name = "Calgary City Sightseeing Tour",
                Category = Category.Tours,
                Price = "from $214.36 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.Tours,
                Description = "Your vintage sidecar motorcycle tour takes you through and around the heart of the city. This is a fully narrated tour with one stop along the way for photos. This is a great way to see Calgary from a unique and invigorating perspective. This is a bucket list must do. If you enjoy an open air sensory experience, look no further. We can offer this trip in French, Portuguese, Swiss German, Mandarin and English.",
                Rating = 5,
                Duration = "60-70 minutes",
                Address = "Traveller pickup is offered.",
                Host = "Rocky Mountain Sidecar Adventures"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Creepy Calgary Ghost Tour",
                Category = Category.Tours,
                Price = "from $382.83 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.Tours | CalgaryPlanIt.Tag.Indoor,
                Description = "Join us on this 3 hour spooky sidecar adventure exploring the ghosts, spirits and dark stories of Calgary's past. Stop at local taverns for a beverage and a ghost story.  Travel through the most historic parts of Calgary and experience the talents of a professional storyteller. Adults only 18+.",
                Rating = 5,
                Duration = "3 hours",
                Address = "Calgary Tower, 101 9 Ave SW, Calgary, AB T2P 1J9, Canada",
                Host = "Rocky Mountain Sidecar Adventures"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Drumheller (Dinosaur Valley) & Horseshoe Canyon 1-Day Tour",
                Category = Category.Tours,
                Price = "from $146.74 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.Tours | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.KidFriendly,
                Description = "Uncover your inner archaeologist as you look for fossils and learn about the prehistoric creatures that roamed the hills and grasslands outside of Alberta's capital. Visit Drumheller on this full-day excursion, explore rugged canyonlands, and get-up close looks at monolothic hoodoo rock formations.",
                Rating = 5,
                Duration = "10 hours",
                Address = "Ramada Plaza by Wyndham Calgary Downtown",
                Host = "Calgary Tours"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Calgary Clue Solving Adventure - Riotous Roundup",
                Category = Category.Tours,
                Price = "from $61.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.Tours | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.KidFriendly,
                Description = "Riotous Roundup is an immersive clue solving adventure for teams of 2 - 4 people, priced per team. Discover the popular sights, hidden gems and spots even the locals don’t know about o the +15 system in Calgary on a 2-3 hour adventure. You and your team work together to solve a series of clues & puzzles delivered to an App on your phone.Help search for all the missing animals, by solving puzzles, with some interactions in local businesses and visiting favourite spots around town. You can start any time between 10am – 4pm any day, and finish before 6 p.m.The puzzle of an escape room, the fun of a scavenger hunt, and the thrill of the amazing race.So! Much! Fun!",
                Rating = 5,
                Duration = "1 hours 30 minutes",
                Address = "304 8 Ave SW, Calgary, AB T2P 1C1, Canada",
                Host = "Mystery Towns"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "The Best of Calgary Walking Tour",
                Category = Category.Tours,
                Price = "from $606.06 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.Tours | CalgaryPlanIt.Tag.KidFriendly,
                Description = "Together with the professional guide, you will visit the most charming places in the city. You will have a chance to explore the city, while hearing fascinating facts and legends. Do you consider yourself an animal lover? How about festivals? The 10-day Calgary Stampede can trace its roots back to the 1880s and is the highlight of Calgary's summer, cementing this Alberta city's reputation as Canada's 'Stampede City'. You will be surprised how many stories are hidden in the streets, buildings and corners of Calgary.Your charming guide will tell you what is special and unique about living in this city.Perfect for those who are visiting the city for the first time and want to get the most of it!",
                Rating = 5,
                Duration = "2 hours",
                Address = "1900 Heritage Dr, Calgary, AB T2V 2X3, Canada, By the entrance of Heritage Park Historical Village",
                Host = "Fun Top Fun Canada"
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Heritage Park Historical Village",
                Category = Category.Popular,
                Price = "from $9.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor,
                Description = "When you have the choice of everything from a thundering steam train to antique midway rides to beautifully preserved heritage buildings and homesteads, deciding what to do first isn't easy. Located in Calgary, Alberta, Heritage Park is Canada's largest living history museum, with hundreds of exhibits, rides, shops, restaurants and daily demonstrations and activities to keep the young and the young-at-heart captivated in the past. No two days at the Park are the same, so come back as often as you like for a history lesson you won't find in any textbook. Our operating season is from mid-May to early October each year.",
                Rating = 4,
                Duration = "More than 3 hours",
                Address = "1900 Heritage Dr SW, Calgary, Alberta T2V 2X3 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Heritage Park Historical Village",
                Category = Category.Parks,
                Price = "from $9.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor,
                Description = "When you have the choice of everything from a thundering steam train to antique midway rides to beautifully preserved heritage buildings and homesteads, deciding what to do first isn't easy. Located in Calgary, Alberta, Heritage Park is Canada's largest living history museum, with hundreds of exhibits, rides, shops, restaurants and daily demonstrations and activities to keep the young and the young-at-heart captivated in the past. No two days at the Park are the same, so come back as often as you like for a history lesson you won't find in any textbook. Our operating season is from mid-May to early October each year.",
                Rating = 4,
                Duration = "More than 3 hours",
                Address = "1900 Heritage Dr SW, Calgary, Alberta T2V 2X3 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Heritage Park Historical Village",
                Category = Category.Promotions,
                Price = "from $9.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor,
                Description = "When you have the choice of everything from a thundering steam train to antique midway rides to beautifully preserved heritage buildings and homesteads, deciding what to do first isn't easy. Located in Calgary, Alberta, Heritage Park is Canada's largest living history museum, with hundreds of exhibits, rides, shops, restaurants and daily demonstrations and activities to keep the young and the young-at-heart captivated in the past. No two days at the Park are the same, so come back as often as you like for a history lesson you won't find in any textbook. Our operating season is from mid-May to early October each year.",
                Rating = 4,
                Duration = "More than 3 hours",
                Address = "1900 Heritage Dr SW, Calgary, Alberta T2V 2X3 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "The Calgary Zoo",
                Category = Category.Parks,
                Price = "from $19.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor,
                Description = "From the splendor of the Rocky Mountains to the heart of Destination Africa, The Calgary Zoo takes you to see almost 900 animals from around the world. Trek through the gorilla’s rainforest, safari over the Savannah to watch the hippos swim, or climb the Canadian Wilds to see a grizzly bear. Cool off and get beak to nose with 4 species of penguins in our popular Penguin Plunge or visit endangered red pandas and amur tigers in Eurasia! Wind down your trip with a walk through our fragrant Dorothy Harvie Gardens or sit with the butterflies in ENMAX Conservatory. You'll love knowing that every visit helps protect endangered animals at home and in the wild. NEW in 2017 is the unique and immersive permanent Land of Lemurs exhibit. Coming in 2018 are the iconic giant pandas!",
                Rating = 4,
                Duration = "More than 3 hours",
                Address = "1300 Zoo Rd NE, Calgary, Alberta T2E 7V6 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "The Calgary Zoo",
                Category = Category.Parks,
                Price = "from $19.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor,
                Description = "From the splendor of the Rocky Mountains to the heart of Destination Africa, The Calgary Zoo takes you to see almost 900 animals from around the world. Trek through the gorilla’s rainforest, safari over the Savannah to watch the hippos swim, or climb the Canadian Wilds to see a grizzly bear. Cool off and get beak to nose with 4 species of penguins in our popular Penguin Plunge or visit endangered red pandas and amur tigers in Eurasia! Wind down your trip with a walk through our fragrant Dorothy Harvie Gardens or sit with the butterflies in ENMAX Conservatory. You'll love knowing that every visit helps protect endangered animals at home and in the wild. NEW in 2017 is the unique and immersive permanent Land of Lemurs exhibit. Coming in 2018 are the iconic giant pandas!",
                Rating = 4,
                Duration = "More than 3 hours",
                Address = "1300 Zoo Rd NE, Calgary, Alberta T2E 7V6 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "The Calgary Zoo",
                Category = Category.NatureAndWildlife,
                Price = "from $19.95 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor,
                Description = "From the splendor of the Rocky Mountains to the heart of Destination Africa, The Calgary Zoo takes you to see almost 900 animals from around the world. Trek through the gorilla’s rainforest, safari over the Savannah to watch the hippos swim, or climb the Canadian Wilds to see a grizzly bear. Cool off and get beak to nose with 4 species of penguins in our popular Penguin Plunge or visit endangered red pandas and amur tigers in Eurasia! Wind down your trip with a walk through our fragrant Dorothy Harvie Gardens or sit with the butterflies in ENMAX Conservatory. You'll love knowing that every visit helps protect endangered animals at home and in the wild. NEW in 2017 is the unique and immersive permanent Land of Lemurs exhibit. Coming in 2018 are the iconic giant pandas!",
                Rating = 4,
                Duration = "More than 3 hours",
                Address = "1300 Zoo Rd NE, Calgary, Alberta T2E 7V6 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Modern Steak",
                Category = Category.FoodAndDrink,
                Price = "$24 - $49 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "Four key pieces separate us from a traditional steakhouse. 1) We only serve ranch specific Alberta Beef. That means we don’t serve beef from anywhere else on the planet. We know our farmers and ranchers personally and respect the hard work they put into produce our beef. We like our beef to be, hormone & antibiotic free and pasture raised. Happy cattle make for better steaks. 2) We are one of just a handful of steakhouses in Canada that serves, Grass-fed, Grain-fed, Wet-aged, Dry-aged and Waygu beef 365 days a year. Our beef is always fresh never frozen. 3) We use an 1800o Infrared Grill to cook your steaks. Our grill stays at very consistent high heat versus a traditional open flame grill.",
                Rating = 4,
                Duration = "",
                Address = "107 10a St NW Located on Stephen Ave and Centre Street, Calgary, Alberta T2N 4M7 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Gaga Pizza",
                Category = Category.FoodAndDrink,
                Price = "$4 CAD - $23 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "Pizzeria Gaga comes from the nickname given to Chef Safeta Zeljic by her husband. It started out as draga (darling) and over time became Gaga. We invite you to taste our food, enjoy our company and browse our selection of imported goods. We guarantee that you will leave with a satisfied tummy and a smile on your face. Pizzeria Gaga is proud to support Calgary community events and cater to our beloved downtown core.",
                Rating = 5,
                Duration = "",
                Address = "1236 12 Ave SW, Calgary, Alberta T3C 0P3 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "SS106 Aperitivo Bar",
                Category = Category.FoodAndDrink,
                Price = "$4 CAD - $23 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "SS106 Aperitivo Bar was established in 2016. Full Italian bar with an Italian kitchen. Owner Domenico Spadafora opened his establishment to bring a modern style of Italian dining with authentic flavours to YYC. SS106 Aperitivo Bar is built around a social environment while still being able to eat authentic Italian dishes. Full aperitivo, cocktail & wine menu. Starters, salads, boards, platters, pastas, pizzas and entrees all available.",
                Rating = 5,
                Duration = "",
                Address = "824D Edmonton Trail NE Parking on The Rear, Calgary, Alberta T2E 3J6 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Vero Bistro Moderne",
                Category = Category.FoodAndDrink,
                Price = "$10 CAD - $45 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink ,
                Description = "Vero = True in Italian. 'Staying ‘true’ to my passion which is cooking' \n 'What is really important to me is the flavour of the food, it has to taste great.., and that’s what I strive for everyday.' \n – Chef Jenny –",
                Rating = 5,
                Duration = "",
                Address = "209 10 St NW, Calgary, Alberta T2N 1V5 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Santorini Greek Taverna",
                Category = Category.FoodAndDrink,
                Price = "$19 CAD - $38 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "SINCE 1986 \n WE’RE PROUD TO HAVE SERVED CALGARY FOR OVER 30 YEARS! OVER THE YEARS WE’VE WORKED TO ENSURE THAT ALL CALGARIANS RECEIVE THE VERY BEST; IN SERVICE, QUALITY OF INGREDIENTS AND WARM INVITING AMBIANCE. \n The story of Santorini began well over 30 years ago, when a young Andreas Nicolaides immigrated to Canada from the Mediterranean island of Cyprus.While in Vancouver, Andreas met Maria(also from Cyprus) and after a few short months, the two were engaged to be married. After travelling back home to Cyprus for their wedding, the young couple moved to Calgary for a new beginning. Their first venture was Castle Pizza on Elbow Drive, with several other business partners.After several years at Castle Pizza, the couple, now with three small children, decided to become their own businesses.They bought a small pub on Centre Street and transformed it into a fledgling Greek restaurant. After an incredible 30 years, the restaurant is still turning out incredible dishes and now, the young couple enjoy spending time at the restaurant with their three(soon to be four) grandchildren.",
                Rating = 5,
                Duration = "",
                Address = "1502 Centre St NE, Calgary, Alberta T2E 2R9 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Rouge",
                Category = Category.FoodAndDrink,
                Price = "$40 CAD - $64 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "At Rouge, the foundation of our cuisine begins with the best ingredients.We partner with local food growers and utilize our onsite garden to bring you exceptional handcrafted dishes.",
                Rating = 5,
                Duration = "",
                Address = "1502 Centre St NE, Calgary, Alberta T2E 2R9 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "The Himalayan",
                Category = Category.FoodAndDrink,
                Price = "$40 CAD - $64 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "Nepali Cuisine",
                Rating = 5,
                Duration = "",
                Address = "3218 17 Ave SW, Calgary, Alberta T3E 0B3 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Ten Foot Henry",
                Category = Category.FoodAndDrink,
                Price = "$12 CAD - $34 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "Ten Foot Henry is named after a long time Calgary icon. Our namesake, Ten Foot Henry, is quite literally a 10 foot replica of the famed 1930 comic strip character, Henry. This cartoon boy became larger than life as a muse for Calgary’s creative community in the 1980s. Ten Foot Henry has been many places in his 37 years, including an underground night club named Ten Foot Henry’s in the early 1980s and the iconic Night Gallery at 1209 1st Street SW where he resided for all 19 years its doors were open. Operating partners Stephen Smee and Aja Lapointe have borrowed the original Ten Foot Henry from the One Yellow Rabbit Theatre and have returned him to his longest running address on 1st Street, where his food-filled adventure continues.Ten Foot Henry is an all - day - restaurant that offers a fresh vegetable - anchored menu and fun, family style dining.We bridge the gap between what you should be eating and what you really want to eat.We are open every day from 11am - 11pm.We offer a brunch card in addition to our regular menu on Saturdays and Sundays until 2pm.While we operate at a reduced capacity,reservations are encouraged.",
                Rating = 5,
                Duration = "",
                Address = "1209 1 St SW, Calgary, Alberta T2R 0V3 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Calgary Tower",
                Category = Category.Popular,
                Price = "$9 CAD - $18 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "This place is temporarily closed. Located 191 metres above the downtown core, the Calgary Tower offers the best view in the city and is a must-see on any visitor's itinerary. On the Observation Deck you'll experience a spectacular 360° view of the bustling city, the majestic Rocky Mountains, the foothills, and the prairies.",
                Rating = 4,
                Duration = "1 < Hour",
                Address = "101 9 Ave SW, Calgary, Alberta T2P 1J9 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Calgary Tower",
                Category = Category.Promotions,
                Price = "$9 CAD - $18 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "This place is temporarily closed. Located 191 metres above the downtown core, the Calgary Tower offers the best view in the city and is a must-see on any visitor's itinerary. On the Observation Deck you'll experience a spectacular 360° view of the bustling city, the majestic Rocky Mountains, the foothills, and the prairies.",
                Rating = 4,
                Duration = "1 < Hour",
                Address = "101 9 Ave SW, Calgary, Alberta T2P 1J9 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Calgary Tower",
                Category = Category.Nearby,
                Price = "$9 CAD - $18 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Indoor | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "This place is temporarily closed. Located 191 metres above the downtown core, the Calgary Tower offers the best view in the city and is a must-see on any visitor's itinerary. On the Observation Deck you'll experience a spectacular 360° view of the bustling city, the majestic Rocky Mountains, the foothills, and the prairies.",
                Rating = 4,
                Duration = "1 < Hour",
                Address = "101 9 Ave SW, Calgary, Alberta T2P 1J9 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Studio Bell, home of the National Music Centre",
                Category = Category.Promotions,
                Price = "Free, courtesy of ATB, until end of 2021",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible,
                Description = "Explore five floors of exhibitions that tell the story of music in Canada, celebrate music icons at the Canadian Halls of Fame, rock out with our interactive instrument installations and sing along in our vocal booths! Everyone is welcome to Studio Bell, home of the National Music Centre.",
                Rating = 4,
                Duration = "1-2 Hours",
                Address = "850 4 Street SE East Village, Calgary, Alberta T2G 1R1 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Studio Bell, home of the National Music Centre",
                Category = Category.Nearby,
                Price = "Free, courtesy of ATB, until end of 2021",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible,
                Description = "Explore five floors of exhibitions that tell the story of music in Canada, celebrate music icons at the Canadian Halls of Fame, rock out with our interactive instrument installations and sing along in our vocal booths! Everyone is welcome to Studio Bell, home of the National Music Centre.",
                Rating = 4,
                Duration = "1-2 Hours",
                Address = "850 4 Street SE East Village, Calgary, Alberta T2G 1R1 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "The New Central Library",
                Category = Category.Nearby,
                Price = "Free, courtesy of ATB, until end of 2021",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible,
                Description = "An architectural gem!",
                Rating = 5,
                Duration = "1 < Hours",
                Address = "800 3 St SE In the East Village, Calgary, Alberta T2G 2E7 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Prince's Island Park",
                Category = Category.Nearby,
                Price = "Free",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.PetFriendly,
                Description = "This recreational island is located in the Bow River, providing stressed-out urbanites opportunities bike, hike and fish.",
                Rating = 5,
                Duration = "1 < Hours",
                Address = "200 Barclay Parade SW, Calgary, Alberta T2P 4R5 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "River Cafe",
                Category = Category.Nearby,
                Price = "$12 CAD - $54 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.PetFriendly,
                Description = "An extraordinary dining experience is to embark upon a journey. We have created a beautiful place in a spectacular setting and have tended to the details that make you feel at home: staff who care, chefs who are passionate about quality and the regional seasonal ingredients that bring to your palate a taste of place.",
                Rating = 4,
                Duration = "1 < Hours",
                Address = "25 Prince's Island Pk SW, Calgary, Alberta T2P 0R1 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Chinese Cultural Centre Museum",
                Category = Category.Nearby,
                Price = "$12 CAD - $54 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.Outdoor | CalgaryPlanIt.Tag.PetFriendly,
                Description = "The Calgary Chinese Cultural Centre is a community centre dedicated to serve the Chinese community of Calgary and the surrounding area.  In carrying out its mandate, it also dedicates itself to undertake educational and cultural programs to enrich the lives of all Calgarians." +
                "As a non - profit organization, we rely on dedicated volunteers and talented and knowledgeable community members to organize various meaningful programs and events to celebrate our heritages and diversity.Together, we work toward fostering goodwill and friendship among all members of the general community to cultivate a harmonious society that defines who we are, as Canadians, to the world.Since its grand opening on September 27,1992, the Chinese Cultural Centre has remained the focal point for community activities and has become a premier venue which offers a stimulating environment for cultural activities, arts, and recreational programs to the public.",
                Rating = 4,
                Duration = "1 < Hours",
                Address = "Calgary Chinese Cultural Cntre 197 1 St SW, Calgary, Alberta T2P 4M4 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Minas Brazilian Steakhouse",
                Category = Category.Nearby,
                Price = "$15 CAD - $42 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible | CalgaryPlanIt.Tag.FoodAndDrink,
                Description = "Minas Brazilian Steakhouse brings you traditional cuisine from the heart of Southeast Brazil, under the guidance of master head chef Jose Montes. It’s Brazilian open barbecue cuisine, known as rodizio. Served tableside on skewers, our delicious selection of meats is complemented by a wide variety of delicious dishes, ensuring a unique dining experience for every eater in your family, friend, group, or corporate outing.",
                Rating = 4,
                Duration = "2 < Hours",
                Address = "136 2 St SW, Calgary, Alberta T2P 0S7 Canada",
                Host = ""
            });
            AttractionsList.Add(new Attraction()
            {
                Name = "Eau Claire Smokestack",
                Category = Category.Nearby,
                Price = "$15 CAD - $42 CAD",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExternalLink = "",
                Tags = CalgaryPlanIt.Tag.KidFriendly | CalgaryPlanIt.Tag.WheelchairAccessible,
                Description = "A Historic Smokestack.",
                Rating = 3,
                Duration = "",
                Address = "136 2 St SW, Calgary, Alberta T2P 0S7 Canada",
                Host = ""
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

        private void CreateList()
        {
            ListofLists = new List<Lis>();
            ListofLists.Add(new Lis()
            {
                Name = "Date Night",
                NumItems = 12

            });
            ListofLists.Add(new Lis()
            {
                Name = "Kids",
                NumItems = 5,
                Attractions = AttractionsList.FindAll((a) => a.Tags.HasFlag(CalgaryPlanIt.Tag.KidFriendly) && (a.Category == Category.MuseumsAndGalleries || a.Category == Category.Promotions))
            });
            ListofLists.Add(new Lis()
            {
                Name = "Late Night",
                NumItems = 4,
                Attractions = AttractionsList.FindAll((a) => a.Tags.HasFlag(CalgaryPlanIt.Tag.Nightlife))
            });
            ListofLists.Add(new Lis()
            {
                Name = "Near AirBNB",
                Attractions = AttractionsList.FindAll((a) => a.Address.Contains("Centre St NE")),
                NumItems = 4
            });
        }
    }
}
