using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Character
    {
        // Attribute sind Eigenschaften einer Klasse
        public string character_class { get; set; }
        public string character_race { get; set; }
        public string character_name { get; set; }

        public int character_level { get; set; }
        public double health_max { get; set; }
        public double health_current { get; set; }
        public double health_previous { get; set; }
        public int hit_dice { get; set; }
        public int mana_max { get; set; }
        public int mana_current { get; set; }

        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }

        public bool alive { get; set; }
        public bool fainted { get; set; }
        public bool is_a_summon { get; set; }
        public int initiative { get; set; }
        public int deathcount { get; set; }


        public Dictionary<string, int> learned_spells = new Dictionary<string, int>();
        public List<string> learnable_spells = new List<string>();

        public Dictionary<string, int> learned_special_attacks = new Dictionary<string, int>();
        public List<string> learnable_special_attacks = new List<string>();

        public Dictionary<string, int> learned_regular_attacks { get; set; }
        public Dictionary<string, int> learned_support_attacks { get; set; }


        public Item? current_item { get; set; }

        public Inventory_System inventory = new Inventory_System();
        public Equipment_System equipment = new Equipment_System();
        public Spell spells = new Spell();
        public Special_Attack special_attacks = new Special_Attack();
        public Regular_Attack regular_attacks = new Regular_Attack();
        public Support_Attack support_attacks = new Support_Attack();

        public Character()
        {
            this.character_class = "Fighter";
            this.character_race = "Human";
            this.character_name = "Horst";
            this.learned_regular_attacks = Regular_Attack.attacks_regular;
            this.learned_support_attacks = Support_Attack.attacks_support;
            initialize_defaults();
        }

        // Konstruktor mit Parametern
        public Character(string @class, string race, string name)
        {
            this.character_class = @class;
            this.character_race = race;
            this.character_name = name;
            this.learned_regular_attacks = Regular_Attack.attacks_regular;
            this.learned_support_attacks = Support_Attack.attacks_support;
            initialize_defaults();
        }
        private void initialize_defaults()
        {
            equipment = new Equipment_System();
            inventory = new Inventory_System();
            spells = new Spell();
            special_attacks = new Special_Attack();
            regular_attacks = new Regular_Attack();
            support_attacks = new Support_Attack();


            this.character_level = 1;
            this.hit_dice = 1;

            this.strength = 10;
            this.dexterity = 10;
            this.constitution = 10;
            this.intelligence = 10;
            this.wisdom = 10;

            this.health_max = (int)Math.Round(hit_dice * (constitution / 2.0) * (1.0 + (character_level / 5.0)));
            this.health_current = health_max;
            this.health_previous = 0;
            this.mana_max = 10 + character_level;
            this.mana_current = mana_max;

            this.alive = true;
            this.fainted = false;
            this.is_a_summon = false;
            this.initiative = 0;
            this.deathcount = 0;


            player_add_item("Cloth", 1, 1);
            player_add_item("Map", 1, 1);
            player_add_item("Coin", 1, 10);
            player_add_item("Water", 2, 2);
            player_add_item("Beef", 16, 2);
            player_add_item("Potion of Healing", 50, 1);

            player_equip_item("Cloth", 1);

        }
        public void learn_spell(Character player, string spell_name)
        {
            if (player.learned_spells.ContainsKey(spell_name))
            {
                Console.WriteLine($"{player.character_name} already knows the spell: {spell_name}");
            }
            else
            {
                if (player.learnable_spells.Contains(spell_name))
                {
                    player.learned_spells[spell_name] = 1;
                    Console.WriteLine($"Learned the spell: {spell_name}");
                }
                else
                {
                    Console.WriteLine($"Can not learn the spell: {spell_name}");
                }
            }
        }

        public void increase_spell_level(Character player, string spell_name)
        {
            if (player.learned_spells.ContainsKey(spell_name) && player.learned_spells[spell_name] < 5)
            {
                player.learned_spells[spell_name] += 1;
                Console.WriteLine($"Increased the level of spell: {spell_name}");
            }
            else
            {
                Console.WriteLine($"Can not increase the level of spell: {spell_name}");
            }
        }
        public void player_cast_spell(Character player, Character target, string spell_name)
        {
            spells.cast(player, target, spell_name);
        }
        public void learn_special_attack(Character player, string attack_name)
        {
            if (player.learned_special_attacks.ContainsKey(attack_name))
            {
                Console.WriteLine($"{player.character_name} already knows the attack: {attack_name}");
            }
            else
            {
                if (player.learnable_special_attacks.Contains(attack_name))
                {
                    player.learned_special_attacks[attack_name] = 1;
                    Console.WriteLine($"Learned the attack: {attack_name}");
                }
                else
                {
                    Console.WriteLine($"Can not learn the attack: {attack_name}");
                }
            }
        }

        public void increase_special_attack_level(Character player, string attack_name)
        {
            if (player.learned_special_attacks.ContainsKey(attack_name) && player.learned_special_attacks[attack_name] < 5)
            {
                player.learned_special_attacks[attack_name] += 1;
                Console.WriteLine($"Increased the level of attack: {attack_name}");
            }
            else
            {
                Console.WriteLine($"Can not increase the level of attack: {attack_name}");
            }
        }

        public void player_perform_special_attack(Character player, Character target, string attack_name)
        {
            special_attacks.perform(player, target, attack_name);
        }
        public void player_remove_item(string name, int value, int amount)
        {
            inventory.inventory_remove_item(name, value, amount);
        }
        public void player_add_item(string name, int value, int amount)
        {
            inventory.inventory_add_item(name, value, amount);
        }
        public void player_equip_item(string name, int value)
        {
            equipment.equip_item(name, value);
        }
        public void player_unequip_item(string name, int value)
        {
            equipment.unequip_item(name, value);
        }
        public void print_stats()
        {
            Console.WriteLine($"Character Name: {character_name}, Race: {character_race}, Class: {character_class}");
            Console.WriteLine($"Character Maximum Health: {health_max}, Character Current Health: {health_current}");
            Console.WriteLine($"Attributes - Strength: {strength}, Dexterity: {dexterity}, Constitution: {constitution}, Intelligence: {intelligence}, Wisdom: {wisdom}");
            Console.WriteLine($"Character alive: {alive}, Character fainted: {fainted}");
        }

        public void apply_class_bonus()
        {
            switch (character_class)
            {
                case "Fighter":
                    this.hit_dice = 12;

                    this.strength += 4;
                    this.dexterity += 2;
                    this.constitution += 2;
                    this.intelligence -= 2;
                    this.wisdom -= 2;

                    player_add_item("Sword", 5, 1);
                    player_equip_item("Sword", 5);


                    this.learnable_spells.Add("Fireball");
                    this.learnable_spells.Add("Healing Touch");
                    this.learnable_special_attacks.Add("Mortal Strike");

                    learn_spell(this, "Fireball");
                    learn_spell(this, "Healing Touch");
                    learn_special_attack(this, "Mortal Strike");
                    break;
                case "Ranger":
                    this.hit_dice = 8;

                    this.strength += 2;
                    this.dexterity += 4;
                    this.constitution += 2;
                    this.intelligence -= 2;
                    this.wisdom -= 2;

                    player_add_item("Bow", 5, 1);
                    player_equip_item("Bow", 5);


                    break;
                case "Rogue":
                    this.hit_dice = 6;

                    this.strength += 0;
                    this.dexterity += 4;
                    this.constitution += 0;
                    this.intelligence -= 2;
                    this.wisdom += 2;

                    player_add_item("Dagger", 5, 1);
                    player_equip_item("Dagger", 5);


                    break;
                case "Paladin":
                    this.hit_dice = 10;

                    this.strength += 2;
                    this.dexterity -= 2;
                    this.constitution += 0;
                    this.intelligence += 0;
                    this.wisdom += 4;

                    player_add_item("Mace", 5, 1);
                    player_equip_item("Mace", 5);


                    break;
                case "Priest":
                    this.hit_dice = 4;

                    this.strength -= 2;
                    this.dexterity -= 2;
                    this.constitution += 0;
                    this.intelligence += 4;
                    this.wisdom += 4;

                    player_add_item("Mace", 5, 1);
                    player_equip_item("Mace", 5);


                    break;
                case "Sorcerer":
                    this.hit_dice = 4;

                    this.strength -= 2;
                    this.dexterity -= 2;
                    this.constitution += 0;
                    this.intelligence += 2;
                    this.wisdom += 6;

                    player_add_item("Staff", 5, 1);
                    player_equip_item("Staff", 5);


                    break;
                case "Wizard":
                    this.hit_dice = 4;

                    this.strength -= 4;
                    this.dexterity -= 2;
                    this.constitution += 0;
                    this.intelligence += 8;
                    this.wisdom += 2;

                    player_add_item("Staff", 5, 1);
                    player_equip_item("Staff", 5);


                    break;
                case "Warlock":
                    this.hit_dice = 4;

                    this.strength -= 2;
                    this.dexterity -= 2;
                    this.constitution += 2;
                    this.intelligence += 4;
                    this.wisdom += 2;

                    player_add_item("Staff", 5, 1);
                    player_equip_item("Staff", 5);


                    break;
                default:
                    break;
            }
        }
        public void apply_race_bonus()
        {
            switch (character_race)
            {
                case "Human":
                    this.wisdom += 2;
                    break;
                case "Elven":
                    this.dexterity += 2;
                    break;
                case "Orc":
                    this.strength += 2;
                    break;
                case "Dwarf":
                    this.constitution += 2;
                    break;
                case "Gnome":
                    this.intelligence += 2;
                    break;
                case "Tauren":
                    this.constitution += 2;
                    break;
                case "Troll":
                    this.dexterity += 2;
                    break;
                case "Goblin":
                    this.dexterity += 2;
                    break;
                default:
                    break;
            }
        }
        public void apply_new_health()
        {
            this.health_max = (int)Math.Round((double)hit_dice * (constitution / 2) * (1 + (character_level / 5)));
            this.health_current = health_max;
            this.mana_max = 10 + character_level;
            this.mana_current = mana_max;
        }
        public void regenerate_sleep()
        {
            Console.WriteLine($"{this.character_name} regenerated back to full health and mana.");
            this.health_current = health_max;
            this.mana_current = mana_max;
        }
    }
}
