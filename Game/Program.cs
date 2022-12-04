using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> map_List = new List<List<string>>();
            bool checkNewGame = false;
            int Coin = 0;
            while (true)
            {
                DisplayMenu();
                Console.Write("Please choose an option: ");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice > 4 || choice < 0)
                    {
                        Console.WriteLine("Please enter a valid choice\n");
                        Console.Write("Please enter your option: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                    }

                    if (choice == 0)
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }
                    else if (choice == 1)
                    {
                        newGame(checkNewGame, Coin)
                    }
                    else if (choice == 2)
                    {
                    }
                    else if (choice == 3)
                    {
                    }
                    else if (choice == 4)
                    {
                    }

                }
                catch (FormatException choiceError)
                {
                    Console.WriteLine(choiceError.Message + "\nPlease enter a valid choice\n");
                }
                catch (OverflowException choiceError)
                {
                    Console.WriteLine(choiceError.Message + "\nPlease enter a valid choice\n");
                }
            }
        }
        static void newGame(bool checkNewGame, int Coin)
        {
            checkNewGame = true;
            Coin = 16;
            DisplayMap(map_List, true);
            ChooseBuilding(Coin);
        }
        static void DisplayMenu()
        {

            Console.WriteLine("\n---------------MENU---------------");
            Console.WriteLine("[1] Start New Game");
            Console.WriteLine("[2] Load Saved Game");
            Console.WriteLine("[3] Display High Scores");
            Console.WriteLine("[0] Exit Game");
            Console.WriteLine("----------------------------------\n");

        }

        static string ChooseBuilding(int Coin)
        {
            Console.WriteLine("\n++++++++++++++++++++");
            Console.WriteLine("[1] Residential ");
            Console.WriteLine("[2] Industry ");
            Console.WriteLine("[3] Commercial ");
            Console.WriteLine("[4] Park ");
            Console.WriteLine("[5] Road ");
            Console.WriteLine("++++++++++++++++++++\n");

            while (true)
            {
                Console.Write("Please Choose Building to place: ");
                string choice = Console.ReadLine();
                choice = choice.Trim();
                string[] o = { "1", "2", "3", "4", "5" };
                string[] b = { "R", "I", "C", "O", "*" };

                for (int i = 0; i < o.Length; i++)
                {
                    if (choice == o[i])
                    {
                        return b[i];
                    }
                }

                Console.WriteLine("Please enter a valid choice\n");
            }
            Coin--;
        }
        static void DisplayMap(List<List<string>> map, bool check)
        {
            if (check)
            {
                map.Clear();
                for (int i = 0; i < 20; i++)
                {
                    List<string> rowOfEmptyBuildings = new List<string>();
                    for (int n = 0; n < 20; i++)
                    {
                        rowOfEmptyBuildings.Add("| ");
                    }
                    map.Add(rowOfEmptyBuildings);
                }
            }
            for (int i = 0; i < 20; i++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");
            for (int i = 0; i < 20; i++)
            {
                for (int n = 0; n < 20; n++)
                {
                    Console.Write(map[i][n]);
                }
                Console.WriteLine("|");
                for (int n = 0; n < 20; n++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");
            }
        }
    }
}
