using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item_Weapon : Item
    {
        public Item_Weapon()
        {
            item_name = "Sword";
            item_tooltip = "Weapon";
            item_value = 1;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Weapon";
        }
        public Item_Weapon(string name) : base(name)
        {
            item_name = name;
            item_tooltip = "Weapon";
            item_value = generate_weapon_value(name);
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Weapon";
        }
        public Item_Weapon(string name, int amount) : base(name, amount)
        {
            item_name = name;
            item_tooltip = "Weapon";
            item_value = generate_weapon_value(name);
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Weapon";
        }
        public Item_Weapon(string name, int value, int amount) : base(name, value, amount)
        {
            item_name = name;
            item_tooltip = "Weapon";
            item_value = value;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Weapon";
        }

        public int generate_weapon_value(string name) // werte anpassen
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
}
