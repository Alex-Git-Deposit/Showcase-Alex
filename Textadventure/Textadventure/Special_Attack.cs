using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public static class Special_Attack
    {
        static Random rnd = new Random();

        public static string? name { get; set; }
        public static int mana_cost { get; set; }
        public static int duration { get; set; }
        public static double damage { get; set; }
        public static double healing { get; set; }
        public static bool aura { get; set; }
        public static bool summon { get; set; }
        public static string? other { get; set; }


        public static Dictionary<string, int> attacks_special = new Dictionary<string, int>
        {
            { "Mortal Strike", 20 },
            { "Aimed Shot", 15 },
            { "Backstab", 25 },
            { "Smite", 10 },
            { "Stealth", 5 }
        };

        static public void perform(Character attacker, Character target, string attack_name)
        {
            detect_attack(attacker, target, attack_name);
        }
        static public void detect_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker != null)
            {
                if (attacker.learned_special_attacks.ContainsKey(attack_name))
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
        static public void target_validate(Character attacker, Character target, string attack_name)
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
        static public void calculate_attack_power(Character attacker, Character target, string attack_name)
        {
            double change_power_percentage = 1;
            double change_power_value = 0;
            double weapon_damage = 0;

            if (change_power_percentage <= 0)
            {
                change_power_percentage = 0.01;
            }
            if (attacker.equipment.equipment_get().Any(item => item.item_type == "Weapon"))
            {
                weapon_damage = attacker.equipment.equipment_get().First(item => item.item_type == "Weapon").item_value;
            }
            if (weapon_damage == 0)
            {
                attack_name = "Punch";
            }
            double power_damaging = 0;
            int d6 = rnd.Next(1, 7);
            if (attacks_special.ContainsKey(attack_name))
            {
                power_damaging = ((attacks_special[attack_name] * 0.5) + (attacks_special[attack_name] 
                    * 0.5 * attacker.learned_special_attacks[attack_name]) + (attacks_special[attack_name] 
                    * 0.2 * attacker.character_level) + d6 + (attacker.strength / 2) + weapon_damage + change_power_value) * change_power_percentage;
            }
            switch (attack_name)
            {
                case "Mortal Strike":
                    name = "Mortal Strike";
                    mana_cost = 1;
                    damage = power_damaging + weapon_damage;
                    other = "None";
                    break;
                case "Aimed Shot":
                    name = "Aimed Shot";
                    mana_cost = 1;
                    damage = power_damaging + weapon_damage;
                    other = "None";
                    break;
                case "Backstab":
                    name = "Backstab";
                    mana_cost = 1;
                    damage = power_damaging + weapon_damage;
                    other = "None";
                    break;
                case "Smite":
                    name = "Smite";
                    mana_cost = 1;
                    damage = power_damaging + weapon_damage;
                    other = "None";
                    break;
                case "Stealth":
                    name = "Stealth";
                    mana_cost = 1;
                    damage = 0;
                    other = "The player becomes invisible.";
                    break;
                default:
                    Regular_Attack.calculate_attack_power(attacker, target, attack_name);
                    Console.WriteLine("You do not have a weapon, so you just use Punch");
                    break;
            }
            launch_attack(attacker, target, attack_name);
        }
        static public void launch_attack(Character attacker, Character target, string attack_name)
        {
            if (attacker.mana_current < mana_cost)
            {
                Console.WriteLine($"{attacker.character_name} does not have enough mana to perform {attack_name}.");
                return;
            }

            attacker.mana_current -= mana_cost;
            Console.WriteLine($"{attacker.character_name} performs {attack_name} on {target.character_name}.");
            target.health_previous = target.health_current;
            apply_attack_effect(attacker, target);
        }
        static private void apply_attack_effect(Character attacker, Character target)
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
