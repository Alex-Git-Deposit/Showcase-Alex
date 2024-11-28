using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Item_Factory
    {
        public static Item create_item(string name, int amount)
        {
            // Logik zur Auswahl der richtigen Item-Klasse basierend auf dem Namen
            switch (name)
            {
                case "Sword":
                case "Dagger":
                case "Bow":
                case "Mace":
                case "Staff":
                    return new Item_Weapon(name, amount);

                case "Cloth":
                case "Leather":
                case "Studded Leather":
                case "Chainmail":
                case "Plate":
                case "Cloak":
                case "Necklace":
                case "Ring":
                case "Shield":
                    return new Item_Armor(name, amount);

                case "Potion of Healing":
                case "Potion of Poison":
                case "Potion of Strength":
                case "Potion of Weakness":
                    return new Item_Potion(name, amount);

                case "Scroll":
                    return new Item_Scroll(name, amount);

                case "Rope":
                case "Wood":
                case "Flint and Steel":
                case "Bag":
                case "Coin":
                case "Map":
                    return new Item_Utility(name, amount);

                case "Fish":
                case "Chicken":
                case "SoupSoupSoup":
                case "Beef":
                case "Vegetables":
                case "Herbs":
                case "Water":
                    return new Item_Food(name, amount);

                default:
                    return new Item(name, amount); // Fallback für allgemeine Items
            }
        }
    }
}
