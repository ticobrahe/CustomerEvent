using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Viagogo
{
    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class EventsByDistance
    {
        public int Distance { get; set; }
        public List<Event> Events { get; set; }
    }
    public class EventsByPrice
    {
        public int Price { get; set; }
        public Event Event { get; set; }
    }
    public enum SortKey
    {
        Distance = 1,
        Price
    }
    public class Solution
    {
        static void Main(string[] args)
        {
            var events = new List<Event>{
                new Event{ Name = "Phantom of the Opera", City = "New York"},
                new Event{ Name = "Metallica", City = "Los Angeles"},
                new Event{ Name = "Metallica", City = "New York"},
                new Event{ Name = "Metallica", City = "Boston"},
                new Event{ Name = "LadyGaGa", City = "New York"},
                new Event{ Name = "LadyGaGa", City = "Boston"},
                new Event{ Name = "LadyGaGa", City = "Chicago"},
                new Event{ Name = "LadyGaGa", City = "San Francisco"},
                new Event{ Name = "LadyGaGa", City = "Washington"}
            };
            //1. find out all events that arein cities of customer
            // then add to email.
            var customer = new Customer { Name = "Mr. Fake", City = "New York" };

            // 1. TASK
            var otherLocationEvents = new Dictionary<string, List<Event>>();
            var matchedLocationEvents = new List<Event>();

            foreach (var @event in events)
            {
                if (@event.City.Equals(customer.City))
                {
                    matchedLocationEvents.Add(@event);
                    continue;
                }
                if (otherLocationEvents.ContainsKey(@event.City))
                {
                    otherLocationEvents[@event.City].Add(@event);
                    continue;
                }

                otherLocationEvents.Add(@event.City, new List<Event> { @event });
            }
            //add to mail all events in customer city
            foreach (var item in matchedLocationEvents)
            {
                AddToEmail(customer, item);
            }

            //add closest events to customer location
            var closestEvents = GetCloseCityByDistance(otherLocationEvents, customer.City);
            foreach (var item in closestEvents)
            {
                AddToEmail(customer, item);
            }
            /*
            * We want you to send an email to this customer with all events in their city
            * Just call AddToEmail(customer, event) for each event you think they should get
            */
        }
        private static List<Event> GetCloseCityByDistance(Dictionary<string, List<Event>> eventsDict, string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return new List<Event>();
            }

            var distanceEvents = new List<EventsByDistance>();
            foreach (var @event in eventsDict)
            {
                int distance = 0;
                try
                {
                    distance = GetDistance(city, @event.Key);
                }
                catch (Exception)
                {
                    continue;
                }

                distanceEvents.Add(new EventsByDistance
                {
                    Distance = distance,
                    Events = @event.Value
                });
            }

            return distanceEvents.OrderBy(x => x.Distance).SelectMany(x => x.Events).Take(5).ToList();
        }

        private static List<Event> GetCloseCityByPrice(List<Event> events)
        {
            var eventsByPrices = new List<EventsByPrice>();
            foreach (var @event in events)
            {
                int price = 0;
                try
                {
                    price = GetPrice(@event);
                }
                catch (Exception)
                {
                    continue;
                }

                eventsByPrices.Add(new EventsByPrice
                {
                    Price = price,
                    Event = @event
                });
            }

            return eventsByPrices.OrderBy(x => x.Price).Select(x => x.Event).Take(5).ToList();
        }

        private static List<Event> GetCloseEvent(Dictionary<string, List<Event>> eventsDict, SortKey sortKey, string city = null)
        {
            if (sortKey == SortKey.Distance)
            {
                
                return GetCloseCityByDistance(eventsDict, city);
            }

            var events = eventsDict.SelectMany(x => x.Value).ToList();
            return GetCloseCityByPrice(events);
        }
        // You do not need to know how these methods work
        static void AddToEmail(Customer c, Event e, int? price = null)
        {
            var distance = GetDistance(c.City, e.City);
            Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
            + (distance > 0 ? $" ({distance} miles away)" : "")
            + (price.HasValue ? $" for ${price}" : ""));
        }
        static int GetPrice(Event e)
        {
            return (AlphebiticalDistance(e.City, "") + AlphebiticalDistance(e.Name, "")) / 10;
        }
        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }
        private static int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                result += Math.Abs(s[i] - t[i]);
            }
            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                result += s.Length > t.Length ? s[i] : t[i];
            }
            return result;
        }
    }
}
