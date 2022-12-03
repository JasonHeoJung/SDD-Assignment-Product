using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        ChooseBuilding();
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
    }
}
