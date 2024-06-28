using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Castle : Place
    {
        public Place_Castle()
        {
            place_name = "Castle";
            places_currently_available();
        }
        public Place_Castle(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the grand Castle.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Forest");
        }
    }
}
