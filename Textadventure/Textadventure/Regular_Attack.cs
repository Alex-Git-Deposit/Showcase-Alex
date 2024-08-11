using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Regular_Attack
    {
        public static string? name { get; set; }
        public static int mana_cost { get; set; }
        public static int duration { get; set; }
        public static double damage { get; set; }
        public static double healing { get; set; }
        public static bool aura { get; set; }
        public static bool summon { get; set; }
        public static string? other { get; set; }


        public static Dictionary<string, int> attacks_regular = new Dictionary<string, int>
            {
                { "Punch", 1 },
                { "Strike", 1 }, // braucht waffe
                { "Shoot", 1 }, // braucht distanzwaffe
                { "Throw", 1 }, // als spaß, wirf die waffe weg / what was that made of? it must have been something weak like papermachee or radditz
                { "Run", 1 } // versuch dem Kmapf zu entkommen
            };

        public static void perform(Character attacker, Character target, string attack_name)
        {
            detect_attack(attacker, target, attack_name);
        }
        public static void detect_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker != null)
            {
                if (attacker.learned_regular_attacks.ContainsKey(attack_name))
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
            Program_Main.death_fainted_check(target);
            if (target.player_status.conditions["alive"])
            {
                calculate_attack_power(attacker, target, attack_name);
            }
            else
            {
                Console.WriteLine($"{target.character_name} is already dead");
            }
        }
        public static void calculate_attack_power(Character attacker, Character target, string attack_name)
        {
            double change_power_percentage = 1;
            double change_power_value = 0;
            double weapon_damage = 0;
            // get change power values from character
            // Get the weapon damage from the character's equipment
            if (change_power_percentage <= 0) 
            {
                change_power_percentage = 0.01;
            }
            if (attacker.equipment.equipment_get().Any(item => item.item_type == "Weapon"))
            {
                weapon_damage = attacker.equipment.equipment_get().First(item => item.item_type == "Weapon").item_value;
            }
            if (weapon_damage == 0 && attack_name == "Strike" || attack_name == "Shoot")
            {
                attack_name = "Punch";
                Console.WriteLine("You do not have a weapon, so you just use Punch");
            }
            Random rnd = new Random();
            int d4 = rnd.Next(1,5);
            double power_damaging = 0;
            if (attacks_regular.ContainsKey(attack_name))
            {
                power_damaging = ((attacks_regular[attack_name]) + (attacks_regular[attack_name] * 0.2 * attacker.character_level)
                                     + d4 + (attacker.strength / 2 + weapon_damage + change_power_value) * change_power_percentage);
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
                    damage = power_damaging + weapon_damage;
                    other = "None";
                    break;
                case "Shoot":
                    name = "Shoot";
                    mana_cost = 0;
                    damage = power_damaging + weapon_damage;
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
        public static void launch_attack(Character attacker, Character target, string attack_name)
        {
            if (attack_name == "Throw")
            {
                string name = "Staff"; // testwert
                attacker.equipment.unequip_item(name, 5);
                attacker.inventory.inventory_remove_item(name, 5, 1);
                Console.WriteLine($"{name} got thrown away and broke");
                Console.WriteLine($"It must have been made out of something weak, like paper mâché or radish...");
                return;
            }
            if (attacker.mana_current < mana_cost)
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
