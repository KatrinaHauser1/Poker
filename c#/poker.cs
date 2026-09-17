
using System;
using System.IO;


public class poker {

        public static void Main(string[] args) {
                cards myCards = new cards("", 0, ""); 
                printIntro(args);

                cards[] activeHand;
                if (args.Length == 0) {
                        myCards.deliverStackandHand();
                        activeHand = myCards.getMyHand();
                }
                else {
                        activeHand = testDeck(args);
                }
                myCards.evaluateHand(activeHand);
        }


        public static void printIntro(string[] args){
                Console.WriteLine("✦✦✦✦✦ POKER ✦✦✦✦✦ HAND ✦✦✦✦✦ ANALYZER ✦✦✦✦✦");
                if (args.Length == 0) { 
                        Console.WriteLine("\n✦✦✦✦✦ USING ✦✦✦✦ RANDOMIZED ✦✦✦✦ DECK ✦✦✦✦✦");
                }

                else { 
                        Console.WriteLine("✦✦✦✦✦✦ File - " + args[0] + " ✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
                        Console.WriteLine("\n✦✦✦✦✦ USING ✦✦✦✦✦ TEST ✦✦✦✦✦✦✦ DECK ✦✦✦✦✦"); 
                }
        }


        public static cards[] testDeck(string[] args) {

                cards[] testHand = new cards[7];

                string testFile = args[0];
                if (File.Exists(testFile)) {
	

                        string[] teststrings = new string[0];

                        string line = File.ReadAllText(testFile).TrimEnd('\r', '\n');
                        teststrings = line.Split(",");

                        

                        if (teststrings.Length != 7) {
                                Console.WriteLine("\nError: File contains too many/not enough cards");
                                Environment.Exit(0);
                        }

                        for (int i = 0; i < teststrings.Length; i++) {
                                if (teststrings[i].Length != 3) {
                                        Console.WriteLine("\nError: Incorrect Card Format");
                                        Environment.Exit(0);
                                }
                                        int value = 0;
                                        switch (teststrings[i].Substring(0,2)){
                                                case " J":
                                                        value = 11;
                                                        break;
                                                case " Q":
                                                        value = 12;
                                                        break;
                                                case " K":
                                                        value = 13;
                                                        break;
                                                case " A":
                                                        value = 14;
                                                        break;
                                                case "10":
                                                        value = 10;
                                                        break;
                                                default:
                                                        value = int.Parse(teststrings[i].Substring(1,1));
                                                        break;
                                        }
                                testHand[i] = new cards(teststrings[i], value, teststrings[i].Substring(2));

                        }

                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦ Your Hand: ✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
                for (int h = 0; h< 7;h++) {
                          Console.Write(testHand[h].card + " ");
                }
                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");


                cards[] tempHand = testHand;
                Array.Sort(tempHand);
                for (int k = 0; k < tempHand.Length-1; k++) {
                        if ((tempHand[k].card) == tempHand[k+1].card) {
                                Console.WriteLine("\nError: Duplicate found in Hand");
                                Console.WriteLine("DUPLICATE: " + tempHand[k].card);
                                Environment.Exit(0);
                        }
                }

                }

                else {
                        Console.WriteLine("\nError: File not found");
                        System.Environment.Exit(0); 
                }
                 return testHand;
        }

}
