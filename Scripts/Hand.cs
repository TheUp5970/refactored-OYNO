using Godot;
using System;
using System.Collections.Generic;	//for List

using DeckOfCards;

public class Hand : Spatial	//Contains a Players Cards, as well as useful functions.
{
	//Attributes//
	//public List<Card> hand = new List<Card>();
	[Export]
	public List<Card> my_cards {get; private set;}
	
	[Export] 
	private Curve spread_curve;
	[Export] 
	private Curve height_curve;
	[Export] 
	private Curve rotation_curve;
	//----------//
	
	private Camera camera;
	
	//CONSTRUCTOR//
	public Hand(){
		my_cards = new List<Card>();
	}
	//-----------//
	
	//Functions//
	public void AddCard(Card crd, Players.Player player){
	
			my_cards.Add(crd);
			
			// Connect the signal
			crd.Connect(nameof(Card.CardClicked), player, nameof(Players.Player.SignaledCard));
	
			RevealHand();
	}
//	public void AddCards(int numCards, Deck theDeck){
//		var cardResource = (PackedScene)GD.Load("res://Card.tscn");
//
//		int i = 0;
//		Card[] cardsToAdd = theDeck.GiveCards(numCards);
//		do{
//			my_cards.Add(cardsToAdd[i]);
//
//			//Card mycard = (Card)cardResource.Instance();
//
//			//mycard.SetValue(cardsToAdd[i].GetValue());
//			//mycard.SetColor(cardsToAdd[i].GetColor());
//			//AddChild(cardsToAdd[i]);
//
//			i++;
//			RevealHand();
//		}
//		while(i<numCards);
//
//		//RevealHand();
//	}
	
	private void RemoveCard(int cardIndex){
		my_cards.RemoveAt(cardIndex);
	}
	public void PlayCard(int cardIndex, Pile pile){
		pile.SetCurrentCard(my_cards[cardIndex]);
		RemoveCard(cardIndex);
	}
	
	public void RevealHand(){
	   
	 const float HAND_WIDTH = 1.5F;
		int i = 0;
	
		foreach (Node child in GetChildren())
		{
			Card mycard = child as Card;
			if (mycard != null)
			{
				float hand_ratio = 0.5F;
	
				if (GetChildCount() > 1)
				{
					hand_ratio = (float)(i) / (float)(GetChildCount());
				}
				Transform destination = GlobalTransform;
	
	
				// Transform local X direction to global space
				Vector3 localX = GlobalTransform.basis.x;
				destination.origin += localX * spread_curve.Interpolate(hand_ratio) * HAND_WIDTH;
				//destination.origin.x += spread_curve.Interpolate(hand_ratio) * HAND_WIDTH;
				
				destination.origin += height_curve.Interpolate(hand_ratio) * Vector3.Up;
	
				var mycardtransform = mycard.GlobalTransform;
	
				mycardtransform.origin = destination.origin;
				
				mycard.GlobalTransform = mycardtransform;
	
				var mycardrotation = mycard.Rotation;
				mycardrotation.z = (float)rotation_curve.Interpolate(hand_ratio) * 0.5F;
				
				mycard.Rotation = mycardrotation;
				
				// Apply rotation in local space
		   		// mycardtransform.basis = mycardtransform.basis.Rotated(Vector3.Up, mycardrotation.z);
				//mycard.GlobalTransform = mycardtransform;
				//mycard.Rotation = mycardrotation;
				
				i++;
			}
		}
	}
	//-----------//
	public override void _Ready()
	{
		GD.Print("Hand "+ GetNode<Node>(".").Name + " is Ready!");
		//var theDeck = GetNode<Deck>("Deck");
		//var DeckScene = (PackedScene)GD.Load("res://Deck.tscn");
		//Deck theDeck = (Deck)DeckScene.Instance();
		//var deck = GetNode<DeckOfCards.Deck>("../../../Deck");
		//AddCards(5, deck);
		camera = GetViewport().GetCamera();
		
	}
}
