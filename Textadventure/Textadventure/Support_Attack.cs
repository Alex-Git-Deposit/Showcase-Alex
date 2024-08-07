using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Support_Attack
    {
        public static string? name { get; set; }
        public static int mana_cost { get; set; }
        public static int duration { get; set; }
        public static double damage { get; set; }
        public static double healing { get; set; }
        public static bool aura { get; set; }
        public static bool summon { get; set; }
        public static string? other { get; set; }

        public static Dictionary<string, int> attacks_support = new Dictionary<string, int>
        {
            { "Trap", 1 }, // braucht Fallenwerkzeug (noch schreiben)
            { "Ensnare", 2 }, // braucht Netz (noch schreiben)
            { "Aid", 3 }, // helfe jemanden zu treffen
            { "Block", 4 }, // mit Schild massiv besser
        };

        public static void perform(Character attacker, Character target, string attack_name)
        {
            detect_attack(attacker, target, attack_name);
        }
        public static void detect_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker != null)
            {
                if (attacker.learned_support_attacks.ContainsKey(attack_name))
                {
                    target_validate(attacker, target, attack_name);
                }
                else
                {
                    Console.WriteLine($"{attacker.character_name} does not know {attack_name}");
                }
            }
            else
            {
                Console.WriteLine("Player does not exist!");
            }
        }
        public static void target_validate(Character attacker, Character target, string attack_name)
        {
            // unbedingt an Support Aktionen anpassen
            Program_Main.death_fainted_check(target);
            if (target.player_status.conditions["alive"])
            {
                launch_attack(attacker, target, attack_name);
            }
            else
            {
                Console.WriteLine($"{target.character_name} is already dead");
            }
            launch_attack(attacker, target, attack_name);
        }

        public static void launch_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker.mana_current < mana_cost) // umschreiben, den namen brauch ich noch
            {
                Console.WriteLine($"{attacker.character_name} does not have enough mana to perform {name}.");
                return;
            }

            attacker.mana_current -= mana_cost;
            Console.WriteLine($"{attacker.character_name} performs {name} on {target.character_name}.");
            target.health_previous = target.health_current;
            apply_attack_effect(attacker, target);
        }
        private static void apply_attack_effect(Character attacker, Character target)
        {
            if (damage > 0)
            {
                Console.WriteLine($"{target.character_name} takes {damage} damage.");
                target.health_current -= damage;
                
            }
            if (other != "None")
            {
                Console.WriteLine(other);
            }
            Program_Main.death_fainted_check(target);
        }
    }
}
