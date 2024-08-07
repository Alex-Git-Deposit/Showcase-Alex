using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Textadventure
{
    public static class Attack_System
    {
        public static void check_attack(Character attacker, Character target, string action)
        {
            if(Regular_Attack.attacks_regular.ContainsKey(action))
            {
                Regular_Attack.perform(attacker, target, action);
            }
            else if (Special_Attack.attacks_special.ContainsKey(action))
            {
                Special_Attack.perform(attacker, target, action);
            }
            else if (Support_Attack.attacks_support.ContainsKey(action))
            {
                Support_Attack.perform(attacker, target, action);
            }
            else if (Spell.spells_damaging.ContainsKey(action) || Spell.spells_healing.ContainsKey(action)
                         || Spell.spells_supporting.ContainsKey(action) || Spell.spells_summoning.ContainsKey(action))
            {
                Spell.cast(attacker, target, action);
            }
            else
            {
                Console.WriteLine("Attack does not exist! Or is an use item, which is not completed");
            }
        }
    }
}

