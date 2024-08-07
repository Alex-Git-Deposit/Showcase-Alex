using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Library : Place
    {
        public Place_Library()
        {
            place_name = "Library";
            places_currently_available();
        }
        public Place_Library(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void describe_place()
        {
            Console.WriteLine("You are in a Library, be quiet!");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Town");
        }
    }
}
