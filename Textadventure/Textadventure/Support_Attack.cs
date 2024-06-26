using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Support_Attack
    {
        public string name { get; set; }
        public int mana_cost { get; set; }
        public double damage { get; set; }
        public string other { get; set; }

        private Character? player;

        public static Dictionary<string, int> attacks_support = new Dictionary<string, int>
        {
            { "Trap", 1 }, // braucht Fallenwerkzeug (noch schreiben)
            { "Ensnare", 2 }, // braucht Netz (noch schreiben)
            { "Aid", 3 }, // helfe jemanden zu treffen
            { "Block", 4 }, // mit Schild massiv besser
        };

        public Support_Attack()
        {
            name = "Support Attack";
            mana_cost = 0;
            damage = 0;
            other = "None";
        }
        public Support_Attack(Character attacker, Character target, string attack_name)
        {
            name = attack_name;
            other = "None";

            this.player = attacker;
        }
        public void perform(Character attacker, Character target, string attack_name)
        {
            detect_attack(attacker, target, attack_name);
        }
        public void detect_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker != null)
            {
                if (attacker.learned_support_attacks.ContainsKey(attack_name))
                {
                    target_validate(attacker, target, attack_name);
                }
                else
                {
                    Console.WriteLine($"{player?.character_name} does not know {attack_name}");
                }
            }
            else
            {
                Console.WriteLine("Player does not exist!");
            }
        }
        public void target_validate(Character attacker, Character target, string attack_name)
        {
            // unbedingt an Support Aktionen anpassen
            Program_Main.death_fainted_check(target);
            if (target.alive)
            {
                launch_attack(attacker, target, attack_name);
            }
            else
            {
                Console.WriteLine($"{target.character_name} is already dead");
            }
            launch_attack(attacker, target, attack_name);
        }

        public void launch_attack(Character attacker, Character target, string attack_name)
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
        private void apply_attack_effect(Character attacker, Character target)
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
