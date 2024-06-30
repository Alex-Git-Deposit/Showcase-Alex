using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Textadventure.Program_Main;

namespace Textadventure
{
    public class Inventory_System
    {
        private Character? player;
        public List<Item> inventory;

        // Konstruktor, der das Inventar initialisiert
        public Inventory_System()
        {
            inventory = new List<Item>();
        }
        public Inventory_System(Character player)
        {
            inventory = new List<Item>();
            this.player = player;
        }
        public List<Item> inventory_get()
        {
            return inventory;
        }

        // Methode zum Hinzufügen eines Items zum Inventar
        public void inventory_add_item(string name, int value, int quantity)
        {
            var existing_item = inventory.Find(item => item.item_name == name && item.item_value == value);
            if (existing_item != null)
            {
                existing_item.item_amount += quantity;
            }
            else
            {
                Item new_item = Item_Factory.create_item(name, quantity);
                inventory.Add(new_item);
            }
        }

        // Methode zum Entfernen eines Items aus dem Inventar
        public void inventory_remove_item(string name, int value, int remove_quantity)
        {
            var existing_item = inventory.Find(item => item.item_name == name && item.item_value == value);
            if (existing_item != null)
            {
                if (existing_item.item_amount >= remove_quantity)
                {
                    existing_item.item_amount -= remove_quantity;
                    if (existing_item.item_amount <= 0)
                    {
                        inventory.Remove(existing_item);
                        Console.WriteLine($"{name} (Wert: {value}) vollständig aus dem Inventar entfernt.");
                    }
                    else
                    {
                        Console.WriteLine($"{remove_quantity} {name}(s) (Wert: {value}) aus dem Inventar entfernt.");
                    }
                }
                else
                {
                    Console.WriteLine($"Nicht genügend {name} (Wert: {value}) im Inventar.");
                }
            }
            else
            {
                Console.WriteLine($"Entfernen fehlgeschlagen. {name} (Wert: {value}) nicht im Inventar gefunden.");
            }
        }

        // Methode zum Ausdrucken des Inventars eines Spielers
        public static void inventory_print(Character player)
        {
            Console.WriteLine($"Inventar von {player.character_name}:");
            List<Item> character_inventory = player.inventory.inventory_get();
            foreach (var item in character_inventory)
            {
                Console.WriteLine($"{item.item_name}: Wert: {item.item_value}, Anzahl: {item.item_amount}");
            }
        }
    }
}
