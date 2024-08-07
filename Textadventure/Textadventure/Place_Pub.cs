using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Pub : Place
    {
        public Place_Pub()
        {
            place_name = "Pub";
            places_currently_available();
        }
        public Place_Pub(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void describe_place()
        {
            Console.WriteLine("You are in the lively Pub.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Village");
        }
    }
}
