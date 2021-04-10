# VehicleTrackingSystem
Program Overview
Please, write a web application that allows users to add Vehicle Details. The details are:
 • Owner’s Name
 • Manufacturer
 • Year of manufacture
 • Weight in kilograms (allow up to two decimal places)

 All fields are required. The application will store these details in a database.

Manufacturers are predefined but there is a very small possibility that the list could be changed in the future. At launch the Manufacturers will be:
1. Mazda
2. Mercedes
3. Honda
4. Ferrari
5. Toyota

The application will automatically assign each vehicle a Category based on its weight. Categories are configurable and can be changed in the future. The default categories that must be implemented in the initial version of the application are.
1. “Light” — Up to 500kg ￼
2. “Medium” — 500kg to 2500kg ￼
3. “Heavy” — 2500kg and higher ￼

Editing Categories
It must be possible for the User to create, change, and delete categories, subject to the following rules:
 • User must assign an icon for each Category. (Use any icons/graphics you like).
 • All possible weights must be covered, i.e. there should be no gaps between categories.
 • Categories should not overlap, i.e. any Weight value will have exactly one category.
 • If one or more categories change, the Vehicle list (see below) must display the new correct category for each vehicle.

 Vehicle List
All vehicles in the database should be displayed on a List page in the application. The list should show the Owner’s Name, Manufacturer, Year of Manufacture, and the Category icon. The list should include the ability to sort by Owner’s Name, Manufacturer, Year of Manufacture, and Weight.
List items should look like this example:
John Smith | Mazda | 2019 | ￼ (category icon)
Jane Turei | Mercedes | 2015 | ￼ (category icon)
