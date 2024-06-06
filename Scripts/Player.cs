using Godot;
using System;

using DeckOfCards;

namespace Players
{
	
	public class Player : Node	//Contains the useful functions and stats for a Player
	{
		//Attributes//
		protected string myName;
		protected int myScore;
		protected bool wantsPass = false;
		protected bool wantsDraw = false;
		public bool getsToChooseColor = false;
		protected bool declaredUno = false;
		
		public Card chosenThatCard;
		
		
		public Hand myHand;	//Players Hand
		public int numPlayers = 0;
		public int turnNum = 0;
		
		[Signal]
		public delegate void CardPlayed(Card card);
		//----------//
		
		//Constuctor//
		public Player(){
			myName = "Player1";
			//myHand = new Hand();
			myScore = 0;
			numPlayers++;
		}
		public Player(string name){
			myName = name;
			//myHand = new Hand();
			myScore = 0;
			numPlayers++;
		}
		//------------//
		
		//Functions//
		public string GetPlayerName(){
			return myName;
		}
		public void SetPlayerName(string name){
			myName = name;
		}
		
		public int AddPoints(int points){
			myScore += points;
			return myScore;
		}
		public void SignaledCard(Card sig_card){
			chosenThatCard = sig_card;
		}
		public virtual Card ChooseCard(){	//GUI integration placeholder
			//Random rn = new Random();
			//Card ret_card = myHand.my_cards[rn.Next(myHand.my_cards.Count)];
			Card chsn_card = chosenThatCard;
			
			return chsn_card;
		}
		
		public override void _Ready()
		{
			GD.Print("Player: ", myName, " has Joined!");
			if(myName == "Player One")
				myHand = GetNode<Hand>("../Hands/Hand_P1");
			GD.Print(" his hand is ", myHand.Name);
			
		}
		//-------------//
	}
}
