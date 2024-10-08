﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public static class Spell
    {
        public static string? name { get; set; }
        public static int mana_cost { get; set; }
        public static int duration { get; set; }
        public static double damage { get; set; }
        public static double healing { get; set; }
        public static bool aura { get; set; }
        public static bool summon { get; set; }
        public static string? other { get; set; }

        public static Dictionary<string, int> spells_healing = new Dictionary<string, int>
        {
            { "Cure Wounds", 5 },
            { "Healing Touch", 15 },
            { "Rejuvenate", 5 },
            { "Holy Light", 50 },
            { "Resurrection", 1 }
        };
        public static Dictionary<string, int> spells_damaging = new Dictionary<string, int>
        {
            { "Fireball", 15 },
            { "Thunder", 25 },
            { "Frostbolt", 12 },
            { "Acid", 5 },
            { "Shadowbolt", 18 }
        };
        public static Dictionary<string, int> spells_supporting = new Dictionary<string, int>
        {
            { "Aura of Devotion", 2 },
            { "Aura of Retribution", 5 },
            { "Banish", 10 },
            { "Haste", 2 },
            { "Curse", 10 }
        };
        public static Dictionary<string, int> spells_summoning = new Dictionary<string, int>
        {
            { "Familiar", 1 },
            { "Elemental Servant", 2 },
            { "Celestial Guardian", 3 },
            { "Demon", 4 },
            { "Nature's Ally", 5 }
        };
        public static void cast(Character caster, Character target, string spell_name)
        {
            detect_spell(caster, target, spell_name);
        }
        public static void detect_spell(Character caster, Character target, string spell_name)
        {
            if (caster != null)
            {
                if (caster.learned_spells.ContainsKey(spell_name))
                {
                    target_validate(caster, target, spell_name);

                }
                else
                {
                    Console.WriteLine($"{caster.character_name} does not know {spell_name}");
                }
            }
            else
            {
                Console.WriteLine("Player does not exist!");
            }

        }
        public static void target_validate(Character caster, Character target, string spell_name)
        {
            Program_Main.death_fainted_check(target);
            if (target.player_status.conditions["alive"])
            {
                calculate_spell_power(caster, target, spell_name);
            }
            else if (spell_name == "Resurrection")
            {
                target.player_status.conditions["alive"] = true;
                target.health_current = target.health_max;
                target.mana_current = target.mana_max;
                Console.WriteLine($"{target.character_name} ressurected!");
            }
            else
            {
                Console.WriteLine($"{target.character_name} is already dead");
            } 
        }
        public static void calculate_spell_power(Character caster, Character target, string spell_name)
        {
            double change_spell_power_percentage = 1;
            double change_spell_power_value = 0;

            double power_healing = 0;
            double power_damaging = 0;
            int d4 = rnd.Next(1, 5);

            if (change_spell_power_percentage <= 0)
            {
                change_spell_power_percentage = 0.01;
            }
            if (spells_healing.ContainsKey(spell_name))
            {
                power_healing = (((spells_healing[spell_name] * 0.5) + (spells_healing[spell_name] 
                    * 0.5 * caster.learned_spells[spell_name]) + (spells_healing[spell_name] 
                    * 0.2 * caster.character_level) + 2 * d4 + (caster.intelligence / 2) + change_spell_power_value) * change_spell_power_percentage);
            }
            if (spells_damaging.ContainsKey(spell_name))
            {
                power_damaging = (((spells_damaging[spell_name] * 0.5) + (spells_damaging[spell_name] 
                    * 0.5 * caster.learned_spells[spell_name]) + (spells_damaging[spell_name] 
                    * 0.2 * caster.character_level) + d4 + (caster.intelligence / 2) + change_spell_power_value) * change_spell_power_percentage);
            }
            if (power_damaging <= 0)
            {
                power_damaging = 1;
            }
            if (power_healing <= 0)
            {
                power_healing = 1;
            }
            switch (spell_name)
            {
                case "Cure Wounds":
                    name = "Cure Wounds";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = power_healing;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Healing Touch":
                    name = "Healing Touch";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = power_healing;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Rejuvenate":
                    name = "Rejuvenate";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = power_healing;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Holy Light":
                    name = "Holy Light";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = power_healing;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Resurrection":
                    name = "Resurrection";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                /////////////////////////////////////////////
                case "Fireball":
                    name = "Fireball";
                    mana_cost = 1;
                    duration = 0;
                    damage = power_damaging;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Thunder":
                    name = "Thunder";
                    mana_cost = 1;
                    duration = 0;
                    damage = power_damaging;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Frostbolt":
                    name = "Frostbolt";
                    mana_cost = 1;
                    duration = 0;
                    damage = power_damaging;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Acid":
                    name = "Acid";
                    mana_cost = 1;
                    duration = 0;
                    damage = power_damaging;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Shadowbolt":
                    name = "Shadowbolt";
                    mana_cost = 1;
                    duration = 0;
                    damage = power_damaging;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                //////////////////////////
                case "Aura of Devotion":
                    name = "Aura of Devotion";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Aura of Retribution":
                    name = "Aura of Retribution";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Banish":
                    name = "Banish";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Haste":
                    name = "Haste";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Curse":
                    name = "Curse";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                ///////////////////////////////////////
                case "Familiar":
                    name = "Familiar";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Elemental Servant":
                    name = "Elemental Servant";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Celestial Guardian":
                    name = "Celestial Guardian";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Demon":
                    name = "Demon";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                case "Nature's Ally":
                    name = "Nature's Ally";
                    mana_cost = 1;
                    duration = 0;
                    damage = 0;
                    healing = 0;
                    aura = false;
                    summon = false;
                    other = "None";
                    break;
                /////////////////////////////////////
                default:
                    break;
            }
            launch_cast(caster, target, spell_name);
        }
        public static void launch_cast(Character caster, Character target, string spell_name)
        {
            if (caster.mana_current < mana_cost)
            {
                Console.WriteLine($"{caster.character_name} does not have enough mana to cast {name}.");
                return;
            }

            caster.mana_current -= mana_cost;
            Console.WriteLine($"{caster.character_name} casts {name} on {target.character_name}.");
            target.health_previous = target.health_current;
            if (duration == 0)
            {
                apply_spell_effect(caster, target);
            }
            else
            {
                for (int i = duration; i > 0; i--)
                {
                    apply_spell_effect(caster, target);
                }
            }
        }
        private static void apply_spell_effect(Character caster, Character target)
        {
            if (damage > 0)
            {
                Console.WriteLine($"{target.character_name} takes {damage} damage.");
                target.health_current -= damage;
            }
            if (healing > 0)
            {
                Console.WriteLine($"{target.character_name} heals {healing} health.");
                target.health_current += healing;
            }
            if (aura)
            {
                Console.WriteLine($"{caster.character_name} activates their aura.");
            }
            if (summon)
            {
                Console.WriteLine("A creature is summoned.");
            }
            if (other != "None")
            {
                Console.WriteLine(other);
            }
            Program_Main.death_fainted_check(target);
        }

    }
}
