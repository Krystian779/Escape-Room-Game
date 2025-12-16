using NAudio.Dmo;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

using System.Threading.Tasks;

namespace EscapeRoom
{
    public class Game
    {
        private Player player;
        private List<Room> rooms = new List<Room>();
        private Room currentRoom;
        private bool gameOver;
        private BlackJack blackjack;
        private bool details = true;
        public Game()
        {
            blackjack = new BlackJack(this);
            CreateRooms();
            currentRoom = ChooseRoom();
            player = new Player(3, currentRoom, gameOver);
        }

        // This method is for handling the change in rooms and the first bit of dialogue
        public void Start()
        {
            PrintWithEffect("The Escape Room");
            Console.Clear();
            PrintWithEffect("\nYou wake Up");
            PrintWithEffect("Dizzy and disorientated");
            PrintWithEffect("The walls close in on you as you look around");
            PrintWithEffect("You see a half opened door infront of you");
            PrintWithEffect("You move closer");
            PrintWithEffect("You open it...\n");


            while (!gameOver)
            {
                while (!currentRoom.PuzzleSovled && player.Health > 0)
                {
                    RunGame();
                    // RoomDetails();
                }

                if (player.Health <= 0)
                {
                    Console.Clear();
                    PrintWithEffectRed("Game Over");
                    return;
                }

                if (rooms.All(r => r.PuzzleSovled))
                {
                    Console.Clear();
                    PrintWithEffectGreen("You Win!!!");
                    return;
                }

                // Move to next room
                currentRoom = ChooseRoom();
                details = true;
                Console.Clear();
            }
        }

        // Main Program for the game to Run
        public void RunGame()
        {

            if (currentRoom.PuzzleSovled)
            {
                return; // Exit immediately
            }
            else if (details == true)
            {
                RoomDetails();
                details = false;
            }


            if (currentRoom == rooms[0])
                PrintWithEffect("\n1 - Inspect the table of books and papers");
            else if (currentRoom == rooms[1])
                PrintWithEffect("\n1 - Inspect the notes on the table");
            else
                PrintWithEffect("\n1 - Pick up Box of Coins");

            GeneralCommands();

            int input;
            if (int.TryParse(Console.ReadLine(), out input))
            {
                switch (input)
                {
                    case 1:
                        InspectRoom();
                        break;
                    case 2:
                        if (currentRoom == rooms[0])
                            InteractRoom1();
                        else if (currentRoom == rooms[1])
                            InteractRoom2();
                        else
                            InteractRoom3();
                        break;
                    case 3:
                        RoomDetails();
                        break;
                    case 4:
                        OpenInventory();
                        break;
                    case 5:
                        PrintWithEffect("Quitting Game...");
                        // Syntax to Exit the Program --- New to me :)
                        Environment.Exit(0);
                        break;
                    default:
                        PrintWithEffect("\nInvalid Input");
                        PrintWithEffect("Please enter a Number (1 - 5)");
                        break;
                }
            }
            else
            {
                PrintWithEffect("\nInvalid Input");
                PrintWithEffect("Please enter a Number (1 - 5)");
            }
        }

        #region Methods
        private void CreateRooms()
        {
            rooms.Clear();
            rooms.Add(new Room("Code Room", "A poorly lit room overgrown in cobwebs with an out of place futuristic number buttonpannel... You see a booklet with a clue", "You look around and see a messy table with lots of papers what looks like books."));
            rooms[0].Items.Add(new Item("Picked up Old Booklet", "I’m four numbers: the number of continents, the days in a week, the planets in our solar system, and the seasons in a year — written in that order."));

            rooms.Add(new Room("Castle of Knowledge", "An old library flourishing with knowledge along with a pile of old notes on a table. Looks Like theres a clue...", "You scan the area and spot one sheet with something written on it"));
            rooms[1].Items.Add(new Item("Picked up Clue to Room 2", "I speak in riddles: forward I am heavy, but backward I am not. What am I?"));

            rooms.Add(new Room("The Card Game", "A fancy room and a mysterious man standing at a table with a box and deck of cards.", "It looks like he wants me to sit down and play?"));
            rooms[2].Items.Add(new Item("Picked up a Box of Coins", "There are 500 Coins in this box"));
        }
        public void PrintWithEffect(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in text)
            {
                Console.Write(c);
                //PlayAudio();
                Thread.Sleep(20);
            }

