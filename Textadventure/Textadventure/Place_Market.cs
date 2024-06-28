using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Market : Place
    {
        public Place_Market()
        {
            place_name = "Market";
            places_currently_available();
        }
        public Place_Market(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the bustling Market.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Market");
            places_available.Add("Tavern");
            places_available.Add("Forest");
        }
    }
}
