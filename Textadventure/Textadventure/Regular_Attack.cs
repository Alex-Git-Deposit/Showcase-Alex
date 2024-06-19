using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Regular_Attack
    {
        public string name { get; set; }
        public int mana_cost { get; set; }
        public double damage { get; set; }
        public string other { get; set; }

        private Character? player;

        static Dictionary<string, int> attacks_regular = new Dictionary<string, int>
            {
                { "Punch", 1 },
                { "Strike", 1 }, // braucht waffe
                { "Shoot", 1 }, // braucht distanzwaffe
                { "Throw", 1 }, // als spaß, wirf die waffe weg / what was that made of? it must have been something weak like papermachee or radditz
                { "Run", 1 } // versuch dem Kmapf zu entkommen
            };

        public Regular_Attack()
        {
            name = "Regular Attack";
            mana_cost = 0;
            damage = 0;
            other = "None";
        }
        public Regular_Attack(Character attacker, Character target, string attack_name)
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
                if (attacker.learned_regular_attacks.ContainsKey(attack_name))
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
            calculate_attack_power(attacker, target, attack_name);
        }
        public void calculate_attack_power(Character attacker, Character target, string attack_name)
        {
            Random rnd = new Random();
            int d4 = rnd.Next(1,5);
            double power_damaging = 0;
            if (attacks_regular.ContainsKey(attack_name))
            {
                power_damaging = (attacks_regular[attack_name]) + d4 + (attacks_regular[attack_name] * 0.2 * attacker.character_level);
            }
            switch (attack_name)
            {               
                case "Punch":
                    name = "Punch";
                    mana_cost = 0;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Strike":
                    name = "Strike";
                    mana_cost = 0;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Shoot":
                    name = "Shoot";
                    mana_cost = 0;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Throw":
                        name = "Throw";
                        mana_cost = 0;
                        damage = 0;
                        other = "None"; // wirf das Item weg
                        break;
                case "Run":
                    name = "Run";
                    mana_cost = 0;
                    damage = 0;
                    other = "None"; // versuche zu fliehen
                    break;

            }
            launch_attack(attacker, target, attack_name);
        }
        public void launch_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker.mana_current < mana_cost)
            {
                Console.WriteLine($"{attacker.character_name} does not have enough stamina to perform {name}.");
                return;
            }

            attacker.mana_current -= mana_cost;
            Console.WriteLine($"{attacker.character_name} performs {name} on {target.character_name}.");

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
        }
    }
}
