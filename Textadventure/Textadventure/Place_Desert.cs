using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Desert : Place
    {
        public Place_Desert()
        {
            place_name = "Desert";
            places_currently_available();
        }
        public Place_Desert(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the vast Desert.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Town");
            places_available.Add("Forest");
        }
    }
}
