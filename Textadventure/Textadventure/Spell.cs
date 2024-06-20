using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Spell
    {
        public string name { get; set; }
        public int mana_cost { get; set; }
        public int duration { get; set; }
        public double damage { get; set; }
        public double healing { get; set; }
        public bool aura { get; set; }
        public bool summon { get; set; }
        public string other { get; set; }

        private Character? player;

        static Dictionary<string, int> spells_healing = new Dictionary<string, int>
            {
                { "Cure Wounds", 5 },
                { "Healing Touch", 15 },
                { "Rejuvenate", 5 },
                { "Holy Light", 50 },
                { "Resurrection", 1 }
            };
        static Dictionary<string, int> spells_damaging = new Dictionary<string, int>
            {
                { "Fireball", 15 },
                { "Thunder", 15 },
                { "Frostbolt", 15 },
                { "Acid", 5 },
                { "Shadowbolt", 15 }
            };
        static Dictionary<string, int> spells_supporting = new Dictionary<string, int>
            {
                { "Aura of Devotion", 2 },
                { "Aura of Retribution", 5 },
                { "Banish", 10 },
                { "Haste", 2 },
                { "Curse", 10 }
            };
        static Dictionary<string, int> spells_summoning = new Dictionary<string, int>
            {
                { "Familiar", 1 },
                { "Elemental Servant", 2 },
                { "Celestial Guardian", 3 },
                { "Demon", 4 },
                { "Nature's Ally", 5 }
            };

        public Spell()
        {
            name = "Spell";
            mana_cost = 1;
            duration = 0;
            damage = 0;
            healing = 0;
            aura = false;
            summon = false;
            other = "None";
        }
        public Spell(Character caster, Character target, string spell_name)
        {
            name = "Spell";
            other = "None";

            this.player = caster;
        }
        public void cast(Character caster, Character target, string spell_name)
        {
            detect_spell(caster, target, spell_name);
        }
        public void detect_spell(Character caster, Character target, string spell_name)
        {
            if (caster != null)
            {
                if (caster.learned_spells.ContainsKey(spell_name))
                {
                    target_validate(caster, target, spell_name);

                }
                else
                {
                    Console.WriteLine($"{player?.character_name} does not know {spell_name}");
                }
            }
            else
            {
                Console.WriteLine("Player does not exist!");
            }

        }
        public void target_validate(Character caster, Character target, string spell_name)
        {
            calculate_spell_power(caster, target, spell_name);
        }
        public void calculate_spell_power(Character caster, Character target, string spell_name)
        {
            double power_healing = 0;
            double power_damaging = 0;
            if (spells_healing.ContainsKey(spell_name))
            {
                power_healing = (spells_healing[spell_name] * 0.5) + (spells_healing[spell_name] 
                    * 0.5 * caster.learned_spells[spell_name]) + (spells_healing[spell_name] 
                    * 0.2 * caster.character_level);
            }
            if (spells_damaging.ContainsKey(spell_name))
            {
                power_damaging = (spells_damaging[spell_name] * 0.5) + (spells_damaging[spell_name] 
                    * 0.5 * caster.learned_spells[spell_name]) + (spells_damaging[spell_name] 
                    * 0.2 * caster.character_level);
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
        public void launch_cast(Character caster, Character target, string spell_name)
        {
            if (caster.mana_current < mana_cost)
            {
                Console.WriteLine($"{caster.character_name} does not have enough mana to cast {name}.");
                return;
            }

            caster.mana_current -= mana_cost;
            Console.WriteLine($"{caster.character_name} casts {name} on {target.character_name}.");

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
        private void apply_spell_effect(Character caster, Character target)
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
        }

    }
}
