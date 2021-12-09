# CalgaryPlanIt
 
README
CPSC 481 - GROUP 9 - CalgaryPlanIt
Final Project - README

Christina Lu
Muhammad Janjua
Raymond Vong
Ahmed Farazi
Mohamed Mohamed


//About the application// 

The purpose of this project was to create an intuitive application (mostly interface) to help travel agencies
better service their clients. 

//Use Cases Implemented//

**Note: the words Trip and itinerary are used interchangably in our context
The Use Cases implemented to varying degrees include:
1. Creating and managing an itinerary
	a. Creating/Deleting/Naming an itinerary
	b. Search and sort itineraries based on its name
	b. Placing the attraction into a calender
	c. Remind the user of upcoming events based on the calender
	d. Viewing itinerary in map form (Not fully functional)
	e. Printing an intinerary
	f. Reviewing attraction (Not functional currently, interactable not does not actually add a review)
2. Creating and managing lists (attractions that the user may be interested in later)
	a. Creating/Deleting/Naming a list
	b. Adding items from the list to a itinerary
3. Searching for Attractions / Events
	a. Let the user find attractions their interested in through popular categories
	b. Allow the user to sort / filter options based on specified criteria
	c. Allow the user to add the atttraction to a list or itinerary
	d. Map that shows the location of the user relative to the attraction (Not fully functional)
	e. Viewing detailed information for each attraction 

//Running the Application //
No special handling is required, application should run as normal through Visual Studio 2022

Basic Walkthrough:
You will arrive at the home screen on the opening of the application
"Home" Screen:
1. Click on the calendars besides each of the textbox to input the date into the correct format.
2. Click the + / - buttons to add users to the trip.
3. Give your trip a name in the text line below "NAME YOUR TRIP".
4. Click Start Planning to taken to the "Things to Do" Page.
**Note that start planning will produce warning/error message for the user if the start date isn't before the end date,
0 users are given, or the trip has no name.

"Things to Do" Screen
1. You can sort / arrange the categories using the dropdown in the top right.
2. You can search for attractions directly using the search bar in the topright
	a. try "Banff" or "Creepy", then hit Enter on your keyboard.
	b. Click "Things to do" again if you do step a.
3. Lets click the category "Tours"

"Tours" Screen
1. In the top right we have a similiar search bar, except this only searches within the category
	a. try any of the words visible in the attraction names
	b. Then "Clear Filters" on the top left
2. Interactable Map
	a. You can click and drag the map around
	b. You can use the scroll wheel on your mouse to zoom in and out (There is a limitation to the zooming)
3. The button in the top right, "List View" / "Map View" will toggle on and off the map
4. Try clicking on the attraction "Banff 1-Day Tour From Calgary"
	a. A scrollable detailed information card about the attraction will the place of the map. Here we can see reviews and photos left by other users.
	b. Hit the "X" on top right of the card to close, you may need to scroll back up if you scrolled down.
5. You can apply filters to the attractions given the menu on the left side.
	a. Try checking "Kid Friendly"
	b. Then "Apply Filters".
6. Adding something new to a list
	a. Try clicking the "Heart" on the "Banff 1-Day Tour From Calgary" attraction
	b. Lets add the attraction to "Date Night" by click on the "heart" associated with it.
7. Now lets add this to an itinerary as well.
	a. Try clicking the "calendar" on the "Banff 1-Day Tour From Calgary" attraction
	b. Click on the trip you created previously. 
	c. Then "Save Changes"
8. Now lets navigate to the "Trips" menu using the top bar

"Trips" Screen
1. Click "Plan" on the trip you previously created / named. (You can also create a new trip in the upper left corner using "CREATE", basically the same trip creation menu as previous shown"
2. Here you should see the attraction you added previously.
3. Turns out you didn't like Banff trip at 12 AM.
	a. Hover over "Banff 1-Day Tour From Calgary" item and click "REMOVE" on the right side.
4. Lets click "Date Night" on the right hand side, where we previously saved a copy of the attraction "Banff 1-Day Tour From Calgary"
5. Now click and drag "Banff 1-Day Tour From Calgary" into any time slot you want
6. Hit "BACK TO LISTS"
7. Click any of the other lists on the right hand side and items as you place into time slots.
8. Click back on "Trips" on the top bar and then on the name of your "Trip" (Where the ">" icon is)
9. Here we see your laid out exactly as planned
10. In the top left, you have the option to "Print itinerary" or go back and "Edit this trip"
	a. Print itinerary will open the classic print window, allowing you to print to pdf or select other output devices.
11. The review buttons beside each attraction will open a window that lets you review the attraction.
	a) You can click the stars to give a rating
	b) Type in the textbox to enter text
	c) Click "Upload" to upload photos
	d) Then "Post"
12. You can click "Archive" on any of the trips cards to remove it from this menu
13. Now moving to the "Lists" Screen using the top bar

"Lists" Screen
1. Lets try clicking "DATE NIGHT" (or any of the other ones).
	a) You can delete items off your list using the "Garbage Can" Icon.
2. Click "Lists" in the top bar to go back.
3. You can create a new list using the "CREATE" button in the top left.