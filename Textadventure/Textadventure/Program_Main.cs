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
        public static int count = 0;
        static int picked_party_size = 4;
        static int picked_party_max = 10;
        static int pick_a_class_index = 0;
        static int pick_a_race_index = 0;
        public static int total_defeated_enemies = 0;

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

        public static string[] spells_healing_array = { "Cure Wounds", "Healing Touch", "Rejuvenate", "Holy Light", "Ressurect" };
        public static string[] spells_damaging_array = { "Fireball", "Thunder", "Frostbolt", "Acid", "Shadowbolt" };
        public static string[] spells_supporting_array = { "Aura of Devotion", "Haste", "Banish", "Aura of Retribution", "Curse" };
        public static string[] spells_summoning_array = { "Familiar", "Elemental Servant", "Celestial Guardian", "Demon", "Nature's Ally" };
        public static string[] attacks_special_array = { "Mortal Strike", "Aimed Shot", "Backstab", "Smite", "Stealth" };
        public static string[] attacks_regular_array = { "Punch", "Strike", "Shoot", "Throw" };
        public static string[] attacks_support_array = { "Trap", "Ensnare", "Aid", "Block" };
        public static string[] conditions_neg_array = { "Normal", "Weakened", "Rooted", "Frozen", "Poisoned", "Burning", "Bleeding", "Incapacitated", "Banished"};
        public static string[] conditions_pos_array = { "Hasted", "Devoted", "Retributing", "Strentghened", "Stealthed" };
        public static string[] dialog_basic = { "Yes", "No", "Confirm", "Decline", "Buy", "Sell", "Leave", "Enter" };
        public static string[] actions_talk = { };
        public static string[] actions_trade = { };
        public static string[] actions_battle = { };
        public static string[] actions_surrounding = { };
        public static string[] items_weapons = { "Sword", "Dagger", "Bow", "Mace", "Staff" };
        public static string[] items_armor = { "Shield", "Ring", "Necklace", "Cloak", "Plate", "Chainmail", "Studded Leather", "Leather", "Cloth" };
        public static string[] items_potions = { "Potion of Healing", "Potion of Mana", "Potion of Poison", "Potion of Strength", "Potion of Weakness" };
        public static string[] items_scrolls = { "Scroll of Spell" }; // zugriff auf einen Zauber, mal sehen wie das genau wird // NICHT ARRAY 
        public static string[] items_utility = { "Rope", "Wood", "Flint and Steel", "Bag", "Coin", "Map", "Small Key" };
        public static string[] items_food = { "Fish", "Chicken", "SoupSoupSoup", "Beef", "Vegetables", "Herbs", "Water" };
        public static string[] places = { "Town", "Forest", "Desert", "Village", "Cave", "Dungeon", "Castle", "Market", "Mountain", "Mountain-Deeps", "Tavern" };
        public static string[] classes_playable = { "Fighter", "Ranger", "Rogue", "Paladin", "Priest", "Sorcerer", "Wizard", "Warlock" };
        public static string[] races_playable = { "Human", "Elven", "Orc", "Dwarf", "Gnome", "Tauren", "Troll", "Goblin" };
        public static string[] races_enemy = { "Human", "Elven", "Orc", "Dwarf", "Gnome", "Tauren", "Troll", "Goblin", "Creature", "Demon", "Undead" };

        //static bool activate = false;
        public static bool game = true;
        public static bool found_player_for_inventory_equipment = false;
        static bool party_size_complete = false;
        static bool party_class_complete = false;
        static bool party_race_complete = false;
        static bool party_name_complete = false;

        public static Dictionary<string, string> tooltip_classes_playable = new Dictionary<string, string>
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

        public static Dictionary<string, string> tooltip_races_playable = new Dictionary<string, string>
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

        public static Dictionary<string, string> tooltip_spells_healing = new Dictionary<string, string>
        {
            { "Cure Wounds", "Cure Wounds" },
            { "Healing Touch", "Healing Touch" },
            { "Rejuvenate", "Rejuvenate" },
            { "Holy Light", "Holy Light" },
            { "Ressurection", "Ressurection" }
        };
        public static Dictionary<string, string> tooltip_spells_damaging = new Dictionary<string, string>
        {
            { "Fireball", "Fireball" },
            { "Thunder", "Thunder" },
            { "Frostbolt", "Frostbolt" },
            { "Acid", "Acid" },
            { "Shadowbolt", "Shadowbolt" }
        };
        public static Dictionary<string, string> tooltip_spells_supporting = new Dictionary<string, string>
        {
            { "Aura of Devotion", "Aura of Devotion" },
            { "Haste", "Haste" },
            { "Banish", "Banish" },
            { "Aura of Retribution", "Aura of Retribution" },
            { "Curse", "Curse" }
        };
        public static Dictionary<string, string> tooltip_spells_summoning = new Dictionary<string, string>
        {
            { "Familiar", "Familiar" },
            { "Elemental Servant", "Elemental Servant" },
            { "Celestial Guardian", "Celestial Guardian" },
            { "Demon", "Demon" },
            { "Nature's Ally", "Nature's Ally" }
        };

        public static Dictionary<string, string> tooltip_attacks_special = new Dictionary<string, string>
        {
            { "Mortal Strike", "Mortal Strike" },
            { "Aimed Shot", "Aimed Shot" },
            { "Backstab", "Backstab" },
            { "Smite", "Smite" },
            { "Stealth", "Stealth" }
        };
        public static Dictionary<string, string> tooltip_attacks_regular = new Dictionary<string, string>
        {
            { "Punch", "Punch" },
            { "Strike", "Strike"}, // braucht Waffe
            { "Throw", "Throw" }, // mit Schild massiv besser
            { "Shoot", "Shoot" }, // helfe jemanden zu treffen
            { "Run", "Run"}
        };
        public static Dictionary<string, string> tooltip_attacks_support = new Dictionary<string, string>
        {
            { "Trap", "Trap" },
            { "Ensnare", "Ensnare" },
            { "Aid", "Aid" },
            { "Block", "Block" }
        };

        public static Dictionary<string, string> tooltip_conditions_neg = new Dictionary<string, string>
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
        public static Dictionary<string, string> tooltip_conditions_pos = new Dictionary<string, string>
        {
            { "Hasted", "Hasted" },
            { "Devoted", "Devoted" },
            { "Retributing", "Retributing" },
            { "Strentghened", "Strentghened" },
            { "Stealthed", "Stealthed" }
        };

        public static Dictionary<string, string> tooltip_items_weapons = new Dictionary<string, string>
        {
            { "Sword", "Sword" },
            { "Dagger", "Dagger" },
            { "Bow", "Bow" },
            { "Mace", "Mace" },
            { "Staff", "Staff" }
        };
        public static Dictionary<string, string> tooltip_items_armor = new Dictionary<string, string>
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
        public static Dictionary<string, string> tooltip_items_potions = new Dictionary<string, string>
        {
            { "Potion of Healing", "Potion of Healing" },
            { "Potion of Mana", "Potion of Mana" },
            { "Potion of Poison", "Potion of Poison" },
            { "Potion of Strength", "Potion of Strength" },
            { "Potion of Weakness", "Potion of Weakness" }
        };
        public static Dictionary<string, string> tooltip_items_scrolls = new Dictionary<string, string>
        {
            { "Scroll of Spell", "Scroll of Spell" }
        };
        public static Dictionary<string, string> tooltip_items_utility = new Dictionary<string, string>
        {
            { "Rope", "Rope" },
            { "Wood", "Wood" },
            { "Flint and Steel", "Flint and Steel" },
            { "Bag", "Bag" },
            { "Coin", "Coin" },
            { "Map", "Map" },
            { "Small Key", "Small Key" }
        };
        public static Dictionary<string, string> tooltip_items_food = new Dictionary<string, string>
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

        public static void death_fainted_check(Character target)
        {
            if (target.health_current <= 0)
            {
                if (target.player_status.conditions["fainted"] == false)
                {
                    target.player_status.conditions["fainted"] = true;
                    Console.WriteLine($"{target.character_name} fainted");
                    return;
                }
                if (target.player_status.conditions["fainted"] && target.player_status.conditions["alive"])
                {
                    if (target.deathcount >= 3)
                    {
                        target.player_status.conditions["alive"] = false;
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
                    target.player_status.conditions["fainted"] = false;
                    target.deathcount = 0;
                    Console.WriteLine($"{target.character_name} woke up");
                }
            }
            else
            {
                if (target.player_status.conditions["fainted"])
                {
                    target.player_status.conditions["fainted"] = false;
                    Console.WriteLine($"{target.character_name} woke up");
                }
                target.deathcount = 0;
            }
        }
        public static void help_output()
        {
            Console.WriteLine();
            Console.WriteLine("Usually when I present you options with numbers like 1) 2)");
            Console.WriteLine("you can type the pure number to pick that option.");
            Console.WriteLine("Or you can type the option itself.");
            Console.WriteLine("....I hope");
            Console.WriteLine("Some functions may not work right now!");
            Console.WriteLine();
            Console.WriteLine("When I present you \"y / n\" you should type \"y\" to confirm or \"n\" to decline.");
            Console.WriteLine();
            Console.WriteLine("Also, you have a lot of basic options like \"ask [smth]\"");
            Console.WriteLine("to get more information on certain things.");
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
            Console.WriteLine("moveto ) Let's you activly move to another location.");
            Console.WriteLine("[character name] inventory ) Displays the entire Inventory of a Player.");
            Console.WriteLine("[character name] inventory ) Zeigt das Inventar des Spielers an");
            Console.WriteLine("[character name] equipment ) Displays the entire Equipment of a Player.");
            Console.WriteLine("[character name] equip [item name] [item value] ) Player equips item.");
            Console.WriteLine("[character name] unequip [item name] [item value] ) Player unequips item.");
            Console.WriteLine("[character name] use [item] [item value]) Uses whatever you choose to use.");
            Console.WriteLine("[character name] use [item] [item value]) Versucht das entsprechende Item zu benutzen.");
            Console.WriteLine("Not all Items are usable!");
            Console.WriteLine("to cast a spell correctly type: [caster name] cast [spell name] on [target name]");
            Console.WriteLine("to perform an attack correctly type: [attacker name] perform [attack name] on [target name]");
            Console.WriteLine("to start a fight just type: fight");
            Console.WriteLine("fight is a bit buggy, not every attack hits, Fireball is your best bet.");
            Console.WriteLine("Everyone has Fireball because of that");
            Console.WriteLine();
            Console.WriteLine("Oh, and by the way: go to Taverns or Pubs to heal back to full.");
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
                    picked_party_size = Convert.ToInt32(Input_System.input_reader());
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
                confirm_size = Input_System.input_reader();
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
                        pick_a_class_index = Convert.ToInt32(Input_System.input_reader());
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
                    confirm_class = Input_System.input_reader();
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
                        pick_a_race_index = Convert.ToInt32(Input_System.input_reader());
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
                    confirm_race = Input_System.input_reader();
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

                    player_name = Input_System.input_reader();

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
                    confirm_name = Input_System.input_reader();
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
            Place.initialize_default_places();
            location_current = "Tavern";
        }
        static bool is_restricted_name(string name)
        {
            //  Check if name is a restricted keyword or within the range of 0-100
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
        public static void start_menue()
        {
            Console.WriteLine("Welcome to Textadventure!");
            Console.WriteLine();
            Console.WriteLine("To end your game type: quit");
            Console.WriteLine("If you need any support type: help");
            Console.WriteLine();
            Console.WriteLine("Choose your language:");
            Console.WriteLine("Bitte Sprache auswählen:");
            Console.WriteLine("Aus zeitmangel laufen die sprachen parallel");
            Console.WriteLine();
            Console.WriteLine("1) English");
            Console.WriteLine("2) Deutsch");
            Console.WriteLine();

            switch (Input_System.input_reader().ToLower())
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
            switch (Input_System.input_reader().ToLower())
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
                    break;
                default:
                    Console.WriteLine("Invalid selection. New Game starts...");
                    create_player();
                    break;
            }
        }

        public static void save_game()
        {
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
            string part_path = Input_System.input_reader(); // Benutzerwahl lesen

            string completed_path_save = Path.Combine(filePath_save, $"{part_path}.txt");

            Console.WriteLine($"Is this correct? {completed_path_save} y / n ");
            string confirm_file = Input_System.input_reader(); // Bestätigung lesen

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
            string part_path = Input_System.input_reader(); // Benutzerwahl lesen

            if (part_path == "0")
            {
                Console.WriteLine("Loading canceled!");
                return;
            }
            string completed_path_save = Path.Combine(filePath_save, $"{part_path}.txt");

            Console.WriteLine($"Is this correct? {completed_path_save} y / n ");
            string confirm_file = Input_System.input_reader(); // Bestätigung lesen

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
            Console.WriteLine("**************************************************************************************************");
            Console.WriteLine("*                                                                                                *");
            Console.WriteLine("*  TTTTTTT   eeeee  x    x   t      AAAAA      d        eeeee           t                eeeee   *");
            Console.WriteLine("*     T     e     e  x  x  ttttt   A     A     d       e     e nnnnn  ttttt       r rrr e     e  *");
            Console.WriteLine("*     T     eeeeeee   xx     t     AAAAAAA  dddd v   v eeeeeee n    n   t   u   u  rr   eeeeeee  *");
            Console.WriteLine("*     T     e        x  x    t     A     A d   d  v v  e       n    n   t   u   u  r    e        *");
            Console.WriteLine("*     T      eeeee  x    x   tt    A     A  dddd   v    eeeee  n    n   tt   uuuu  r     eeeee   *");
            Console.WriteLine("*                                                                                                *");
            Console.WriteLine("**************************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            /*
             > cat(art, sep = "\n")

                 -------------- 
                Hello 
                 --------------
                    \
                      \
                        \
                            |\___/|
                          ==) ^Y^ (==
                            \  ^  /
                             )=*=(
                            /     \
                            |     |
                           /| | | |\
                           \| | |_|/\
                      jgs  //_// ___/
                               \_)
             */
        }
        static void Main(string[] args)
        {
            title_screen();
            start_menue();
            while (game)
            {
                Input_System.input_reader();
            }
        }
    }
}
