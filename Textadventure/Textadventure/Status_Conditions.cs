using System;
using System.Collections.Generic;

namespace Textadventure
{
    public class Status_Conditions
    {
        private Character? player;
        public Dictionary<string, bool> conditions;

        // Konstruktor, der die Statusbedingungen initialisiert
        public Status_Conditions()
        {
            conditions = new Dictionary<string, bool>
            {
                { "alive", true },
                { "fainted", false },
                { "is_a_summon", false },
                { "hasted", false },
                { "devoted", false },
                { "retributing", false },
                { "strengthened_physical_percentage", false },
                { "strengthened_magical_percentage", false },
                { "strengthened_physical_value", false },
                { "strengthened_magical_value", false },
                { "stealthed", false },
                { "weakened_physical_percentage", false },
                { "weakened_physical_value", false },
                { "weakened_magical_percentage", false },
                { "weakened_magical_value", false },
                { "rooted", false },
                { "frozen", false },
                { "poisoned", false },
                { "burning", false },
                { "bleeding", false },
                { "incapacitated", false },
                { "banished", false }
            };
        }

        public Status_Conditions(Character player) : this()
        {
            this.player = player;
        }

        public static void print_statuses(Character player)
        {
            Console.WriteLine($"Statuses of {player.character_name}:");
            foreach (var status in player.player_status.conditions)
            {
                if (status.Value == true)
                {
                    Console.WriteLine($"{player.character_name}: {status.Key}");
                }
            }
        }
    }
}
