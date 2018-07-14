using System;
using System.Collections.Generic;
using System.Linq;

namespace Abschlussaufgabe___TextAdventure
{
    class Player: Creature
    {
        private static Player _instance;

        public List<PlayerDialogModel> Dialogs {get; set;} = new List<PlayerDialogModel>();
        public Room CurrentRoom {get; private set;}
        public int BaseDamage {get; private set;}
        public Weapon EquippedWeapon {get; private set;}
        public int MaxHealth {get; private set;}

        private Player (string name, string description, int maxHealth, int baseDamage, Room currentRoom){
            Name = name;
            Description = description;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            BaseDamage = baseDamage;
            Damage = BaseDamage;
            CurrentRoom = currentRoom;
        }

        public static void Create (string name, string description, int maxHealth, int baseDamage, Room currentRoom)
        {
            if (_instance != null)
                throw new Exception ("Object already created!");
            _instance = new Player (name, description, maxHealth, baseDamage, currentRoom);
        }

        public static Player Instance
        {
            get
            {
                if(_instance == null)
                    throw new Exception ("Object not created!");
                return _instance;
            }
        }

        #region PlayerActions

        public void Help ()
        {
            Console.WriteLine("You can use the following commands to play the game:");
            Console.WriteLine("'inventory' (i) - to check your pockets");
            Console.WriteLine("'look' (l) - to look around in the room you're in");
            Console.WriteLine("'lookat <name of something in the room / inventory>' (la) - to look at the object or person ");
            Console.WriteLine("'loot <name of an item / corpse in the room>' (lo) - to loot a crate or a corpse");
            Console.WriteLine("'take <name of an item in the room>' (t) - to pick an item up");
            Console.WriteLine("'drop <name of an item in your inventory' (d) - to drop an item from your inventory");
            Console.WriteLine("'use <name of an item in the inventory>' (u) - to drink a potion / eat food or equip a weapon");
            Console.WriteLine("'go <direction / first letter of direction>' (g) - move to the direction (north, east, south or west)");
            Console.WriteLine("'speak <person in the room>' (s) - to start a conversation with the person");
            Console.WriteLine("'attack <creature / person>' (a) - to start a fight with the creature or person");
            Console.WriteLine("'info' (in) - show info about the Player");
            Console.WriteLine("'quit' (q) - to quit the game");
        }

        public void PlayerInfo ()
        {
            Console.WriteLine("Your stats:");
            Console.WriteLine("Health: " + Health + " / " + MaxHealth);
            Console.WriteLine("Damage: " + Damage + " (= base damage + weapon damage)");
            if(EquippedWeapon != null)
            {
                Console.WriteLine("Equipped weapon: " + EquippedWeapon.Name);
                Console.WriteLine("Equipped weapon damage: " + EquippedWeapon.Damage);
            }
            else
                Console.WriteLine("Equipped weapon: none");  
        }

