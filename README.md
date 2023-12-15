# Hostel Booking System

## Description

This repository is the back-end of a simple booking application with a few booking features.
It includes:
* Displaying all hostels in the app, all rooms in each hostel & all reservations for each room
* Displaying each hostel, room & reservation by its unique database ID (SQL), and displaying some information about each
* Adding a hostel to the booking app, adding a room to any hostel & adding a reservation to any room by inputting parameters
* Removing hostels from the app, removing rooms from any hostel, removing reservations from any room. This is also done by inputting unique database ID

Front-end part of the app: https://github.com/VlatCode/BookingApp-FE

## Getting Started
### Installing

To run the application successfully, you need to:
* Download the entire code from this repository
* Open the solution in Visual Studio
* In the project HostelBookingSystem.HelpersClassLib -> DependencyInjectionHelper.cs file, edit the connection string - add your local SQL connection string
* Use EF command-line tools to apply migrations to your local database
* Run the application and test it out on Swagger!

## Authors
Vlatko Nikolovski

