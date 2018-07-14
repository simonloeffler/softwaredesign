using System;
using System.Collections.Generic;

namespace Abschlussaufgabe___TextAdventure
{
    class Crate: Item
    {
        public Item Key {get; private set;}
        public List<Item> Inventory = new List<Item>();

        public Crate (string name, string description, bool carryable, Item key): base (name,  description, carryable)
        {
            Name = name;
            Description = description;
            Carryable = carryable;
            Key = key;           
        }
    }
}