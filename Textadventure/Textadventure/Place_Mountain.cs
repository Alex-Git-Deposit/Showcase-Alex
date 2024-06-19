using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Mountain : Place
    {
        public Place_Mountain()
        {
            place_name = "Mountain";
            places_currently_available();
        }
        public Place_Mountain(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are on the towering Mountain.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Mountain-Deeps");
            places_available.Add("Town");
        }
    }
}
