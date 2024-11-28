using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Place
    {
        public string place_name { get; set; }
        public static List<string> places_available = new List<string>();

        public Place()
        {
            place_name = "Place";
        }

        public Place(string name)
        {
            place_name = name;
        }

        public static void add_place(string name)
        {
            if (!places_available.Contains(name))
            {
                places_available.Add(name);
            }
        }
        public static void location_moveto(string location_move_to)
        {
            try
            {
                Console.WriteLine($"You want to move location:");
                if (places_available.Contains(location_move_to) || (location_move_to == "Campfire" && Program_Main.location_current != "Campfire"))
                {
                    Console.WriteLine();
                    Console.WriteLine($"moving from {Program_Main.location_current} to {location_move_to}");
                    Place new_place = Place_Factory.create_place(location_move_to);
                    new_place.places_currently_available();

                    Program_Main.location_current = location_move_to;
                    
                    if (Program_Main.location_current == "Tavern" || Program_Main.location_current == "Pub")
                    {
                        foreach (Character player in Program_Main.party)
                        {
                            player.regenerate_sleep();
                        }
                    }
                    else if (Program_Main.location_current == "Campfire")
                    {
                        // Spieler nach Material prüfen
                        foreach (Character player in Program_Main.party)
                        {
                            player.regenerate_sleep();
                        }
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("unable to move there");
                    Console.WriteLine("to know which places can be moved to type: places");
                }
            }
            catch
            {
                Console.WriteLine("to move correctly to another place type: moveto [location name]");
                Console.WriteLine("to know which places can be moved to type: places");
            }
        }
        public static void initialize_default_places()
        {
            places_available.Clear();
            add_place("Town");
            add_place("Market");
        }
        public static void list_available_places()
        {
            Console.WriteLine();
            Console.WriteLine("Those are the places you could go now:");
            foreach (var place in places_available)
            {
                Console.WriteLine(place);
            }
        }
        public virtual void on_entering()
        {
            Console.WriteLine($"You are at {Program_Main.location_current} now.");
        }
        public virtual void describe_place()
        {
            Console.WriteLine($"You are at a place.");
        }
        public virtual void places_currently_available()
        {
            places_available.Clear();
            add_place("Tavern");
        }

    }
}
