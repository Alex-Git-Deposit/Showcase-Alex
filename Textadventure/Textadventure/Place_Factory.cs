using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public static class Place_Factory
    {
        public static Place create_place(string place_name)
        {
            switch (place_name)
            {
                case "Castle":
                    return new Place_Castle();
                case "Cave":
                    return new Place_Cave();
                case "Desert":
                    return new Place_Desert();
                case "Dungeon":
                    return new Place_Dungeon();
                case "Forest":
                    return new Place_Forest();
                case "Library":
                    return new Place_Library();
                case "Market":
                    return new Place_Market();
                case "Mountain":
                    return new Place_Mountain();
                case "Mountain-Deeps":
                    return new Place_Mountain_Deeps();
                case "Pub":
                    return new Place_Pub();
                case "Tavern":
                    return new Place_Tavern();
                case "Town":
                    return new Place_Town();
                case "Village":
                    return new Place_Village();
                case "Campfire":
                    return new Place_Campfire();
                default:
                    return new Place_Tavern();
            }
        }
    }
}