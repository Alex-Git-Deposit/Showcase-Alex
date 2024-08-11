using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item_Armor : Item
    {
        public Item_Armor()
        {
            item_name = "Cloth";
            item_tooltip = "Armor";
            item_value = 1;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Armor";
        }
        public Item_Armor(string name) : base(name)
        {
            item_name = name;
            item_tooltip = "Armor"; // Sobald tooltips existieren funktion schreiben
            item_value = generate_armor_value(name);
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Armor";
        }
        public Item_Armor(string name, int amount) : base(name, amount)
        {
            item_name = name;
            item_tooltip = "Armor"; // Sobald tooltips existieren funktion schreiben
            item_value = generate_armor_value(name);
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Armor";
        }
        public Item_Armor(string name, int value, int amount) : base(name, value, amount)
        {
            item_name = name;
            item_tooltip = "Armor"; // Sobald tooltips existieren funktion schreiben
            item_value = value;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
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
}
