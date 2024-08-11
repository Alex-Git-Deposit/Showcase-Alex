using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Forest : Place
    {
        public Place_Forest()
        {
            place_name = "Forest";
            places_currently_available();
        }
        public Place_Forest(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void describe_place()
        {
            Console.WriteLine("You are in the serene Forest.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Town");
            places_available.Add("Village");
            places_available.Add("Mountain");
            places_available.Add("Desert");
            places_available.Add("Dungeon");
            places_available.Add("Castle");
            places_available.Add("Cave");
        }
    }
}
