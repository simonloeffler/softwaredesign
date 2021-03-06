﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Abschlussaufgabe___TextAdventure
{
    static class TextAdventure
    {
        public static Player Player {get; private set;}
        private static Item _winningItem {get; set;}
        private static List<Room> _rooms {get; set;} = new List<Room>();
        private static List<Item> _items {get; set;} = new List<Item>();
        private static List<Npc> _npcs {get; set;} = new List<Npc>();
        public static bool IsFinished {get; set;} = false;

        //private static Dictionary<string, Action<string>> commands = new Dictionary<string, Action<string>>(); 
        
        static void Main(string[] args)
        {
            LoadGameData();
            ConsoleWriteRed(Environment.NewLine + "Welcome to The Fantastic Adventure!" + Environment.NewLine);
            Console.WriteLine("You hit the ground really hard. When getting your head up and checking the situation you acknowledge that you are in a yard in front of a tavern.");
            Console.WriteLine("The carriage behind you is driving away while the carrige driver screams: 'I alredy took the money for taking you with us out of your pockets!'");
            Console.WriteLine("And just in this moment you realise that your throat is really dry and you're very thirsty. You should find some booze quickly.");
            Player.Look();

            // commands.Add("la", (parameter) => 
            // {
            //     Player.LookAt(parameter);
            // });                                  --> add all functions to dictionary...

            // if (!commands.ContainsKey(command)){
            //     Console.WriteLine("Unknwon Command");
            // } 
            // else
            // {
            //     var func = commands[command];      --> get function from dictionary ans set delegate
            //     func(parameter);                   --> call delegate
            // }

            for (;;)
            {
                if(Player.Inventory.Contains(_winningItem))
                {
                    ConsoleWriteGreen("You got the " + _winningItem.Name + " and thus you won the game!");
                    IsFinished = true;
                }

                if (IsFinished == true)
                {
                    ConsoleWriteRed("Thank you for playing.");
                    break;
                }

                Player.CheckWeapon();

                ConsoleWriteDarkYellow("Type 'help' and press Enter if you don't know what to do." + Environment.NewLine);

                string input = Console.ReadLine().ToLower();

                string command = input.IndexOf(" ") > -1 
                  ? input.Substring(0, input.IndexOf(" "))
                  : input;

                string parameter = input.IndexOf(" ") > -1 
                  ? input.Substring(input.IndexOf(" ") + 1, input.Length - (input.IndexOf(" ") + 1))
                  : "";
                
                switch(command)
                {
                    case "quit": case "q":
                        IsFinished = true;
                        break;
                    case "help": case "h":
                        Player.Help();
                        break;
                    case "inventory": case "i":
                        Player.CheckInventory();
                        break;
                    case "look": case "l":
                        Player.Look();
                        break;
                    case "go": case "g":
                        Player.Move(parameter);
                        break;
                    case "take": case "t":
                        Player.Take(parameter);
                        break;
                    case "drop": case "d":
                        Player.Drop(parameter);
                        break;
                    case "lookat": case "la":
                        Player.LookAt(parameter);
                        break;
                    case "loot": case "lo":
                        Player.Loot(parameter);
                        break;
                    case "attack": case "a":
                        Player.Attack(parameter);
                        break;
                    case "info": case "in":
                        Player.PlayerInfo();
                        break;
                    case "use": case "u":
                        Player.Use(parameter);
                        break;
                    case "speak": case "s":
                        Player.SpeakTo(parameter);
                        break;
                    default:
                        ConsoleWriteRed("Unknown command!");
                        break;
                }
            }
        }

        public enum Direction {north=0, east=1, south=2, west=3};

        #region GameData

        private static void LoadGameData ()
        {

            #region Items

            _items.Add(new Item("Cryptkey", "It's a really old key shaped like a bone.", true));
            _items.Add(new Item("Cratekey", "Just an ordinary key.", true));

            _items.Add(new Item("Stone", "And the winner is Rocky!", true));
            _items.Add(new Item("Ring", "It's the servants ring!", true));
            _items.Add(new Item("Well", "Well, well, well. What do we have here?", false));
            _items.Add(new Item("Gravestone", "On the Gravestone it says: 'R.I.P Smitty Werbenjagermanjensen - he was the real #1'", false));
            _items.Add(new Item("Cross", "On the Cross it says: 'Grave of an unknwon hero - fallen while hunting wild hogs.'", false));
            _items.Add(new Item("Grave", "On the Grave it says: 'Arouse me from sleep on the 30th September.'", false));
            _items.Add(new Item("Mirror", "A bored looking face in front of a computer.", false));
            _items.Add(new Item("Barrel", "Not a single drop inside...", false));

            _items.Add(new Weapon("Stick", "It's a bit sticky.", true, 1));
            _items.Add(new Weapon("Hammer", "Not very dangerous...", true, 5));
            _items.Add(new Weapon("Shovel", "It´s a heavy shovel with a long shaft.", true, 8));
            _items.Add(new Weapon("Dagger", "A nice dagger with some ornaments on the handle.", true, 11));
            _items.Add(new Weapon("Sword", "An old, rusty sword.", true, 15));

            _items.Add(new Potion("Potion", "Smells sweet.", true, 50));
            _items.Add(new Potion("Apple", "It's a bit crumpled.", true, 15));
            _items.Add(new Potion("Bread", "Not fresh but still eatable.", true, 20));
            _items.Add(new Potion("Booze", "Just perfect.", true, 100));

            _items.Add(new Crate("Crate", "A big, heavy crate. Seems like you need a key to open it.", false, GetItemByName("Cratekey")));

            _winningItem = GetItemByName("Booze");

            #endregion

            #region Rooms

            _rooms.Add(new Room("Yard", "It's a big yard with a hard ground.", null));
            _rooms.Add(new Room("Tavern", "A cozy tavern. Unfortunately you have no money.", null));
            _rooms.Add(new Room("Stables", "Small stables with no horses at all.", null));
            _rooms.Add(new Room("Graveyard", "A spooky graveyard with some old gravestones.", null));
            _rooms.Add(new Room("Crypt", "It's really dark in here. You can barely see a thing.", GetItemByName("Cryptkey")));
            _rooms.Add(new Room("Cellar", "A cold stone cellar with all kinds of stuff. Maybe you can find something to ease your thirst.", null));

            #endregion

            #region RoomNeighbors

            GetRoomByName("Yard").Neighbors.Add(Direction.north, GetRoomByName("Tavern"));
            GetRoomByName("Yard").Neighbors.Add(Direction.east, GetRoomByName("Stables"));

            GetRoomByName("Tavern").Neighbors.Add(Direction.south, GetRoomByName("Yard"));
            GetRoomByName("Tavern").Neighbors.Add(Direction.east, GetRoomByName("Stables"));
            GetRoomByName("Tavern").Neighbors.Add(Direction.north, GetRoomByName("Cellar"));

            GetRoomByName("Cellar").Neighbors.Add(Direction.south, GetRoomByName("Tavern"));
            GetRoomByName("Cellar").Neighbors.Add(Direction.east, GetRoomByName("Crypt"));

            GetRoomByName("Stables").Neighbors.Add(Direction.south, GetRoomByName("Yard"));
            GetRoomByName("Stables").Neighbors.Add(Direction.west, GetRoomByName("Tavern"));
            GetRoomByName("Stables").Neighbors.Add(Direction.east, GetRoomByName("Graveyard"));

            GetRoomByName("Graveyard").Neighbors.Add(Direction.west, GetRoomByName("Stables"));
            GetRoomByName("Graveyard").Neighbors.Add(Direction.north, GetRoomByName("Crypt"));

            GetRoomByName("Crypt").Neighbors.Add(Direction.west, GetRoomByName("Cellar"));
            GetRoomByName("Crypt").Neighbors.Add(Direction.south, GetRoomByName("Graveyard"));

            #endregion

            #region Player

            Player.Create("Player", "That's me!", 100, 5, GetRoomByName("Yard"));
            Player = Player.Instance;

            #endregion

            #region AddItemsToRooms

            GetRoomByName("Yard").Items.Add(GetItemByName("Stick"));
            GetRoomByName("Yard").Items.Add(GetItemByName("Well"));

            GetRoomByName("Tavern").Items.Add(GetItemByName("Bread"));
            GetRoomByName("Tavern").Items.Add(GetItemByName("Mirror"));

            GetRoomByName("Cellar").Items.Add(GetItemByName("Dagger"));
            GetRoomByName("Cellar").Items.Add(GetItemByName("Crate"));
            GetRoomByName("Cellar").Items.Add(GetItemByName("Barrel"));

            GetRoomByName("Stables").Items.Add(GetItemByName("Apple"));
            GetRoomByName("Stables").Items.Add(GetItemByName("Hammer"));

            GetRoomByName("Graveyard").Items.Add(GetItemByName("Shovel"));
            GetRoomByName("Graveyard").Items.Add(GetItemByName("Gravestone"));
            GetRoomByName("Graveyard").Items.Add(GetItemByName("Grave"));
            GetRoomByName("Graveyard").Items.Add(GetItemByName("Cross"));
            GetRoomByName("Graveyard").Items.Add(GetItemByName("Ring"));

            GetRoomByName("Crypt").Items.Add(GetItemByName("Cratekey"));
            GetRoomByName("Crypt").Items.Add(GetItemByName("Potion"));

            #endregion

            #region Npcs

            _npcs.Add(new Npc("Servant", "He looks bored and somehow a bit worried.", 100, 15, false, false, true));
            _npcs.Add(new Npc("Orlan", "Standing behind the bar it seems like he is the owner of the tavern.", 100, 20, true, false, true));
            _npcs.Add(new Npc("Rat", "A nasty, big rat.", 60, 5, false, true, false));
            _npcs.Add(new Npc("Skeleton", "Maybe he should've drunken some more milk.", 90, 8, true, true, true));

            #endregion

            #region AddItemsToInventories / Crates

            Player.Inventory.Add(GetItemByName("Stone"));
            
            GetCrateByName("Crate").Inventory.Add(GetItemByName("Booze"));

            GetNpcByName("Servant").Inventory.Add(GetItemByName("Cryptkey"));
            GetNpcByName("Skeleton").Inventory.Add(GetItemByName("Sword"));

            #endregion

            #region AddNpcsToRooms

            GetRoomByName("Stables").Npcs.Add(GetNpcByName("Servant"));
            GetRoomByName("Tavern").Npcs.Add(GetNpcByName("Orlan"));
            GetRoomByName("Cellar").Npcs.Add(GetNpcByName("Rat"));
            GetRoomByName("Crypt").Npcs.Add(GetNpcByName("Skeleton"));

            #endregion

            #region NpcDialogLines

            GetNpcByName("Orlan").DialogLines.Add(new CreatureDialogLine("Hello my friend!", 0, null));
            GetNpcByName("Orlan").DialogLines.Add(new CreatureDialogLine("Get out here you needy vagrant!", 1, null));
            GetNpcByName("Orlan").DialogLines.Add(new CreatureDialogLine("Do you have money to pay for it?", 2, null));

            GetNpcByName("Skeleton").DialogLines.Add(new CreatureDialogLine("Who dares to disturb my rest?", 0, null));
            GetNpcByName("Skeleton").DialogLines.Add(new CreatureDialogLine("You won't hoax me you fool! I will now kill you!", 1, null));
            GetNpcByName("Skeleton").DialogLines.Add(new CreatureDialogLine("If you are dying anyway I could also have a little fun with you! Let's fight!", 2, null));

            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("Hello...", 0, null));
            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("Well, actually you can, yes. I lost my engagement ring on the graveyard...please bring it back.", 1, null));
            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("You think I'm a minor background actor? I have to tell you that I have the key to a crypt which is important for your progress!", 2, null));
            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("Only if you get me my ring I lost somewhere nearby!", 3, null));
            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("I don't exactly know...it has to be somwhere around.", 4, null));
            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("Thank you very much. Here, take this key as a reward. (He hands you over a key)", 5, GetItemByName("Cryptkey")));
            GetNpcByName("Servant").DialogLines.Add(new CreatureDialogLine("Ok. Here, take the key. (He hands you over a key)", 6, GetItemByName("Cryptkey")));

            #endregion

            #region PlayerDialogModels

            Player.Dialogs.Add(new PlayerDialogModel(GetNpcByName("Orlan")));
            Player.Dialogs.Add(new PlayerDialogModel(GetNpcByName("Servant")));
            Player.Dialogs.Add(new PlayerDialogModel(GetNpcByName("Skeleton")));

            #endregion

            #region PlayerDialogLines

            GetPlayerDialogModelByDialogPartnerName("Orlan").DialogLines.Add(new PlayerDialogLine("Hello. I need something to drink but I don't have money...", 0, null, 1, 1));
            GetPlayerDialogModelByDialogPartnerName("Orlan").DialogLines.Add(new PlayerDialogLine("Good day Sir, I would like some booze.", 0, null, 2, 2));
            GetPlayerDialogModelByDialogPartnerName("Orlan").DialogLines.Add(new PlayerDialogLine("No.", 2, null, 1, 1));

            GetPlayerDialogModelByDialogPartnerName("Skeleton").DialogLines.Add(new PlayerDialogLine("Hello, my name is Geronimo Röder, I'm living here.", 0, null, 1, 1));
            GetPlayerDialogModelByDialogPartnerName("Skeleton").DialogLines.Add(new PlayerDialogLine("I'm so thirsty I'm literally dying...please give me some booze.", 0, null, 2, 2));
            
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("Hello my friend, you are looking worried, can I help you somehow?", 0, null, 1, 1));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("Oh look, it's one of these totally useless background characters again...", 0, null, 2, 2));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("Here, your ring - take it back! (Give Ring to Servant)", 1, GetItemByName("Ring"), 1, 5));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("Then give me the damn key you moron!", 2, null, 1, 3));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("Where exactly did you lose it?", 3, null, 1, 4));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("I got your damn key - take it! (Give Ring to Servant)", 3, GetItemByName("Ring"), 2, 6));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("I got your damn key - take it! I found it on the Graveyard. (Give Ring to Servant)", 4, GetItemByName("Ring"), 1, 6));

            #endregion
        }

        #endregion

        #region Helper

        private static Room GetRoomByName(string name)
        {
            return _rooms.Find(x => x.Name.ToLower() == name.ToLower());
        }

        private static Item GetItemByName(string name)
        {
            return _items.Find(x => x.Name.ToLower() == name.ToLower());
        }

        private static Crate GetCrateByName(string name)
        {
            return (Crate) _items.Find(x => x.Name.ToLower() == name.ToLower());
        }

        private static Npc GetNpcByName(string name)
        {
            return _npcs.Find(x => x.Name.ToLower() == name.ToLower());
        }

        private static PlayerDialogModel GetPlayerDialogModelByDialogPartnerName(string name)
        {
            return Player.Dialogs.Find(x => x.DialogPartner.Name.ToLower() == name.ToLower());
        }

        #endregion

        #region GUIHelper

        public static void ConsoleWriteGreen (string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteBlue (string input)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteRed (string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteDarkYellow (string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
