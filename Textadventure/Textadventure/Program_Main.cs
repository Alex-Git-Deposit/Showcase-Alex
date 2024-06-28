using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static Textadventure.Program_Main;

namespace Textadventure
{   
    public class Program_Main
    {
        static int count = 0;
        static int picked_party_size = 4;
        static int picked_party_max = 10;
        static int pick_a_class_index = 0;
        static int pick_a_race_index = 0;

        static string player_class = "";
        static string player_race = "";
        static string player_name = "";
        static string confirm_size = "";
        static string confirm_class = "";
        static string confirm_name = "";
        static string confirm_race = "";
        static string location_starting = "Tavern";
        public static string location_current = location_starting;
        static string location_move_to = location_starting;
        public static string filePath_save = "C:\\Meins\\Coding\\C#\\playground\\Textadventure\\save";
        //public static string filePath_save = "C:\\Users\\FP2402389\\source\\repos\\Textadventure\\save";

        public static List<string> file_content = new List<string>();

        static string[] spells_healing_array = { "Cure Wounds", "Healing Touch", "Rejuvenate", "Holy Light", "Ressurect" };
        static string[] spells_damaging_array = { "Fireball", "Thunder", "Frostbolt", "Acid", "Shadowbolt" };
        static string[] spells_supporting_array = { "Aura of Devotion", "Haste", "Banish", "Aura of Retribution", "Curse" };
        static string[] spells_summoning_array = { "Familiar", "Elemental Servant", "Celestial Guardian", "Demon", "Nature's Ally" };
        static string[] attacks_special_array = { "Mortal Strike", "Aimed Shot", "Backstab", "Smite", "Stealth" };
        static string[] attacks_regular_array = { "Punch", "Strike", "Shoot", "Throw" };
        static string[] attacks_support_array = { "Trap", "Ensnare", "Aid", "Block" };
        static string[] conditions_neg_array = { "Normal", "Weakened", "Rooted", "Frozen", "Poisoned", "Burning", "Bleeding", "Incapacitated", "Banished", "Dead" };
        static string[] conditions_pos_array = { "Hasted", "Devoted", "Retributing", "Strentghened", "Stealthed" };
        static string[] dialog_basic = { "Yes", "No", "Confirm", "Decline", "Buy", "Sell", "Leave", "Enter" };
        static string[] actions_talk = { };
        static string[] actions_trade = { };
        static string[] actions_battle = { };
        static string[] actions_surrounding = { };
        public static string[] items_weapons = { "Sword", "Dagger", "Bow", "Mace", "Staff" };
        public static string[] items_armor = { "Shield", "Ring", "Necklace", "Cloak", "Plate", "Chainmail", "Studded Leather", "Leather", "Cloth" };
        public static string[] items_potions = { "Potion of Healing", "Potion of Poison", "Potion of Strength", "Potion of Weakness" };
        public static string[] items_scrolls = { "Scroll of Spell" }; // zugriff auf einen Zauber, mal sehen wie das genau wird // NICHT ARRAY 
        public static string[] items_utility = { "Rope", "Wood", "Flint and Steel", "Bag", "Coin", "Map", "Small Key" };
        public static string[] items_food = { "Fish", "Chicken", "SoupSoupSoup", "Beef", "Vegetables", "Herbs", "Water" };
        static string[] places = { "Town", "Forest", "Desert", "Village", "Cave", "Dungeon", "Castle", "Market", "Mountain", "Mountain-Deeps", "Tavern" };
        static string[] classes_playable = { "Fighter", "Ranger", "Rogue", "Paladin", "Priest", "Sorcerer", "Wizard", "Warlock" };
        static string[] races_playable = { "Human", "Elven", "Orc", "Dwarf", "Gnome", "Tauren", "Troll", "Goblin" };
        static string[] races_enemy = { "Human", "Elven", "Orc", "Dwarf", "Gnome", "Tauren", "Troll", "Goblin", "Creature", "Demon", "Undead" };

        //static bool activate = false;
        public static bool game = true;
        static bool found_player_for_inventory_equipment = false;
        static bool party_size_complete = false;
        static bool party_class_complete = false;
        static bool party_race_complete = false;
        static bool party_name_complete = false;

