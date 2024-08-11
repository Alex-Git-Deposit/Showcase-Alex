using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Dungeon : Place
    {
        public Place_Dungeon()
        {
            place_name = "Dungeon";
            places_currently_available();
        }
        public Place_Dungeon(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void describe_place()
        {
            Console.WriteLine("You are in the eerie Dungeon.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Forest");
        }
    }
}
