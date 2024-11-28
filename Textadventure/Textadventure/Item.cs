using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Item
    {
        public string item_name;
        public string item_tooltip;
        public int item_value;
        public int item_duration;
        public int item_sell_value;
        public int item_amount;
        public string item_type;

        public Item()
        {
            item_name = "Item";
            item_tooltip = "None";
            item_value = 1;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "None";
        }
        public Item(string name)
        {
            item_name = name;
            item_tooltip = "None"; // funktion schreiben die den richtigen tooltip abholt
            item_value = 1;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = 1;
            item_type = "None";
        }
        public Item(string name, int amount)
        {
            item_name = name;
            item_tooltip = "None"; // funktion schreiben die den richtigen tooltip abholt
            item_value = 1;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "None";
        }
        public Item(string name, int value, int amount)
        {
            item_name = name;
            item_tooltip = "None"; // funktion schreiben die den richtigen tooltip abholt
            item_value = value;
            item_duration = 0;
            item_sell_value = 1;
            item_amount = amount;
            item_type = "None";
        }

        public virtual void use(Character player, string item_name, int item_value)
        {
            Console.WriteLine($"{player}: Using {item_name} {item_value}");
        }
    }
}
