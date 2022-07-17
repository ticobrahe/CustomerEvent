 Customer Event

Question 1
 
 1. Create a new list of events to store event in customer city. Iterate through list of 
 events and check for customer city in each event, if true, add the event to the new list.

 2. Iterate through the new created list of events in customer city and call the AddToEmail 
 method and pass customer and event to the method.

 3. No events will be sent to John Smith since client John Smith does not exists.

4. AddToMail method shold be asynchronous.

Question 2

1. Create a new dictionary of city as key and list of events(otherLocationEvents) as value. 
Create a class(EventsByDistance) for distance and list of events. Iterate through list of events 
and check for city not in customer location and add the event to otherLocationEvents. Iterate 
through the dictionary and calculate the distance between customer city and city(key) in the 
dictionary and store in EventsByDistance.

2. Order the EventsByDistance by distance and take the first 5 events. Iterate the events  and call 
the AddToEmail method and pass customer and event to the method.

3. No events will be sent to John Smith since client John Smith does not exists.

4. Cache response from GetDistance method

Question 4
Wrap GetDistance method in try and catch block

How do you verify? - Unit test