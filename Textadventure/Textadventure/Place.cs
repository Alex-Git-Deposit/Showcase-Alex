using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place
    {
        public string place_name { get; set; }
        public static List<string> places_available = new List<string>();

        public Place()
        {
            place_name = "Place";
        }
        public Place(string name)
        {
            place_name = name;
        }
        public void on_entering()
        {
            Console.WriteLine($"You are at {place_name} now.");
        }
        public virtual void Describe()
        {
            Console.WriteLine($"You are at a place.");
        }
        public virtual void places_currently_available()
        {
            places_available.Add(place_name);
        }
    }
}
