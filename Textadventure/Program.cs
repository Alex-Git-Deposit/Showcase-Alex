using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Textadventure
{
    internal class Program
    {
        class inventory_system
        {
            private Dictionary<string, int> inventory;

            // Konstruktor, der das Inventar initialisiert
            public inventory_system()
            {
                inventory = new Dictionary<string, int>(); // Ein leeres Dictionary wird erstellt
            }

            public void inventory_add_item(string item, int quantity)
            {
                if (inventory.ContainsKey(item))
                {
                    inventory[item] += quantity;
                }
                else
                {
                    inventory[item] = quantity;
                }
            }
            public void inventory_remove_item(string item_name, int remove_quantity)
            {
                if (inventory.ContainsKey(item_name))
                {
                    // Überprüfe, ob genügend Gegenstände im Inventar vorhanden sind
                    if (inventory[item_name] >= remove_quantity)
                    {
                        // Reduziere die Anzahl des Gegenstands im Inventar
                        inventory[item_name] -= remove_quantity;
                        Console.WriteLine($"{remove_quantity} {item_name}(s) removed from inventory.");

                        // Wenn die Anzahl auf 0 fällt, entferne den Eintrag komplett
                        if (inventory[item_name] <= 0)
                        {
                            inventory.Remove(item_name);
                            Console.WriteLine($"{item_name} removed from inventory.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Not enough {item_name}s in inventory.");
                    }
                }
                else
                {
                    Console.WriteLine($"Remove failed. {item_name} not found in inventory.");
                }
            }
            public void inventory_print()
            {
                foreach (KeyValuePair<string, int> item in inventory)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
        }
        class player_character // Character darauf ummünzen!
        {
            private Dictionary<string, int> character;

            // Konstruktor, der den Character initialisiert
            public player_character()
            {
                character = new Dictionary<string, int>(); // Ein leeres Dictionary wird erstellt
            }

        }

        static void Main()
        {
            int picked_party_size = 4;
            int picked_party_max = 10;
            int picked_class_index = 0;
            int pick_a_class_index = 0;
            int picked_race_index = 0;
            int pick_a_race_index = 0;
            int picked_name_index = 0;
            int party_level = 1; // es gibt nur 1 Level, alle Spieler sollen "gleich" stark sein

            int lifepoints;
            int armorpoints;
            int attackpoints;
            int hitchance;
            int critchance;

            string confirm_size = "";
            string confirm_class = "";
            string confirm_name = "";
            string confirm_race = "";
            string? input_player = "";
            string location_starting = "Tavern";
            string location_current = location_starting;
            string location_move_to = location_starting;

            string[] spells_healing = { "Cure Wounds", "Soothing Water", "Revitalize", "Holy Light", "Ressurect" };
            string[] spells_damaging = { "Fireball", "Thunder", "Frostbolt", "Acid", "Shadowbolt" };
            string[] spells_supporting = { "Aura of Devotion", "Haste", "Banish", "Aura of Retribution", "Curse" };
            string[] spells_summoning = { "Familiar", "Elemental Servant", "Celestial Guardian", "Demon", "Nature's Ally" };
            string[] attacks_special = { "Mortal Strike", "Aimed Shot", "Backstab", "Smite" };
            string[] attacks_regular = { "Punch", "Slam", "Cut", "Shoot Arrow", "Throw Weapon" };
            string[] attacks_support = { "Trap", "Ensnare", "Help", "Block" };
            string[] conditions_neg = { "normal", "weakened", "rooted", "frozen", "poisoned", "burning", "bleeding", "incapacitated", "banished", "dead" };
            string[] conditions_pos = { "hasted", "Devoted", "Retribute", "Strentghened" };
            string[] dialog_basic = { "Yes", "No", "Confirm", "Decline", "Buy", "Sell", "Leave", "Enter" };
            string[] actions_talk = { };
            string[] actions_trade = { };
            string[] actions_battle = { };
            string[] actions_surrounding = { };
            string[] items_potions = { "Potion of Healing", "Potion of Poison", "Potion of Strength", "Potion of Weakness" };
            string[] items_weapons = { "Sword", "Dagger", "Bow", "Mace", "Staff" };
            string[] items_armor = { "Shield", "Plate", "Chainmail", "Studded Leather", "Leather", "Cloth" };
            string[] items_scrolls = { "Scroll of Spell" }; // zugriff auf einen Zauber, mal sehen wie das genau wird
            string[] items_utility = { "Rope", "Wood", "Flint and Steel", "Bag", "Coins", "Map" };
            string[] items_food = { "Fish", "Chicken", "SoupSoupSoup", "Beef", "Vegetables", "Herbs", "Water" };
            string[] places = { "Town", "Forest", "Desert", "Village", "Cave", "Dungeon", "Castle", "Market", "Mountain", "Mountain-Deeps", "Tavern" };
            string[] classes = { "Fighter", "Ranger", "Rogue", "Paladin", "Priest", "Sorcerer", "Wizard", "Warlock" };
            string[] races_playable = { "Human", "Elven", "Orc", "Dwarf", "Gnome", "Tauren", "Troll", "Goblin" };
            string[] races_enemy = { "Human", "Elven", "Orc", "Dwarf", "Gnome", "Tauren", "Troll", "Goblin", "Creature", "Demon" };

            bool game = true;
            bool party_size_complete = false;
            bool party_class_complete = false;
            bool party_race_complete = false;
            bool party_name_complete = false;

            List<string> player_classes = new List<string>();
            List<string> player_races = new List<string>();
            List<string> player_names = new List<string>();
            List<string> locations_available = new List<string>();
            locations_available.AddRange(places);

            // Queue Initiative für Kämpfe

            Random rnd = new Random();

            //





            //

            // Dialog zum herumspielen
            /*

            Console.WriteLine("You enter a Tavern.");
            Console.WriteLine("You're greeted by the comforting scent of roasted meat and freshly baked bread.");
            Console.WriteLine("Murmurs of conversation and clinking of mugs fill the air.");
            Console.WriteLine("Adventurers and travelers gather around sturdy wooden tables, sharing tales and resting weary feet.");
            Console.WriteLine("Behind the bar, the bartender awaits with a welcoming smile, ready to take your order.");
            Console.WriteLine("\"Welcome to the tavern – where stories begin and adventure awaits around every corner.\"");

            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            Console.WriteLine("1) Buy something to eat/drink");
            Console.WriteLine("2) Talk to the Bartender");
            Console.WriteLine("3) Talk to Guests");
            Console.WriteLine("4) Talk to the Hand");
            Console.WriteLine("5) Cause Trouble");
            Console.WriteLine("6) Leave Tavern");
            Console.WriteLine();

            */

            inventory_system inventory_user = new inventory_system();

            inventory_user.inventory_add_item("Potion", 5);
            inventory_user.inventory_add_item("Sword", 1);
            inventory_user.inventory_add_item("Coins", 100);
            inventory_user.inventory_print();
            inventory_user.inventory_remove_item("Potion", 2);
            inventory_user.inventory_print();

            start_menue();
            start_of_the_game();
            character_creation();

            void character_creation()
            {
                make_party_class();
                make_party_race();
                make_party_name();
            }
            while (game)
            {
                input_reader();
            }

            void location_move()
            {
                Console.WriteLine("If you want to know which places are currently available type \"places\"");
                Console.WriteLine("Tell me the place you want to move to");
                location_move_to = input_reader();
                if (places.Contains(location_move_to))
                {
                    Console.WriteLine();
                    Console.WriteLine($"moving from {location_current} to {location_move_to}");
                    location_current = location_move_to;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("unable to move there");
                }
            }
            void start_menue()
            {
                // Spieler verbinden
                // Sprache auswählen
                // Speicher auswählen // startet das spiel auch
                // Spiel starten
                // Spiel beenden
            }
            void start_of_the_game()
            {
                Console.WriteLine("Welcome to Textadventure!");
                Console.WriteLine();
                Console.WriteLine("If you need any advise on what you can do type \"help\"!");
                Console.WriteLine();

                make_party_size();

                Console.WriteLine();

                make_party_class();

                Console.WriteLine();
                Console.WriteLine("You now have:");

                for (int i = 0; i < player_classes.Count; i++)
                {
                    Console.WriteLine($"Character {i + 1}) {player_classes[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("Now that your party is full let's pick their races.");
                Console.WriteLine();

                make_party_race();

                Console.WriteLine("You now have:");
                Console.WriteLine();

                for (int i = 0; i < player_races.Count; i++)
                {
                    Console.WriteLine($"{player_races[i]} {player_classes[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("Now your party is almost completed.");
                Console.WriteLine();

                make_party_name();

                Console.WriteLine();
                Console.WriteLine("Your Party has names now! YaY");
                Console.WriteLine();
                Console.WriteLine("Your Party is complete:");
                Console.WriteLine();

                for (int i = 0; i < player_names.Count; i++)
                {
                    Console.WriteLine($"{player_names[i]} the {player_races[i]} {player_classes[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("Welcome! The Adventure begins.");
                Console.WriteLine($"You all start in a cosy {location_starting}");

            }

            void make_party_size()
            {
                while (!party_size_complete)
                {
                    Console.WriteLine("Please set your party size, recommended 4, maximum 10.");
                    try
                    {
                        picked_party_size = Convert.ToInt32(input_reader());
                        if (picked_party_size < 1 || picked_party_size > picked_party_max)
                        {
                            picked_party_size = 4;
                            Console.WriteLine("Your input was invalid, so i chose 4 for you!");
                        }
                    }
                    catch (Exception)
                    {
                        picked_party_size = 4;
                        Console.WriteLine("Your input was invalid, so i chose 4 for you!");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Do you want {picked_party_size} partymembers? y / n ");
                    confirm_size = input_reader();
                    Console.WriteLine();
                    if (confirm_size.ToLower() == "y")
                    {
                        Console.WriteLine($"Size of the Party: {picked_party_size}");
                        party_size_complete = true;

                    }
                    else if (confirm_size.ToLower() == "n")
                    {
                        Console.WriteLine("You pressed NO, pick again!");
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again!");
                        Console.WriteLine();
                        continue;
                    }
                }
            }
            void make_party_class()
            {
                while (!party_class_complete)
                {
                    Console.WriteLine("Please pick your party");
                    Console.WriteLine("You have the following classes to choose from:");
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int i = 0; i < classes.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}) {classes[i]}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();

                    picked_class_index = 0;
                    while (picked_class_index < picked_party_size)
                    {
                        Console.WriteLine("Please pick a class:");
                        try
                        {
                            pick_a_class_index = Convert.ToInt32(input_reader());
                            if (pick_a_class_index > 0 && pick_a_class_index <= classes.Length)
                            {
                                player_classes.Add(classes[pick_a_class_index - 1]);
                                Console.WriteLine($"You picked the class {classes[pick_a_class_index - 1]}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid class selection. Please pick again.");
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid class selection. Please pick again.");
                            continue;
                        }

                        Console.WriteLine("Is this correct? y / n ");
                        confirm_class = input_reader();
                        Console.WriteLine();

                        if (confirm_class.ToLower() == "y")
                        {
                            Console.WriteLine($"Class {player_classes[picked_class_index]} confirmed");
                            picked_class_index++;
                            party_class_complete = true;
                        }
                        else if (confirm_class.ToLower() == "n")
                        {
                            Console.WriteLine("You pressed NO, pick again!");
                            Console.WriteLine();
                            party_class_complete = false;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Try again!");
                            Console.WriteLine();
                            party_class_complete = false;
                            continue;
                        }
                    }
                }
            }
            void make_party_race()
            {
                while (!party_race_complete)
                {
                    Console.WriteLine("Please pick your party");
                    Console.WriteLine("You have the following races to choose from:");
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int i = 0; i < races_playable.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}) {races_playable[i]}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();

                    picked_race_index = 0;
                    while (picked_race_index < picked_party_size)
                    {
                        Console.WriteLine("Please pick a race:");
                        try
                        {
                            pick_a_race_index = Convert.ToInt32(input_reader());
                            if (pick_a_race_index > 0 && pick_a_race_index <= races_playable.Length)
                            {
                                player_races.Add(races_playable[pick_a_race_index - 1]);
                                Console.WriteLine($"You picked the race {player_races[picked_race_index]} for {player_classes[picked_race_index]}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid race selection. Please pick again.");
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid race selection. Please pick again.");
                            continue;
                        }

                        Console.WriteLine("Is this correct? y / n ");
                        confirm_race = input_reader();
                        Console.WriteLine();

                        if (confirm_race.ToLower() == "y")
                        {
                            Console.WriteLine($"Race {player_races[picked_race_index]} confirmed");
                            picked_race_index++;
                            party_race_complete = true;
                        }
                        else if (confirm_race.ToLower() == "n")
                        {
                            Console.WriteLine("You pressed NO, pick again!");
                            Console.WriteLine();
                            party_race_complete = false;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Try again!");
                            Console.WriteLine();
                            party_race_complete = false;
                            continue;
                        }
                    }
                }
            }
            void make_party_name()
            {
                while (!party_name_complete)
                {
                    Console.WriteLine("Please name your party members:");
                    picked_name_index = 0;

                    while (picked_name_index < picked_party_size)
                    {
                        Console.WriteLine($"Please pick a name for Character {picked_name_index + 1}:");
                        Console.WriteLine($"Race: {player_races[picked_name_index]}, Class: {player_classes[picked_name_index]}");
                        string playerName = input_reader();
                        Console.WriteLine();
                        Console.WriteLine($"The name of Character {picked_name_index + 1} ({player_races[picked_name_index]} {player_classes[picked_name_index]}) is {playerName}.");
                        Console.WriteLine("Is this correct? y / n ");
                        confirm_name = input_reader();
                        Console.WriteLine();

                        if (confirm_name.ToLower() == "y")
                        {
                            player_names.Add(playerName);
                            Console.WriteLine($"Name {playerName} confirmed");
                            Console.WriteLine();
                            picked_name_index++;
                            party_name_complete = true;
                        }
                        else if (confirm_name.ToLower() == "n")
                        {
                            Console.WriteLine("You pressed NO, pick again!");
                            Console.WriteLine();
                            party_name_complete = false;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Try again!");
                            Console.WriteLine();
                            party_name_complete = false;
                            continue;
                        }
                    }
                }
            }


            // Generating basic enemy appears
            /*
            Console.WriteLine("Generating basic enemy...");
            string enemyClass = classes[rnd.Next(classes.Length)];
            string enemyRace = races[rnd.Next(races.Length)];

            Console.WriteLine($"Basic Enemy: {enemyRace} {enemyClass}");

            Console.WriteLine("Press any key to continue...");
            input_reader();
            */

            // ausbauen


            // Final boss
            // Console.WriteLine("\nType 'Summon Doom-TheEternal' to summon the final boss.");
            // command = Console.ReadLine();
            // if (command.ToLower() == "summon doom-theeternal")
            // {
            //    Console.WriteLine("Final boss 'Doom-TheEternal' summoned!");
            // }
            // den machen wir zum Schluss ;)
            // Console.WriteLine("Press any key to continue...");
            // Console.ReadKey();
            void save_game()
            {
                // das wird lustig
                // denk dran, der Spieler soll das Speicherfile benennen
                // zumindest für den output im Spiel
            }
            string input_reader()
            {
                while (true)
                {
                    input_player = Console.ReadLine();
                    if (input_player != null)
                    {
                        string[] parts = input_player.Split(' ');

                        if (parts[0].ToLower() == "help")
                        {
                            Console.WriteLine("You asked for help");
                            Console.WriteLine();
                            help_output();
                            return input_player;
                        }
                        else if (parts[0].ToLower() == "ask" && classes.Contains(parts[1]))
                        {
                            Console.WriteLine($"You want to know more about {parts[1]}:");
                            Console.WriteLine();
                            return parts[1];
                        }
                        else if (parts[0].ToLower() == "quit")
                        {
                            Console.WriteLine($"You just quit the game. I hope you saved it!");
                            Console.WriteLine();
                            game = false;
                            return input_player;
                        }
                        else if (parts[0].ToLower() == "save")
                        {
                            Console.WriteLine("You want to save the game but i do not like to do that");
                            Console.WriteLine(); // save noch schreiben!
                            save_game();
                        }
                        else if (parts[0].ToLower() == "move")
                        {
                            Console.WriteLine($"You want to move:");
                            location_move();
                            Console.WriteLine();
                            return location_current;
                        }
                        else if (parts[0].ToLower() == "places")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Those are the places you could go now:");
                            for (int i = 0; i < locations_available.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}) {locations_available[i]}");
                            }
                            Console.WriteLine();
                            return location_current;
                        }
                        else if (int.TryParse(input_player, out int classNumber))
                        {
                            return input_player;
                        }
                        else if (!string.IsNullOrWhiteSpace(input_player))
                        {
                            return input_player;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                            Console.WriteLine("Try again or type \"help\"");
                            continue;
                        }
                    }
                    if (input_player == null)
                    {
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("Try again or type \"help\"");
                    }
                }
            }
            void help_output()
            {
                // alles reinschreiben was zum helfen dienen soll
                Console.WriteLine("Help is not completed, please wait for all of eternity!");
            }
        }
    }
}