using Godot;
using System;
using System.Collections.Generic;

using DeckOfCards;

public class Pile : Spatial	//Whre the played Cards go
{
	//Attributes//
	public List<Card> pile;
	private Card currentCard;
	//---------//
	
	//CONSTRUCTOR//
	public Pile(){
		pile = new List<Card>();
		currentCard = null;
	}
	//-----------//
	
	//Functions//
	public Card GetCurrentCard(){
		return this.currentCard;
	}
	public void SetCurrentCard(Card crd){
		Card temp = currentCard;	//Previous current card
		this.currentCard = crd;		//New current card
		if (temp != null) pile.Insert(0, temp);		//Add to Top of Pile	TODO:optimise using linked list
	}
	private void Flush(){	//for reshuffling
		//Deck(pile.ToArray());
		pile.Clear();
	}
	
	public void PrintPile(List<Card> pl){	//For DEBUG
		foreach (Card crd in pl)
			//GD.Print(crd.ToString());
			//GD.Print(pl.LastIndexOf(crd));
			GD.Print(crd.ToString());
	}
	//----------//
	
	public override void _Ready()	//When Enters Scene Tree
	{
		pile.Add(new Card(COLORS.RED, VALUES.two));
		pile.Add(new Card(COLORS.GREEN, VALUES.cinco));
		SetCurrentCard(new Card(COLORS.BLACK, VALUES.wild));
		SetCurrentCard(new Card(COLORS.BLUE, VALUES.two));
		//PrintPile(pil.pile);
		GD.Print(pile[0].ToString());
		GD.Print(pile[1].ToString());
		//GD.Print(pil.pile[2].ToString());
	}

}
