using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Character_Enemy : Character
    {
        public int ai_level { get; set; }
        public List<string> collected_actions_damaging { get; set; }
        public List<string> collected_actions_healing { get; set; }
        public Character_Enemy()
        {
            this.character_class = "Fighter";
            this.character_race = "Human";
            this.character_name = "EH";
            this.learned_regular_attacks = Regular_Attack.attacks_regular;
            this.learned_support_attacks = Support_Attack.attacks_support;
            this.collected_actions_damaging = new List<string>();
            this.collected_actions_healing = new List<string>();
            initialize_defaults();
        }

        // Konstruktor mit Parametern
        public Character_Enemy(string @class, string race, string name)
        {
            this.character_class = @class;
            this.character_race = race;
            this.character_name = name;
            this.learned_regular_attacks = Regular_Attack.attacks_regular;
            this.learned_support_attacks = Support_Attack.attacks_support;
            this.collected_actions_damaging = new List<string>();
            this.collected_actions_healing = new List<string>();
            initialize_defaults();
        }
        private void initialize_defaults()
        {
            equipment = new Equipment_System(this);
            inventory = new Inventory_System(this);

            this.ai_level = 0;

            this.character_level = 1;
            this.hit_dice = 6;

            this.strength = 10;
            this.dexterity = 10;
            this.constitution = 10;
            this.intelligence = 10;
            this.wisdom = 10;

            this.health_max = (int)Math.Round(hit_dice * (constitution / 2.0) * (1.0 + (character_level / 5.0)));
            this.health_current = health_max;
            this.mana_max = 10 + character_level;
            this.mana_current = mana_max;

            this.alive = true;
            this.fainted = false;
            this.is_a_summon = false;
            this.initiative = 0;

            learn_special_attack(this, "Mortal Strike");
            learn_spell(this, "Fireball");
            learn_spell(this, "Healing Touch");

            player_add_item("Cloth", 1, 1);
            player_add_item("Potion of Healing", 50, 1);

            player_equip_item("Cloth", 1);

        }
       

    }
}
