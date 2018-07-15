using System;
using System.Collections.Generic;

namespace Abschlussaufgabe___TextAdventure
{
    class Room: GameObject
    {
        public Dictionary<TextAdventure.Direction, Room> Neighbors {get; set;} = new Dictionary<TextAdventure.Direction, Room>();
        public List<Item> Items {get; set;} = new List<Item>();
        public List<Npc> Npcs {get; set;} = new List<Npc>();
        public Item Key {get; private set;}
        public bool AlreadyVisited {get; set;}

        public Room (string name, string description, Item key)
        {
            Name = name;
            Description = description;       
            Key = key;    
            AlreadyVisited = false;
        }

        public void StartUp()
        {
            this.AlreadyVisited = true;
            foreach (Npc npc in Npcs)
            {
                if(npc.IsActive)
                    npc.Dialog(TextAdventure.Player, npc);
                if (npc.IsAggressive)
                    npc.Fight(TextAdventure.Player, npc);
            }
        }
    }
}