using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item_Utility : Item
    {
        public Item_Utility()
        {
            item_name = "Utility";
            item_tooltip = "Utility";
            item_value = 1;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Utility";
        }
        public Item_Utility(string name) : base(name)
        {
            item_name = name;
            item_tooltip = "Utility";
            item_value = generate_utility_value(name);
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Utility";
        }
        public Item_Utility(string name, int amount) : base(name, amount)
        {
            item_name = name;
            item_tooltip = "Utility";
            item_value = generate_utility_value(name);
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Utility";
        }
        public Item_Utility(string name, int value, int amount) : base(name, value, amount)
        {
            item_name = name;
            item_tooltip = "Utility";
            item_value = value;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Utility";
        }
        private int generate_utility_value(string name) // werte anpassen
        {
            switch (name)
            {
                case "Rope":
                    return 5;
                case "Wood":
                    return 1;
                case "Flint and Steel":
                    return 1;
                case "Bag":
                    return 1;
                case "Coin":
                    return 1;
                case "Map":
                    return 1;
                case "Small Key":
                    return 10;
                default:
                    return 1;
            }
        }
        public override void use(Character player, string item_name, int item_value)
        {
            switch (item_name)
            {
                case "Rope":
                case "Wood":
                case "Flint and Steel":
                case "Bag":
                case "Coin":
                case "Map":
                default:
                    break;
            }
            Console.WriteLine($"{player}: Using {item_name} {item_value}");

        }
    }
}
