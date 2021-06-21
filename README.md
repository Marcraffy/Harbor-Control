# Harbor Control

## To Run:
 - Start HarborControl asp.net host.
 - Start HarborControl.Traffic / interact over API

The Durban harbor control requires a concurrency safe application that will help the staff control the
flow of boats and ships into the harbor.

• The harbor has a perimeter of 10km between the dock, and the open sea.

• The harbor can only allow one boat into the perimeter at a time.

• Any boat that enters the perimeter will complete the 10km journey into the harbor before it
reaches the dock.

• No other boat may enter the 10km perimeter once a boat is inside the perimeter.

• Boats arrive at the perimeter randomly.

• All boats that arrive at the perimeter must wait at the 10km perimeter line before they are
ordered to ender the perimeter by harbor control.

• There are 3 types of boats:

    o Speedboat
        ▪ Speed: 30km/h
    o Sailboat
        ▪ Speed: 15km/h
    o Cargo ship
        ▪ Speed: 5km/h
        
• These types of boats can arrive at the perimeter at any time.

• Once the boat in the perimeter has completed the 10km journey and docked, a boat waiting at
the perimeter may enter.

• Snapshot example:

    o There are 2 speedboats and a Sailboat at the perimeter
    o There is one Cargo ship inside the perimeter
    o The 2 speedboats and sailboat need to wait for the cargo ship to reach the dock before
    harbor control can signal one of them to enter the perimeter.
    
## Requirement

• Build an application using C# that will randomly generate boats that arrive at the perimeter and
control the flow of boats into the harbor.

• Simulate a scheduling engine that will allow boats into the perimeter in the way you best
understand this.

• You may use any UI technology (including a console app, WPF, or Web interface) to
demonstrate the functionality.

• (Bonus) Write a service that will display the current wind speed at Durban harbor. You can make
use of https://openweathermap.org/current#name and create a free account

• Do not allow any sailboats into the harbor if the wind speed is below 10km/h

• Do not allow any sailboats into the harbor if the wind speed is above 30km/h

• (Bonus) You may use any type of database to store any information you might require.

This assessment is designed in a way to allow you to showcase your abilities and coding style. Please
keep this in mind while you architect your assessment