        static Dictionary<string, string> tooltip_classes_playable = new Dictionary<string, string>
        {
            {
                "Fighter",
                "Fighter: Masters of martial prowess, fighters excel in combat, wielding weapons \n" +
                "         with precision and strength unmatched. \n" +
                "Base: Regular combat \n" +
                "Base: \"Mortal Strike\" \n" +
                "Can learn: X \n" +
                "Special: High Lifepoints and Armor \n" +
                "Armortypes: All \n" +
                "Weapontypes: All \n"
            },
            {
                "Ranger",
                "Ranger: Bound to the untamed wilderness, rangers are skilled trackers and expert archers. \n\n" +
                "Base: Regular combat \n" +
                "Base: \"Trap\" \n" + // noch überlegen wie genau
                "Base: \"Aimed Shot\" \n" +
                "Can learn: \"Chainmail\", \"Stealth\" \n" +
                "Special: Excells at \"outside\" locations \n" +
                "Armortypes: \"Studded Leather\", \"Leather\", \"Cloth\" \n" +
                "Weapontypes: \"Dagger\", \"Bow\" \n"
            },
            {
                "Rogue",
                "Rogue: Masters of stealth, deception and finesse, they thrive in the murky underbelly of society. \n\n" +
                "Base: Regular combat \n" +
                "Base: \"Backstab\" \n" +
                "Base: Stealth \n" +
                "Can learn: \"Studded Leather\", \"Bow\" \n" +
                "Special: Excells at \"inside\" locations \n" +
                "Armortypes: \"Leather\", \"Cloth\" \n" +
                "Weapontypes: \"Dagger\", \"Sword\" \n"
            },
            {
                "Paladin",
                "Paladin: Radiant champions of justice, paladins wield holy might in their quest to vanquish evil. \n\n" +
                "Base: Regular combat \n" +
                "Base: \"Smite\" \n" +
                "Base: \"Holy Light\" \n" +
                "Can learn: \"Aura of Devotion\", \"Aura of Retribution\", \"Ressurect\" \n" +
                "Special: Heals and Fights, High Armor \n" +
                "Armortypes: All \n" +
                "Weapontypes: All except \"Dagger\", \"Staff\", \"Bow\" \n"
            },
            {
                "Priest",
                "Priest: Devoted servants of the divine, priests channel the power of gods \n" +
                "        to heal the wounded and smite the wicked. \n" +
                "Base: Regular combat \n" +
                "Base: Some of the healing spells, need to write them \n" +
                "Can learn: \"Ressurect\" and all the other healing spells, need to write them \n" +
                "Can learn: \"Chainmail\" and maybe some damage spells \n" +
                "Special: Heals \n" +
                "Armortypes: Cloth \n" +
                "Weapontypes: \"Staff\", \"Mace\" \n"
            },
            {
                "Sorcerer",
                "Sorcerer: Born of magic itself, sorcerers command arcane energies \n" +
                "          with an innate power that defies explanation. \n" +
                "          Their spells twist reality and shape the very fabric of existence. \n" +
                "Base: Regular combat \n" +
                "Base: Some of the damaging spells, need to write them \n" +
                "Base: Wild Magic \n" + // das wird so richtig lustig
                "Can learn: More damaging spells, need to write them \n" +
                "Can learn: \"Chainmail\" and maybe some damage spells \n" +
                "Special: Spellcasting, learns spells not buying \n" +
                "Armortypes: Cloth \n" +
                "Weapontypes: \"Staff\", \"Dagger\", \"Sword\""
            },
            {
                "Wizard",
                "Wizard: The embodiment of the adage \"knowledge is power\", relentlessly pursuing \n" +
                "        boundless understanding and arcane wisdom. Their insatiable thirst for knowledge \n" +
                "        fuels their mastery of the mystical arts, making them unparalleled wielders of magic. \n" +
                "Base: Regular combat \n" +
                "Base: Some of the damaging spells, need to write them \n" +
                "Can learn: All spells \n" +
                "Special: Spellcasting \n" +
                "Armortypes: Cloth \n" +
                "Weapontypes: \"Staff\", \"Dagger\", \"Sword\""
            },
            {
                "Warlock",
                "Warlock: Bound by pacts with otherworldly entities, wielding eldritch power \n" +
                "         that seeps into their very being. Their abilities are as varied as the beings they serve, \n" +
                "         granting them both boons and burdens. \n" +
                "Base: Regular combat \n" +
                "Base: Some of the damaging spells, need to write them \n" +
                "Can learn: All \"evil\" spells, need to write them \n" +
                "Special: Spellcasting \n" +
                "Armortypes: Cloth \n" +
                "Weapontypes: \"Staff\", \"Dagger\", \"Sword\""
            }
        };

        static Dictionary<string, string> tooltip_races_playable = new Dictionary<string, string>
        {
            {   "Human",
                "Human: Versatile and determined, humans are known for their resilience and adaptability,  \n" +
                "       making them true masters in various professions and ways of life. \n"
            },
            {
                "Elven",
                "Elven: Graceful and majestic, the elves are stewards of nature and ancient wisdom. \n" +
                "       Their refined skill in magic and art reflects their deep connection to the world around them. \n"
            },
            {
                "Orc",
                "Orc: Proud and martial, orcs are a people of strength and determination. \n" +
                "     Their wild nature and unyielding loyalty make them powerful allies or fearsome foes. \n"
            },
            {
                "Dwarf",
                "Dwarf: Stubborn and unyielding, dwarves are masters of mining and craftsmanship. \n" +
                "       Their deep roots in the mountains and their iron determination make them invaluable allies in any battle. \n"
            },
            {
                "Gnome",
                "Gnome: Brilliant and curious, gnomes are inventors and tinkerers by nature. \n" +
                "       Their boundless curiosity and thirst for knowledge drive them to create new technologies and magical artifacts. \n"
            },
            {
                "Tauren",
                "Tauren: Honest and respectful, tauren are keepers of wisdom and natural harmony. \n" +
                "        Their strong connection to nature and honorable way of life make them peaceful protectors of their people. \n"
            },
            {
                "Troll",
                "Troll: Wild and mystical, trolls are mysterious denizens of tropical forests and jungles. \n" +
                "       Their connection to ancient spirits and anarchic nature make them feared warriors and cunning survivors. \n"
            },
            {
                "Goblin",
                "Goblin: Shrewd and cunning, goblins are ingenious traders and inventors. \n" +
                "        Their love for profit and ability to capitalize on any situation \n" +
                "        make them driving forces in the world of commerce and technology. \n"
            },
        };

        static Dictionary<string, string> tooltip_spells_healing = new Dictionary<string, string>
        {
            { "Cure Wounds", "Cure Wounds" },
            { "Healing Touch", "Healing Touch" },
            { "Rejuvenate", "Rejuvenate" },
            { "Holy Light", "Holy Light" },
            { "Ressurection", "Ressurection" }
        };
        static Dictionary<string, string> tooltip_spells_damaging = new Dictionary<string, string>
        {
            { "Fireball", "Fireball" },
            { "Thunder", "Thunder" },
            { "Frostbolt", "Frostbolt" },
            { "Acid", "Acid" },
            { "Shadowbolt", "Shadowbolt" }
        };
        static Dictionary<string, string> tooltip_spells_supporting = new Dictionary<string, string>
        {
            { "Aura of Devotion", "Aura of Devotion" },
            { "Haste", "Haste" },
            { "Banish", "Banish" },
            { "Aura of Retribution", "Aura of Retribution" },
            { "Curse", "Curse" }
        };
        static Dictionary<string, string> tooltip_spells_summoning = new Dictionary<string, string>
        {
            { "Familiar", "Familiar" },
            { "Elemental Servant", "Elemental Servant" },
            { "Celestial Guardian", "Celestial Guardian" },
            { "Demon", "Demon" },
            { "Nature's Ally", "Nature's Ally" }
        };

        static Dictionary<string, string> tooltip_attacks_special = new Dictionary<string, string>
        {
            { "Mortal Strike", "Mortal Strike" },
            { "Aimed Shot", "Aimed Shot" },
            { "Backstab", "Backstab" },
            { "Smite", "Smite" },
            { "Stealth", "Stealth" }
        };
        static Dictionary<string, string> tooltip_attacks_regular = new Dictionary<string, string>
        {
            { "Punch", "Punch" },
            { "Strike", "Strike"}, // braucht Waffe
            { "Throw", "Throw" }, // mit Schild massiv besser
            { "Shoot", "Shoot" }, // helfe jemanden zu treffen
            { "Run", "Run"}
        };
        static Dictionary<string, string> tooltip_attacks_support = new Dictionary<string, string>
        {
            { "Trap", "Trap" },
            { "Ensnare", "Ensnare" },
            { "Aid", "Aid" },
            { "Block", "Block" }
        };

