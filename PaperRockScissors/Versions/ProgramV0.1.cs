using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rock_Paper_Scissors
{
    class Program
    {

        static bool title = true;
        static bool gameLoop = true;
        static bool throwLoop = true;
        static string playerName = "";
        static int playerCounter = 0;
        static int cpuCounter = 0;
        static int tieCounter = 0;
        static int playerStat = 0;

        static void Main()
        {
            do
            {
                mainMenu();
                title = false;
            }
            while (gameLoop == true);
        }

        static string mainMenu()
        {
            if (title == true)
            {
                Console.WriteLine("*********************");
                Console.WriteLine("*ROCK PAPER SCISSORS*");
                Console.WriteLine("*********************\n");
            }

            if (playerName == "")
            {
                Console.WriteLine("Please enter you name.");
                playerName = Console.ReadLine();
            }

            Console.WriteLine("\n<N>ew Game - <S>tats - <E>xit");

            bool menuLoop = true;

            while (menuLoop == true)
            {
                string menuPrompt = Console.ReadLine();
                menuPrompt = menuPrompt.ToUpper();

                switch (menuPrompt)
                {
                    case "N":
                        runGame(0);
                        menuLoop = false;
                        break;

                    case "S":
                        Console.WriteLine("Stats are currently unavailable!");
                        //need to create stats method;
                        menuLoop = false;
                        break;

                    case "E":

                        Console.WriteLine("\nThanks for playing " + playerName + "!");
                        gameLoop = false;
                        menuLoop = false;
                        break;

                    default:

                        Console.WriteLine("Invalid, enter <N> for New Game, <S> for Stats, <E> for Exit.");
                        break;

                }
            }
            return null;
        }

        static int runGame(int cpuHand)
        {
            int i = 0;
            int j = 1;
            int playerHand = 0;
            string cpuHandString = "";

            Console.WriteLine("How many hands would you like to play?");

            string handCounterString = Console.ReadLine();

            int handCounter = Int32.Parse(handCounterString);

            if (handCounter != 0)
            {
                do
                {
                    string playerHandString = "";

                    bool throwLoop = true;

                    while (throwLoop == true)
                    {
                        Console.WriteLine("\n\nThrow(" + j + "/" + handCounter + ") - Enter <R>ock - <P>aper - <S>cissors");
                        playerHandString = Console.ReadLine();
                        playerHandString = playerHandString.ToUpper();
                        switch (playerHandString)
                        {

                            case "R":
                                playerHand = 1;
                                playerHandString = "Rock";
                                throwLoop = false;
                                break;

                            case "P":
                                playerHand = 2;
                                playerHandString = "Paper";
                                throwLoop = false;
                                break;

                            case "S":
                                playerHand = 3;
                                playerHandString = "Scissors";
                                throwLoop = false;
                                break;

                            default:
                                Console.WriteLine("Invalid, enter <R> for Rock, <P> for Paper, <S> for Scissors.");
                                break;

                        }

                    }

                    cpuHand = cpuPlayer();

                    //Compare hands

                    if (playerHand == cpuHand)
                    {
                        tieCounter++;
                    }

                    else
                    {
                        if (playerHand == 2 && cpuHand == 1 || playerHand == 1 && cpuHand == 3 || playerHand == 3 && cpuHand == 2)
                        {
                            playerCounter++;
                            playerStat = playerCounter;
                        }

                        else
                        {
                            cpuCounter++;
                        }

                    }
                    
                    switch (cpuHand)
                    {
                        case 1:
                            cpuHandString = "Rock";
                            break;

                        case 2:
                            cpuHandString = "Paper";
                            break;

                        case 3:
                            cpuHandString = "Scissors";
                            break;

                        default:
                            break;
                    }

                    Console.WriteLine(playerName + " plays: " + playerHandString + " - CPU player plays: " + cpuHandString);
                    Console.WriteLine(playerName + " won: (" + playerCounter + "/" + handCounter + ") -" + " CPU player won: (" + cpuCounter + "/" + handCounter + ") -" + " Tied: (" + tieCounter + "/" + handCounter + ")");
                    i++;
                    j++;
                }

                while (i < handCounter);

                if (playerCounter == cpuCounter)
                {
                    Console.WriteLine(playerName + " ties with CPU player, try again!");
                }

                if (playerCounter > cpuCounter)
                {
                    Console.WriteLine(playerName + " wins!");
                }

                if (playerCounter < cpuCounter)
                {
                    Console.WriteLine("CPU player wins!");
                }

                stats();//run the stats method...I think it should go here
                playerCounter = 0;
                cpuCounter = 0;
                tieCounter = 0;
                playerStat = 0;
            }

            else
            {
                Console.WriteLine("Exiting to the Main Menu.");
            }

            return playerHand;
        }
        
        static int cpuPlayer()
        {
            Random cpuChoice = new Random();
            int cpuHand = cpuChoice.Next(1, 4);
            return cpuHand;
        }

        static void stats()
        {
            StreamWriter open;
            open = new StreamWriter(playerName + ".txt");
            int temp = playerStat;
            open.WriteLine(temp);
            open.Close();
            
        }


    }

}

