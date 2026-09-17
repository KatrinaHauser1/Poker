//namespace ratings;


using System;


public class cards : IComparable<cards> {

        public const int CARDS_IN_DECK = 52;
        public const int CARDS_DRAWN = 7;
        public const int NUM_COMBINATIONS = 21;

	public string card  { get; set; }
        public int value  { get; set; }
        public string suit  { get; set; }
        public cards[] myHand  { get; set; }

        public cards(string card, int value, string suit){
                this.card = card;
                this.value = value;
                this.suit = suit;
        }

        public int CompareTo(cards other) {
                if (this.value != other.value) {
                        return this.value.CompareTo(other.value);  
                }
                return getSuitLevel(this.suit).CompareTo(getSuitLevel(other.suit));
        }

        public int getValue() {
                return this.value;
        }

        public string getSuit() {
                return this.suit;
        }

        public cards[] getMyHand() {
            return this.myHand;
        }

        // order of the suits for tie-breaking
        public int getSuitLevel(string suit) {

                switch (suit) {
                case "D":
                        return 1;
                case "C":
                        return 2;
                case "H":
                        return 3;
                case "S":
                        return 4;
                }

                return 0;
                }


        public cards[] initializeStack(){

                string[] cardstrings = {" 2H", " 3H", " 4H", " 5H", " 6H", " 7H", " 8H", " 9H", "10H", " JH", " QH", " KH", " AH", 
                                   " 2D", " 3D", " 4D", " 5D", " 6D", " 7D", " 8D", " 9D", "10D", " JD", " QD", " KD", " AD", 
                                   " 2C", " 3C", " 4C", " 5C", " 6C", " 7C", " 8C", " 9C", "10C", " JC", " QC", " KC", " AC", 
                                   " 2S", " 3S", " 4S", " 5S", " 6S", " 7S", " 8S", " 9S", "10S", " JS", " QS", " KS", " AS"
                };

                cards[] myStack = new cards[CARDS_IN_DECK];
                for (int i = 0; i < cardstrings.Length;i++){
                        int value = 0;
                        switch ( cardstrings[i].Substring(0,2)){
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
                                        value = int.Parse(cardstrings[i].Substring(1,1));
                                        break;
                        }
                        myStack[i] = new cards(cardstrings[i], value, cardstrings[i].Substring(2));

                }


                return myStack;
        }


        public cards[] shuffleStack(cards[] myStack) {

                cards[] myShuffledStack = (cards[])myStack.Clone();
		Random random = new Random();

                for (int i = myShuffledStack.Length - 1; i > 0; i--) {
			int newPos = random.Next(0, i + 1);

			cards temp = myShuffledStack[i];
        		myShuffledStack[i] = myShuffledStack[newPos];
        		myShuffledStack[newPos] = temp;
                }
                return myShuffledStack;

        }


        public cards[] drawHand(cards[] myStack) {

                cards[] myHand = new cards[CARDS_DRAWN];

                for (int i = 0; i < CARDS_DRAWN; i++) {
                        myHand[i] = myStack[i];
                }
                return myHand;

        }



        public void printStack(cards[] myStack){

                Console.WriteLine("        ✦✦✦ Shuffled " + CARDS_IN_DECK + " card deck: ✦✦✦");
                for (int i = 0; i< myStack.Length;i++) {
                        Console.Write(myStack[i].card + " ");
                        if ((i+1)%9 == 0) { Console.WriteLine(""); }
                }
                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");

        }

        public void printHand(cards[] myHand){

                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦ Your Hand: ✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
                for (int i = 0; i< CARDS_DRAWN;i++) {
                        Console.Write(myHand[i].card + " ");
                }
                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");

        }

        public void deliverStackandHand(){

                cards[] myStack = initializeStack();
                myStack = shuffleStack(myStack);
                myHand = drawHand(myStack);
                printStack(myStack);
                printHand(myHand);

        }

        // grade every hand and break ties
        public void evaluateHand(cards[] myHand) {
                cards[,] myCombinations = getCombinations(myHand);
                printCombinations(myCombinations);


                ratings[] handRatings = new ratings[NUM_COMBINATIONS];
                for (int i = 0; i < NUM_COMBINATIONS; i++) {
			cards[] rowHand = new cards[CARDS_DRAWN];
			for (int j = 0; j < CARDS_DRAWN; j++) {
                        	rowHand[j] = myCombinations[i, j];
                	}
			handRatings[i] = new ratings(rowHand);
                }

                Array.Sort(handRatings);

                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦HIGH HAND ORDER✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
                for (int i = 0; i < NUM_COMBINATIONS; i++) {
            
                        for (int j = 0; j < 5; j++) {
                                Console.Write(handRatings[i].playedHand[j].card + " ");
                        }
                        Console.Write(" | "  + handRatings[i].extraCards[0].card + " " + handRatings[i].extraCards[1].card);
                        string Hand = handRatings[i].convertScoreToHand(handRatings[i].score);
                        Console.WriteLine(" --- " + Hand);
                }

                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");

        }


        // get all x chose y combinations for all card hands
        public cards[,] getCombinations(cards[] myHand) {

            cards[,] myCombinations = new cards[NUM_COMBINATIONS, CARDS_DRAWN];;

            int combination = 0;

            for (int i = 0; i < CARDS_DRAWN; i++) {
                for (int j = i + 1; j < CARDS_DRAWN; j++) {

                    int index = 0;

                    for (int k = 0; k < CARDS_DRAWN; k++) {
                        if (k != i && k != j) {
                            myCombinations[combination,index] = myHand[k];
                            index++;
                        }
                    }

                    myCombinations[combination, 5] = myHand[i];
                    myCombinations[combination, 6] = myHand[j];

                    combination++;
                }
            }

            return myCombinations;
        }


             
        public void printCombinations(cards[,] myCombinations) {

                Console.WriteLine("\n✦✦✦✦✦✦✦✦✦✦ Hand Combinations: ✦✦✦✦✦✦✦✦✦✦✦✦✦");
                for (int i = 0; i < myCombinations.GetLength(0); i++) {
                        for (int j = 0; j < myCombinations.GetLength(1); j++) {
                                Console.Write(myCombinations[i,j].card + " ");
                                if (j == 4) {
                                        Console.Write(" | ");
                                }
                        }
                        Console.WriteLine("");
                }
        }

}
