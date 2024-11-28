using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snake
{
    public static class File_Saving_And_Loading
    {
        public static List<string> collect_file_content(string completed_path_save)
        {
            // Definiere die verschiedenen Spielfeldgrößen
            var sizes = new[] { 20, 40, 60, 80, 100 };

            // Mapping der Spielfeldgrößen auf die entsprechenden Variablen
            var sizeValues = new Dictionary<int, (int Name, int Value)>
            {
                { 20, (Main_Window.HS_S_Name_20, Main_Window.HS_S_Value_20) },
                { 40, (Main_Window.HS_S_Name_40, Main_Window.HS_S_Value_40) },
                { 60, (Main_Window.HS_S_Name_60, Main_Window.HS_S_Value_60) },
                { 80, (Main_Window.HS_S_Name_80, Main_Window.HS_S_Value_80) },
                { 100, (Main_Window.HS_S_Name_100, Main_Window.HS_S_Value_100) }
            };

            List<string> savingInfoList = new List<string>();

            // Kopfzeile hinzufügen
            string headerLine = "Tempo|";
            foreach (var size in sizes)
            {
                headerLine += $"{size}x{size}|";
            }
            savingInfoList.Add(headerLine);

            // Liste der Tempo-Werte
            var tempos = new[] { 0, 1, 2, 4, 8, 10 };

            // Erzeuge die Zeilen für die verschiedenen Tempo-Werte
            foreach (var tempo in tempos)
            {
                string line = $"T {tempo}|";
                foreach (var size in sizes)
                {
                    var (name, value) = sizeValues[size];
                    line += $"{name},{value}|";
                }
                savingInfoList.Add(line);
            }

            return savingInfoList;
        }

        public static void saving_feature(string file_path, List<string> file_content)
        {
            try
            {
                File.WriteAllLines(file_path, file_content);
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        public static void loading_feature(string file_path, List<string> file_content)
        {
            try
            {
                string[] lines = File.ReadAllLines(file_path);

                // Mapping der Spielfeldgrößen auf die entsprechenden Variablen in Main_Window
                var sizeValues = new Dictionary<int, (Action<int> NameSetter, Action<int> ValueSetter)>
                {
                    { 20, (val => Main_Window.HS_S_Name_20 = val, val => Main_Window.HS_S_Value_20 = val) },
                    { 40, (val => Main_Window.HS_S_Name_40 = val, val => Main_Window.HS_S_Value_40 = val) },
                    { 60, (val => Main_Window.HS_S_Name_60 = val, val => Main_Window.HS_S_Value_60 = val) },
                    { 80, (val => Main_Window.HS_S_Name_80 = val, val => Main_Window.HS_S_Value_80 = val) },
                    { 100, (val => Main_Window.HS_S_Name_100 = val, val => Main_Window.HS_S_Value_100 = val) }
                };

                var sizes = new[] { 20, 40, 60, 80, 100 };

                foreach (string line in lines.Skip(1)) // Die Kopfzeile überspringen
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('|');

                    // Verarbeite nur Zeilen, die mit "T " beginnen
                    if (parts[0].StartsWith("T "))
                    {
                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (i > sizes.Length) break; // Sicherstellen, dass nicht mehr Werte als Größen verarbeitet werden

                            string[] values = parts[i].Split(',');
                            if (values.Length == 2)
                            {
                                int nameValue = int.Parse(values[0]);
                                int dataValue = int.Parse(values[1]);

                                // Setze die entsprechenden Werte in Main_Window
                                var size = sizes[i - 1];
                                var setters = sizeValues[size];
                                setters.NameSetter(nameValue);
                                setters.ValueSetter(dataValue);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
        }
    }
}
