using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Duration_System
    {
        public static void duration_decrement_all_characters()
        {
            List<List<Character>> all_groups = new List<List<Character>>
            {
                Program_Main.party,
                Program_Main.enemies,
                Program_Main.active_npc_list
            };

            foreach (var group in all_groups)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    var character = group[i];
                    character.reduce_all_effect_durations();
                }
            }
        }
        public static void duration_increment_all_characters() // increase all party schreiben nicht vergessen
        {
            List<List<Character>> all_groups = new List<List<Character>>
            {
                Program_Main.party,
                Program_Main.enemies,
                Program_Main.active_npc_list
            };

            foreach (var group in all_groups)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    var character = group[i];
                    character.increase_all_effect_durations();
                }
            }
        }

        public static void duration_ending(Character character, string effect)
        {
            if (character.player_status.conditions["is_a_summon"])
            {
                if (character is Character_Summon_Enemy summon_e)
                {
                    if (summon_e.duration_left <= 0)
                    {
                        Program_Main.enemies.Remove(summon_e);
                        Console.WriteLine($"{summon_e.character_name} fades out of existence (it was a Summon)");
                    }
                }
                else if (character is Character_Summon_Neutral_NPC summon_n)
                {
                    if (summon_n.duration_left <= 0)
                    {
                        Program_Main.active_npc_list.Remove(summon_n);
                        Console.WriteLine($"{summon_n.character_name} fades out of existence (it was a Summon)");
                    }
                }
                else if (character is Character_Summon_Player summon_p)
                {
                    if (summon_p.duration_left <= 0)
                    {
                        Program_Main.party.Remove(summon_p);
                        Console.WriteLine($"{summon_p.character_name} fades out of existence (it was a Summon)");
                    }
                }
            }
            if (effect == "Potion of Strength")
            {
                double restore = - character.character_status_value[effect];
                character.change_physical_power_percentage += restore;
            }
            else if (effect == "Potion of Weakness")
            {
                double restore = character.character_status_value[effect];
                character.change_physical_power_percentage += restore;
            }
            else if (effect == "Potion of Poison")
            {
                // Status wird entfernt, vielleicht noch einmal zum Abschluss einen Tick ausführen
            }
        }
    }

}
