using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using static Textadventure.Battle_System;

namespace Textadventure
{
    public static class File_Saving_And_Loading
    {
        public static List<string> get_file_names()
        {
            string folder_path = Program_Main.filePath_save; // Geben Sie hier den Pfad zu Ihrem Ordner an

            Console.WriteLine("Checking path: " + folder_path); // Debug-Ausgabe zum Überprüfen des Pfades

            // Schritt 1: Finden aller .txt-Dateien im Ordner
            if (Directory.Exists(folder_path))
            {
                string[] txt_files = Directory.GetFiles(folder_path, "*.txt");

                // Schritt 2: Speichern der Dateipfade in einer Liste
                List<string> file_list = new List<string>(txt_files);

                // Debug-Ausgabe, um gefundene Dateien anzuzeigen
                Console.WriteLine("Found files:");
                foreach (var file in file_list)
                {
                    Console.WriteLine(file);
                }

                // Schritt 3: Rückgabe der Dateiliste
                return file_list;
            }
            else
            {
                Console.WriteLine("The directory does not exist.");
                return new List<string>();
            }
        }
        public static List<string> collect_file_content(string completed_path_save)
        {
            List<string> saving_info_list = new List<string>();

            //string line_0 = $"Game State ->|{Program_Main.location_current}|";
            //saving_info_list.Add(line_0);
            foreach (Character_Player player in Program_Main.party)
            {
                string line_1 = $"{player.character_name}|character data|{player.character_class}|{player.character_race}|{player.character_level}|{player.hit_dice}|{player.strength}|{player.dexterity}|" +
                                $"{player.constitution}|{player.intelligence}|{player.wisdom}|{player.health_max}|{player.health_current}|{player.mana_max}|{player.mana_current}|{player.initiative}|" +
                                $"{player.is_a_summon}|{player.fainted}|{player.alive}|";
                string line_2 = $"{player.character_name}|";
                string line_3 = $"{player.character_name}|";
                string line_4 = $"{player.character_name}|";
                string line_5 = $"{player.character_name}|";
                string line_6 = $"{player.character_name}|";
                string line_7 = $"{player.character_name}|";
                string line_8 = $"{player.character_name}|";
                string line_9 = $"{player.character_name}|";

                saving_info_list.Add(line_1);

                line_2 += "learnable_spells|";
                foreach (string entry in player.learnable_spells)
                {
                    line_2 += $"{entry}|";
                }
                saving_info_list.Add(line_2);

                line_3 += "learnable_special_attacks|";
                foreach (string entry in player.learnable_special_attacks)
                {
                    line_3 += $"{entry}|";
                }
                saving_info_list.Add(line_3);

                line_4 += "learned_regular_attacks|";
                foreach (KeyValuePair<string, int> kvp in player.learned_regular_attacks)
                {
                    line_4 += $"{kvp.Key},{kvp.Value}|";
                }
                saving_info_list.Add(line_4);

                line_5 += "learned_support_attacks|";
                foreach (KeyValuePair<string, int> kvp in player.learned_support_attacks)
                {
                    line_5 += $"{kvp.Key},{kvp.Value}|";
                }
                saving_info_list.Add(line_5);

                line_6 += "learned_spells|";
                foreach (KeyValuePair<string, int> kvp in player.learned_spells)
                {
                    line_6 += $"{kvp.Key},{kvp.Value}|";
                }
                saving_info_list.Add(line_6);

                line_7 += "learned_special_attacks|";
                foreach (KeyValuePair<string, int> kvp in player.learned_special_attacks)
                {
                    line_7 += $"{kvp.Key},{kvp.Value}|";
                }
                saving_info_list.Add(line_7);

                line_8 += "equipment|";
                foreach (var entry in player.equipment.equipment_get())
                {
                    line_8 += $"{entry.item_name}, {entry.item_value}|";
                }
                saving_info_list.Add(line_8);

                line_9 += "inventory|";
                foreach (var entry in player.inventory.inventory_get())
                {
                    line_9 += $"{entry.item_name}, {entry.item_value}, {entry.item_amount}|";
                }
                saving_info_list.Add(line_9);

            }

            return saving_info_list;
        }
        public static void saving_feature(string file_path, List<string> file_content)
        {
            try
            {
                // Datei erstellen und Inhalt schreiben
                File.WriteAllLines(file_path, file_content); // WriteAllLines verwendet, um eine Liste von Zeichenfolgen zu schreiben
                Console.WriteLine("Datei erfolgreich erstellt und Inhalt geschrieben.");
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine("Fehler: " + ex.Message);
            }
        }
        public static void loading_feature(string file_path, List<string> file_content)
        {
            Program_Main.game = true;
            Program_Main.player_active_names.Clear();
            Program_Main.party.Clear();
            Program_Main.enemies.Clear();
            Program_Main.active_npc_list.Clear();
            Program_Main.multi_word_strings = null;
            Place.places_available.Clear();

            Character_Player? current_player_loading = null;
            try
            {
                string[] lines = File.ReadAllLines(file_path);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (line.StartsWith("Game State ->"))
                    {
                        Program_Main.location_current = parts[1];

                        // Hier können allgemeine Spielzustände geladen werden
                    }
                    else
                    {
                        switch (parts[1])
                        {
                            case "character data":
                                current_player_loading = new Character_Player();

                                current_player_loading.character_name = parts[0];
                                current_player_loading.character_class = parts[2];
                                current_player_loading.character_race = parts[3];
                                current_player_loading.character_level = int.Parse(parts[4]);
                                current_player_loading.hit_dice = int.Parse(parts[5]);
                                current_player_loading.strength = int.Parse(parts[6]);
                                current_player_loading.dexterity = int.Parse(parts[7]);
                                current_player_loading.constitution = int.Parse(parts[8]);
                                current_player_loading.intelligence = int.Parse(parts[9]);
                                current_player_loading.wisdom = int.Parse(parts[10]);
                                current_player_loading.health_max = double.Parse(parts[11]);
                                current_player_loading.health_current = double.Parse(parts[12]);
                                current_player_loading.mana_max = int.Parse(parts[13]);
                                current_player_loading.mana_current = int.Parse(parts[14]);
                                current_player_loading.initiative = int.Parse(parts[15]);
                                current_player_loading.is_a_summon = bool.Parse(parts[16]);
                                current_player_loading.fainted = bool.Parse(parts[17]);
                                current_player_loading.alive = bool.Parse(parts[18]);

                                current_player_loading.learned_spells = new Dictionary<string, int>();
                                current_player_loading.learnable_spells = new List<string>();

                                current_player_loading.learned_special_attacks = new Dictionary<string, int>();
                                current_player_loading.learnable_special_attacks = new List<string>();

                                current_player_loading.learned_regular_attacks = new Dictionary<string, int>();
                                current_player_loading.learned_support_attacks = new Dictionary<string, int>();

                                current_player_loading.equipment = new Equipment_System();
                                current_player_loading.inventory = new Inventory_System();

                                Program_Main.party.Add(current_player_loading);
                                break;

                            case "learnable_spells":
                                if (current_player_loading != null)
                                {
                                    current_player_loading.learnable_spells.AddRange(parts.Skip(2));
                                }
                                break;

                            case "learnable_special_attacks":
                                if (current_player_loading != null)
                                {
                                    current_player_loading.learnable_special_attacks.AddRange(parts.Skip(2));
                                }
                                break;

                            case "learned_regular_attacks":
                                if (current_player_loading != null)
                                {
                                    load_in_learned_attacks_and_spells(current_player_loading.learned_regular_attacks, parts.Skip(2));
                                }
                                break;

                            case "learned_support_attacks":
                                if (current_player_loading != null)
                                {
                                    load_in_learned_attacks_and_spells(current_player_loading.learned_support_attacks, parts.Skip(2));
                                }
                                break;

                            case "learned_spells":
                                if (current_player_loading != null)
                                {
                                    load_in_learned_attacks_and_spells(current_player_loading.learned_spells, parts.Skip(2));
                                }
                                break;

                            case "learned_special_attacks":
                                if (current_player_loading != null)
                                {
                                    load_in_learned_attacks_and_spells(current_player_loading.learned_special_attacks, parts.Skip(2));
                                }
                                break;

                            case "equipment":
                                if (current_player_loading != null)
                                {
                                    load_equipment(current_player_loading.equipment, parts.Skip(2));
                                }
                                break;

                            case "inventory":
                                if (current_player_loading != null)
                                {
                                    load_inventory(current_player_loading.inventory, parts.Skip(2));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                // places_available braucht wahrscheinlich eine neue funktion dafür
                Console.WriteLine("Load succesful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loading Error: {ex}");
            }
        }
        private static void load_in_learned_attacks_and_spells(Dictionary<string, int> attacks_and_spells, IEnumerable<string> parts)
        {
            foreach (var part in parts)
            {
                var kvp = part.Split(',');
                if (kvp.Length == 2)
                {
                    attacks_and_spells[kvp[0]] = int.Parse(kvp[1]);
                }
            }
        }
        private static void load_equipment(Equipment_System player_equipment, IEnumerable<string> parts)
        {
            foreach (var part in parts)
            {
                var entry = part.Split(',');
                if (entry.Length == 2)
                {
                    string item_name = entry[0];
                    int item_value = int.Parse(entry[1]);
                    player_equipment.equip_item(item_name, item_value);
                }
            }
        }
        private static void load_inventory(Inventory_System player_inventory, IEnumerable<string> parts)
        {
            foreach (var part in parts)
            {
                var entry = part.Split(',');
                if (entry.Length == 3)
                {
                    string item_name = entry[0];
                    int item_value = int.Parse(entry[1]);
                    int item_amount = int.Parse(entry[2]);

                    player_inventory.inventory_add_item(item_name, item_value, item_amount);
                }
            }
        }
    }
}

