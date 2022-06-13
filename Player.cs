using Godot;
using System;

using DeckOfCards;

public class Player : Node	//Contains the useful functions and stats for a Player
{
	//Attributes//
	protected string myName;
	protected int myScore;
	protected bool wantsPass = false;
	protected bool wantsDraw = false;
	
	public Hand myHand;	//Players Hand
	public int turnNum = 0;
	//----------//
	
	//Constuctor//
	public Player(){
		myName = "Player1";
		myHand = new Hand();
		myScore = 0;
	}
	public Player(string name){
		myName = name;
		myHand = new Hand();
	}
	//------------//
	
	//Functions//
	new public string GetName(){
		return myName;
	}
	new public void SetName(string name){
		myName = name;
	}
	
	public int AddPoints(int points){
		myScore += points;
		return myScore;
	}
	public Card ChooseCard(){	//GUI integration placeholder
		Random rn = new Random();
		return myHand.hand[rn.Next(myHand.hand.Count)];
	}
	
	public override void _Ready()
	{
		GD.Print("Player: ", myName, " has Joined!");
	}
	//-------------//
}
