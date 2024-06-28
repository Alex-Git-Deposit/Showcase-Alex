﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Battle_System
    {
        public static Simple_Random rnd = new Simple_Random();

        public static bool fighton = false;

        public static List<Character> valid_attack_target = new List<Character>();
        public static List<Character> valid_heal_target = new List<Character>();
        public static List<string> actions = new List<string>();
        public static void generate_enemies()
        {
            int amount_to_spawn_in = rnd.Next(1, 3);

            for (int i = 0; i < amount_to_spawn_in; i++)
            {
                Program_Main.enemies.Add(new Character_Enemy());
            }
            Console.WriteLine($"{amount_to_spawn_in} enemies generated and added to the fight.");

        }
        public static void collect_actions_ai(Character_Enemy enemy)
        {
            actions.AddRange(enemy.learned_regular_attacks.Keys);
            actions.AddRange(enemy.learned_support_attacks.Keys);
            actions.AddRange(enemy.learned_spells.Keys);
            actions.AddRange(enemy.learned_special_attacks.Keys);

            enemy.collected_actions_damaging = actions;
        }
        public static void collect_targets_ai()
        {
            // Angreifbare Ziele sammeln
            valid_attack_target.Clear();
            valid_heal_target.Clear();

            foreach (var character in Program_Main.party)
            {
                if (character.alive)
                {
                    valid_attack_target.Add(character);
                }
            }

            foreach (var character in Program_Main.active_npc_list) // solange nur Enemy im Kampf ist passt das
            {
                if (character.alive)
                {
                    valid_attack_target.Add(character);
                }
            }

            foreach (var enemy in Program_Main.enemies)
            {
                if (enemy.alive)
                {
                    valid_heal_target.Add(enemy);
                }
            }
        }
        public static void inventory_inspect_ai(Character_Enemy enemy)
        {
            // Schaue was im Inventar ist
            // Sortiere nach benutzbarkeit
            {
                var positive_items = new List<Item>();
                var negative_items = new List<Item>();

                /*foreach (var item in enemy.inventory)
                {
                    if (item.name == "Potion of Healing")
                    {
                        positive_items.Add(item);
                    }
                    else
                    {
                        negative_items.Add(item);
                    }
                }*/

                //enemy.PositiveItems = positive_items;
                //enemy.NegativeItems = negative_items; 
            }
        }
        public static void decide_action_target_ai_level_zero(Character_Enemy enemy)
        {
            Simple_Random rnd = new Simple_Random();

            int run = 2;
            if (rnd.check_probability(run))
            {
                // Wegrennen
                Console.WriteLine($"{enemy.character_name} tries to run away. But running away is a non existent feature!");
                enemy.collected_actions_damaging.Clear();
                enemy.collected_actions_healing.Clear();
                actions.Clear();
                return;
            }

            if (enemy.collected_actions_damaging.Count == 0)
            {
                Console.WriteLine($"{enemy.character_name} has no damaging actions available.");
                return;
            }

            int action_index = rnd.Next(0, enemy.collected_actions_damaging.Count);
            string action = enemy.collected_actions_damaging[action_index];

            int friendly_fire = 10;
            if (rnd.check_probability(friendly_fire))
            {
                // Schadhaftes Item noch einbauen
                if (valid_attack_target.Count > 0)
                {
                    int target_index = rnd.Next(0, valid_attack_target.Count);
                    Character target = valid_attack_target[target_index];
                    Attack_System.check_attack(enemy, target, action);
                    Console.WriteLine($"{enemy.character_name} uses harmful action on {target.character_name}");
                    enemy.collected_actions_damaging.Clear();
                    enemy.collected_actions_healing.Clear();
                    actions.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine($"{enemy.character_name} has no valid attack targets.");
                }
            }

            if (valid_attack_target.Count > 0)
            {
                int target_index = rnd.Next(0, valid_attack_target.Count);
                Character target = valid_attack_target[target_index];
                Attack_System.check_attack(enemy, target, action);
                Console.WriteLine($"{enemy.character_name} uses {action} on {target.character_name}");
            }
            else
            {
                Console.WriteLine($"{enemy.character_name} has no valid attack targets.");
            }

            enemy.collected_actions_damaging.Clear();
            enemy.collected_actions_healing.Clear();
            actions.Clear();
        }
        public static void combat_ai(Character character)
        {
            Character_Enemy? enemy = null;
            Character_Neutral_NPC? neutral = null;

            if (character is Character_Enemy)
            {
                enemy = (Character_Enemy)character;
            }
            else if (character is Character_Neutral_NPC)
            {
                neutral = (Character_Neutral_NPC)character;
            }

            int aiLevel = 0;

            if (enemy != null)
            {
                aiLevel = enemy.ai_level;
            }
            else if (neutral != null)
            {
                aiLevel = neutral.ai_level;
            }
            if (enemy != null)
            {
                switch (aiLevel)
                {
                    case 0:
                        collect_actions_ai(enemy);
                        collect_targets_ai();
                        inventory_inspect_ai(enemy);
                        decide_action_target_ai_level_zero(enemy);
                        break;
                    case 1:
                        collect_actions_ai(enemy);
                        collect_targets_ai();
                        inventory_inspect_ai(enemy);
                        decide_action_target_ai_level_zero(enemy);
                        break;
                    case 2:
                        collect_actions_ai(enemy);
                        collect_targets_ai();
                        inventory_inspect_ai(enemy);
                        decide_action_target_ai_level_zero(enemy);
                        break;
                    case 3:
                        collect_actions_ai(enemy);
                        collect_targets_ai();
                        inventory_inspect_ai(enemy);
                        decide_action_target_ai_level_zero(enemy);
                        break;
                    case 4:
                        collect_actions_ai(enemy);
                        collect_targets_ai();
                        inventory_inspect_ai(enemy);
                        decide_action_target_ai_level_zero(enemy);
                        break;
                    default:
                        break;
                }
            }
            /* 
            Level 0 Sammel alle gültigen Aktionen // Sammel alle gültigen Ziele
            --> zufällige Auswahl // kleine Chance auf Friendly Fire // kleine Chance auf Fluchtversuch
            --> Inventar manchmal benutzen

            Level 1 Sammeln // minimale Chance auf Friendly Fire // minimale Chance auf Fluchtversuch
            --> Chance auf freunde heilen falls möglich falls unter 50%
            --> aid // block  je nach dem wie ich block schreibe
            --> Ziele unter 75% chance erhöht unter 50% chance erhöht
            --> kleine Chance gezielt Mana Fähigkeit zu benutzen
            --> Inventar manchmal benutzen

            Level 2 kein Friendly Fire // kein Fluchtversuch 
            --> höhere Chance auf freunde heilen falls möglich falls unter 50%
            --> Ziele unter 50% Leben bevorzugen unter 25% leicht höher
            --> aid // block im blick halten
            --> kleine Chance auf Ziel mit niedrigsten gesamtpunkten
            --> moderate chance auf einen Zufall
            --> erhöhte Chance gezielt Mana Fähigkeit zu benutzen
            --> Inventar öfter benutzen

            Level 3 heilt grundsätzlich falls möglich
            --> moderate Chance auf Ziel mit niedrigsten gesamtpunkten
            --> Ziele nach unter 50% sortieren höhere Chance bei unter 25%
            --> aid // block im blick halten
            --> kleine chance auf einen Zufall
            --> hohe Chance gezielt Mana Fähigkeit zu benutzen
            --> Inventar immer benutzen

            Level 4 *
            --> Fokus auf niedriges gesamtleben oder unter 25%
            --> aid // block im blick halten
            --> Fokus Fire
            --> kein Zufall
            --> nutzt Mana
            */
            Console.WriteLine("Enemy Action performed");
        }
        public static void gain_exp(Character_Player player)
        {
            int exp_gain = 25;
            int exp_to_lvl_up = 250;
            player.character_exp += exp_gain;
            Console.WriteLine($"{player.character_name} gained {exp_gain} Experience");
            if (player.character_exp >= exp_to_lvl_up)
            {
                player.character_exp -= exp_to_lvl_up;
                player.character_level++;
                Console.WriteLine($"{player.character_name} is now Level {player.character_level}");
            }
            if (player.character_level == 10)
            {
                Console.WriteLine("You reached Level 10. YOU WIN THE GAME! But i allow you to continue: endless mode activated");
            }
        }
        public static void roll_initiative(Character character)
        {
            int d20 = rnd.Next(1, 21); // Zufällige Zahl zwischen 1 und 20
            if (d20 == 1)
            {
                d20 -= 10;
            }
            else if (d20 == 20)
            {
                d20 += 10;
            }
            character.initiative = d20 + character.dexterity;
        }
        public static void print_initiative(Character character)
        {
            Console.WriteLine($"{character.character_name}: initiative {character.initiative}");
        }
        public static bool action_detect(Character player, string input)
        {
            if (input.Contains("cast") || input.Contains("perform") || input.Contains("use"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void send_next_wave()
        {
            Console.WriteLine("More enemies enter the fight!");
            generate_enemies();

        }
        public static void create_loot() // in eigene Klasse auslagern bald
        {
            Simple_Random rnd = new Simple_Random();
            int dropchance = 25;
            if (rnd.check_probability(dropchance))
            {
                int type_choise_rnd = rnd.Next(1, 101);
                int item_choice_rnd = -1;
                string item_pick_name = "Item";
                try
                {
                    if (type_choise_rnd >= 1 && type_choise_rnd <= 10)
                    {
                        item_choice_rnd = rnd.Next(0, Program_Main.items_weapons.Length);
                        item_pick_name = Program_Main.items_weapons[item_choice_rnd];
                    }
                    else if (type_choise_rnd >= 11 && type_choise_rnd <= 20)
                    {
                        item_choice_rnd = rnd.Next(0, Program_Main.items_armor.Length);
                        item_pick_name = Program_Main.items_armor[item_choice_rnd];
                    }
                    else if (type_choise_rnd >= 21 && type_choise_rnd <= 30)
                    {
                        item_choice_rnd = rnd.Next(0, Program_Main.items_potions.Length);
                        item_pick_name = Program_Main.items_potions[item_choice_rnd];
                    }
                    else if (type_choise_rnd == 999)
                    {
                        // solange ich scrolls noch nicht geschrieben habe bleibt es bei unmöglich zu erreichen
                        item_choice_rnd = rnd.Next(0, Program_Main.items_scrolls.Length);
                        item_pick_name = Program_Main.items_scrolls[item_choice_rnd];
                    }
                    else if (type_choise_rnd >= 31 && type_choise_rnd <= 40)
                    {
                        item_choice_rnd = rnd.Next(0, Program_Main.items_utility.Length);
                        item_pick_name = Program_Main.items_utility[item_choice_rnd];
                    }
                    else if (type_choise_rnd >= 41 && type_choise_rnd <= 50)
                    {
                        item_choice_rnd = rnd.Next(0, Program_Main.items_food.Length);
                        item_pick_name = Program_Main.items_food[item_choice_rnd];
                    }
                    else if (type_choise_rnd >= 51 && type_choise_rnd <= 100) // Platzhalter
                    {
                        item_choice_rnd = rnd.Next(0, Program_Main.items_food.Length);
                        item_pick_name = Program_Main.items_food[item_choice_rnd];
                    }
                    else
                    {
                        // um den folgenden part auf kein loot zu setzen
                        item_choice_rnd = -1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("This should not have happened!");
                }

                if (item_pick_name == "Item")
                {
                    // Für den Notfall
                    item_choice_rnd = -1;
                }

                if (item_choice_rnd >= 0)
                {

                    List<Character_Player> looter = new List<Character_Player>();
                    foreach (Character_Player player in Program_Main.party)
                    {
                        if (player.alive == true)
                        {
                            looter.Add(player);
                        }
                    }
                    int x = rnd.Next(0, looter.Count);
                    Item item_created = Item_Factory.create_item(item_pick_name, 1);
                    Console.WriteLine($"{looter[x].character_name} loots: {item_created.item_name}");
                    looter[x].player_add_item(item_created.item_name, item_created.item_value, 1);
                    looter.Clear();
                }
                else
                {
                    Console.WriteLine("There is no loot from this enemy");
                }
            }
            else
            {
                Console.WriteLine("There is no loot from this enemy");
            }
        }
        public static void game_over() // eigene klasse bald drauß machen
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine("Choose one option:");
            Console.WriteLine("1) Load Save File");
            Console.WriteLine("2) Start New Game");
            Console.WriteLine("3) Quit Game");

            string input = Program_Main.input_reader();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Loading Save File...");
                    Console.WriteLine("Or not, i don't have Saves right now.");
                    Program_Main.load_game();
                    break;
                case "2":
                    Console.WriteLine("Starting New Game...");

                    // Clean up von Load
                    Program_Main.game = true;
                    Program_Main.player_active_names.Clear();
                    Program_Main.party.Clear();
                    Program_Main.enemies.Clear();
                    Program_Main.active_npc_list.Clear();
                    Program_Main.multi_word_strings = null;
                    Place.places_available.Clear();

                    Program_Main.start_menue();

                    // Startsequenz
                    break;
                case "3":
                    Console.WriteLine("Quitting Game...");
                    Environment.Exit(0); // Beendet die Anwendung
                    break;
                default:
                    Console.WriteLine("Invalid pick: Quitting Game...");
                    Environment.Exit(0);
                    break;
            }
        }
        public static void infight()
        {
            Console.WriteLine("Fight starts!");
            Console.WriteLine("Es gibt ein Problem mit dem erlenen Regular und Support attacks");
            Console.WriteLine("Es gibt ein Problem mit dem verwenden von Special attacks (Schaden nicht ausgeführt)");
            Console.WriteLine("Zum Schaden machen bitte nur Zauber verwenden, Mana ist genug vorhanden");
            Console.WriteLine("Fireball kann jeder (fürs erste)");
            Console.WriteLine("Außerdem heißen alle Feinde EH aus Zeitmangel");
            Console.WriteLine();
            fighton = true;
            bool first_round = true;
            List<Character_Enemy> defeated_enemies = new List<Character_Enemy>();
            List<Character> fight_round_list = new List<Character>();
            Queue<Character> fight_round_queue = new Queue<Character>();
            int wavecount = rnd.Next(1, 3);
            int turncount = 0;

            while (fighton)
            {
                turncount++;
                foreach (Character_Player player in Program_Main.party)
                {
                    if (player.alive == true)
                    {
                        fight_round_list.Add(player);
                    }
                }
                if (first_round)
                {
                    generate_enemies();
                    first_round = false;
                }

                // Temporäre Liste für zu entfernende Feinde
                List<Character_Enemy> enemies_to_remove = new List<Character_Enemy>();

                foreach (Character_Enemy enemy in Program_Main.enemies)
                {
                    if (enemy.alive == true)
                    {
                        fight_round_list.Add(enemy);
                    }
                    else
                    {
                        enemies_to_remove.Add(enemy);
                        defeated_enemies.Add(enemy);
                    }
                }

                // Entfernen von Feinden nach der Iteration
                foreach (Character_Enemy enemy in enemies_to_remove)
                {
                    Program_Main.enemies.Remove(enemy);
                }

                foreach (Character character in fight_round_list)
                {
                    roll_initiative(character);
                }

                fight_round_list = fight_round_list.OrderByDescending(character => character.initiative).ToList();

                foreach (Character character in fight_round_list)
                {
                    print_initiative(character);
                    fight_round_queue.Enqueue(character);
                }

                while (fight_round_queue.Count > 0)
                {
                    Character character = fight_round_queue.Peek(); // Das nächste Element in der Warteschlange anzeigen, ohne es zu entfernen
                    if (character.fainted == false)
                    {
                        if (Program_Main.party.Contains(character))
                        {
                            bool player_action_bool = false;
                            while (!player_action_bool)
                            {
                                /*if action "saved" == block||aid --> add buff */
                                /* irgendwas in der Richtung */
                                Console.WriteLine($"{character.character_name}, please type your action:");
                                Console.WriteLine();
                                if (action_detect(character, Program_Main.input_reader()))
                                {
                                    player_action_bool = true;
                                    Console.WriteLine($"action performed, next player!");
                                }
                                else
                                {
                                    player_action_bool = false;
                                    Console.WriteLine($"Invalid action: type help/ask or punch, whatever i decided on"); // ausschreiben
                                }
                            }
                        }
                        else if (Program_Main.enemies.Contains(character))
                        {
                            /*if action "saved" == block||aid --> add buff */
                            combat_ai(character); // ausbauen
                        }
                        else if (Program_Main.active_npc_list.Contains(character))
                        {
                            // Platzhalter 
                            combat_ai(character); // ausbauen
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong! Empty Character in Queue");
                        }
                    }
                    else if (character.fainted == true)
                    {
                        Console.WriteLine($"{character.character_name} is fainted --> turn skipped");
                    }
                    else
                    {
                        Console.WriteLine("Something went horribly wrong! Empty Entry in Queue");
                    }
                    fight_round_queue.Dequeue(); // Das Element entfernen, nachdem es bearbeitet wurde
                }
                if (Program_Main.party.All(player => player.alive == false))
                {
                    fighton = false;
                    Console.WriteLine("The entire Party is dead");
                    game_over();
                }
                else if ((turncount % 5 == 0 || Program_Main.enemies.All(enemy => enemy.alive == false)
                        || Program_Main.enemies.All(enemy => enemy.fainted == true)) && wavecount > 0)
                {
                    send_next_wave();
                    wavecount --;
                    fighton = true;
                }
                else if ((Program_Main.enemies.All(enemy => enemy.alive == false) || Program_Main.enemies.All(enemy => enemy.fainted == true)) && wavecount <= 0)
                {
                    Console.WriteLine("All enemies defeated!");
                    wavecount = -1;
                    fighton = false;
                }

                fight_round_queue.Clear();
                fight_round_list.Clear();
            }
            Console.WriteLine("Fight ended!");
            for (int i = 0; i < defeated_enemies.Count; i++)
            {
                create_loot();
                foreach (Character_Player player in Program_Main.party)
                {
                    gain_exp(player);
                }
            }
            defeated_enemies.Clear();
        }
    }
}