        public void CheckInventory ()
        {
            Console.WriteLine("Your stuff:");
            foreach (Item item in Inventory)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void Look ()
        {
            Console.WriteLine("You are looking around in the " + CurrentRoom.Name + ":");
            Console.WriteLine(CurrentRoom.Description);
            Console.WriteLine(Environment.NewLine + "The following places are nearby:");
            foreach(KeyValuePair<TextAdventure.Direction, Room> neighbor in CurrentRoom.Neighbors)
            {
                Console.WriteLine("   You can reach the " + neighbor.Value.Name + " in the " + neighbor.Key + ".");
            }
            Console.WriteLine(Environment.NewLine + "The persons in the location are:");
            foreach (NPC npc in CurrentRoom.NPCs)
            {
                Console.WriteLine("   " + npc.Name);
            }
            Console.WriteLine(Environment.NewLine + "Also you can see the following things nearby:");
            foreach (Item item in CurrentRoom.Items)
                Console.WriteLine("   " + item.Name);
        }

        public void LookAt (string thing)
        {
           if (String.IsNullOrWhiteSpace(thing))
                Console.WriteLine ("Please select a Object to look at.");
            else
            {
                GameObject subject = CurrentRoom.Items.Find(x => x.Name.ToLower() == thing);
                if (subject == null)
                    subject = CurrentRoom.NPCs.Find(x => x.Name.ToLower() == thing);
                if(subject == null)
                    subject = Inventory.Find(x => x.Name.ToLower() == thing);
                if(subject == null)
                    Console.WriteLine("There is nothing with the name '" + thing + "' in the room.");
                else
                {
                    Console.WriteLine(subject.Description);
                    if(subject is Weapon)
                    {
                        Weapon weapon = (Weapon) subject;
                        Console.WriteLine(weapon.Name + " can be used as a weapon and causes " + weapon.Damage + " damage.");
                    } 
                    else if (subject is Potion)
                    {
                        Potion potion = (Potion) subject;
                        Console.WriteLine(potion.Name  + " can be consumed and is capable of curing " + potion.Cure + " healthpoints.");
                    } 
                }
                
            } 
        }

        public void Attack (string person)
        {
           if (String.IsNullOrWhiteSpace(person))
                Console.WriteLine ("Please select a Object to look at.");
            else
                if(CurrentRoom.NPCs.Find(x =>  x.Name.ToLower() == person) != null)
                    if(CurrentRoom.NPCs.Find(x =>  x.Name.ToLower() == person).Health == 0)
                        Console.WriteLine("You can't attack corpses. But you can loot them.");
                    else
                        Fight(TextAdventure.Player, CurrentRoom.NPCs.Find(x =>  x.Name.ToLower() == person));
                else if (CurrentRoom.Items.Find(x =>  x.Name.ToLower() == person) != null)
                    Console.WriteLine ("You can't fight this...");
                else
                    Console.WriteLine ("There is no " + person + " nearby.");
        }

        public void SpeakTo (string person)
        {
           if (String.IsNullOrWhiteSpace(person))
                Console.WriteLine ("Please select a Person to speak to.");
            else
                if(CurrentRoom.NPCs.Find(x =>  x.Name.ToLower() == person) != null)
                    if(CurrentRoom.NPCs.Find(x =>  x.Name.ToLower() == person).Health == 0)
                        Console.WriteLine("You can't speak to corpses. But you can loot them.");
                    else
                        Dialog(TextAdventure.Player, CurrentRoom.NPCs.Find(x =>  x.Name.ToLower() == person));
                else if (CurrentRoom.Items.Find(x =>  x.Name.ToLower() == person) != null)
                    Console.WriteLine ("You can't talk to this...");
                else
                    Console.WriteLine ("There is no " + person + " nearby.");
        }

        public void Loot (string thing)
        {
            if (String.IsNullOrWhiteSpace(thing))
                Console.WriteLine ("Please select a Object to loot.");
            else 
            {
                if(CurrentRoom.Items.Find(x => x.Name.ToLower() == thing) != null)
                    if (CurrentRoom.Items.Find(x => x.Name.ToLower() == thing).GetType() == typeof(Crate))
                    {
                        Crate subject = (Crate) CurrentRoom.Items.Find(x => x.Name.ToLower() == thing);
                        if (subject.Key != null)
                            if(Inventory.Contains(subject.Key))
                            {
                                Console.WriteLine("You successfully opened the " + subject.Name + ". Things you got from the " + subject.Name + ":");
                                foreach(Item item in subject.Inventory.ToList())
                                {
                                    Console.WriteLine(item.Name);
                                    Inventory.Add(item);
                                    subject.Inventory.Remove(item);
                                }
                            }
                            else
                                Console.WriteLine("You are missing the key to open this!");
                        else
                        {
                            Console.WriteLine("Things you got from the " + subject.Name + ":");
                            foreach(Item item in subject.Inventory.ToList())
                            {
                                Console.WriteLine(item.Name);
                                Inventory.Add(item);
                                subject.Inventory.Remove(item);
                            }
                        }
                    }
                    else
                        Console.WriteLine("It's not possible to loot that.");
                    
                else if (CurrentRoom.NPCs.Find(x => x.Name.ToLower() == thing) != null)
                {
                    NPC subject = CurrentRoom.NPCs.Find(x => x.Name.ToLower() == thing);
                    if (subject.Health == 0)
                    {
                        Console.WriteLine("Things you got from " + subject.Name + "s corpse:");
                        foreach(Item item in subject.Inventory.ToList())
                        {
                            Console.WriteLine(item.Name);
                            Inventory.Add(item);
                            subject.Inventory.Remove(item);
                        }
                    }
                    else
                     Console.WriteLine("Come on! You have to kill " + subject.Name + " before you can loot the corpse.");
                } 
                else
                    Console.WriteLine("There is no " + thing + " nearby.");     
            } 
        }
        
        public void Move (string direction)
        {
            Room nextRoom = null;
            bool validDirection = false;
            TextAdventure.Direction selectedDirection = TextAdventure.Direction.north;

            switch (direction)
            {
                case "north": case "n":
                    CurrentRoom.Neighbors.TryGetValue(TextAdventure.Direction.north, out nextRoom);
                    validDirection = true;
                    selectedDirection = TextAdventure.Direction.north;
                    break;
                case "south": case "s":
                    CurrentRoom.Neighbors.TryGetValue(TextAdventure.Direction.south, out nextRoom);
                    validDirection = true;
                    selectedDirection = TextAdventure.Direction.south;
                    break;
                case "east": case "e":
                    CurrentRoom.Neighbors.TryGetValue(TextAdventure.Direction.east, out nextRoom);
                    validDirection = true;
                    selectedDirection = TextAdventure.Direction.east;
                    break;
                case "west": case "w":
                    CurrentRoom.Neighbors.TryGetValue(TextAdventure.Direction.west, out nextRoom);
                    validDirection = true;
                    selectedDirection = TextAdventure.Direction.west;
                    break;
                default:
                    Console.WriteLine("'" + direction + "'" + " is not a valid direction to go.");
                    break;
            }

            if (validDirection && nextRoom == null)
                Console.WriteLine("There is no place to go in the " + selectedDirection.ToString() + ".");
            else if (nextRoom != null)
            {
                if(nextRoom.Key != null && Inventory.Contains(nextRoom.Key) || nextRoom.Key == null)
                {
                    CurrentRoom = nextRoom;
                    Console.WriteLine("You are now in the " + CurrentRoom.Name + ".");
                    if(!CurrentRoom.AlreadyVisited)
                        CurrentRoom.StartUp();
                }
                else
                    Console.WriteLine("You can't go there - you are missing the right key!");
            }   
        }

        public void Take (string thing)
        {
            if (String.IsNullOrWhiteSpace(thing))
                Console.WriteLine ("Please select a Object to pick up.");
            else
            {
                Item item = CurrentRoom.Items.Find(x => x.Name.ToLower() == thing);
                if(!item.Carryable)
                    Console.WriteLine("You can't pick up a " + item.Name + ".");
                else
                {
                    if (item == null)
                        Console.WriteLine("There is no item with the name '" + thing + "' in the room.");
                    else
                    {
                        Inventory.Add(item);
                        CurrentRoom.Items.Remove(item);
                        Console.WriteLine("You added a " + item.Name + " to your inventory.");
                    }
                }
            }
        }

        public void Drop (string thing)
        {
            if (String.IsNullOrWhiteSpace(thing))
                Console.WriteLine ("Please select a object to drop.");
            else
            {
                Item item = Inventory.Find(x => x.Name.ToLower() == thing);
                if (item == null)
                    Console.WriteLine("There is no item with the name '" + thing + "' in your inventory.");
                else
                {
                    CurrentRoom.Items.Add(item);
                    Inventory.Remove(item);
                    Console.WriteLine("You dropped a " + item.Name + " from your inventory.");
                }
            }
        }

        public void Use (string thing)
        {
            if (String.IsNullOrWhiteSpace(thing))
                Console.WriteLine ("Please select a object to use.");
            else
            {
                Item item = Inventory.Find(x => x.Name.ToLower() == thing);
                if (item == null)
                    Console.WriteLine("There is no item with the name '" + thing + "' in your inventory.");
                else
                {
                    if(item is Weapon)
                    {
                        Weapon newWeapon = (Weapon) item;
                        EquippedWeapon = newWeapon;
                        Damage = BaseDamage + newWeapon.Damage;
                        Console.WriteLine("You equipped " + newWeapon.Name + " as your Weapon. Your damage is now " + Damage + ".");
                    } 
                    else if (item is Potion)
                    {
                        Potion potion = (Potion) item;
                        Health += potion.Cure;
                        if(Health >= MaxHealth)
                            Health = MaxHealth;
                        Inventory.Remove(potion);
                        Console.WriteLine("You consumed " + potion.Name  + ". You now have " + Health + " healthpoints.");
                    } 
                    else
                        Console.WriteLine("You can't use this.");
                }
            }
        }

        public void CheckWeapon() 
        {
            if(EquippedWeapon != null)
                if(Inventory.Find(x => x.Name == EquippedWeapon.Name) == null)
                {
                    Damage = BaseDamage;
                    EquippedWeapon = null;
                }
        }

        #endregion

        #region Helper
        #endregion
    }
}