            Console.WriteLine("", Console.ForegroundColor);
            Thread.Sleep(900);
        }
        public void PrintWithEffectGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (char c in text)
            {
                Console.Write(c);
                //PlayAudio();
                Thread.Sleep(20);
            }

            Console.WriteLine("", Console.ForegroundColor);
            Thread.Sleep(900);
        }
        public void PrintWithEffectRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char c in text)
            {
                Console.Write(c);
                //PlayAudio();
                Thread.Sleep(20);
            }

            Console.WriteLine("", Console.ForegroundColor);
            Thread.Sleep(900);
        }
        private void RoomDetails()
        {
            PrintWithEffect($"{currentRoom.Name}\n");
            PrintWithEffect($"{currentRoom.Description}");
            PrintWithEffect($"{currentRoom.Inspect}");
        }
        private void OpenInventory()
        {
            if (player.Inventory.Count() == 0)
            {
                PrintWithEffect("You have nothing in your inventory");
            }
            else

                foreach (Item item in player.Inventory)
                {
                    PrintWithEffect(item.Name);
                }
        }
        private Room ChooseRoom()
        {
            currentRoom = null;
            Random rand = new Random();


            while (currentRoom == null)
            {
                int random = rand.Next(1, 4);
                switch (random)
                {
                    case 1:
                        if (rooms[0].Picked == false)
                            currentRoom = rooms[0];
                        break;
                    case 2:
                        if (rooms[1].Picked == false)
                            currentRoom = rooms[1];
                        break;
                    case 3:
                        if (rooms[2].Picked == false)
                            currentRoom = rooms[2];
                        break;
                }
            }

            currentRoom.Picked = true;
            return currentRoom;
        }
        private void InspectRoom()
        {

            player.AddToInventory(currentRoom.Items[0]);
            PrintWithEffect($"{currentRoom.Items[0].Name}");
            PrintWithEffect($"{currentRoom.Items[0].Description}");
        }
        private void InteractRoom1()
        {
            PrintWithEffect("Enter Code:\n");

            int answer = 7784;

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == answer)
                {
                    currentRoom.PuzzleSovled = true;
                    PrintWithEffectGreen("\nCorrect! The door unlocks...");
                }
                else
                {
                    player.LoseHealth();
                    PrintWithEffectRed($"\nWrong! You have {player.Health} lives left!");

                }
            }
            else
            {
                PrintWithEffect("\nInvalid input. Please enter a number.");
            }

        }
        private void InteractRoom2()
        {
            PrintWithEffect("Enter Answer:\n");
            string answer = "ton";
            string input = Console.ReadLine().ToLower();

            if (input == answer)
            {
                currentRoom.PuzzleSovled = true;
                PrintWithEffectGreen("Correct! Moving to the next room...");
            }
            else
            {
                player.LoseHealth();
                PrintWithEffect($"Wrong! You have {player.Health} lives left!");
            }
        }
        private void InteractRoom3()
        {
            blackjack.IntroToBlackJack();
            int coins = 500;

            while (coins < 1000 && player.Health > 0 && currentRoom.PuzzleSovled == false)
            {
                coins = blackjack.PlayBlackJack(coins);
                PrintWithEffect($"Your coins : {coins}");

                if (coins <= 0)
                {
                    Console.Clear();
                    PrintWithEffectRed("Game Over");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
                else if (coins >= 1000)
                {
                    PrintWithEffectGreen("You won the Blackjack! Moving to the next room...");
                    currentRoom.PuzzleSovled = true;

                }
            }


        }
        private void GeneralCommands()
        {
            PrintWithEffect($"2 - Interact");
            PrintWithEffect($"3 - Repeat room details");
            PrintWithEffect($"4 - Open inventory");
            PrintWithEffect($"5 - Quit\n");
        }
        #endregion

        // Try to add sound when printing animation plays, give effect of a computer talking
        // Audio doesnt have enough time to play, at 50ms i cant hear anything

        /*
        private static void PlayAudio()
        {
            using (var audioFile = new AudioFileReader("C:\\Users\\chimm\\source\\repos\\oop-ca2-2025-2026-escaperoom-Krystian779\\EscapeRoom\\text.mp3"))
            {
                using (var outPutDevice = new WaveOutEvent())
                {
                    outPutDevice.Init(audioFile);
                    outPutDevice.Play();
                    Thread.Sleep(50);
                    outPutDevice.Stop();
                }
            }
        }
        */
    }
}