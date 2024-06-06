using Godot;
using System;
using System.Linq;	//for random generator

namespace DeckOfCards
{
	
	public class Deck : Spatial
	{
	//Attributes//
	public static Card[] containsCards = new Card[108];	//Array of cards
	//----------//
	
	//CONSTRUCTOR//
	public Deck(){	//Primary Constructor, create At Beggining of Game
		for (int i = 0; i< containsCards.Length-8;  i++){	//For Colored CARDS
			
			containsCards[i] = new Card((COLORS)(i/25), VALUES.cero);	//Makes 25 of RED,G,B,Y in series, init to zero
			
			if(i>0 && i<=24){	//REDs
				containsCards[i].SetValue((VALUES)(((i-1)%12)+1));	//Skip cero, assign uno(1) to two(draw2) to the 24 cards
				if (containsCards[i].GetValue() == VALUES.reverse || containsCards[i].GetValue() == VALUES.skip || containsCards[i].GetValue() == VALUES.two)
				containsCards[i].is_special = true;
			}
			else if(i>25 && i<=49){	//GREENs
				containsCards[i].SetValue((VALUES)(((i-2)%12)+1));	//Skip cero, assign uno(1) to two(draw2) to the 24 cards
				if (containsCards[i].GetValue() == VALUES.reverse || containsCards[i].GetValue() == VALUES.skip || containsCards[i].GetValue() == VALUES.two)
				containsCards[i].is_special = true;
			}
			else if(i>50 && i<=74){	//BLUEs
				containsCards[i].SetValue((VALUES)(((i-3)%12)+1));	//Skip cero, assign uno(1) to two(draw2) to the 24 cards
				if (containsCards[i].GetValue() == VALUES.reverse || containsCards[i].GetValue() == VALUES.skip || containsCards[i].GetValue() == VALUES.two)
				containsCards[i].is_special = true;
			}
			else if(i>75 && i<=99){	//YELLOWs
				containsCards[i].SetValue((VALUES)(((i-4)%12)+1));	//Skip cero, assign uno(1) to two(draw2) to the 24 cards
				if (containsCards[i].GetValue() == VALUES.reverse || containsCards[i].GetValue() == VALUES.skip || containsCards[i].GetValue() == VALUES.two)
				containsCards[i].is_special = true;
			}
		}
		for (int i = containsCards.Length-8; i< containsCards.Length;  i++){	//For Black CARDS
			
			containsCards[i] = new Card(COLORS.Black, VALUES.wild);	//Makes 8 black cards, init to wild
			containsCards[i].is_special = true;
			if (i>=containsCards.Length-4) containsCards[i].SetValue(VALUES.four);	//Skips 4 wild, makes 4 four(draw4)
		}
		
//		int j = 0;	/*DEBUG: Prints all cards' color & value in init Deck*/
//		foreach (Card cd in containsCards){
//			GD.Print("Card ", j, ": ",cd.GetColor()," \t", cd.GetValue());
//			j++;
//		}
	
	}
	
	public Deck(Pile discardPile){	//Secondary Constructor, when Creating Again from Discard Pile
		Card[] newCards = discardPile.pile.ToArray();
		containsCards = newCards;
		Shuffle();
	}
	//-----------//
	
	//Functions//
	Random rn = new Random();
	
	public void Shuffle(){	//Shuffles Deck
		for (int i = containsCards.Length - 1; i>0; --i){
			int j = rn.Next(i+1);
			Card temp = containsCards[i];
			containsCards[i] = containsCards[j];
			containsCards[j] = temp;
		}
		return;
	}
	public Card[] GiveCards(int numCards){
		Card[] givenCards = new Card[numCards];
		int i = 0;
		do{
			givenCards[i] = containsCards[0];
			containsCards = containsCards.Skip(1).ToArray();	//TODO optimize
			
			GD.Print(givenCards[i].ToString());
			i++;
		}
		while(i<numCards);
		
		
		return givenCards;
	}
	
	public void PrintDeck(){	//DEBUG: helps in knowing Deck
		int i = 1;
		foreach(Card crd in containsCards){
			GD.Print("Card ", i, ": ", crd.ToString());
			//System.PrintLine();
			i++;
		}
	}
	//---------//
	
//	public void Main(string[] args){	DEBUG:not processed
//		GD.Print("main\n");
//	}
	public override void _Ready()
	{
		Deck myDeck = new Deck();
		//GD.Print(myDeck.containsCards[45].ToString());
		//PrintDeck();
		//Shuffle(containsCards);
		//DEBUG
		//myDeck.PrintDeck();
	}
}

}
