using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public class Simple_Random
    {
        private Random rnd;

        public Simple_Random()
        {
            rnd = new Random();
        }
        // Generiere eine Zufallszahl zwischen min (einschließlich) und max (ausschließlich).
        public int Next(int min, int max)
        {
            return rnd.Next(min, max);
        }

        // Überprüfe, ob ein Ereignis basierend auf der Wahrscheinlichkeit ausgelöst wird.
        public bool check_probability(int threshold_percentage)
        {
            int rnd_number = rnd.Next(1, 101); // Zufällige Zahl von 1 bis 100 generieren
            return rnd_number <= threshold_percentage; // Ereignis wird ausgelöst, wenn die zufällige Zahl kleiner oder gleich dem Schwellenwert ist
        }
    }
}
