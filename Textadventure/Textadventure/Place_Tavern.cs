using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Tavern : Place
    {
        public Place_Tavern()
        {
            place_name = "Tavern";
            places_currently_available();
        }
        public Place_Tavern(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the lively Tavern.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Market");
            places_available.Add("Town");
        }
    }
}
