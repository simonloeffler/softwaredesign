using System;
using System.Collections.Generic;

namespace Abschlussaufgabe___TextAdventure
{
    class Npc: Creature
    {
        public bool IsActive {get; private set;}
        public bool IsAggressive {get; private set;}
        public List<CreatureDialogLine> DialogLines {get; set;} = new List<CreatureDialogLine>();
        public bool CanSpeak {get; private set;}

        public Npc (string name, string description, int health, int damage, bool isActive, bool isAggressive, bool canSpeak)
        {
            Name = name;
            Description = description;
            Health = health;
            _damage = damage;
            IsActive = isActive;
            IsAggressive = isAggressive;
            CanSpeak = canSpeak;
        }
    }
}