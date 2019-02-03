using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMInd
{
    class Program
    {
        public static int[] Randomiser(int Numbers)
        {
            int randomNumber;
            int[] SecretNumberArray = new int[Numbers];
            Random secretNumber = new Random();// change random 

            for (int i = 0; i < Numbers; i++)

            {
                randomNumber = secretNumber.Next(1, 9);//change 
                SecretNumberArray[i] = randomNumber;



            }


            return SecretNumberArray;
        }
        public static int[] PlayerInputs(int Numbers)
        {
            int inputNumber = 0;
            int[] playerInput = new int[Numbers];
            Console.WriteLine("Input your gueses one by one");
            for (int i = 0; i < Numbers; i++)
            {
                Console.Write((i + 1) + ("."));
                //if (Console.ReadLine() == null || Console.ReadLine() == "" || Console.ReadLine() == "0")
                //{
                //    i--;
                //}
                //else
                {
                    try
                    {
                        inputNumber = Convert.ToInt32(Console.ReadLine());
                        playerInput[i] = inputNumber;
                        if (inputNumber <= 0 || inputNumber > 9)
                        {
                            i--;
                        }
                    }
                    catch (Exception)
                    {
                        i--;
                    }
                }

            }

            return playerInput;
        }

        //public static int BlackResult(int Numbers, int[] SecretArray, int[] PlayerInput)// need to create a match algorithm in order to check specific position and color
        //{
        //    //int rightPosition=0;
        //    int black = 0;
        //    for (int i = 0; i < Numbers; i++)
        //    {
        //        if (SecretArray[i] == PlayerInput[i])
        //        {
        //            // rightPosition = rightPosition + 1;
        //            //  black = rightPosition;
        //            black++;
        //        }
        //        // else if ()

        //    }
        //    Console.WriteLine("Results: {0} Black(s)", black);
        //    return black;
        //}

        public static bool ContainsInArray(int searchParam, int[] searchInArray, int length)
        {
            bool contains = false;
            for (int i = 0; i < length; i++)
            {
                if (searchInArray[i] == searchParam)
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }

        public static string BlackResult(int Numbers, int[] SecretArray, int[] PlayerInput, out int blackNumber)// need to create a match algorithm in order to check specific position and color
        {
            //int rightPosition=0;
            int black = 0, white = 0;
            int[] temp2 = new int[Numbers];
            for (int i = 0; i < Numbers; i++)
            {
                if (ContainsInArray(PlayerInput[i], SecretArray, Numbers))
                {
                    if (!ContainsInArray(PlayerInput[i], temp2, Numbers))
                    {
                        temp2[white] = PlayerInput[i];
                        white++;
                        if (PlayerInput[i] == SecretArray[i])
                        {
                            black++;
                        }
                    }
                }
            }

            white = white - black;
            //Console.WriteLine("Secret "+ String.Join(",",SecretArray));
            //Console.WriteLine("Inputs "+ String.Join(",", PlayerInput));

            string res = "Results: " + black + " Black(s) " + white + " Whites(s)";
            //Console.WriteLine(res);
            blackNumber = black;
            return res;
        }
        //public static List<int> GameHistory(int PlayerInput)// need to create a list
        //{

        //    return inputList;
        //}
        static void Main(string[] args)
        {
            String playerName;
            int numbers = 0;

            Console.WriteLine("Please enter you Name");
            playerName = Console.ReadLine();
            replay:
            Console.WriteLine("{0}, Choose your memory length (min = 3, max = 9)", playerName);
            try
            {
                numbers = Convert.ToInt32(Console.ReadLine());// add if else to check the player's choice
            }
            catch (Exception)
            {
                Console.Clear();
                goto replay;
            }
            int blackNumber = 0;
            int[] randomArray = new int[numbers];
            int[] userInput = new int[numbers];
            int[][] userInputHistory = new int[2][];
            int[][] userInputHistoryLast = new int[2][];
            if (numbers > 0 && numbers <= 9)
            {

                //int[][] GameHistory = new int[numbers][];
                randomArray = Randomiser(numbers);
                bool play = true;
                int historyCount = 0;
                while (play)
                {
                    userInput = PlayerInputs(numbers);
                    string res = BlackResult(numbers, randomArray, userInput, out blackNumber);
                    Console.WriteLine(res);

                    historyCount++;

                    if (historyCount > 1)
                    {
                        userInputHistory = new int[historyCount][];
                        userInputHistory = userInputHistoryLast;
                        userInputHistoryLast = new int[historyCount][];
                        for (int i = 0; i < historyCount - 1; i++)
                        {
                            userInputHistoryLast[i] = userInputHistory[i];
                        }
                        userInputHistoryLast[historyCount - 1] = userInput;
                    }
                    else
                    {
                        userInputHistory[historyCount - 1] = userInput;
                        userInputHistoryLast[historyCount - 1] = userInput;
                    }

                    play = false;
                    if (blackNumber != numbers)
                    {
                        Console.WriteLine(playerName + ", Try Again ? Y/Press Enter (Yes) / N (No)");
                        string p = Console.ReadLine();
                        play = (p != "n" && p != "N");
                    }
                }
                Console.Clear();
                if (blackNumber == numbers)
                {
                    Console.WriteLine("Congrats " + playerName + "! You've got all Correct !");
                }
                for (int i = 0; i < historyCount; i++)
                {
                    string x = "";
                    for (int j = 0; j < numbers; j++)
                    {
                        x = x + userInputHistoryLast[i][j] + ",";
                    }
                    Console.WriteLine("Input {0} = {1} {2}", (i + 1), x, BlackResult(numbers, randomArray, userInputHistoryLast[i], out blackNumber));
                }
                Console.WriteLine(playerName + ", Try Another Length ? Y/Press Enter (Yes) / N (No)");
                string p2 = Console.ReadLine();
                play = (p2 != "n" && p2 != "N");
                if (play)
                {
                    Console.Clear();
                    goto replay;
                }
            }
            else
            {

                Console.WriteLine("Enter only numbers between 1-9");// not working!!! check on it need to deal exception on whole program
            }
            // add code to re-play!!!!!not goto 
            Console.ReadKey();
        }
    }
}