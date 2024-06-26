using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Mountain_Deeps : Place
    {
        public Place_Mountain_Deeps()
        {
            place_name = "Mountain-Deeps";
            places_currently_available();
        }
        public Place_Mountain_Deeps(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the deep Mountain caverns.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Mountain");
        }
    }
}
