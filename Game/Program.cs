using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nested List to store grid for each row 
            List<List<string>> map_List = new List<List<string>>();
            //Boolean to check if a new game is generated
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
                        Console.WriteLine("\n++++++++++++++++++++");
                        Console.WriteLine("[1] Residential ");
                        Console.WriteLine("[2] Industry ");
                        Console.WriteLine("[3] Commercial ");
                        Console.WriteLine("[4] Park ");
                        Console.WriteLine("[5] Road ");
                        Console.WriteLine("++++++++++++++++++++\n");
                        checkNewGame = true;
                        newGame(map_List);
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
        static void newGame(List<List<string>> map_List)
        {
            int Coin = 16;
            while (Coin != 0)
            {
                DisplayMap(map_List, true);
                ChooseBuilding();
                Coin--;
            }
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

        static string ChooseBuilding()
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
        }
        static void DisplayMap(List<List<string>> map, bool check)
        {
            //Generates a new empty 20x20 grid if new game is generated
            if (check)
            {
                //Empty list containing grid
                map.Clear();
                for (int i = 0; i < 20; i++)
                {
                    map.Add(new List<string> { "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| ", "| " });
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
                    //If length > 2, it indicates there is a building, hence it prints one less space
                    if(map[i][n].Length > 2)
                    {
                        Console.Write(map[i][n].ToString() + " ");
                    }
                    else
                    {
                        Console.Write(map[i][n].ToString() + "  ");
                    }
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
