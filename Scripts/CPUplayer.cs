using Godot;
using System;
using System.Linq;

using DeckOfCards;

namespace Players{
		
	public class CPUplayer : Player
	{
		//Attributes//
		public enum AI_DIFFICULTY{easy, hard};
		private AI_DIFFICULTY myDifficulty;
		
		static int sCount = 0;
		//----------//
		
		//Constuctor//
		public CPUplayer(){
			GD.Print("Constructed Basic CPU.\n");
			//myName = $"CPUplayer_{++sCount}";
			myHand = new Hand();	//TODO: Scene.AddNode("Hand");
			myScore = 0;
			myDifficulty = AI_DIFFICULTY.easy;
		}
		public CPUplayer(string name, AI_DIFFICULTY diff = AI_DIFFICULTY.easy){
			GD.Print("Constructed Custom CPU.\n");
			myName = name;
			myHand = new Hand();	//TODO: Scene.AddNode("Hand");
			myDifficulty = diff;

			
			sCount++;
		}
		//------------//
		
		//Functions//
		public override Card ChooseCard(){	//AI logic
			
			if (myDifficulty == AI_DIFFICULTY.hard){
				// Try to play a special card first
				var specialCard = myHand.my_cards.FirstOrDefault(card => card.is_special);
				if (specialCard != null)
				{
					return specialCard;
				}
			}
			Random rn = new Random();
			return myHand.my_cards[rn.Next(myHand.my_cards.Count)]; //selects random card
		}
		//---------//
		
		public override void _Ready()
		{
			myName = $"CPUplayer_{++sCount}";
			GD.Print($"CPU Player number {sCount} has Joined!");
			myHand = GetNode<Hand>($"../Hands/Hand_P{sCount+1}");
			GD.Print(" his hand is: ", myHand.Name);
		}
	}
}
