﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Special_Attack
    {
        public string name { get; set; }
        public int mana_cost { get; set; }
        public double damage { get; set; }
        public string other { get; set; }

        private Character? player;

        static Dictionary<string, int> attacks_special = new Dictionary<string, int>
            {
                { "Mortal Strike", 20 },
                { "Aimed Shot", 15 },
                { "Backstab", 25 },
                { "Smite", 10 },
                { "Stealth", 5 }
            };

        public Special_Attack()
        {
            name = "Special Attack";
            mana_cost = 1;
            damage = 0;
            other = "None";
        }
        public Special_Attack(Character attacker, Character target, string attack_name)
        {
            name = "Special Attack";
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
                if (attacker.learned_special_attacks.ContainsKey(attack_name))
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
            double power_damaging = 0;
            if (attacks_special.ContainsKey(attack_name))
            {
                power_damaging = (attacks_special[attack_name] * 0.5) + (attacks_special[attack_name] 
                    * 0.5 * attacker.learned_special_attacks[attack_name]) + (attacks_special[attack_name] 
                    * 0.2 * attacker.character_level);
            }
            switch (attack_name)
            {
                case "Mortal Strike":
                    name = "Mortal Strike";
                    mana_cost = 1;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Aimed Shot":
                    name = "Aimed Shot";
                    mana_cost = 1;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Backstab":
                    name = "Backstab";
                    mana_cost = 1;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Smite":
                    name = "Smite";
                    mana_cost = 1;
                    damage = power_damaging;
                    other = "None";
                    break;
                case "Stealth":
                    name = "Stealth";
                    mana_cost = 1;
                    damage = 0;
                    other = "The player becomes invisible.";
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