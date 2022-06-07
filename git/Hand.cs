using Godot;
using System;
using System.Collections.Generic;

using DeckOfCards;

public class Hand : Spatial	//Contains a Players Cards, as well as useful functions.
{
	//Attributes//
	public List<Card> hand = new List<Card>();
	//----------//
	
	//CONSTRUCTOR//
	public Hand(){
		
	}
	//-----------//
	
	//Functions//
	public void AddCards(int numCards, Deck theDeck){
		int i = 0;
		Card[] cardsToAdd = theDeck.GiveCards(numCards);
		do{
			hand.Add(cardsToAdd[i]);
			
			i++;
		}
		while(i<numCards);
	}
	
	private void RemoveCard(int cardIndex){
		hand.RemoveAt(cardIndex);
	}
	public void PlayCard(int cardIndex, Pile pile){
		pile.SetCurrentCard(hand[cardIndex]);
		RemoveCard(cardIndex);
	}
	//-----------//
}
