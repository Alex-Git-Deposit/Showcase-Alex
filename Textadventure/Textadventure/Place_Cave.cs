using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Cave : Place
    {
        public Place_Cave()
        {
            place_name = "Cave";
            places_currently_available();
        }
        public Place_Cave(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the dark Cave.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Forest");
        }
    }
}
