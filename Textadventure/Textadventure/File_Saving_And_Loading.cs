using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using static Textadventure.Filler_Kampfsystem;

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
            foreach (Character_Player player in Program_Main.party)
            {
                string line_1 = $"{player.character_name}|character data |{player.character_class}|{player.character_race}|{player.character_level}|{player.hit_dice}|{player.strength}|{player.dexterity}|{player.constitution}|{player.intelligence}|{player.wisdom}|{player.health_max}|{player.health_current}|{player.mana_max}|{player.mana_current}|";
                string line_2 = $"{player.character_name}|";
                string line_3 = $"{player.character_name}|";
                string line_4 = $"{player.character_name}|";
                string line_5 = $"{player.character_name}|";
                saving_info_list.Add(line_1);

                line_2 += "learnable_spells |";
                foreach (string entry in player.learnable_spells)
                {
                    line_2 += $"{entry}|";
                }
                line_2 += "learnable_special_attacks |";
                foreach (string entry in player.learnable_special_attacks)
                {
                    line_2 += $"{entry}|";
                }
                saving_info_list.Add(line_2);

                line_3 += "learned_regular_attacks |";
                foreach (KeyValuePair<string, int> kvp in player.learned_regular_attacks)
                {
                    line_3 += $"{kvp.Key},{kvp.Value}|";
                }
                line_3 += "learned_support_attacks |";
                foreach (KeyValuePair<string, int> kvp in player.learned_support_attacks)
                {
                    line_3 += $"{kvp.Key},{kvp.Value}|";
                }
                line_3 += "learned_spells |";
                foreach (KeyValuePair<string, int> kvp in player.learned_spells)
                {
                    line_3 += $"{kvp.Key},{kvp.Value}|";
                }
                line_3 += "learned_special_attacks |";
                foreach (KeyValuePair<string, int> kvp in player.learned_special_attacks)
                {
                    line_3 += $"{kvp.Key},{kvp.Value}|";
                }
                saving_info_list.Add(line_3);

                line_4 += "equipment |";
                foreach (var entry in player.equipment.equipment_get()  )
                {
                    line_4 += $"{entry.item_name}, {entry.item_value}, {entry.item_amount}|";
                }
                saving_info_list.Add(line_4);

                line_5 += "inventory |";
                foreach (var entry in player.inventory.inventory_get())
                {
                    line_5 += $"{entry.item_name}, {entry.item_value}, {entry.item_amount}|";
                }
                saving_info_list.Add(line_5);
            }
            try
            {
                File.WriteAllLines(completed_path_save, saving_info_list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Saving failed, missing information!");
                Console.WriteLine("Error: " + ex.Message);
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
    }
}
