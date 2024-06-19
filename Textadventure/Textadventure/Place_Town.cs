using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place_Town : Place
    {
        public Place_Town()
        {
            place_name = "Town";
            places_currently_available();
        }
        public Place_Town(string name) : base(name)
        {
            place_name = name;
            places_currently_available();
        }
        public override void Describe()
        {
            Console.WriteLine("You are in the bustling Town center.");
        }
        public override void places_currently_available()
        {
            places_available.Clear();
            places_available.Add("Market");
            places_available.Add("Tavern");
            places_available.Add("Forest");
            places_available.Add("Library");
        }
    }
}
