﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Character_Neutral_NPC : Character
    {
        public int ai_level { get; set; }
        public List<string> collected_actions { get; set; }
        public Character_Neutral_NPC()
        {
            this.character_class = "Fighter";
            this.character_race = "Human";
            this.character_name = "Friendly_Horst";
            this.collected_actions = new List<string>();
            this.learned_regular_attacks = new Dictionary<string, int>(attacks_regular);
            this.learned_support_attacks = new Dictionary<string, int>(attacks_support);
            this.character_status_duration = new Dictionary<string, int>();
            this.character_status_value = new Dictionary<string, int>();
            initialize_defaults();
        }

        // Konstruktor mit Parametern
        public Character_Neutral_NPC(string @class, string race, string name)
        {
            this.character_class = @class;
            this.character_race = race;
            this.character_name = name;
            this.collected_actions = new List<string>();
            this.learned_regular_attacks = new Dictionary<string, int>(attacks_regular);
            this.learned_support_attacks = new Dictionary<string, int>(attacks_support);
            this.character_status_duration = new Dictionary<string, int>();
            this.character_status_value = new Dictionary<string, int>();
            initialize_defaults();
        }
        private void initialize_defaults()
        {
            equipment = new Equipment_System(this);
            inventory = new Inventory_System(this);

            this.ai_level = 0;

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

            this.initiative = 0;
            this.deathcount = 0;

            this.change_physical_power_percentage = 1;
            this.change_physical_power_value = 0;
            this.change_spell_power_percentage = 1;
            this.change_spell_power_value = 0;

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
