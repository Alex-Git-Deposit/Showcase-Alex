using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item_Scroll : Item
    {
        public Item_Scroll()
        {
            item_name = "Scroll";
            item_tooltip = "Scroll";
            item_value = 1; // Beispielwert
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Scroll";
        }
        public Item_Scroll(string name) : base(name)
        {
            item_name = name;
            item_tooltip = "Scroll";
            item_value = 1; // Beispielwert
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "Scroll";
        }
        public Item_Scroll(string name, int amount) : base(name, amount)
        {
            item_name = name;
            item_tooltip = "Scroll";
            item_value = 1; // Beispielwert
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Scroll";
        }
        public Item_Scroll(string name, int value, int amount) : base(name, value, amount)
        {
            item_name = name;
            item_tooltip = "Scroll";
            item_value = value; // Beispielwert
            item_sell_value = 1;
            item_amount = amount;
            item_type = "Scroll";
        }
        public override void use(Character player, string item_name, int item_value)
        {
            Console.WriteLine($"{player}: Using {item_name} {item_value}");
        }
    }
}
