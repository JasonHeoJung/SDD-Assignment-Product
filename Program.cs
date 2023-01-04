using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sampleScores = new List<int> { 56, 54, 53, 52, 51, 50, 49, 48, 47, 45, 40 };
            List<string> sampleNames = new List<string>() { "These", "Are", "Some", "Of", "The", "Sample", "Scores", "I","Have", "Chosen","Top 10 displayed only"};

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
                        newGame(sampleNames, sampleScores);
                    }
                    else if (choice == 2)
                    {
                        LoadGame(sampleNames,sampleScores);
                    }
                    else if (choice == 3)
                    {
                        displayLeaderboard(sampleNames, sampleScores);
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
        static void newGame(List<string> sampleNames, List<int> sampleScores)
        {
            List<List<string>> map_List = new List<List<string>>();
            int Coin = 16;
            while (Coin != 0)
            {
                DisplayMap(map_List, true);
                Console.WriteLine("=========================");
                Console.WriteLine("Coins: " + Coin);
                string b = ChooseBuilding(map_List, Coin);
                if (b == "Exit")
                {
                    break;
                }
                PlaceBuilding(map_List, b, 16 - Coin);
                Coin--;
            }

            if(Coin ==0)
            {
                newHighScore(sampleNames, sampleScores, map_List);
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

        static string ChooseBuilding(List<List<string>> map, int Coin)
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
                Console.WriteLine("[{0}] {1}", i + 1, buildFull[num]);
                allBuilding.RemoveAt(num);
                buildFull.RemoveAt(num);
            }
            Console.WriteLine("Other options: ");
            Console.WriteLine("[3] See Current Scores");
            Console.WriteLine("[4] Save Current Game");
            Console.WriteLine("[5] Return to Main Menu");

            while (true)
            {
                // Return building chose by player
                Console.Write("Enter Option: ");
                string choice = Console.ReadLine().Trim();

                if (choice == "1" || choice == "2")
                {
                    int index = Convert.ToInt32(choice);
                    return randomBuilding[index - 1];
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Current score: " + CurrentTotalScore(map));
                }
                else if (choice == "4")
                {
                    SaveGame(map, Coin);
                }
                else if (choice == "5")
                {
                    return "Exit";
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice\n");
                }
            }
        }

        static void PlaceBuilding(List<List<string>> map, string building, int bcount)
        {
            List<string> allBuilding = new List<string>() { "R", "I", "C", "O", "*" };
            while (true)
            {
                // Turn written location into a coordinate
                Console.Write("Choose a location: ");

                // Get location
                string buildLoc = Console.ReadLine().Trim().ToUpper();
                string gridX = "ABCDEFGHIJKLMNOPQRST";

                int y = 20; // Y coordinate of the map
                int x = 20; // X coordinate of the map

                // Getting the Y coordinate
                for (int i = 0; i < gridX.Length; i++)
                {
                    if (buildLoc[0] == gridX[i])
                    {
                        x = i;
                    }
                }

                // Getting the X coordinate
                y = Convert.ToInt32(Regex.Match(buildLoc, @"\d+").Value);
                y -= 1;

                //Console.WriteLine("X: {0} Y: {1}", x, y);
                if (0 <= x && x < 20 && 0 <= y && y < 20)
                {
                    //  if map is empty, can place anywhere
                    if (bcount == 0)
                    {
                        map[y][x] = building;
                        break;
                    }

                    // map is not empty, check for adjacent buildings
                    else
                    {
                        List<string> check = new List<string>();
                        if (y != 19)
                        {
                            check.Add(map[y + 1][x]);
                        }

                        if (y != 0)
                        {
                            check.Add(map[y - 1][x]);
                        }

                        if (x != 19)
                        {
                            check.Add(map[y][x + 1]);
                        }

                        if (x != 0)
                        {
                            check.Add(map[y][x - 1]);
                        }


                        // check if current location has a building 
                        if (allBuilding.Contains(map[y][x]))
                        {
                            Console.WriteLine("New Building cannot be placed on an existing building!");
                        }
                        // location selected is valid
                        else if (check.Any(item => allBuilding.Contains(item)))
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
            //if (map.Count > 0)
            //{
            //    check = false;
            //}
            //Generates a new empty 20x20 grid if new game is generated
            if (check)
            {
                for (int i = 0; i < 20; i++)
                {
                    map.Add(new List<string> { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
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
                    if (map[i][n].Length > 0)
                    {
                        Console.Write("| " + map[i][n].ToString() + " ");
                    }
                    else
                    {
                        Console.Write("|   ");
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

        /*static void OtherOptions(List<List<string>> map)
        {
            Console.WriteLine("Other options: ");
            Console.WriteLine("[3] See Current Scores");
            Console.WriteLine("[4] Save Current Game");
            Console.WriteLine("[5] Return to Main Menu");
            Console.Write("Please enter your option (1 or 2 to place a building, 3 to see high scores, 4 to save current game, or 5 to return to main menu respectively): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 3)
            {
                Console.WriteLine("Current score: " + CurrentTotalScore(map));
            }
            if (choice == 4)
            {
                SaveGame(map, Coin);
            }
            if (choice == 5)
            {
                DisplayMenu();
            }
        }*/

        // Calculate Points from all Residential Buildings on the map
        static int ResidentialPoints(List<List<string>> map)
        {
            int residentialPoints = 0;

            // loop through the map to find each Residential Building
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (map[y][x] == "R")
                    {
                        int currentResP = 0;

                        //Create a List of all the buildings adjacent to the Residential Building
                        List<string> check = new List<string>();
                        if (y != 19)
                        {
                            check.Add(map[y + 1][x]);
                        }

                        if (y != 0)
                        {
                            check.Add(map[y - 1][x]);
                        }

                        if (x != 19)
                        {
                            check.Add(map[y][x + 1]);
                        }

                        if (x != 0)
                        {
                            check.Add(map[y][x - 1]);
                        }

                        // Calculate points for the residential building
                        foreach (string b in check)
                        {
                            if (b == "R" || b == "C")
                            {
                                currentResP += 1;
                            }
                            else if (b == "O")
                            {
                                currentResP += 2;
                            }
                        }

                        if (check.Contains("I"))
                        {
                            currentResP = 1;
                        }

                        //Add to the total points for all residential buildings
                        residentialPoints += currentResP;
                    }
                }
            }
            return residentialPoints;
        }

        static int IndustryPoints(List<List<string>> map)
        {
            int IndustryPoints = 0;
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (map[x][y] == "I")
                    {
                        IndustryPoints++;

                        for (int a = 0; a < 20; a++)
                        {
                            for (int b = 0; b < 20; b++)
                            {
                                if (map[a][b] == "I")
                                {
                                    IndustryPoints++;
                                }
                            }
                        }
                    }
                }
            }
            return IndustryPoints;
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
                        if (map[x][y - 1] == "R")
                        {
                            IndustryCoins++;
                        }
                        else if (map[x - 1][y] == "R")
                        {
                            IndustryCoins++;
                        }
                        else if (map[x][y + 1] == "R")
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

        static int RoadPoints(List<List<string>> map)
        {
            int roadPoints = 0;
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (map[x][y] == "*")
                    {
                        //Checks if there is a road building next to it
                        if (map[x][y + 1] == "*")
                        {
                            if (y == 0)
                            {
                                roadPoints++;
                            }
                            else
                            {
                                //Checks whether there is a road building in the previous index, if there is it means the points is already added and if there isn't it means the point has not yet been added
                                if (map[x][y - 1] != "*" && map[x][y - 1] != "")
                                {
                                    roadPoints++;
                                }
                            }
                        }
                    }
                }
            }
            return roadPoints;
        }

        static int ParkPoints(List<List<string>> map)
        {
            int parkPoints = 0;

            // loop through the map to find each Park
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (map[x][y] == "O")
                    {
                        //Checks if there is another park adjacent to it on the right
                        if (map[x][y + 1] == "O")
                        {
                            parkPoints += 1;
                        }

                        //Checks if there is another park adjacent to it on the left
                        if (map[x][y - 1] == "O")
                        {
                            parkPoints += 1;
                        }

                        //Checks if there is another park above that is adjacent to it
                        if (map[x + 1][y] == "O")
                        {
                            parkPoints += 1;
                        }

                        //Checks if there is another park below that is adjacent to it
                        if (map[x + 1][y] == "O")
                        {
                            parkPoints += 1;
                        }
                    }
                }
            }
            return parkPoints;
        }
        static int CommercialPoints(List<List<string>> map)
        {
            int comPoints = 0;

            // loop through the map to find each Commercial
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (map[x][y] == "C")
                    {
                        if (map[x][y + 1] == "C")
                        {
                            comPoints += 1;
                        }
                        if (map[x][y - 1] == "C")
                        {
                            comPoints += 1;
                        }
                        if (map[x + 1][y] == "C")
                        {
                            comPoints += 1;
                        }
                        if (map[x + 1][y] == "C")
                        {
                            comPoints += 1;
                        }
                    }
                }
            }
            return comPoints;
        }
        static int CommercialCoins(List<List<string>> map)
        {
            int comCoins = 0;

            // loop through the map to find each Commercial
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (map[x][y] == "C")
                    {
                        if (map[x][y + 1] == "R")
                        {
                            comCoins += 1;
                        }
                        if (map[x][y - 1] == "R")
                        {
                            comCoins += 1;
                        }
                        if (map[x + 1][y] == "R")
                        {
                            comCoins += 1;
                        }
                        if (map[x + 1][y] == "R")
                        {
                            comCoins += 1;
                        }
                    }
                }
            }
            return comCoins;
        }

        static int CurrentTotalScore(List<List<string>> map )
        {
            // Sum of points at current position
            return ResidentialPoints(map) + IndustryPoints(map) + RoadPoints(map) + ParkPoints(map) + CommercialPoints(map);

        }

        static void SaveGame(List<List<string>> map, int Coin)
        {
            using (StreamWriter sw = new StreamWriter("SavedGame.txt", false))
            {
                for (int i = 0; i < 20; i++)
                {
                    for (int n = 0; n < 20; n++)
                    {
                        //if (map[i][n] == "")
                        //{
                        //    sw.Write("?, ");
                        //}
                        //else
                        //{
                        //    sw.Write(map[i][n] + ", ");
                        //}
                        sw.Write(map[i][n] + ",");
                    }
                    sw.WriteLine();
                }
                sw.WriteLine(Coin);
            }
            Console.WriteLine("Game Saved!");
        }

        static void LoadGame(List<string> sampleNames, List<int> sampleScores)
        {
            List<List<string>> map = new List<List<string>>();
            int Coin = 0;
            if (File.Exists("SavedGame.txt"))
            {
                using (StreamReader sr = new StreamReader("SavedGame.txt"))
                {
                    string s;
                    int row = 0;
                    for(int n = 0; n < 20; n++)
                    {
                        s = sr.ReadLine();
                        List<string> tempRow = new List<string>();
                        string[] builds = s.Split(',');
                        for (int i = 0; i < 20; i++)
                        {
/*                            if (builds[i] == " ")
                            {
                                tempRow.Add("");
                            }
                            else
                            {
                                tempRow.Add(builds[i]);
                            }*/
                            tempRow.Add(builds[i]);
                        }
                        map.Add(tempRow);
                        row++;
                        //Coin = Convert.ToInt32(builds[20]);
                    }
                    s = sr.ReadLine();
                    Coin = Convert.ToInt32(s);
                }
                Console.WriteLine("Game Loaded!");
                while (Coin != 0)
                {
                    DisplayMap(map, false);
                    Console.WriteLine("=========================");
                    Console.WriteLine("Coins: " + Coin);
                    string b = ChooseBuilding(map, Coin);
                    if (b == "Exit")
                    {
                        break;
                    }
                    PlaceBuilding(map, b, 16 - Coin);
                    Coin--;
                }
            }
            else
            {
                Console.WriteLine("There is currently no game saved. Please start a new game to proceed.");
            }
        if(Coin == 0)
            {
                newHighScore(sampleNames, sampleScores, map);
            }
        }

        static void displayLeaderboard(List<string>sampleNames, List<int>sampleScores)
        {
            int index = 1;
            Console.WriteLine("\n--------- HIGH SCORES ---------\n");
            Console.WriteLine("Pos\tPlayer\t\tScore");
            Console.WriteLine("---\t------\t\t-----");
            foreach( string i in sampleNames) 
            {
                if(index < 11)
                {
                    Console.WriteLine(String.Format("{0}.\t{1}\t\t{2}", index, i, sampleScores[index - 1]));
                    index++;
                }
            }
            Console.WriteLine("-------------------------------");
        }
        
        static void newHighScore(List<string> sampleNames, List<int> sampleScores, List<List<string>> map)
        {
            int totalFinalScore = CurrentTotalScore(map);
            int listSize = sampleScores.Count;

            // add item at specified index
            for (int i =0; i < listSize; i++) 
            {
                if(i < totalFinalScore) 
                {
                    sampleScores.Insert(i, totalFinalScore);
                    Console.Write("Enter a name: ");
                    string name = Console.ReadLine();
                    sampleNames.Add(name);
                }
            }   

            //limiting list to 10 elements (delete all element starting from index 10 onwards)
            if(sampleScores.Count > 10) 
            { 
                sampleScores.RemoveRange(10, sampleScores.Count-10);
                sampleNames.RemoveRange(10, sampleNames.Count - 10);
            }
        }
    }
}
