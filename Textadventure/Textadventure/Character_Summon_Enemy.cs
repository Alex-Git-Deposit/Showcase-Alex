using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Character_Summon_Enemy : Character_Enemy
    {
        public int duration_left { get; set; }
        public Character_Summon_Enemy()
        {
            this.character_class = "Fighter";
            this.character_race = "Human";
            this.character_name = "Summon_Enemy_Horst";
            initialize_defaults();
        }

        // Konstruktor mit Parametern
        public Character_Summon_Enemy(string @class, string race, string name)
        {
            this.character_class = @class;
            this.character_race = race;
            this.character_name = name;
            initialize_defaults();
        }
        private void initialize_defaults()
        {
            equipment = new Equipment_System(this);
            inventory = new Inventory_System(this);

            this.ai_level = 0;
            this.duration_left = 1;

            this.character_level = 1;
            this.hit_dice = 1;

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
            this.is_a_summon = true;
            this.initiative = 0;

            player_add_item("Cloth", 1, 1);
            player_add_item("Map", 1, 1);
            player_add_item("Coin", 1, 10);
            player_add_item("Water", 2, 2);
            player_add_item("Beef", 16, 2);
            player_add_item("Potion of Healing", 50, 1);

            player_equip_item("Cloth", 1);
            Console.WriteLine($"{character_name} equipped Cloth 1");
        }
    }
}
