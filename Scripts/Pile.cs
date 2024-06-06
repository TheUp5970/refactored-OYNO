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
		RevealCard(crd);
	}
	private void Flush(){	//for reshuffling
		//Deck(pile.ToArray());
		pile.Clear();
	}
	
	public void PrintPile(){	//For DEBUG
		foreach (Card crd in pile)
			//GD.Print(crd.ToString());
			//GD.Print(pl.LastIndexOf(crd));
			GD.Print(crd.ToString());
	}
	
	private void RevealCard(Card cardShown){
		//Make new Card.tscn instance
		//var cardScene = ((PackedScene)GD.Load("res://Card.tscn"));
		//RigidBody cardShown = (RigidBody)cardScene.Instance();
		AddChild(cardShown);
		
		Vector3 rot = new Vector3(0,Mathf.Pi,0);
		cardShown.Rotation = rot;
		
		//Texture it
		var cardFaceMat = (SpatialMaterial)GD.Load("res://Materials/mat_cardFrontFace(MULTI).tres");
		cardFaceMat.SetupLocalToScene();
		cardFaceMat.AlbedoTexture = (Texture)GD.Load("res://Card PNGs/OYNO_FrontFace"+ GetCurrentCard().ToString()+".png");
		//GD.Print(GetCurrentCard().ToString());
		var mesh = (MeshInstance)cardShown.GetNode("Face_Front");
		mesh.MaterialOverride = cardFaceMat;
		
	}
	//----------//
	
	public override void _Ready()	//When Enters Scene Tree
	{
		/*Testing
		pile.Insert(0, new Card(COLORS.RED, VALUES.two));
		pile.Insert(0, new Card(COLORS.GREEN, VALUES.cinco));
		pile.Insert(0, new Card(COLORS.YELLOW, VALUES.cinco));
		SetCurrentCard(new Card(COLORS.BLACK, VALUES.wild));
		SetCurrentCard(new Card(COLORS.BLUE, VALUES.two));
		GD.Print("before: ");
		PrintPile(pile);
		Flush();
		GD.Print("after: ");
		PrintPile(pile);
//		GD.Print(pile[0].ToString());
//		GD.Print(pile[1].ToString());
//		GD.Print(pile[2].ToString());
//		GD.Print(pile[3].ToString());
		
		SetCurrentCard(new Card(COLORS.Red, VALUES.cero));
		SetCurrentCard(new Card(COLORS.Yellow, VALUES.cero));
		SetCurrentCard(new Card(COLORS.Green, VALUES.cero));
		SetCurrentCard(new Card(COLORS.Green, VALUES.skip));
		*/
		/*
		//Make new Card.tscn instance
		var cardScene = ((PackedScene)GD.Load("res://Card.tscn"));
		RigidBody cardShown = (RigidBody)cardScene.Instance();
		AddChild(cardShown);
		
		Vector3 rot = new Vector3(0,Mathf.Pi,0);
		cardShown.Rotation = rot;
		
		//Texture it
		var cardFaceMat = (SpatialMaterial)GD.Load("res://Materials/mat_cardFrontFace(MULTI).tres");
		cardFaceMat.SetupLocalToScene();
		cardFaceMat.AlbedoTexture = (Texture)GD.Load("res://Card PNGs/OYNO_BackFace.png");
		//GD.Print(GetCurrentCard().ToString());
		var mesh = (MeshInstance)cardShown.GetNode("Face_Front");
		mesh.MaterialOverride = cardFaceMat;
		*/
		
	}

}
