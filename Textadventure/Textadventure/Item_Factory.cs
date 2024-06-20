using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Item_Factory
    {
        public static Item create_item(string name, int value, int amount)
        {
            // Logik zur Auswahl der richtigen Item-Klasse basierend auf dem Namen
            switch (name)
            {
                case "Sword":
                case "Dagger":
                case "Bow":
                case "Mace":
                case "Staff":
                    return new Item_Weapon(name, value, amount);

                case "Cloth":
                case "Leather":
                case "Studded Leather":
                case "Chainmail":
                case "Plate":
                case "Cloak":
                case "Necklace":
                case "Ring":
                case "Shield":
                    return new Item_Armor(name, value, amount);

                case "Potion of Healing":
                case "Potion of Poison":
                case "Potion of Strength":
                case "Potion of Weakness":
                    return new Item_Potion(name, value, amount);

                case "Scroll":
                    return new Item_Scroll(name, value, amount);

                case "Rope":
                case "Wood":
                case "Flint and Steel":
                case "Bag":
                case "Coin":
                case "Map":
                    return new Item_Utility(name, value, amount);

                case "Fish":
                case "Chicken":
                case "SoupSoupSoup":
                case "Beef":
                case "Vegetables":
                case "Herbs":
                case "Water":
                    return new Item_Food(name, value, amount);

                default:
                    return new Item(name, value, amount); // Fallback für allgemeine Items
            }
        }
    }
}