        static Dictionary<string, string> tooltip_conditions_neg = new Dictionary<string, string>
        {
            { "Normal", "Normal" },
            { "Weakened", "Weakened" },
            { "Rooted", "Rooted" },
            { "Frozen", "Frozen" },
            { "Poisoned", "Poisoned" },
            { "Burning", "Burning" },
            { "Bleeding", "Bleeding" },
            { "Incapacitated", "Incapacitated" },
            { "Banished", "Banished" },
            { "Dead", "Dead" }
        };
        static Dictionary<string, string> tooltip_conditions_pos = new Dictionary<string, string>
        {
            { "Hasted", "Hasted" },
            { "Devoted", "Devoted" },
            { "Retributing", "Retributing" },
            { "Strentghened", "Strentghened" },
            { "Stealthed", "Stealthed" }
        };

        static Dictionary<string, string> tooltip_items_weapons = new Dictionary<string, string>
        {
            { "Sword", "Sword" },
            { "Dagger", "Dagger" },
            { "Bow", "Bow" },
            { "Mace", "Mace" },
            { "Staff", "Staff" }
        };
        static Dictionary<string, string> tooltip_items_armor = new Dictionary<string, string>
        {
            { "Shield", "Shield" },
            { "Ring", "Ring" },
            { "Necklace", "Necklace" },
            { "Cloak", "Cloak" },
            { "Plate", "Plate" },
            { "Chainmail", "Chainmail" },
            { "Studded Leather", "Studded Leather" },
            { "Leather", "Leather" },
            { "Cloth", "Cloth" }
        };
        static Dictionary<string, string> tooltip_items_potions = new Dictionary<string, string>
        {
            { "Potion of Healing", "Potion of Healing" },
            { "Potion of Poison", "Potion of Poison" },
            { "Potion of Strength", "Potion of Strength" },
            { "Potion of Weakness", "Potion of Weakness" }
        };
        static Dictionary<string, string> tooltip_items_scrolls = new Dictionary<string, string>
        {
            { "Scroll of Spell", "Scroll of Spell" }
        };
        static Dictionary<string, string> tooltip_items_utility = new Dictionary<string, string>
        {
            { "Rope", "Rope" },
            { "Wood", "Wood" },
            { "Flint and Steel", "Flint and Steel" },
            { "Bag", "Bag" },
            { "Coin", "Coin" },
            { "Map", "Map" },
            { "Small Key", "Small Key" }
        };
        static Dictionary<string, string> tooltip_items_food = new Dictionary<string, string>
        {
            { "Fish", "Fish" },
            { "Chicken", "Chicken" },
            { "SoupSoupSoup", "SoupSoupSoup" },
            { "Beef", "Beef" },
            { "Vegetables", "Vegetables" },
            { "Herbs", "Herbs" },
            { "Water", "Water" }
        };

        public static List<string> player_active_names = new List<string>(); 
        public static List<Character> party = new List<Character>();
        public static List<Character> enemies = new List<Character>();
        public static List<Character> active_npc_list = new List<Character>();

        public static Simple_Random rnd = new Simple_Random();

        public static List<string>? multi_word_strings;

        public static void reset_multi_word_strings()
        {
            multi_word_strings = null;
        }
        static void initialize_multi_word_strings()
        {
            if (multi_word_strings == null)
            {
                List<string> multi_word_strings = extract_multi_word_strings();
                if (multi_word_strings.Contains("fatal_error"))
                {
                    Console.WriteLine("Fatal Error: could not make multi_word_strings!");
                }

            }
        }
        private static List<string> extract_multi_word_strings()
        {
            List<string> multi_word_strings = new List<string>();

            // Arrays
            string[][] arrays = {
                spells_healing_array, spells_damaging_array, spells_supporting_array, spells_summoning_array,
                attacks_special_array, attacks_regular_array, attacks_support_array, conditions_neg_array,
                conditions_pos_array, dialog_basic, items_weapons, items_armor, items_potions, items_utility, items_food, places,
                classes_playable, races_playable, races_enemy
            };

            foreach (var array in arrays)
            {
                foreach (var item in array)
                {
                    if (item.Contains(" "))
                    {
                        multi_word_strings.Add(item);
                    }
                }
            }

            Dictionary<string, string>[] dictionaries = {
                tooltip_classes_playable, tooltip_races_playable, tooltip_spells_healing, tooltip_spells_damaging,
                tooltip_spells_supporting, tooltip_spells_summoning, tooltip_attacks_special, tooltip_attacks_regular,
                tooltip_attacks_support, tooltip_conditions_neg, tooltip_conditions_pos, tooltip_items_weapons,
                tooltip_items_armor, tooltip_items_potions, tooltip_items_scrolls, tooltip_items_utility, tooltip_items_food
            };

            foreach (var dictionary in dictionaries)
            {
                foreach (var key in dictionary.Keys)
                {
                    if (key.Contains(" "))
                    {
                        multi_word_strings.Add(key);
                    }
                }
            }

            if (multi_word_strings.Count == 0)
            {
                multi_word_strings.Add("fatal_error");
            }

            return multi_word_strings;
        }

