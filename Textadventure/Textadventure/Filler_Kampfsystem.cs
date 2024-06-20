using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    internal class Filler_Kampfsystem
    { 
        public static Random rnd = new Random();
        static List<Player_Character> party = new List<Player_Character>();
        static List<Player_Character> enemies = new List<Player_Character>();

        public class Item
        {
            public string item_name;
            public string item_tooltip;
            public int item_value;
            public int item_amount;
            public string item_type;

            public Item()
            {
                item_name = "None";
                item_tooltip = "None";
                item_value = 0;
                item_amount = 1;
                item_type = "None";
            }
            public Item(string name, int value, int amount)
            {
                item_name = name;
                item_tooltip = "None"; // funktion schreiben die den richtigen tooltip abholt
                item_value = value;
                item_amount = amount;
                item_type = "None";
            }

            public virtual void use(Player_Character player, string item_name, int item_value)
            {
                Console.WriteLine($"{player}: Using {item_name} {item_value}");
            }
        }

        public class Weapon_Item : Item
        {
            public Weapon_Item()
            {
                item_name = "Sword";
                item_tooltip = "Weapon";
                item_value = 1;
                item_amount = 1;
                item_type = "Weapon";
            }
            public Weapon_Item(string name, int value, int amount) : base(name, value, amount)
            {
                item_name = name;
                item_tooltip = "Weapon";
                item_value = generate_weapon_value(name);
                item_type = "Weapon";
            }

            private int generate_weapon_value(string name) // werte anpassen
            {
                switch (name)
                {
                    case "Sword":
                        return 5 + rnd.Next(0, 6);
                    case "Dagger":
                        return 5 + rnd.Next(0, 6);
                    case "Bow":
                        return 5 + rnd.Next(0, 6);
                    case "Mace":
                        return 5 + rnd.Next(0, 6);
                    case "Staff":
                        return 5 + rnd.Next(0, 6);
                    default:
                        return 5;
                }
            }
        }

        public class Armor_Item : Item
        {
            public Armor_Item()
            {
                item_name = "Cloth";
                item_tooltip = "Armor";
                item_value = 1;
                item_amount = 1;
                item_type = "Armor";
            }
            public Armor_Item(string name, int value, int amount) : base(name, value, amount)
            {
                item_name = name;
                item_value = generate_armor_value(name);
                item_tooltip = "Armor"; // Sobald tooltips existieren funktion schreiben
                item_type = "Armor";
            }
            private int generate_armor_value(string name)
            {
                switch (name)
                {
                    case "Cloth":
                        return 1 + rnd.Next(0, 3);
                    case "Leather":
                        return 3 + rnd.Next(0, 4);
                    case "Studded Leather":
                        return 5 + rnd.Next(0, 4);
                    case "Chainmail":
                        return 7 + rnd.Next(0, 6);
                    case "Plate":
                        return 10 + rnd.Next(0, 7);
                    case "Cloak":
                        return 1 + rnd.Next(0, 2);
                    case "Necklace":
                        return 1 + rnd.Next(0, 3);
                    case "Ring":
                        return 1 + rnd.Next(0, 4);
                    case "Shield":
                        return 2 + rnd.Next(0, 4);
                    default:
                        return 1;
                }
            }
        }

        public class SimpleRandom
        {
            private Random rnd;

            public SimpleRandom()
            {
                rnd = new Random();
            }

            // Generiere eine Zufallszahl zwischen min (einschließlich) und max (ausschließlich).
            public int Next(int min, int max)
            {
                return rnd.Next(min, max);
            }

            // Überprüfe, ob ein Ereignis basierend auf der Wahrscheinlichkeit ausgelöst wird.
            public bool check_probability(int threshold_percentage)
            {
                int rnd_number = rnd.Next(1, 101); // Zufällige Zahl von 1 bis 100 generieren
                return rnd_number <= threshold_percentage; // Ereignis wird ausgelöst, wenn die zufällige Zahl kleiner oder gleich dem Schwellenwert ist
            }
        }

        public static class Item_Factory
        {
            public static Item create_item(string name, int value, int amount)
            {
                switch (name)
                {
                    default:
                        return new Item(name, value, amount); // Fallback für allgemeine Items
                }
            }
        }

        public class Inventory_System
        {
            private Player_Character? player;
            public List<Item> inventory;

            public Inventory_System()
            {
                inventory = new List<Item>();
            }
            public Inventory_System(Player_Character player)
            {
                inventory = new List<Item>();
                this.player = player;
            }
            public List<Item> inventory_get()
            {
                return inventory;
            }

            // Methode zum Ausdrucken des Inventars eines Spielers
            public static void inventory_print(Player_Character player)
            {
                Console.WriteLine($"Inventar von {player.character_name}:");
                List<Item> character_inventory = player.inventory.inventory_get();
                foreach (var item in character_inventory)
                {
                    Console.WriteLine($"{item.item_name}: Wert: {item.item_value}, Anzahl: {item.item_amount}");
                }
            }
        }

        public class Equipment_System
        {
            private Player_Character? player;
            public List<Item> equipment;

            // Konstruktor, der das Equipment initialisiert
            public Equipment_System()
            {
                equipment = new List<Item>();
            }
            public Equipment_System(Player_Character player)
            {
                equipment = new List<Item>();
                this.player = player;
            }

            public List<Item> equipment_get()
            {
                return equipment;
            }

            // Methode zum Ausdrucken des Equipments eines Spielers
            public static void equipment_print(Player_Character player)
            {
                Console.WriteLine($"Equipment von {player.character_name}:");
                List<Item> character_equipment = player.equipment.equipment_get();
                foreach (var item in character_equipment)
                {
                    Console.WriteLine($"{item.item_name}: Wert: {item.item_value}");
                }
            }
        }

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

            private Player_Character? target;
            private Player_Character? player;

            //private Spell? spell;

            static Dictionary<string, int> spells_healing = new Dictionary<string, int>
                {
                    { "Healing_Touch", 15 }
                };
            static Dictionary<string, int> spells_damaging = new Dictionary<string, int>
                {
                    { "Fireball", 15 }
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
            public Spell(Player_Character caster, Player_Character target, string spell_name)
            {
                name = spell_name;
                other = "None";
                this.player = caster;
                this.target = target;
            }
            public void cast(Player_Character caster, Player_Character target, string spell_name)
            {
                detect_spell(caster, target, spell_name);
            }
            public void detect_spell(Player_Character caster, Player_Character target, string spell_name)
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
            public void target_validate(Player_Character caster, Player_Character target, string spell_name)
            {
                calculate_spell_power(caster, target, spell_name);
            }
            public void calculate_spell_power(Player_Character caster, Player_Character target, string spell_name)
            {
                double power_healing = 0;
                double power_damaging = 0;
                if (spells_healing.ContainsKey(spell_name))
                {
                    power_healing = (spells_healing[spell_name] * 0.5) + (spells_healing[spell_name] * 0.5 * caster.learned_spells[spell_name]) + (spells_healing[spell_name] * 0.2 * caster.character_level);
                }
                if (spells_damaging.ContainsKey(spell_name))
                {
                    power_damaging = (spells_damaging[spell_name] * 0.5) + (spells_damaging[spell_name] * 0.5 * caster.learned_spells[spell_name]) + (spells_damaging[spell_name] * 0.2 * caster.character_level);
                }
                spell_resolve(caster, target, spell_name, power_healing, power_damaging);
            }
            public void spell_resolve(Player_Character caster, Player_Character target, string spell_name, double power_healing, double power_damaging)
            {
                if (spells_healing.ContainsKey(spell_name))
                {
                    target.health_current += power_healing;
                    Console.WriteLine($"{caster.character_name} healed {target.character_name} for {power_healing}");
                }
                if (spells_damaging.ContainsKey(spell_name))
                {
                    target.health_current -= power_damaging;
                    Console.WriteLine($"{caster.character_name} dealt {power_damaging} damage to {target.character_name}");
                }
            }
        }

        public class Player_Character
        {
            public string character_class { get; set; }
            public string character_race { get; set; }
            public string character_name { get; set; }

            public int character_level { get; set; }
            public double health_max { get; set; }
            public double health_current { get; set; }
            public int hit_dice { get; set; }
            public int mana_max { get; set; }
            public int mana_current { get; set; }
            public int death_counter { get; set; }

            public int strength { get; set; }
            public int dexterity { get; set; }
            public int constitution { get; set; }
            public int intelligence { get; set; }
            public int wisdom { get; set; }
            public bool alive { get; set; }
            public bool fainted { get; set; }
            public int initiative { get; set; }

            public Item? current_item { get; set; }

            public Dictionary<string, int> learned_spells = new Dictionary<string, int>();
            public List<string> learnable_spells = new List<string>();

            public Inventory_System inventory = new Inventory_System();
            public Equipment_System equipment = new Equipment_System();
            public Spell spells = new Spell();

            public Player_Character()
            {
                initialize_defaults();
                this.character_class = "Fighter";
                this.character_race = "Human";
                this.character_name = "Horst";
            }

            public Player_Character(string @class, string race, string name)
            {
                initialize_defaults();
                this.character_class = @class;
                this.character_race = race;
                this.character_name = name;
            }

            private void initialize_defaults()
            {
                equipment = new Equipment_System(this);
                inventory = new Inventory_System(this);
                spells = new Spell();

                this.character_level = 1;
                this.hit_dice = 1;

                this.strength = 10;
                this.dexterity = 10;
                this.constitution = 10;
                this.intelligence = 10;
                this.wisdom = 10;

                this.health_max = (int)Math.Round((double)hit_dice * ((double)constitution / 2) * (1 + ((double)character_level / 5)));
                this.health_current = health_max;
                this.mana_max = 1000 + character_level;
                this.mana_current = mana_max;
                this.death_counter = 0;

                this.alive = true;
                this.fainted = false;
            }

            public void learn_spell(string spell_name)
            {
                if (learned_spells.ContainsKey(spell_name))
                {
                    Console.WriteLine($"{character_name} already knows the spell: {spell_name}");
                }
                else
                {
                    if (learnable_spells.Contains(spell_name))
                    {
                        learned_spells[spell_name] = 1;
                        Console.WriteLine($"Learned the spell: {spell_name}");
                    }
                    else
                    {
                        Console.WriteLine($"Cannot learn the spell: {spell_name}");
                    }
                }
            }

            public void increase_spell_level(string spell_name)
            {
                if (learned_spells.ContainsKey(spell_name) && learned_spells[spell_name] < 5)
                {
                    learned_spells[spell_name] += 1;
                    Console.WriteLine($"Increased the level of spell: {spell_name}");
                }
                else
                {
                    Console.WriteLine($"Cannot increase the level of spell: {spell_name}");
                }
            }

            public void player_cast_spell(Player_Character player, Player_Character target, string spell_name)
            {
                spells.cast(player, target, spell_name);
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

                        this.learnable_spells.Add("Fireball");
                        this.learn_spell("Fireball");
                        this.learnable_spells.Add("Healing_Touch");
                        this.learn_spell("Healing_Touch");
                        break;
                    default:
                        break;
                }
                apply_new_health();
            }

            public void apply_race_bonus()
            {
                switch (character_race)
                {
                    case "Human":
                        this.wisdom += 2;
                        break;
                    default:
                        break;
                }
            }

            public void apply_new_health()
            {
                this.health_max = (int)Math.Round((double)hit_dice * ((double)constitution / 2) * (1 + ((double)character_level / 5)));
                this.health_current = health_max;
                this.mana_max = 10 + character_level;
                this.mana_current = mana_max;
            }

            public virtual void fight_start(Player_Character opponent)
            {
                Console.WriteLine("Fight!");
            }

            public virtual void perform_attack(Player_Character opponent)
            {
                double damage = 5 + rnd.Next(1, 6);
                opponent.health_current -= damage;
                Console.WriteLine($"{character_name} hits {opponent.character_name} for {damage} damage.");

                if (opponent.health_current <= 0)
                {
                    Console.WriteLine($"{opponent.character_name} has been slain!");
                }
            }
        }

        public class Hero : Player_Character
        {
            public Hero()
            {
                this.character_name = "Horst_The_Ultimate_Hero";
                this.health_max = 100;
                this.health_current = 50;
                this.learned_spells.Add("Fireball", 1);
                this.learned_spells.Add("Healing_Touch", 1);
            }

            public override void fight_start(Player_Character opponent)
            {
                if (opponent is Enemy enemy)
                {
                    Console.WriteLine($"Hero {character_name} starts fight with Enemy {enemy.character_name}");
                    while (this.health_current > 0 && opponent.health_current > 0)
                    {
                        Console.Write("Enter attack (Fireball/Healing_Touch or any other key for normal attack): ");
                        string attack = input_reader();

                        if (attack == "Fireball" || attack == "Healing_Touch")
                        {
                            this.player_cast_spell(this, opponent, attack);
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Eingabe, verwende normalen Schlag.");
                            this.perform_attack(opponent);
                        }

                        if (opponent.health_current <= 0)
                        {
                            Console.WriteLine($"{opponent.character_name} already is defeated!");
                            break;
                        }

                        enemy.perform_attack(this);

                        if (this.health_current <= 0)
                        {
                            Console.WriteLine($"{this.character_name} down!");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Hero can only fight an Enemy.");
                }
            }
        }
        public static string input_reader()
        {
            string? input = Console.ReadLine();
            if (input == null || string.IsNullOrWhiteSpace(input))
            {
                return input = "Invalid input";
            }
            return input;
        }
        public class Enemy : Player_Character
        {
            public Enemy()
            {
                this.character_name = "Erik_The_Enemy";
                this.health_max = 100;
                this.health_current = 50;
            }

            public override void fight_start(Player_Character opponent)
            {
                if (opponent is Enemy enemy)
                {
                    Console.WriteLine($"Hero {character_name} starts fight with Enemy {enemy.character_name}");
                    while (this.health_current > 0 && opponent.health_current > 0)
                    {
                        Console.Write("Enter attack (Fireball/Healing_Touch or any other key for normal attack): ");
                        string attack = input_reader();

                        if (attack == "Fireball" || attack == "Healing_Touch")
                        {
                            if (this.learned_spells.ContainsKey(attack) && this.learned_spells[attack] > 0) // Überprüfen, ob der Spieler den Zauber beherrscht
                            {
                                this.player_cast_spell(this, opponent, attack);
                            }
                            else
                            {
                                Console.WriteLine($"You do not know {attack}. Using normal attack instead.");
                                this.perform_attack(opponent);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Using normal attack.");
                            this.perform_attack(opponent);
                        }

                        if (opponent.health_current <= 0)
                        {
                            Console.WriteLine($"{opponent.character_name} is defeated already!");
                            break;
                        }

                        enemy.perform_attack(this);

                        if (this.health_current <= 0)
                        {
                            Console.WriteLine($"{this.character_name} down and done!");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Hero can only fight an Enemy.");
                }
            }
            public static class Game_Controller
            {
                private static List<Player_Character> characters = new List<Player_Character>();

                public static void Start()
                {
                    bool running = true;
                    while (running)
                    {
                        Console.WriteLine("Options:");
                        Console.WriteLine("1. Start a fight");
                        Console.WriteLine("2. Create a new character");
                        Console.WriteLine("3. List all characters");
                        Console.WriteLine("4. Remove a character");
                        Console.WriteLine("0. Quit");

                        string input = input_reader();

                        switch (input)
                        {
                            case "0":
                                running = false;
                                break;
                            case "1":
                                StartFight();
                                break;
                            case "2":
                                CreateCharacter();
                                break;
                            case "3":
                                ListCharacters();
                                break;
                            case "4":
                                RemoveCharacter();
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }
                    }
                }

                public static void StartFight()
                {
                    Console.Write("Enter hero's name: ");
                    string heroName = input_reader();
                    Console.Write("Enter enemy's name: ");
                    string enemyName = input_reader();

                    Player_Character? hero = characters.Find(c => c.character_name == heroName);
                    Player_Character? enemy = characters.Find(c => c.character_name == enemyName);

                    if (hero != null && enemy != null)
                    {
                        hero.fight_start(enemy);
                    }
                    else
                    {
                        Console.WriteLine("Hero or enemy not found.");
                    }
                }

                public static void CreateCharacter()
                {
                    Console.WriteLine("Create a new character:");
                    Console.Write("Enter character name: ");
                    string name = input_reader();
                    Console.Write("Enter character class (Hero/Enemy): ");
                    string characterClass = input_reader();

                    Player_Character newCharacter;
                    if (characterClass.ToLower() == "hero")
                    {
                        newCharacter = new Hero();
                    }
                    else if (characterClass.ToLower() == "enemy")
                    {
                        newCharacter = new Enemy();
                    }
                    else
                    {
                        Console.WriteLine("Invalid character class.");
                        return;
                    }

                    newCharacter.character_name = name;
                    characters.Add(newCharacter);
                    Console.WriteLine($"Character {name} created.");
                }

                public static void ListCharacters()
                {
                    Console.WriteLine("List of characters:");
                    foreach (var character in characters)
                    {
                        Console.WriteLine($"- {character.character_name} ({character.character_class})");
                    }
                }

                public static void RemoveCharacter()
                {
                    Console.Write("Enter character name to remove: ");
                    string name = input_reader();

                    Player_Character? characterToRemove = characters.Find(c => c.character_name == name);

                    if (characterToRemove != null)
                    {
                        characters.Remove(characterToRemove);
                        Console.WriteLine($"Character {name} removed.");
                    }
                    else
                    {
                        Console.WriteLine("Character not found.");
                    }
                }
            }
            public void generate_enemies()
            {
                enemies.Add(new Enemy()); // Enemy klasse schreiben
            }
            public void enemy_combat_ai()
            {
                Console.WriteLine("Enemy Action performed");
                /* 
                Level 0 Sammel alle gültigen Aktionen // Sammel alle gültigen Ziele
                --> zufällige Auswahl // kleine Chance auf Friendly Fire // kleine Chance auf Fluchtversuch
                --> Inventar manchmal benutzen

                Level 1 Sammeln // minimale Chance auf Friendly Fire // minimale Chance auf Fluchtversuch
                --> Chance auf freunde heilen falls möglich falls unter 50%
                --> aid // block  je nach dem wie ich block schreibe
                --> Ziele unter 75% chance erhöht unter 50% chance erhöht
                --> kleine Chance gezielt Mana Fähigkeit zu benutzen
                --> Inventar manchmal benutzen

                Level 2 kein Friendly Fire // kein Fluchtversuch 
                --> höhere Chance auf freunde heilen falls möglich falls unter 50%
                --> Ziele unter 50% Leben bevorzugen unter 25% leicht höher
                --> aid // block im blick halten
                --> kleine Chance auf Ziel mit niedrigsten gesamtpunkten
                --> moderate chance auf einen Zufall
                --> erhöhte Chance gezielt Mana Fähigkeit zu benutzen
                --> Inventar öfter benutzen

                Level 3 heilt grundsätzlich falls möglich
                --> moderate Chance auf Ziel mit niedrigsten gesamtpunkten
                --> Ziele nach unter 50% sortieren höhere Chance bei unter 25%
                --> aid // block im blick halten
                --> kleine chance auf einen Zufall
                --> hohe Chance gezielt Mana Fähigkeit zu benutzen
                --> Inventar immer benutzen

                Level 4 *
                --> Fokus auf niedriges gesamtleben oder unter 25%
                --> aid // block im blick halten
                --> Fokus Fire
                --> kein Zufall
                --> nutzt Mana
                */
            }
            public void roll_initiative(Player_Character character)
            {
                Random rnd = new Random();
                int d20 = rnd.Next(1, 21); // Zufällige Zahl zwischen 1 und 20 (einschließlich)
                if (d20 == 1)
                {
                    d20 -= 10;
                }
                else if (d20 == 20)
                {
                    d20 += 10;
                }
                character.initiative = d20 + character.dexterity;
            }
            public void print_initiative(Player_Character character)
            {
                Console.WriteLine($"{character.character_name}: initiative {character.initiative}");
            }
            public bool action_detect(string input)
            {
                if (input == "test") // ausschreiben per Liste actions valid /-> ob auch ausgeführt wurde
                {                   // vielleicht auch in den input_reader packen
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public void send_next_wave()
            {
                generate_enemies();
            }
            public void create_loot()
            {
                int x = 0; // Platzhalter

                if (x == 1)
                {
                    Console.WriteLine("You loot the following: ");
                    // rnd item schreiben
                    // dropchance / party size (alive) *-> pro player_character
                }
                else
                {
                    Console.WriteLine("There is no loot this time");
                }
            }
            public void game_over()
            {
                Console.WriteLine("Game Over!");
                Console.WriteLine("Choose one option:");
                Console.WriteLine("1) Load Save File");
                Console.WriteLine("2) Start New Game");
                Console.WriteLine("3) Quit Game");

                string input = input_reader();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Loading Save File...");
                        Console.WriteLine("Or not, i don't have Saves right now.");
                        // Clean up
                        // Hier kannst du den Code zum Laden des gespeicherten Spiels einfügen
                        break;
                    case "2":
                        Console.WriteLine("Starting New Game...");
                        // Clean up
                        // Startsequenz
                        break;
                    case "3":
                        Console.WriteLine("Quitting Game...");
                        Environment.Exit(0); // Beendet die Anwendung
                        break;
                    default:
                        Console.WriteLine("Invalid pick: Quitting Game...");
                        Environment.Exit(0);
                        break;
                }
            }
            public void infight()
            {
                bool fighton = true;
                List<Player_Character> fight_round_list = new List<Player_Character>();
                Queue<Player_Character> fight_round_queue = new Queue<Player_Character>();
                int wavecount = rnd.Next(1, 3);
                int turncount = 0;

                while (fighton)
                {
                    turncount++;
                    foreach (Player_Character player in party)
                    {
                        if (player.alive == true)
                        {
                            fight_round_list.Add(player);
                        }
                    }
                    generate_enemies();
                    foreach (Player_Character enemy in enemies)
                    {
                        fight_round_list.Add(enemy);
                    }
                    foreach (Player_Character character in fight_round_list)
                    {
                        roll_initiative(character);
                    }
                    fight_round_list.OrderBy(character => character.initiative);
                    party.OrderBy(player => player.initiative);
                    enemies.OrderBy(enemy => enemy.initiative);
                    foreach (Player_Character character in fight_round_list)
                    {
                        print_initiative(character);
                    }
                    foreach (Player_Character character in fight_round_list)
                    {
                        fight_round_queue.Enqueue(character);
                    }
                    while (fight_round_queue.Count > 0)
                    {
                        Player_Character character = fight_round_queue.Peek(); // Das nächste Element in der Warteschlange anzeigen, ohne es zu entfernen
                        if (character.fainted == false)
                        {

                            if (party.Contains(character))
                            {
                                bool player_action_bool = false;
                                while (!player_action_bool)
                                {
                                    /*if action "saved" == block||aid --> add buff */
                                    Console.WriteLine($"{character.character_name}, please type your action:");
                                    // action detect schreiben
                                    if (action_detect(input_reader()))
                                    {
                                        player_action_bool = true;
                                        Console.WriteLine($"action performed, next player!");
                                    }
                                    else
                                    {
                                        player_action_bool = false;
                                        Console.WriteLine($"Invalid action: type help/ask or punch"); // ausschreiben
                                    }
                                }
                            }
                            else if (enemies.Contains(character))
                            {
                                /*if action "saved" == block||aid --> add buff */
                                enemy_combat_ai(); // ausbauen
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong! Empty Character in Queue");
                            }
                        }
                        else if (character.fainted == true)
                        {
                            Console.WriteLine($"{character.character_name} is fainted --> turn skipped");
                        }
                        else
                        {
                            Console.WriteLine("Something went horribly wrong! Empty Entry in Queue");
                        }
                        fight_round_queue.Dequeue(); // Das Element entfernen, nachdem es bearbeitet wurde
                    }
                    if (party.All(player => player.alive == false))
                    {
                        fighton = false;
                        Console.WriteLine("The entire Party is dead");
                        game_over();
                    }
                    else if ((turncount % 5 == 0 || enemies.All(enemy => enemy.alive == false)
                            || enemies.All(enemy => enemy.fainted == true)) && wavecount >= 0)
                    {
                        send_next_wave();
                        wavecount--;
                        fighton = true;
                    }
                    else if ((enemies.All(enemy => enemy.alive == false) || enemies.All(enemy => enemy.fainted == true)) && wavecount <= 0)
                    {
                        Console.WriteLine("All enemies defeated!");
                        wavecount = 0;
                        fighton = false;
                    }
                    fight_round_queue.Clear();
                    fight_round_list.Clear();

                }
                create_loot();

            }

        }
    }
}

