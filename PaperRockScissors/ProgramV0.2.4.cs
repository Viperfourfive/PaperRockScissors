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
        static int playerWin = 0; 
        static int playerLoss = 0;
        static int playerTie = 0;


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
                        readStats(); 
                        menuLoop = false;
                        break;

                    case "E":

                        Console.WriteLine("\nThanks for playing " + playerName + "!");
                        Console.WriteLine("Press any key to exit.");
                        menuPrompt = Console.ReadLine();
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
            string savePrompt = "";

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
                        playerTie = tieCounter; //takes the stat for the stat() method
                    }

                    else
                    {
                        if (playerHand == 2 && cpuHand == 1 || playerHand == 1 && cpuHand == 3 || playerHand == 3 && cpuHand == 2)
                        {
                            playerCounter++;
                            playerWin = playerCounter; //takes the stat for the stat() method
                        }

                        else
                        {
                            cpuCounter++;
                            playerLoss = cpuCounter; //takes the stat for the stat() method
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

                //ask user if stats are to be saved
                Console.WriteLine("Would you like your save your games score? Enter <Y/N>");
                savePrompt = Console.ReadLine();
                savePrompt = savePrompt.ToUpper();
                if (savePrompt == "Y")
                {
                    writeStats();
                    Console.WriteLine("Game Score Saved!");
                }
                else
                {
                    Console.WriteLine("Your games score will not be saved!");
                }
                
                playerCounter = 0;
                cpuCounter = 0;
                tieCounter = 0;
                playerWin = 0; 
                playerLoss = 0; 
                playerTie = 0; 
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

        static void writeStats() //method for actually creating the stats in the game
        {
            string path1 = @"C:\PRS\Save";
            string path2 = playerName + ".txt";

            if (System.IO.File.Exists(Path.Combine(path1, path2))) //this part of the if statement decides if the player has a txt file already
            {
                int one = 0;
                int two = 0;
                int three = 0;
                using (StreamReader sr = new StreamReader(Path.Combine(path1, path2)))
                {
                    one = Int32.Parse(sr.ReadLine());
                    two = Int32.Parse(sr.ReadLine());
                    three = Int32.Parse(sr.ReadLine());
                }

                File.Delete(Path.Combine(path1, path2));

                using (StreamWriter sw = new StreamWriter(Path.Combine(path1, path2)))
                {
                    int win1 = playerWin;
                    int loss1 = playerLoss;
                    int tie1 = playerTie;

                    sw.WriteLine(one + win1);
                    sw.WriteLine(two + loss1);
                    sw.WriteLine(three + tie1);
                }
            }

            else //if the player doesn't have a txt file it creates one here
            {
                DirectoryInfo di = Directory.CreateDirectory(path1); //creates directory if it doesn't exist                
                StreamWriter open;
                open = new StreamWriter(@"c:\PRS\Save\" + playerName + ".txt"); //new pathing for release.
                //open = new StreamWriter(playerName + ".txt"); //old used during development
                int win = playerWin;
                int loss = playerLoss;
                int tie = playerTie;
                open.WriteLine(win);
                open.WriteLine(loss);
                open.WriteLine(tie);
                open.Close();
            }
        }

        static void readStats() //this method reads the stats for the stat call or "S" in the main menu of the game
        {

            string path1 = @"C:\PRS\Save";
            string path2 = playerName + ".txt";

            if (System.IO.File.Exists(Path.Combine(path1, path2)))
            {
                int one = 0;
                int two = 0;
                int three = 0;

                using (StreamReader sr = new StreamReader(Path.Combine(path1, path2)))
                {
                    one = Int32.Parse(sr.ReadLine());
                    two = Int32.Parse(sr.ReadLine());
                    three = Int32.Parse(sr.ReadLine());
                }

                Console.WriteLine("\nYour Record Is:\n");
                Console.WriteLine("Wins:     " + one);
                Console.WriteLine("Losses:   " + two);
                Console.WriteLine("Ties:     " + three);
            }
            else
            {
                Console.WriteLine("\nYou don't have any stats to show yet.  Get playing!!!");
            }

        }
    }

}
