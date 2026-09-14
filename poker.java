import java.io.File;
import java.util.Arrays;
import java.util.Scanner;
import java.io.FileNotFoundException;

public class poker {

        public static void main(String[] args) {
                cards myCards = new cards("", 0, ""); 
                printIntro(args);

                cards[] activeHand;
                if (args.length == 0) {
                        myCards.deliverStackandHand();
                        activeHand = myCards.getMyHand();
                }
                else {
                        activeHand = testDeck(args);
                }
                myCards.evaluateHand(activeHand);
        }


        public static void printIntro(String[] args){
                System.out.println("✦✦✦✦✦ POKER ✦✦✦✦✦ HAND ✦✦✦✦✦ ANALYZER ✦✦✦✦✦");
                if (args.length == 0) { 
                        System.out.println("\n✦✦✦✦✦ USING ✦✦✦✦ RANDOMIZED ✦✦✦✦ DECK ✦✦✦✦✦");
                }

                else { 
                        System.out.println("✦✦✦✦✦✦ File - " + args[0] + " ✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
                        System.out.println("\n✦✦✦✦✦ USING ✦✦✦✦✦ TEST ✦✦✦✦✦✦✦ DECK ✦✦✦✦✦"); 
                }
        }


        public static cards[] testDeck(String[] args) {

                cards[] testHand = new cards[7];

                File testFile = new File(args[0]);
                try {
                        Scanner reader = new Scanner(testFile);

                        String[] testStrings = new String[0];
                        if (reader.hasNextLine()) {
                                String line = reader.nextLine();
                                testStrings = line.split(",");

                        }

                        if (testStrings.length != 7) {
                                System.out.println("\nError: File contains too many/not enough cards");
                                System.exit(0);
                        }

                        for (int i = 0; i < testStrings.length; i++) {
                                if (testStrings[i].length() != 3) {
                                        System.out.println("\nError: Incorrect Card Format");
                                        System.exit(0);
                                }
                                        int value = 0;
                                        switch (testStrings[i].substring(0,2)){
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
                                                        value = Integer.parseInt(testStrings[i].substring(1,2));
                                                        break;
                                        }
                                testHand[i] = new cards(testStrings[i], value, testStrings[i].substring(2));

                        }

                System.out.println("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦ Your Hand: ✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
                for (int h = 0; h< 7;h++) {
                          System.out.print(testHand[h].card + " ");
                }
                System.out.println("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");


                cards[] tempHand = testHand;
                Arrays.sort(tempHand);
                for (int k = 0; k < tempHand.length-1; k++) {
                        if ((tempHand[k].card).equals(tempHand[k+1].card)) {
                                System.out.println("\nError: Duplicate found in Hand");
                                System.out.println("DUPLICATE: " + tempHand[k].card);
                                System.exit(0);
                        }
                }

                }

                catch (FileNotFoundException e) {
                        System.out.println("\nError: File not found");
                        System.exit(0);
                }
                 return testHand;
        }

}
