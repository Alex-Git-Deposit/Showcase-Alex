using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Equipment_System
    {
        private Character? player;
        public List<Item> equipment;

        // Konstruktor, der das Equipment initialisiert
        public Equipment_System()
        {
            equipment = new List<Item>();
        }
        public Equipment_System(Character player)
        {
            equipment = new List<Item>();
            this.player = player;
        }

        public List<Item> equipment_get()
        {
            return equipment;
        }

        // Methode zum Hinzufügen eines Items zum Equipment
        public void equip_item(string name, int value)
        {
            if (player != null)
            {
                if (player.inventory.inventory.Exists(item => item.item_name == name && item.item_value == value))
                {
                    if (equipment.Exists(item => item is Item_Potion || item is Item_Scroll || item is Item_Utility
                                              || item is Item_Food))
                    {
                        Console.WriteLine($"You can not equip this Item!");
                    }
                    else if (equipment.Exists(item => item.item_name == name))
                    {
                        Console.WriteLine($"{name} ist bereits ausgerüstet.");
                    }
                    else if (equipment.Exists(item => item.item_type == "Weapon"))
                    {
                        Console.WriteLine($"You already have a weapon equipped");
                    }
                    else if (equipment.Exists(item => item.item_type == "Armor" && !item.item_name.Contains("Shield")))
                    {
                        Console.WriteLine($"You already have an armor equipped");
                    }
                    else if (equipment.Exists(item => item.item_type == "Armor" && item.item_name.Contains("Shield")))
                    {
                        Console.WriteLine($"You already have a shield equipped");
                    }
                    else
                    {
                        equipment.Add(new Item(name, value, 1));
                        player.player_remove_item(name, value, 1);
                        Console.WriteLine($"Item: {name} {value} equipped!");
                    }
                }
                else
                {
                    Console.WriteLine("You can not equip an item that is not in your inventory!");
                }
            }
        }
        public void unequip_item(string name, int value)
        {
            if (player != null)
            {
                var existing_item = equipment.Find(item => item.item_name == name && item.item_value == value);
                if (existing_item != null)
                {
                    equipment.Remove(existing_item);
                    player.player_add_item(name, value, 1);
                    Console.WriteLine($"Item: {name} {value} unequipped!");
                }
            }
        }

        // Methode zum Ausdrucken des Equipments eines Spielers
        public static void equipment_print(Character player)
        {
            Console.WriteLine($"Equipment von {player.character_name}:");
            List<Item> character_equipment = player.equipment.equipment_get();
            foreach (var item in character_equipment)
            {
                Console.WriteLine($"{item.item_name}: Wert: {item.item_value}");
            }
        }
    }
}
