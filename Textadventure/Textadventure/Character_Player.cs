using Textadventure;

public class Character_Player : Character
{
    public int character_exp { get; set; }

    // Konstruktor ohne Parameter
    public Character_Player()
    {
        this.character_class = "Fighter";
        this.character_race = "Human";
        this.character_name = "Player_Horst";
        this.learned_regular_attacks = new Dictionary<string, int>(attacks_regular);
        this.learned_support_attacks = new Dictionary<string, int>(attacks_support);
        initialize_defaults();
    }

    // Konstruktor mit Parametern
    public Character_Player(string @class, string race, string name)
    {
        this.character_class = @class;
        this.character_race = race;
        this.character_name = name;
        this.learned_regular_attacks = new Dictionary<string, int>(attacks_regular);
        this.learned_support_attacks = new Dictionary<string, int>(attacks_support);
        initialize_defaults();
    }

    private void initialize_defaults()
    {
        equipment = new Equipment_System(this);
        inventory = new Inventory_System(this);

        this.character_level = 1;
        this.character_exp = 0;
        this.hit_dice = 1;

        this.strength = 10;
        this.dexterity = 10;
        this.constitution = 10;
        this.intelligence = 10;
        this.wisdom = 10;

        this.health_max = (int)Math.Round(hit_dice * (constitution / 2.0) * (1.0 + (character_level / 5.0)));
        this.health_current = health_max;
        this.mana_max = 100 + character_level;
        this.mana_current = mana_max;

        this.alive = true;
        this.fainted = false;
        this.is_a_summon = false;
        this.initiative = 0;


        this.learnable_spells.Add("Fireball");
        this.learnable_spells.Add("Healing Touch");
        this.learnable_special_attacks.Add("Mortal Strike");

        learn_special_attack(this, "Mortal Strike");
        learn_spell(this, "Fireball");
        learn_spell(this, "Healing Touch");

        player_add_item("Cloth", 1, 1);
        player_add_item("Map", 1, 1);
        player_add_item("Coin", 1, 10);
        player_add_item("Water", 2, 2);
        player_add_item("Beef", 16, 2);
        player_add_item("Potion of Healing", 50, 1);

        player_equip_item("Cloth", 1);
    }
}