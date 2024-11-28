using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item_Potion : Item
    {
        public Item_Potion()
        {
            item_name = "Potion";
            item_tooltip = "Potion";
            item_value = 1;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Potion";
        }
        public Item_Potion(string name) : base(name)
        {
            item_name = name;
            item_tooltip = "Potion";
            item_value = generate_potion_value(name);
            item_duration = generate_potion_duration(name);
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Potion";
        }
        public Item_Potion(string name, int amount) : base(name, amount)
        {
            item_name = name;
            item_tooltip = "Potion";
            item_value = generate_potion_value(name);
            item_duration = generate_potion_duration(name);
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Potion";
        }
        public Item_Potion(string name, int value, int amount) : base(name, value, amount)
        {
            item_name = name;
            item_tooltip = "Potion";
            item_value = value;
            item_duration = generate_potion_duration(name);
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Potion";
        }
        private int generate_potion_duration(string name)
        {
            switch (name)
            {
                case "Potion of Healing":
                    return 0;
                case "Potion of Mana":
                    return 0;
                case "Potion of Poison":
                    return 5 * rnd.Next(1, 6);
                case "Potion of Strength":
                    return 5 * rnd.Next(1, 6);
                case "Potion of Weakness":
                    return 5 * rnd.Next(1, 6);
                default:
                    return 0;
            }
        }
        private int generate_potion_value(string name)
        {
            switch (name)
            {
                case "Potion of Healing":
                    return 50 * rnd.Next(1, 6);
                case "Potion of Mana":
                    return 5 * rnd.Next(1, 6);
                case "Potion of Poison":
                    return 2 + rnd.Next(0, 4);
                case "Potion of Strength":
                    return 5 * rnd.Next(1, 6); 
                case "Potion of Weakness":
                    return 5 * rnd.Next(1, 6);
                default:
                    return 1;
            }
        }
        public override void use(Character player, string item_name, int item_value)
        {
            Console.WriteLine($"{player}: Using {item_name} {item_value}");
            double increase = 0;
            switch (item_name)
            {
                case "Potion of Healing":

                    player.health_current += item_value;
                    if (player.health_current > player.health_max)
                    {
                        player.health_current = player.health_max;
                    }
                    Console.WriteLine($"{player} regenerated {item_value} Lifepoints!");
                    break;

                case "Potion of Mana":

                    player.mana_current += item_value;
                    if (player.mana_current > player.mana_max)
                    {
                        player.mana_current = player.mana_max;
                    }
                    Console.WriteLine($"{player} regenerated {item_value} Manapoints!");
                    break;

                case "Potion of Poison":

                    player.character_status_duration.Add("Potion of Poison", item_duration);
                    player.character_status_value.Add("Potion of Poison", item_value);
                    // muss Ziel geben use function anpassen
                    break;
                case "Potion of Strength":

                    increase = (item_value / 100);
                    player.character_status_duration.Add("Potion of Strength", item_duration);
                    player.character_status_value.Add("Potion of Strength", item_value);
                    player.change_physical_power_percentage += increase;
                    break;
                case "Potion of Weakness":

                    increase = (item_value / 100);
                    player.character_status_duration.Add("Potion of Weakness", item_duration);
                    player.character_status_value.Add("Potion of Weakness", item_value);
                    player.change_physical_power_percentage -= increase;
                    break;
                default:
                    break;
            }
        }
    }
}
