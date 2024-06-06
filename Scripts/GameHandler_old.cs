using Godot;
using System;
using System.Collections.Generic;	//For List

using DeckOfCards;
using Players;

namespace Game{
	
	//Enums//
	public enum DIFF_LVL : byte	//Color
	{
		EASY,
		HARD
	}
	
	public class GameHandler : Node
	{
		//Attributes//
		public int playerCount = -1;	//selected player num
		public DIFF_LVL diffLevel;		//selected difficulty
		
		private const int MAX_PLAYERS = 4;
		
		public List<Player> players;	//Player Objects
		
		private int currentPlayerIndex = 0;
		private int previousPlayerIndex = -1;
		
		public Player currentPlayer;
		public Player previousPlayer;
		public int whosTurnIndex = -1;
		
		private bool is_order_reversed = false;
		
		
		public Deck theDeck;
		public Pile thePile;
		int i=0;//frames
		//----------//
		
		//Constructor//
		public GameHandler(){	//Default Constructor
			
			playerCount = 2;
			diffLevel = DIFF_LVL.EASY;	//TODO: Add AI Difficulty Levels
			
			GD.Print("Session Just Started!");	//Is Running
			
			players = new List<Player>(playerCount);
			
			theDeck = new Deck();	//TODO GD.GetNode("Deck");
			thePile = new Pile();	//TODO GD.GetNode("Pile");
			
			Player human = new Player("Player One");	//Human Player
			players.Add(human);
			
	//		int i = 1;
	//		do{
	//			Player plr = new CPUplayer();
	//			players.Add(plr);
	//
	//			i++;
	//		}
	//		while(i<playerCount);
			
		}
		public GameHandler(int plCnt, DIFF_LVL difLvl){
			GD.Print("Game Handler Created!");
			if(plCnt <= MAX_PLAYERS)
			{
				playerCount = plCnt;
				players = new List<Player>(playerCount);
			}
			
			var deckNode = ((PackedScene)GD.Load("res://Deck.tscn"));
			theDeck = (Deck)deckNode.Instance();
			GD.Print(theDeck.Name);
			
			var pileNode = (PackedScene)GD.Load("res://Pile.tscn");
			thePile = (Pile)pileNode.Instance();
			GD.Print(thePile.Name);
			
			AddChild(theDeck);
			AddChild(thePile);
//			theDeck = new Deck();	//TODO GD.GetNode("Deck");
//			thePile = new Pile();	//TODO GD.GetNode("Pile");
			
			
			//Player human = new Player("Player One");	//Human Player
			//players.Add(human);
			
	//		int i = 1;
	//		do{
	//			Player plr = new CPUplayer();
	//			players.Add(plr);
	//
	//			i++;
	//		}
	//		while(i<playerCount);
		}
	//------------//
	
	//Functions//
	public bool CheckCard(Card toThrow){
		//Rule Checking
		if(toThrow.GetColor() == thePile.GetCurrentCard().GetColor() 
		|| toThrow.GetValue() == thePile.GetCurrentCard().GetValue() 
		|| toThrow.is_special){
			return true;
		}
		else return false;
	}
	
	public void SpecialCardResult(Card thrown){
		//Card Checking
		if(thrown.GetColor() == COLORS.Black){
			if (thrown.GetValue() == VALUES.four){
				int turn =0;
				//players[turn].
			}
		}
		
	}
	public void Update(){
		foreach(Player pl in players){
			if (pl.myHand.hand.Count != 0) pl.myHand.hand.Clear();
		}
	}
	public void NextPlayer(){
		i += 1;
		if (i % 1000 == 0) GD.Print("Awaiting Input: ");
		
		if(!(currentPlayer is CPUplayer)){
			//Placeholder
			GD.Print("Human Played!");
			
		}
		else{
			Card playCard = currentPlayer.ChooseCard();
			if (CheckCard(playCard)){
				thePile.SetCurrentCard(playCard);
			}
			else{//Draw one card
				currentPlayer.myHand.AddCards(1, theDeck);
			}	
		}
		
		previousPlayer = currentPlayer;
		
		if (!is_order_reversed){
			if(whosTurnIndex+1 == players.Count) whosTurnIndex = 0;
			currentPlayer = players[++whosTurnIndex];
		}
		else{
			if(whosTurnIndex-1 == -1) whosTurnIndex = players.Count-1;
			currentPlayer = players[--whosTurnIndex];
		
		}
	}
	//----------//
	
	
	// Called when the node enters the scene tree for the first time//
	public override void _Ready()
	{
		//theDeck =  GetNode<Deck>("Deck");
		//thePile =  GetNode<Pile>("Pile");
		StartGame();
	}
	
	public void StartGame()
	{
		GameHandler gh = new GameHandler(2, DIFF_LVL.EASY);
		
		GD.Print("Setting Up Game");
		
		GD.Print("...");
		GD.Print("Shuffling Deck...");
		theDeck.Shuffle();
		GD.Print("Shuffling Deck...Done!");
		//TODO: Progress Loading Bar
		GD.Print("There are ", players.Count, " players waiting to be dealt a Hand.");
		
		GD.Print("...");
		GD.Print("Dealing Cards...");
		for(int i = 1; i<= 7; i++){
			foreach (Player plr in players){
				plr.myHand.AddCards(1, theDeck);	//Deal the right way
				//plr.myHand.RevealHand();
			}
		}
		GD.Print("Dealing Cards...Done!");
		GD.Print("The players were each dealt a Hand of 7 Cards.");
		
		GD.Print("...");
		Card firstCard;
		do{		//Ensure that 1st card is not a special.
			firstCard = theDeck.GiveCards(1)[0];
			thePile.SetCurrentCard(firstCard);
		}
		while(firstCard.is_special);
		GD.Print("First Card Placed");
		GD.Print("First Card is: "+ firstCard.ToString());
		
		currentPlayer = players[0];	//human goes first
		
		GD.Print("...");
		GD.Print("Ready to Play");
	}
	//---------------------------------------------------------------//

	// Called every frame (delta is elapsed time since previous frame)
	public override void _Process(float delta)
	{
//	  //Game Loop
//	if(players.Count == 1) {
//		Update();
//		return;	//Ends Session
	}
//
//	NextPlayer();
//
//	if (currentPlayer.myHand.hand.Count == 0){
//		players.Remove(currentPlayer);
//		GD.Print("Player {0} has won this round", currentPlayer.GetName() );
//	}
//
//  }
//-----------------------------------------------------------------//
	}
}
