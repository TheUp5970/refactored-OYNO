using Godot;
using System;

using DeckOfCards;

public class CPUplayer : Player
{
	//Attributes//
	public enum AI_DIFFICULTY{easy, hard};
	private AI_DIFFICULTY myDifficulty = AI_DIFFICULTY.easy;
	
	static int sCount = 0;
	//----------//
	
	//Constuctor//
	public CPUplayer(){
		myName = "CPUplayer";
		myHand = new Hand();	//TODO: Scene.AddNode("Hand");
		myScore = 0;
		
		sCount++;
	}
	public CPUplayer(string name){
		myName = name;
		myHand = new Hand();	//TODO: Scene.AddNode("Hand");
		
		sCount++;
	}
	//------------//
	
	//Functions//
	public Card ChooseCard(){	//AI placeholder
		Random rn = new Random();
		if (myDifficulty == AI_DIFFICULTY.hard){
			
			return myHand.hand[rn.Next(myHand.hand.Count)];
		}
		return myHand.hand[0];	//selects nth card
	}
	//---------//
	
	public override void _Ready()
	{
		GD.Print("CPU Player number ", sCount, " has Joined!");
	}
}
