using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Campfire : Place
    {
        public Place_Campfire()
        {
            place_name = "Campfire";
            places_currently_available();
        }
        public Place_Campfire(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void describe_place()
        {
            Console.WriteLine("You are at your own Campfire.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add(Program_Main.location_current);
        }
    }
}