        private static string[] combine_multi_word_strings(string input)
        {
            // Multi-word strings aus den Arrays und Dictionaries extrahieren
            initialize_multi_word_strings();

            foreach (var name in player_active_names)
            {
                if (input.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    input = input.Replace(name, name.Replace(" ", "<X!"), StringComparison.OrdinalIgnoreCase);
                }
            }
            if (multi_word_strings != null)
            {
                foreach (var multi_word in multi_word_strings)
                {
                    if (input.Contains(multi_word, StringComparison.OrdinalIgnoreCase))
                    {
                        input = input.Replace(multi_word, multi_word.Replace(" ", "<X!"), StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            return input.Split(' ').Select(part => part.Replace("<X!", " ")).ToArray();
        }
        public static void death_fainted_check(Character target)
        {
            if (target.health_current <= 0)
            {
                if (target.fainted == false)
                {
                    target.fainted = true;
                    Console.WriteLine($"{target.character_name} fainted");
                    return;
                }
                if (target.fainted && target.alive)
                {
                    if (target.deathcount >= 3)
                    {
                        target.alive = false;
                        Console.WriteLine($"{target} died!");
                        target.deathcount = 0;
                    }
                    else if (target.health_previous > target.health_current)
                    {
                        Console.WriteLine($"{target.character_name} took damage while fainted: DANGER");
                        target.deathcount ++;
                    }
                    else if (target.health_previous < target.health_current)
                    {
                        Console.WriteLine($"{target.character_name} recieved healing while fainted: Good!");
                        target.deathcount --;
                    }

                    if (target.deathcount < 0) 
                    { 
                        target.deathcount = 0;
                    }
                }
                if (target.health_current > 0)
                {
                    target.fainted = false;
                    target.deathcount = 0;
                    Console.WriteLine($"{target.character_name} woke up");
                }
            }
            else
            {
                if (target.fainted)
                {
                    target.fainted = false;
                    Console.WriteLine($"{target.character_name} woke up");
                }
                target.deathcount = 0;
            }
        }
        public static string input_reader() // eigene Input verarbeitungsklasse machen
        {
            string? input_player = null;

            while (input_player == null || string.IsNullOrWhiteSpace(input_player))
            {
                input_player = Console.ReadLine();

                if (input_player == null || string.IsNullOrWhiteSpace(input_player))
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Try again or type \"help\"");
                }
                else
                {
                    string[] parts = combine_multi_word_strings(input_player);

                    if (parts.Length == 0)
                    {
                        Console.WriteLine("Invalid input");
                        input_player = null;
                        continue;
                    }

                    if (parts[0].ToLower() == "help")
                    {
                        Console.WriteLine("You asked for help");
                        Console.WriteLine();
                        help_output();
                    }
                    else if (parts.Length > 1 && parts[0].ToLower() == "ask")
                    {
                        try
                        {
                            Console.WriteLine($"du willst etwas über {parts[1]} wissen:");
                            Console.WriteLine($"You want to know more about {parts[1]}:");
                            ask_something(parts[1]);
                            Console.WriteLine();
                        }
                        catch
                        {
                            Console.WriteLine("This message should be 2 parts: ask [word]");
                            Console.WriteLine("Diese Nachricht sollte aus 2 Teilen bestehen: ask [word]");
                        }
                    }
                    else if (parts[0].ToLower() == "quit")
                    {
                        Console.WriteLine($"Do you want to quit the game? y / n ");
                        string confirm_quit = input_reader();
                        Console.WriteLine();

                        if (confirm_quit.ToLower() == "y")
                        {
                            Console.WriteLine($"You just quit the game. I hope you saved it!");
                            Console.WriteLine();
                            game = false;
                            break;
                        }
                        else if (confirm_quit.ToLower() == "n")
                        {
                            Console.WriteLine("Game Goes On!");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input, the game continues");
                        }

                    }
                    else if (parts[0].ToLower() == "save")
                    {
                        Console.WriteLine();
                        save_game();
                    }
                    else if (parts[0].ToLower() == "load")
                    {
                        Console.WriteLine("You want to load? ok...");
                        Console.WriteLine(); // load noch schreiben!
                        load_game();
                    }
                    else if (parts.Length > 1 && parts[0].ToLower() == "moveto")
                    {
                        Console.WriteLine($"Du willst den Ort wechseln:");
                        Console.WriteLine($"You want to move:");
                        location_moveto(parts[1]);
                        Console.WriteLine();
                    }
                    else if (parts[0].ToLower() == "fight" && Battle_System.fighton == false)
                    {
                        Console.WriteLine($"You want to pick a fight? Ok...");
                        Console.WriteLine();
                        Battle_System.infight();
                    }
                    else if (parts[0].ToLower() == "places") // umschreiben
                    {
                        Console.WriteLine();
                        Console.WriteLine("Those are the places you could go now:");
                        for (int i = 0; i < Place.places_available.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {Place.places_available[i]}");
                        }
                        Console.WriteLine();
                    }
                    else if (parts.Length > 1 && parts[1].ToLower() == "inventory")
                    {
                        try
                        {
                            found_player_for_inventory_equipment = false;
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    Console.WriteLine();
                                    Inventory_System.inventory_print(player);
                                    Console.WriteLine();
                                    found_player_for_inventory_equipment = true;
                                    break;
                                }
                            }
                            if (!found_player_for_inventory_equipment)
                            {
                                Console.WriteLine($"Player \"{parts[0]}\" not found in the party.");
                            }
                        }
                        catch 
                        {
                            Console.WriteLine("to call someones inventory correctly type: [character name] inventory");
                        }
                    }
                    else if (parts.Length > 1 && parts[1].ToLower() == "equipment")
                    {
                        try
                        {
                            found_player_for_inventory_equipment = false;
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    Console.WriteLine();
                                    Equipment_System.equipment_print(player);
                                    Console.WriteLine();
                                    found_player_for_inventory_equipment = true;
                                    break;
                                }
                            }
                            if (!found_player_for_inventory_equipment)
                            {
                                Console.WriteLine($"Player \"{parts[0]}\" not found in the party.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("to call someones equipment correctly type: [character name] equipment");
                        }
                    }
                    else if (parts.Length > 3 && party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) && parts[1] == "use")
                    {
                        try
                        {
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    var existing_item = player.inventory.inventory.Find(item => item.item_name == parts[2]
                                                                                     && item.item_value == Convert.ToInt32(parts[3]));
                                    if (existing_item != null)
                                    {
                                        if (existing_item is Item_Weapon || existing_item is Item_Armor)
                                        {
                                            Console.WriteLine($"{parts[2]} is not a usable item!");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{parts[0]} used {parts[2]} {parts[3]}");
                                            player.current_item?.use(player, parts[2], Convert.ToInt32(parts[3]));
                                            player.player_remove_item(parts[2], Convert.ToInt32(parts[3]), 1);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Could not find {parts[2]} {parts[3]} in inventory of {parts[0]}");
                                    }
                                    break;
                                }
                            }
                        }
                        catch 
                        {
                            Console.WriteLine("to use an item correctly type: [character name] use [item name] [item value]");
                        }
                    }
                    else if (parts.Length > 4 && party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) 
                                                                 && parts[1] == "cast" && parts[3] == "on")
                    {
                        try
                        {
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in party)
                                        if (target.character_name == parts[4])
                                        {
                                            player.player_cast_spell(player, target, parts[2]);
                                            break;
                                        }
                                    break;
                                }
                            }
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in enemies)
                                        if (target.character_name == parts[4])
                                        {
                                            player.player_cast_spell(player, target, parts[2]);
                                            break;
                                        }
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("to cast a spell correctly type: [caster name] cast [spell name] on [target name]");
                        }
                    }
                    else if (parts.Length > 4 && party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase))
                                             && parts[1] == "perform" && parts[3] == "on")
                    {
                        try
                        {
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in party)
                                        if (target.character_name == parts[4])
                                        {
                                            player.player_perform_special_attack(player, target, parts[2]);
                                            break;
                                        }
                                    break;
                                }
                            }
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in enemies)
                                        if (target.character_name == parts[4])
                                        {
                                            player.player_cast_spell(player, target, parts[2]);
                                            break;
                                        }
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("to perform an attack correctly type: [attacker name] perform [attack name] on [target name]");
                        }
                    }
                    else if (parts.Length > 3 && party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) && parts[1] == "equip")
                    {
                        try
                        {
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    var existing_item = player.inventory.inventory.Find(item => item.item_name == parts[2] && item.item_value == Convert.ToInt32(parts[3]));
                                    if (existing_item != null)
                                    {
                                        if (existing_item is Item_Potion || existing_item is Item_Scroll || existing_item is Item_Utility || existing_item is Item_Food)
                                        {
                                            Console.WriteLine($"{parts[2]} is not a equippable item!");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{parts[0]} equipped {parts[2]} {parts[3]}");
                                            player.equipment.equip_item(parts[2], Convert.ToInt32(parts[3]));
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Could not find {parts[2]} {parts[3]} in inventory of {parts[0]}");
                                    }
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("to equip an item correctly type: [character name] equip [item name] [item value]");
                        }
                    }
                    else if (parts.Length > 3 && party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) && parts[1] == "unequip")
                    {
                        try
                        {
                            foreach (Character player in party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    var existing_item = player.equipment.equipment.Find(item => item.item_name == parts[2] && item.item_value == Convert.ToInt32(parts[3]));
                                    if (existing_item != null)
                                    {
                                        Console.WriteLine($"{parts[0]} unequipped {parts[2]} {parts[3]}");
                                        player.equipment.unequip_item(parts[2], Convert.ToInt32(parts[3]));
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Could not find {parts[2]} {parts[3]} in equipment of {parts[0]}");
                                    }
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("to unequip an item correctly type: [character name] unequip [item name] [item value]");
                        }
                    }
                    /*else if (int.TryParse(input_player, out int classNumber))
                    {
                        return input_player;
                    }*/
                    else if (!string.IsNullOrWhiteSpace(input_player))
                    {
                        return input_player;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("Try again or type \"help\"");
                        input_player = null;
                    }
                }
            }
            return input_player;
        }
        static void help_output()
        {
            Console.WriteLine();
            Console.WriteLine("Usually when I present you options with numbers like 1) 2)");
            Console.WriteLine("you can type the pure number to pick that option.");
            Console.WriteLine("In vielen Fällen kannst du bei einer Auswahl entweder die Zahl oder den Namen eingeben (zeitmangel nicht alle)");
            Console.WriteLine("Or you can type the option itself.");
            Console.WriteLine("....I hope");
            Console.WriteLine("Some functions may not work right now!");
            Console.WriteLine("Ein paar Funktionen gehen nicht wie sie sollen!");
            Console.WriteLine();
            Console.WriteLine("When I present you \"y / n\" you should type \"y\" to confirm or \"n\" to decline.");
            Console.WriteLine("y und n sind Ja und Nein zum bestätigen");
            Console.WriteLine();
            Console.WriteLine("Also, you have a lot of basic options like \"ask [smth]\"");
            Console.WriteLine("to get more information on certain things.");
            Console.WriteLine("Schreibe ask und ein zweites Wort um mehr über Dinge zu erfahren (sind leider nicht alle vorhanden)");
            Console.WriteLine();
            Console.WriteLine("Here are the already existing basic options:");
            Console.WriteLine();
            Console.WriteLine("help ) What you just used.");
            Console.WriteLine("ask class ) To get a display of classes. Then type in the class you want information about.");
            Console.WriteLine("ask race ) To get a display of races. Then type in the race you want information about.");
            Console.WriteLine("ask item ) To get a display of items. Then type in the item you want information about.");
            Console.WriteLine("ask spell ) To get a display of spells. Then type in the spell you want information about.");
            Console.WriteLine("ask attack ) To get a display of attacks. Then type in the attack you want information about.");
            Console.WriteLine("ask condition ) To get a display of conditions. Then type in the condition you want information about.");
            Console.WriteLine("save ) Saves the Game to a slot.");
            Console.WriteLine("quit ) Ends the Game, please save your game beofre quitting!");
            Console.WriteLine("load ) Loads a file you pick.");
            Console.WriteLine("places ) Tells you what locations are available to you.");
            Console.WriteLine("move ) Let's you activly move to another location.");
            Console.WriteLine("[character name] inventory ) Displays the entire Inventory of a Player.");
            Console.WriteLine("[character name] inventory ) Zeigt das Inventar des Spielers an");
            Console.WriteLine("[character name] equipment ) Displays the entire Equipment of a Player.");
            Console.WriteLine("[character name] equip [item name] [item value] ) Player equips item.");
            Console.WriteLine("[character name] unequip [item name] [item value] ) Player unequips item.");
            Console.WriteLine("[character name] use [item] [item value]) Uses whatever you choose to use.");
            Console.WriteLine("[character name] use [item] [item value]) Versucht das entsprechende Item zu benutzen.");
            Console.WriteLine("Ja der [item value] Teil ist nötig. Items werden mit zufälligen werten erstellt.");
            Console.WriteLine("Und deswegen werden die Items auch mit value im Inventar und Equipment gespeichert.");
            Console.WriteLine("Nicht alle Items sind benutzbar. Teils beabsichtigt");
            Console.WriteLine("Not all Items are usable!");
            Console.WriteLine();
            Console.WriteLine("Oh, and by the way: go to Taverns or Pubs to heal back to full.");
            Console.WriteLine("Wenn du Tavern oder Pub betretest heilst du alle Spieler auf voll.");
            // alles reinschreiben was zum helfen dienen soll
        }
        public static void create_player()
        {
            Console.WriteLine("How many Players do you want to add?");
            party_size_complete = false;
            while (!party_size_complete)
            {
                Console.WriteLine("Please choose the size of your party (Minimum: 1, Recomandation: 4, Maximum: 10):");
                try
                {
                    picked_party_size = Convert.ToInt32(input_reader());
                    if (picked_party_size < 1 || picked_party_size > picked_party_max)
                    {
                        picked_party_size = 4;
                        Console.WriteLine("Invalid entry. Size of the Party is 4.");
                    }
                }
                catch (Exception)
                {
                    picked_party_size = 4;
                    Console.WriteLine("Invalid entry. Size of the Party is 4.");
                }
                Console.WriteLine();
                Console.WriteLine($"Do you want a party size of {picked_party_size}? y / n");
                confirm_size = input_reader();
                Console.WriteLine();
                if (confirm_size.ToLower() == "y")
                {
                    Console.WriteLine($"Size of your party: {picked_party_size}");
                    Console.WriteLine();
                    party_size_complete = true;
                }
                else if (confirm_size.ToLower() == "n")
                {
                    Console.WriteLine("You pressed NO, please try again!");
                    Console.WriteLine();
                    continue;
                }
                else
                {
                    Console.WriteLine("Could not confirm party size, please try again!");
                    Console.WriteLine();
                    continue;
                }
            }
            for (int i = 0; i < picked_party_size; i++)
            {
                // Neues player_character-Objekt erstellen und der Liste hinzufügen
                party_class_complete = false;
                while (!party_class_complete)
                {
                    Console.WriteLine();
                    Console.WriteLine("You have the following classes to choose from:");
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int j = 0; j < classes_playable.Length; j++)
                    {
                        Console.WriteLine($"{j + 1}) {classes_playable[j]}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please pick a class:");
                    try
                    {
                        pick_a_class_index = Convert.ToInt32(input_reader());
                        if (pick_a_class_index > 0 && pick_a_class_index <= classes_playable.Length)
                        {
                            Console.WriteLine($"You picked the class {classes_playable[pick_a_class_index - 1]}");

                        }
                        else
                        {
                            Console.WriteLine("Invalid class selection. I picked Fighter for you.");
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid class selection. I picked Fighter for you.");
                        continue;
                    }

                    Console.WriteLine("Is this correct? y / n ");
                    confirm_class = input_reader();
                    Console.WriteLine();

                    if (confirm_class.ToLower() == "y")
                    {
                        Console.WriteLine($"Class {classes_playable[pick_a_class_index - 1]} confirmed");
                        player_class = classes_playable[pick_a_class_index - 1];
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
                party_race_complete = false;
                while (!party_race_complete)
                {
                    Console.WriteLine();
                    Console.WriteLine("You have the following races to choose from:");
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int j = 0; j < races_playable.Length; j++)
                    {
                        Console.WriteLine($"{j + 1}) {races_playable[j]}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Please pick a race:");
                    try
                    {
                        pick_a_race_index = Convert.ToInt32(input_reader());
                        if (pick_a_race_index > 0 && pick_a_race_index <= races_playable.Length)
                        {
                            Console.WriteLine($"You picked the class {races_playable[pick_a_race_index - 1]}");

                        }
                        else
                        {
                            Console.WriteLine("Invalid class selection. I picked Human for you.");
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid class selection. I picked Human for you.");
                        continue;
                    }

                    Console.WriteLine("Is this correct? y / n ");
                    confirm_race = input_reader();
                    Console.WriteLine();

                    if (confirm_race.ToLower() == "y")
                    {
                        Console.WriteLine($"Race {races_playable[pick_a_race_index - 1]} confirmed");
                        player_race = races_playable[pick_a_race_index - 1];
                        party_race_complete = true;
                    }
                    else if (confirm_class.ToLower() == "n")
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
                party_name_complete = false;
                while (!party_name_complete)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please pick a name:");

                    player_name = input_reader();

                    if (string.IsNullOrWhiteSpace(player_name) || is_restricted_name(player_name) 
                            || party.Exists(p => p.character_name.ToLower() == player_name.ToLower()))
                    {
                        if (string.IsNullOrWhiteSpace(player_name))
                        {
                            Console.WriteLine("Name cannot be empty or whitespace. Please choose a different name.");
                        }
                        else if (is_restricted_name(player_name))
                        {
                            Console.WriteLine("This name is restricted. Please choose a different name.");
                        }
                        else
                        {
                            Console.WriteLine("A player with that name already exists. Please choose a different name.");
                        }
                        continue;
                    }
                    Console.WriteLine("Is this correct? y / n ");
                    confirm_name = input_reader();
                    Console.WriteLine();

                    if (confirm_name.ToLower() == "y")
                    {
                        Console.WriteLine($"Name {player_name} confirmed");
                        Console.WriteLine();
                        party_name_complete = true;
                        player_active_names.Add(player_name);
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
                Character_Player player = new Character_Player(player_class, player_race, player_name);
                player.apply_class_bonus();
                player.apply_race_bonus();
                player.apply_new_health();
                party.Add(player);
            }

            Console.WriteLine("Those are the Characters you have:");
            Console.WriteLine();
            foreach (Character player in party)
            {
                Console.WriteLine($"{player.character_name} the {player.character_race} {player.character_class}");
            }
            Console.WriteLine();
        }
        public static void location_moveto(string location_move_to)
        {
            try
            {
                Console.WriteLine("If you want to know which places are currently available type \"places\"");
                Console.WriteLine("Tell me the place you want to move to");

                if (places.Contains(location_move_to))
                {
                    Console.WriteLine();
                    Console.WriteLine($"moving from {location_current} to {location_move_to}");
                    location_current = location_move_to;
                    if (location_current == "Tavern" || location_current == "Pub") // campfire erstellen
                    {
                        foreach (Character player in party)
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
        static bool is_restricted_name(string name)
        {
            // Check if name is a restricted keyword or within the range of 0-100
            if (name.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("n", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("Invalid input", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (int.TryParse(name, out int number) && number >= 0 && number <= 100)
            {
                return true;
            }

            // Check if name is an ability or item name
            if (races_playable.Contains(name, StringComparer.OrdinalIgnoreCase)
                  || classes_playable.Contains(name, StringComparer.OrdinalIgnoreCase) 
                  || tooltip_classes_playable.ContainsKey(name) 
                  || tooltip_races_playable.ContainsKey(name) 
                  || tooltip_spells_healing.ContainsKey(name) 
                  || tooltip_spells_damaging.ContainsKey(name) 
                  || tooltip_spells_supporting.ContainsKey(name) 
                  || tooltip_spells_summoning.ContainsKey(name) 
                  || tooltip_attacks_special.ContainsKey(name) 
                  || tooltip_attacks_regular.ContainsKey(name) 
                  || tooltip_attacks_support.ContainsKey(name) 
                  || tooltip_conditions_neg.ContainsKey(name) 
                  || tooltip_conditions_pos.ContainsKey(name) 
                  || tooltip_items_weapons.ContainsKey(name) 
                  || tooltip_items_armor.ContainsKey(name) 
                  || tooltip_items_potions.ContainsKey(name) 
                  || tooltip_items_scrolls.ContainsKey(name) 
                  || tooltip_items_utility.ContainsKey(name) 
                  || tooltip_items_food.ContainsKey(name))
            {
                return true;
            }

            return false;
        }
        public static void ask_something(string ask_p1)
        {
            List<string> ask_list = new List<string>();
            count = 0;
            string ask_p2 = "";

            if (ask_p1.ToLower() == "class")
            {

                for (int i = 0; i < classes_playable.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {classes_playable[i]}");
                    ask_list.Add(classes_playable[i]);
                }
            }
            else if (ask_p1.ToLower() == "race")
            {
                for (int i = 0; i < races_playable.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {races_playable[i]}");
                    ask_list.Add(races_playable[i]);
                }
            }
            else if (ask_p1.ToLower() == "item")
            {
                
                for (int i = 0; i < items_weapons.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {items_weapons[i]}");
                    ask_list.Add(items_weapons[i]);
                    count++;
                }
                for (int i = 0; i < items_armor.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {items_armor[i]}");
                    ask_list.Add(items_armor[i]);  
                    count++;
                }
                for (int i = 0; i < items_potions.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {items_potions[i]}");
                    ask_list.Add(items_potions[i]);
                    count++;
                }
                for (int i = 0; i < items_scrolls.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {items_scrolls[i]}");
                    ask_list.Add(items_scrolls[i]); 
                    count++;
                }
                for (int i = 0; i < items_utility.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {items_utility[i]}");
                    ask_list.Add(items_utility[i]);    
                    count++;
                }
                for (int i = 0; i < items_food.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {items_food[i]}");
                    ask_list.Add(items_food[i]);
                    count++;
                }
            }
            else if (ask_p1.ToLower() == "spell")
            {

                for (int i = 0; i < spells_healing_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {spells_healing_array[i]}");
                    ask_list.Add(spells_healing_array[i]);
                    count++;
                }
                for (int i = 0; i < spells_damaging_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {spells_damaging_array[i]}");
                    ask_list.Add(spells_damaging_array[i]);
                    count++;
                }
                for (int i = 0; i < spells_supporting_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {spells_supporting_array[i]}");
                    ask_list.Add(spells_supporting_array[i]);
                    count++;
                }
                for (int i = 0; i < spells_summoning_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {spells_summoning_array[i]}");
                    ask_list.Add(spells_summoning_array[i]);
                    count++;
                }
            }
            else if (ask_p1.ToLower() == "attack")
            {

                for (int i = 0; i < attacks_special_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {attacks_special_array[i]}");
                    ask_list.Add(attacks_special_array[i]);  
                    count++;
                }
                for (int i = 0; i < attacks_regular_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {attacks_regular_array[i]}");
                    ask_list.Add(attacks_regular_array[i]);
                    count++;
                }
                for (int i = 0; i < attacks_support_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {attacks_support_array[i]}");
                    ask_list.Add(attacks_support_array[i]);
                    count++;
                }
            }
            else if (ask_p1.ToLower() == "condition")
            {

                for (int i = 0; i < conditions_neg_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {conditions_neg_array[i]}");
                    ask_list.Add(conditions_neg_array[i]);   
                    count++;
                }
                for (int i = 0; i < conditions_pos_array.Length; i++)
                {
                    Console.WriteLine($"{count + 1}) {conditions_pos_array[i]}");
                    ask_list.Add(conditions_pos_array[i]);    
                    count++;
                }
            }
            else
            {
                Console.WriteLine("Invalid ask: currently available asks are: class / race / item / spell / attack / condition");
            }

            Console.WriteLine();
            Console.WriteLine("Choose by typing the name or the number.");
            string ask_input = input_reader();
            if (int.TryParse(ask_input, out int parsed_value))
            {
                parsed_value -= 1;
                if (parsed_value >= 0 && parsed_value < ask_list.Count)
                {
                    ask_p2 = ask_list[parsed_value];
                }
            }
            else
            {
                ask_p2 = ask_input.Substring(0, 1).ToUpper() + ask_input.Substring(1);
                // falls statt der Zahl der Name verwendet wird!
            }
            ask_list.Clear();
            if (tooltip_classes_playable.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_classes_playable[ask_p2]);
            }
            else if (tooltip_races_playable.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_races_playable[ask_p2]);
            }
            else if (tooltip_items_weapons.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_items_weapons[ask_p2]);
            }
            else if (tooltip_items_armor.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_items_armor[ask_p2]);
            }
            else if (tooltip_items_potions.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_items_potions[ask_p2]);
            }
            else if (tooltip_items_scrolls.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_items_scrolls[ask_p2]);
            }
            else if (tooltip_items_utility.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_items_utility[ask_p2]);
            }
            else if (tooltip_items_food.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_items_food[ask_p2]);
            }
            else if (tooltip_spells_healing.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_spells_healing[ask_p2]);
            }
            else if (tooltip_spells_damaging.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_spells_damaging[ask_p2]);
            }
            else if (tooltip_spells_supporting.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_spells_supporting[ask_p2]);
            }
            else if (tooltip_spells_summoning.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_spells_summoning[ask_p2]);
            }
            else if (tooltip_attacks_special.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_attacks_special[ask_p2]);
            }
            else if (tooltip_attacks_regular.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_attacks_regular[ask_p2]);
            }
            else if (tooltip_attacks_support.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_attacks_support[ask_p2]);
            }
            else if (tooltip_conditions_neg.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_conditions_neg[ask_p2]);
            }
            else if (tooltip_conditions_pos.ContainsKey(ask_p2))
            {
                Console.WriteLine(tooltip_conditions_pos[ask_p2]);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }
        public static void start_menue()
        {
            Console.WriteLine("Welcome to Textadventure!");
            Console.WriteLine();
            Console.WriteLine("To end your game type: quit");
            Console.WriteLine("If you need any support type: help");
            Console.WriteLine();
            Console.WriteLine("Um das Spiel zu beenden Tippe folgendes ein: quit");
            Console.WriteLine("Um hilfe zu erhalten Tippe folgendes ein: help");
            Console.WriteLine();
            Console.WriteLine("Choose your language:");
            Console.WriteLine("Bitte Sprache auswählen:");
            Console.WriteLine("Aus zeitmangel laufen die sprachen parallel");
            Console.WriteLine();
            Console.WriteLine("1) English");
            Console.WriteLine("2) Deutsch");
            Console.WriteLine();

            switch (input_reader().ToLower())
            {
                case "1":
                case "english":
                    Console.WriteLine("You selected English.");
                    // Hier würdest du den Code für Englisch hinzufügen
                    break;
                case "2":
                case "deutsch":
                    Console.WriteLine("Du hast Deutsch ausgewählt.");
                    Console.WriteLine("Leider hab ich das noch nicht geschrieben dafür");
                    // Hier würdest du den Code für Deutsch hinzufügen
                    break;
                default:
                    Console.WriteLine("Invalid selection. Using English!");
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("1) New Game");
            Console.WriteLine("2) Load Game");
            Console.WriteLine("3) Multiplayer");
            Console.WriteLine("4) Quit");
            switch (input_reader().ToLower())
            {
                case "1":
                case "new game":
                    Console.WriteLine("You selected New Game");
                    create_player();
                    break;
                case "2":
                case "load game":
                    Console.WriteLine("Which save file do you want to load?");
                    load_game();
                    break;
                case "3":
                case "multiplayer":
                    Console.WriteLine("This option is not available now!");
                    Console.WriteLine("selected New Game instead");
                    create_player();
                    break;
                case "4":
                case "quit":
                    Console.WriteLine("Goodbye!");
                    game = false;
                    Environment.Exit(0);
                    // Hier würdest du den Code für Deutsch hinzufügen
                    break;
                default:
                    Console.WriteLine("Invalid selection. New Game starts...");
                    create_player();
                    break;
            }
        }

        static void save_game()
        {
            Console.WriteLine("Du willst speichern? Tippe das File ein das du benutzen möchtest oder schreibe ein neues");
            Console.WriteLine("Do you want to save your game?");
            Console.WriteLine("Please choose a savefile or make a new one");
            Console.WriteLine("0) make a new Save File by typing something that is not listed");

            List<string> file_name_list = File_Saving_And_Loading.get_file_names();
            for (int i = 0; i < file_name_list.Count; i++)
            {
                string file_name = Path.GetFileName(file_name_list[i]);
                string fn_without_ending = file_name.Substring(0, file_name.Length - 4);
                Console.WriteLine($"{i + 1}) {fn_without_ending}");
            }

            Console.WriteLine();
            string part_path = input_reader(); // Benutzerwahl lesen

            string completed_path_save = Path.Combine(filePath_save, $"{part_path}.txt");

            Console.WriteLine($"Is this correct? {completed_path_save} y / n ");
            string confirm_file = input_reader(); // Bestätigung lesen

            Console.WriteLine();


            if (confirm_file.ToLower() == "y")
            {
                List<string> file_content = File_Saving_And_Loading.collect_file_content(completed_path_save);
                File_Saving_And_Loading.saving_feature(completed_path_save, file_content);
            }
            else if (confirm_file.ToLower() == "n")
            {
                Console.WriteLine("Failed to save file, because you pressed NO!");
            }
            else
            {
                Console.WriteLine("Failed to save file! Incorrect input");
            }
        }
        public static void load_game()
        {
            Console.WriteLine("Please choose a savefile to load.");
            // ausbauen!
            Console.WriteLine("0) cancel");

            List<string> file_name_list = File_Saving_And_Loading.get_file_names();
            for (int i = 0; i < file_name_list.Count; i++)
            {
                string file_name = Path.GetFileName(file_name_list[i]);
                string fn_without_ending = file_name.Substring(0, file_name.Length - 4);
                Console.WriteLine($"{i + 1}) {fn_without_ending}");
            }

            Console.WriteLine();
            string part_path = input_reader(); // Benutzerwahl lesen

            if (part_path == "0")
            {
                Console.WriteLine("Loading canceled!");
                return;
            }
            string completed_path_save = Path.Combine(filePath_save, $"{part_path}.txt");

            Console.WriteLine($"Is this correct? {completed_path_save} y / n ");
            string confirm_file = input_reader(); // Bestätigung lesen

            Console.WriteLine();


            if (confirm_file.ToLower() == "y")
            {
                List<string> file_content = File_Saving_And_Loading.collect_file_content(completed_path_save);
                File_Saving_And_Loading.loading_feature(completed_path_save, file_content);
            }
            else if (confirm_file.ToLower() == "n")
            {
                Console.WriteLine("Failed to load file, because you pressed NO!");
            }
            else
            {
                Console.WriteLine("Failed to load file! Incorrect input");
            }
        }
        public static void title_screen()
        {
            // ASCII Art für "TA"
            Console.WriteLine("******************************");
            Console.WriteLine("*                            *");
            Console.WriteLine("*  TTTTTTTTTT      AAAAAA    *");
            Console.WriteLine("*      TT        AA      AA  *");
            Console.WriteLine("*      TT        AAAAAAAAAA  *");
            Console.WriteLine("*      TT        AA      AA  *");
            Console.WriteLine("*      TT        AA      AA  *");
            Console.WriteLine("*                            *");
            Console.WriteLine("******************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            title_screen();
            start_menue();

            while (game)
            {
                input_reader();
            }

        }
    }
}
