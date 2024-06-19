using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item_Food : Item
    {
        public Item_Food()
        {
            item_name = "Food";
            item_tooltip = "Food";
            item_value = 1;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Food";
        }
        public Item_Food(string name) : base(name)
        {
            item_name = name;
            item_tooltip = "Food";
            item_value = generate_food_value(name);
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Food";
        }
        public Item_Food(string name, int amount) : base(name, amount)
        {
            item_name = name;
            item_tooltip = "Food";
            item_value = generate_food_value(name);
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Food";
        }
        public Item_Food(string name, int value, int amount) : base(name, value, amount)
        {
            item_name = name;
            item_tooltip = "Food";
            item_value = value;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Food";
        }
        private int generate_food_value(string name)
        {
            switch (name)
            {
                case "Fish":
                    return 15;
                case "Chicken":
                    return 14;
                case "SoupSoupSoup":
                    return 70;
                case "Beef":
                    return 16;
                case "Vegetables":
                    return 4;
                case "Herbs":
                    return 2;
                case "Water":
                    return 2;
                default:
                    return 1;
            }
        }
        public override void use(Character player, string item_name, int item_value)
        {
            switch (item_name)
            {
                case "Fish":
                case "Chicken":
                case "SoupSoupSoup":
                case "Beef":
                case "Vegetables":
                case "Herbs":
                case "Water":
                default:
                    break;
            }
            Console.WriteLine($"{player}: Using {item_name} {item_value}");
        }
    }
}
