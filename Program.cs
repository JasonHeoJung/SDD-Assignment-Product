using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

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

            List<string> allBuilding = new List<string>() { "R", "I", "C", "O", "*" };
            List<string> randomBuilding = new List<string>();
            List<string> buildFull = new List<string>() { "Residential", "Industry", "Commercial", "Park", "Road" };
            Random rnd = new Random();

            Console.WriteLine("=========================");
            // Get Two random buildings for options
            for (int i = 0; i < 2; i++)
            {
                int num = rnd.Next(allBuilding.Count);
                randomBuilding.Add(allBuilding[num]);
                Console.WriteLine("[{0}] {1}", i+1, buildFull[num]);
                allBuilding.RemoveAt(num);
                buildFull.RemoveAt(num);
            }

            OtherOptions();

            while (true)
            {
                // Return building chose by player
                Console.Write("Choose Building to place: ");
                string choice = Console.ReadLine().Trim();

                if (choice == "1" || choice == "2")
                {
                    int index = Convert.ToInt32(choice);
                    return randomBuilding[index - 1];
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice\n");
                }
            }

        }

        static void PlaceBuilding(List<List<string>> map, string building)
        {
            List<string> allBuilding = new List<string>() { "R", "I", "C", "O", "*" };
            while (true)
            {
                // Turn written location into a coordinate
                Console.Write("Choose a location: ");

                // Get location
                string buildLoc = Console.ReadLine().Trim().ToUpper();
                string gridY = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                int y = 20; // Y coordinate of the map
                int x = 20; // X coordinate of the map

                // Getting the Y coordinate
                for (int i = 0; i < gridY.Length; i++)
                {
                    if (buildLoc[0] == gridY[i])
                    {
                        y = i;
                    }
                }

                // Getting the X coordinate
                x = Convert.ToInt32(Regex.Match(buildLoc, @"\d+").Value);

                if ( 0 <= x && x < 20 && 0 <= y && y < 20)
                {
                    //  if map is empty, can place anywhere
                    if (map.Count == 0)
                    {
                        map[y][x] = building;
                        break;
                    }
                    // map is not empty, check for adjacent buildings
                    else
                    {
                        List<string> check = new List<string>();
                        check.Add(map[y + 1][x]);
                        check.Add(map[y - 1][x]);
                        check.Add(map[y][x + 1]);
                        check.Add(map[y][x - 1]);
                        // location selected is valid
                        if (check.Any(item => allBuilding.Contains(item)))
                        {
                            map[y][x] = building;
                            break;
                        }
                        // location selected is not valid
                        else
                        {
                            Console.WriteLine("New Building must be placed next to an existing building!");
                        }
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please enter a valid location");
                }
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
            string letterGrid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Console.Write("    ");
            for (int i = 0; i < 20; i++)
            {
                Console.Write(" " + letterGrid[i] + "  ");
            }
            Console.WriteLine();
            Console.Write("   ");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");
            for (int i = 0; i < 20; i++)
            {
                if (i >= 9)
                {
                    Console.Write((i + 1) + " ");
                }
                else
                {
                    Console.Write((i + 1) + "  ");
                }
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
                Console.Write("   ");
                for (int n = 0; n < 20; n++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");
            }
        }

        static void OtherOptions()
        {
            Console.WriteLine("Other options: ");
            Console.WriteLine("[3] See Current Scores");
            Console.WriteLine("[4] Return to Main Menu");
            Console.WriteLine("Please enter your option: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 3)
            {

            }

            if (choice == 4)
            {
                DisplayMenu();
            }
        }
        static int IndustryPoints(List<List<string>> map)
        {
            int IndustryPoints = 0;
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (map[x][y]=="I")
                    {
                        IndustryPoints++;
                        for (int x = 0; x < 20; x++)
                        {
                            for (int y = 0; y < 20; y++)
                            {
                                if (map[x][y] == "I")
                                {
                                    IndustryPoints++;
                                    if (map[x])
                                }
                            }
                        }
                    }
                }
            }
            return IndustryPoints
        }
        static int IndustryCoins(List<List<string>> map)
        {
            int IndustryCoins = 0;
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (map[x][y] == "I")
                    {
                        if (map[x][y-1]=="R")
                        {
                            IndustryCoins++;
                        }
                        else if (map[x - 1][y] == "R")
                        {
                            IndustryCoins++;
                        }
                        else if (map[x][y + 1][ == "R")
                        {
                            IndustryCoins++;
                        }
                        else if (map[x + 1][y] == "R")
                        {
                            IndustryCoins++;
                        }
                    }
                }
            }
            return IndustryCoins;
        }
    }
}
