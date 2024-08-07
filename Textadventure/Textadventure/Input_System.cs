using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Input_System
    {
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
                Program_Main.spells_healing_array, Program_Main.spells_damaging_array, Program_Main.spells_supporting_array, Program_Main.spells_summoning_array,
                Program_Main.attacks_special_array, Program_Main.attacks_regular_array, Program_Main.attacks_support_array, Program_Main.conditions_neg_array,
                Program_Main.conditions_pos_array, Program_Main.dialog_basic, Program_Main.items_weapons, Program_Main.items_armor, Program_Main.items_potions, Program_Main.items_utility, Program_Main.items_food, Program_Main.places,
                Program_Main.classes_playable, Program_Main.races_playable, Program_Main.races_enemy
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
                Program_Main.tooltip_classes_playable, Program_Main.tooltip_races_playable, Program_Main.tooltip_spells_healing, Program_Main.tooltip_spells_damaging,
                Program_Main.tooltip_spells_supporting, Program_Main.tooltip_spells_summoning, Program_Main.tooltip_attacks_special, Program_Main.tooltip_attacks_regular,
                Program_Main.tooltip_attacks_support, Program_Main.tooltip_conditions_neg, Program_Main.tooltip_conditions_pos, Program_Main.tooltip_items_weapons,
                Program_Main.tooltip_items_armor, Program_Main.tooltip_items_potions, Program_Main.tooltip_items_scrolls, Program_Main.tooltip_items_utility, Program_Main.tooltip_items_food
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

            foreach (var name in Program_Main.player_active_names)
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
        public static void ask_something(string ask_p1)
        {
            List<string> ask_list = new List<string>();
            Program_Main.count = 0;
            string ask_p2 = "";

            if (ask_p1.ToLower() == "class")
            {

                for (int i = 0; i < Program_Main.classes_playable.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {Program_Main.classes_playable[i]}");
                    ask_list.Add(Program_Main.classes_playable[i]);
                }
            }
            else if (ask_p1.ToLower() == "race")
            {
                for (int i = 0; i < Program_Main.races_playable.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {Program_Main.races_playable[i]}");
                    ask_list.Add(Program_Main.races_playable[i]);
                }
            }
            else if (ask_p1.ToLower() == "item")
            {

                for (int i = 0; i < Program_Main.items_weapons.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.items_weapons[i]}");
                    ask_list.Add(Program_Main.items_weapons[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.items_armor.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.items_armor[i]}");
                    ask_list.Add(Program_Main.items_armor[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.items_potions.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.items_potions[i]}");
                    ask_list.Add(Program_Main.items_potions[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.items_scrolls.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.items_scrolls[i]}");
                    ask_list.Add(Program_Main.items_scrolls[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.items_utility.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.items_utility[i]}");
                    ask_list.Add(Program_Main.items_utility[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.items_food.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.items_food[i]}");
                    ask_list.Add(Program_Main.items_food[i]);
                    Program_Main.count++;
                }
            }
            else if (ask_p1.ToLower() == "spell")
            {

                for (int i = 0; i < Program_Main.spells_healing_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.spells_healing_array[i]}");
                    ask_list.Add(Program_Main.spells_healing_array[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.spells_damaging_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.spells_damaging_array[i]}");
                    ask_list.Add(Program_Main.spells_damaging_array[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.spells_supporting_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.spells_supporting_array[i]}");
                    ask_list.Add(Program_Main.spells_supporting_array[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.spells_summoning_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.spells_summoning_array[i]}");
                    ask_list.Add(Program_Main.spells_summoning_array[i]);
                    Program_Main.count++;
                }
            }
            else if (ask_p1.ToLower() == "attack")
            {

                for (int i = 0; i < Program_Main.attacks_special_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.attacks_special_array[i]}");
                    ask_list.Add(Program_Main.attacks_special_array[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.attacks_regular_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.attacks_regular_array[i]}");
                    ask_list.Add(Program_Main.attacks_regular_array[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.attacks_support_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.attacks_support_array[i]}");
                    ask_list.Add(Program_Main.attacks_support_array[i]);
                    Program_Main.count++;
                }
            }
            else if (ask_p1.ToLower() == "condition")
            {

                for (int i = 0; i < Program_Main.conditions_neg_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.conditions_neg_array[i]}");
                    ask_list.Add(Program_Main.conditions_neg_array[i]);
                    Program_Main.count++;
                }
                for (int i = 0; i < Program_Main.conditions_pos_array.Length; i++)
                {
                    Console.WriteLine($"{Program_Main.count + 1}) {Program_Main.conditions_pos_array[i]}");
                    ask_list.Add(Program_Main.conditions_pos_array[i]);
                    Program_Main.count++;
                }
            }
            else
            {
                Console.WriteLine("Invalid ask: currently available asks are: class / race / item / spell / attack / condition");
            }

            Console.WriteLine();
            Console.WriteLine("Choose by typing the name or the number.");
            string ask_input = Input_System.input_reader();
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
            if (Program_Main.tooltip_classes_playable.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_classes_playable[ask_p2]);
            }
            else if (Program_Main.tooltip_races_playable.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_races_playable[ask_p2]);
            }
            else if (Program_Main.tooltip_items_weapons.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_items_weapons[ask_p2]);
            }
            else if (Program_Main.tooltip_items_armor.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_items_armor[ask_p2]);
            }
            else if (Program_Main.tooltip_items_potions.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_items_potions[ask_p2]);
            }
            else if (Program_Main.tooltip_items_scrolls.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_items_scrolls[ask_p2]);
            }
            else if (Program_Main.tooltip_items_utility.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_items_utility[ask_p2]);
            }
            else if (Program_Main.tooltip_items_food.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_items_food[ask_p2]);
            }
            else if (Program_Main.tooltip_spells_healing.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_spells_healing[ask_p2]);
            }
            else if (Program_Main.tooltip_spells_damaging.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_spells_damaging[ask_p2]);
            }
            else if (Program_Main.tooltip_spells_supporting.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_spells_supporting[ask_p2]);
            }
            else if (Program_Main.tooltip_spells_summoning.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_spells_summoning[ask_p2]);
            }
            else if (Program_Main.tooltip_attacks_special.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_attacks_special[ask_p2]);
            }
            else if (Program_Main.tooltip_attacks_regular.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_attacks_regular[ask_p2]);
            }
            else if (Program_Main.tooltip_attacks_support.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_attacks_support[ask_p2]);
            }
            else if (Program_Main.tooltip_conditions_neg.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_conditions_neg[ask_p2]);
            }
            else if (Program_Main.tooltip_conditions_pos.ContainsKey(ask_p2))
            {
                Console.WriteLine(Program_Main.tooltip_conditions_pos[ask_p2]);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }
        public static void input_help()
        {
            Console.WriteLine("You asked for help");
            Console.WriteLine();
            Program_Main.help_output();
        }
        public static void input_ask(string part_ask)
        {
            try
            {
                Console.WriteLine($"You want to know more about {part_ask}:");
                ask_something(part_ask);
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("This message should be 2 parts: ask [word]");
            }
        }
        public static void input_quit()
        {
            Console.WriteLine($"Do you want to quit the game? y / n ");
            string confirm_quit = input_reader();
            Console.WriteLine();

            if (confirm_quit.ToLower() == "y")
            {
                Console.WriteLine($"You just quit the game. I hope you saved it!");
                Console.WriteLine();
                Program_Main.game = false;
                //break; Schleifen beenden lassen nicht vergessen!
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
        public static void input_inventory(string player_to_check)
        {
            try
            {
                Program_Main.found_player_for_inventory_equipment = false;
                foreach (Character player in Program_Main.party)
                {
                    if (player.character_name == player_to_check)
                    {
                        Console.WriteLine();
                        Inventory_System.inventory_print(player);
                        Console.WriteLine();
                        Program_Main.found_player_for_inventory_equipment = true;
                        break;
                    }
                }
                if (!Program_Main.found_player_for_inventory_equipment)
                {
                    Console.WriteLine($"Player \"{player_to_check}\" not found in the party.");
                }
            }
            catch
            {
                Console.WriteLine("to call someones inventory correctly type: [character name] inventory");
            }
        }
        public static void input_equipment(string player_to_check)
        {
            try
            {
                Program_Main.found_player_for_inventory_equipment = false;
                foreach (Character player in Program_Main.party)
                {
                    if (player.character_name == player_to_check)
                    {
                        Console.WriteLine();
                        Equipment_System.equipment_print(player);
                        Console.WriteLine();
                        Program_Main.found_player_for_inventory_equipment = true;
                        break;
                    }
                }
                if (!Program_Main.found_player_for_inventory_equipment)
                {
                    Console.WriteLine($"Player \"{player_to_check}\" not found in the party.");
                }
            }
            catch
            {
                Console.WriteLine("to call someones equipment correctly type: [character name] equipment");
            }
        }
        public static void input_use(string using_character, string used_item, string used_item_value)
        {
            try
            {
                foreach (Character player in Program_Main.party)
                {
                    if (player.character_name == using_character)
                    {
                        var existing_item = player.inventory.inventory.Find(item => item.item_name == used_item
                                                                         && item.item_value == Convert.ToInt32(used_item_value));
                        if (existing_item != null)
                        {
                            if (existing_item is Item_Weapon || existing_item is Item_Armor)
                            {
                                Console.WriteLine($"{used_item} is not a usable item!");
                            }
                            else
                            {
                                Console.WriteLine($"{using_character} used {used_item} {used_item_value}");
                                player.current_item?.use(player, used_item, Convert.ToInt32(used_item_value));
                                player.player_remove_item(used_item, Convert.ToInt32(used_item_value), 1);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Could not find {used_item} {used_item_value} in inventory of {using_character}");
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
        public static void input_equip_item(string equipping_character, string equip_item, string equip_item_value)
        {
            try
            {
                foreach (Character player in Program_Main.party)
                {
                    if (player.character_name == equipping_character)
                    {
                        var existing_item = player.inventory.inventory.Find(item => item.item_name == equip_item && item.item_value == Convert.ToInt32(equip_item_value));
                        if (existing_item != null)
                        {
                            if (existing_item is Item_Potion || existing_item is Item_Scroll || existing_item is Item_Utility || existing_item is Item_Food)
                            {
                                Console.WriteLine($"{equip_item} is not a equippable item!");
                            }
                            else
                            {
                                Console.WriteLine($"{equipping_character} equipped {equip_item} {equip_item_value}");
                                player.equipment.equip_item(equip_item, Convert.ToInt32(equip_item_value));
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Could not find {equip_item} {equip_item_value} in inventory of {equipping_character}");
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
        public static void input_unequip_item(string unequipping_character, string unequip_item, string unequip_item_value)
        {
            try
            {
                foreach (Character player in Program_Main.party)
                {
                    if (player.character_name == unequipping_character)
                    {
                        var existing_item = player.equipment.equipment.Find(item => item.item_name == unequip_item && item.item_value == Convert.ToInt32(unequip_item_value));
                        if (existing_item != null)
                        {
                            Console.WriteLine($"{unequipping_character} unequipped {unequip_item} {unequip_item_value}");
                            player.equipment.unequip_item(unequip_item, Convert.ToInt32(unequip_item_value));
                        }
                        else
                        {
                            Console.WriteLine($"Could not find {unequip_item} {unequip_item_value} in equipment of {unequipping_character}");
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
        public static void input_help1()
        {

        }
        public static void input_help21()
        {

        }
        public static string input_reader()
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
                        input_help();
                    }
                    else if (parts.Length > 1 && parts[0].ToLower() == "ask")
                    {
                        input_ask(parts[1]);
                    }
                    else if (parts[0].ToLower() == "quit")
                    {
                        input_quit();
                    }
                    else if (parts[0].ToLower() == "save")
                    {
                        Program_Main.save_game(); // funktioniert prima beides, aber
                    }
                    else if (parts[0].ToLower() == "load")
                    {
                        Program_Main.load_game(); // bei gelegenheit besser Strukturieren
                    }
                    else if (parts.Length > 1 && parts[0].ToLower() == "moveto")
                    {
                        Place.location_moveto(parts[1]);
                        Duration_System.duration_decrement_all_characters();
                    }
                    else if (parts[0].ToLower() == "camp")
                    {
                        Place.location_moveto("Campfire");
                        Duration_System.duration_decrement_all_characters();
                    }
                    else if (parts[0].ToLower() == "places")
                    {
                        Place.list_available_places();
                    }
                    else if (parts[0].ToLower() == "fight" && Battle_System.fighton == false)
                    {
                        Console.WriteLine($"You want to pick a fight? Ok...");
                        Console.WriteLine();
                        Battle_System.infight();
                    }
                    else if (parts.Length > 1 && parts[1].ToLower() == "inventory")
                    {
                        input_inventory(parts[0]);
                    }
                    else if (parts.Length > 1 && parts[1].ToLower() == "equipment")
                    {
                        input_equipment(parts[0]);
                    }
                    else if (parts.Length > 3 && Program_Main.party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) && parts[1] == "use")
                    {
                        input_use(parts[0], parts[2], parts[3]);
                    }
                    else if (parts.Length > 4 && Program_Main.party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase))
                                                                 && parts[1] == "cast" && parts[3] == "on")
                    {   // noch in Ruhe lassen, später neu Strukturieren
                        try
                        {
                            foreach (Character player in Program_Main.party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in Program_Main.party)
                                        if (target.character_name == parts[4])
                                        {
                                            player.player_cast_spell(player, target, parts[2]);
                                            break;
                                        }
                                    break;
                                }
                            }
                            foreach (Character player in Program_Main.party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in Program_Main.enemies)
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
                    else if (parts.Length > 4 && Program_Main.party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase))
                                             && parts[1] == "perform" && parts[3] == "on")
                    {   // noch in Ruhe lassen, später neu Strukturieren
                        try
                        {
                            foreach (Character player in Program_Main.party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in Program_Main.party)
                                        if (target.character_name == parts[4])
                                        {
                                            player.player_perform_special_attack(player, target, parts[2]);
                                            break;
                                        }
                                    break;
                                }
                            }
                            foreach (Character player in Program_Main.party)
                            {
                                if (player.character_name == parts[0])
                                {
                                    foreach (Character target in Program_Main.enemies)
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
                    else if (parts.Length > 3 && Program_Main.party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) && parts[1] == "equip")
                    {
                        input_equip_item(parts[0], parts[2], parts[3]);
                    }
                    else if (parts.Length > 3 && Program_Main.party.Any(p => p.character_name.Equals(parts[0], StringComparison.OrdinalIgnoreCase)) && parts[1] == "unequip")
                    {
                        input_unequip_item(parts[0], parts[2], parts[3]);
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
    }
}
