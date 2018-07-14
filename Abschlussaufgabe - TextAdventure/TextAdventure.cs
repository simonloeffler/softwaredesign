using System;
using System.Collections.Generic;
using System.Linq;

namespace Abschlussaufgabe___TextAdventure
{
    static class TextAdventure
    {
        public static Player Player {get; private set;}
        public static Item WinningItem {get; private set;}
        public static List<Room> Rooms {get; private set;} = new List<Room>();
        public static List<Item> Items {get; private set;} = new List<Item>();
        public static List<NPC> NPCs {get; private set;} = new List<NPC>();
        public static bool IsFinished {get; set;} = false;
        
        static void Main(string[] args)
        {
            LoadGameData();
            ConsoleWriteRed(Environment.NewLine + "Welcome to The Fantastic Adventure!" + Environment.NewLine);
            Console.WriteLine("You hit the ground really hard. When getting your head up and checking the situation you acknowledge that you are in a yard in front of a tavern.");
            Console.WriteLine("The carriage behind you is driving away while the carrige driver screams: 'I alredy took the money for taking you with us out of your pockets!'");
            Console.WriteLine("And just in this moment you realise that your throat is really dry and you're very thirsty. You should find some booze quickly.");

            for (;;)
            {
                if(Player.Inventory.Contains(WinningItem))
                {
                    ConsoleWriteGreen("You got the " + WinningItem.Name + " and thus you won the game!");
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

        public static void LoadGameData ()
        {

            #region Items

            Items.Add(new Item("Cryptkey", "It's a really old key shaped like a bone.", true));
            Items.Add(new Item("Cratekey", "Just an ordinary key.", true));

            Items.Add(new Item("Stone", "And the winner is Rocky!", true));
            Items.Add(new Item("Ring", "It's the servants ring!", true));
            Items.Add(new Item("Well", "Well, well, well. What do we have here?", false));
            Items.Add(new Item("Gravestone", "On the Gravestone it says: 'R.I.P Smitty Werbenjagermanjensen - he was the real #1'", false));
            Items.Add(new Item("Cross", "On the Cross it says: 'Grave of an unknwon hero - fallen while hunting wild hogs.'", false));
            Items.Add(new Item("Grave", "On the Grave it says: 'Arouse me from sleep on the 30th September.'", false));
            Items.Add(new Item("Mirror", "A bored looking face in front of a computer.", false));
            Items.Add(new Item("Barrel", "Not a single drop inside...", false));

            Items.Add(new Weapon("Stick", "It's a bit sticky.", true, 1));
            Items.Add(new Weapon("Hammer", "Not very dangerous...", true, 5));
            Items.Add(new Weapon("Shovel", "It´s a heavy shovel with a long shaft.", true, 8));
            Items.Add(new Weapon("Dagger", "A nice dagger with some ornaments on the handle.", true, 11));
            Items.Add(new Weapon("Sword", "An old, rusty sword.", true, 15));

            Items.Add(new Potion("Potion", "Smells sweet.", true, 50));
            Items.Add(new Potion("Apple", "It's a bit crumpled.", true, 15));
            Items.Add(new Potion("Bread", "Not fresh but still eatable.", true, 20));
            Items.Add(new Potion("Booze", "Just perfect.", true, 100));

            Items.Add(new Crate("Crate", "A big, heavy crate. Seems like you need a key to open it.", false, GetItemByName("Cratekey")));

            WinningItem = GetItemByName("Booze");

            #endregion

            #region Rooms

            Rooms.Add(new Room("Yard", "It's a big yard with a hard ground.", null));
            Rooms.Add(new Room("Tavern", "A cozy tavern. Unfortunately you have no money.", null));
            Rooms.Add(new Room("Stables", "Small Stables with no horses at all.", null));
            Rooms.Add(new Room("Graveyard", "A spooky graveyard with some old gravestones.", null));
            Rooms.Add(new Room("Crypt", "It's really dark in here. You can barely see a thing.", GetItemByName("Cryptkey")));
            Rooms.Add(new Room("Cellar", "A cold stone cellar with all kinds of stuff. Maybe you can find something to ease your thirst.", null));

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

            #region NPCs

            NPCs.Add(new NPC("Servant", "He looks bored and somehow a bit worried.", 100, 15, false, false, true));
            NPCs.Add(new NPC("Orlan", "Standing behind the bar it seems like he is the owner of the tavern.", 100, 20, true, false, true));
            NPCs.Add(new NPC("Rat", "A nasty, big rat.", 60, 5, false, true, false));
            NPCs.Add(new NPC("Skeleton", "Maybe he should've drunken some more milk.", 90, 8, true, true, true));

            #endregion

            #region AddItemsToInventories / Crates

            Player.Inventory.Add(GetItemByName("Stone"));
            
            GetCrateByName("Crate").Inventory.Add(GetItemByName("Booze"));

            GetNPCByName("Servant").Inventory.Add(GetItemByName("Cryptkey"));
            GetNPCByName("Skeleton").Inventory.Add(GetItemByName("Sword"));

            #endregion

            #region AddNPCsToRooms

            GetRoomByName("Stables").NPCs.Add(GetNPCByName("Servant"));
            GetRoomByName("Tavern").NPCs.Add(GetNPCByName("Orlan"));
            GetRoomByName("Cellar").NPCs.Add(GetNPCByName("Rat"));
            GetRoomByName("Crypt").NPCs.Add(GetNPCByName("Skeleton"));

            #endregion

            #region NPCDialogLines

            GetNPCByName("Orlan").DialogLines.Add(new CreatureDialogLine("Hello my friend!", 0, null));
            GetNPCByName("Orlan").DialogLines.Add(new CreatureDialogLine("Get out here you needy vagrant!", 1, null));
            GetNPCByName("Orlan").DialogLines.Add(new CreatureDialogLine("Do you have money to pay for it?", 2, null));

            GetNPCByName("Skeleton").DialogLines.Add(new CreatureDialogLine("Who dares to disturb my rest?", 0, null));
            GetNPCByName("Skeleton").DialogLines.Add(new CreatureDialogLine("You won't hoax me you fool! I will now kill you!", 1, null));
            GetNPCByName("Skeleton").DialogLines.Add(new CreatureDialogLine("If you are dying anyway I could also have a little fun with you! Let's fight!", 2, null));

            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("Hello...", 0, null));
            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("Well, actually you can, yes. I lost my engagement ring on the graveyard...please bring it back.", 1, null));
            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("You think I'm a minor background actor? I have to tell you that I have the key to a crypt wich is important for your progress!", 2, null));
            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("Only if you get me my ring I lost somewhere nearby!", 3, null));
            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("I don't exactly know...it has to be somwhere around.", 4, null));
            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("Thank you very much. Here, take this key as a reward. (He hands you over a key)", 5, GetItemByName("Cryptkey")));
            GetNPCByName("Servant").DialogLines.Add(new CreatureDialogLine("Ok. Here, take the key. (He hands you over a key)", 6, GetItemByName("Cryptkey")));

            #endregion

            #region PlayerDialogModels

            Player.Dialogs.Add(new PlayerDialogModel(GetNPCByName("Orlan")));
            Player.Dialogs.Add(new PlayerDialogModel(GetNPCByName("Servant")));
            Player.Dialogs.Add(new PlayerDialogModel(GetNPCByName("Skeleton")));

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
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("Where exactly did you lost it?", 3, null, 1, 4));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("I got your damn key - take it! (Give Ring to Servant)", 3, GetItemByName("Ring"), 2, 6));
            GetPlayerDialogModelByDialogPartnerName("Servant").DialogLines.Add(new PlayerDialogLine("I got your damn key - take it! I found it on the Graveyard. (Give Ring to Servant)", 4, GetItemByName("Ring"), 1, 6));

            #endregion
        }

        #endregion

        #region Helper

        public static Room GetRoomByName(string name)
        {
            return Rooms.Find(x => x.Name.ToLower() == name.ToLower());
        }

        public static Item GetItemByName(string name)
        {
            return Items.Find(x => x.Name.ToLower() == name.ToLower());
        }

        public static Crate GetCrateByName(string name)
        {
            return (Crate) Items.Find(x => x.Name.ToLower() == name.ToLower());
        }

        public static NPC GetNPCByName(string name)
        {
            return NPCs.Find(x => x.Name.ToLower() == name.ToLower());
        }

        public static PlayerDialogModel GetPlayerDialogModelByDialogPartnerName(string name)
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
