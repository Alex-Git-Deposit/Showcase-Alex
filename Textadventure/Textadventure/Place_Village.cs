using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Village : Place
    {
        public Place_Village()
        {
            place_name = "Village";
            places_currently_available();
        }
        public Place_Village(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void describe_place()
        {
            Console.WriteLine("You are in the quaint Village.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Forest");
            places_available.Add("Pub");
        }
    }
}
