using Godot;
using System;

using DeckOfCards;

public class HumanPlayer : Player
{
	//Attributes//

	//----------//
	
	//Constuctor//
	public CPUplayer(){
		myName = "Player1";
		myHand = new Hand();	//TODO: Scene.AddNode("Hand");
		myScore = 0;
	}
	public CPUplayer(string name){
		myName = name;
		myHand = new Hand();	//TODO: Scene.AddNode("Hand");
	}
	//------------//
	
	//Functions//

	//---------//
	
	public override void _Ready()
	{
		GD.Print("Player ", GetName(), " has Joined!");
	}
}
