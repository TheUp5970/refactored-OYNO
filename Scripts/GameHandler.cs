using Godot;
using System;
using System.Collections.Generic;	//For List

using DeckOfCards;
using Players;

namespace Game{
	//Enums//
	public enum DIFF_LVL : byte
	{
		EASY,
		HARD
	}
	
	public class GameHandler : Node
	{
		//Attributes//
		public int playerCount;		//selected player num
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
		public Node thePlayers;
		int i=0;//frames
		
		public bool has_received_params = false;
		//----------//
		
		//Constructor//
		public GameHandler(){	//Default Constructor 
			//Intentionally left blank because it depends on external parameters
			//and cannot be instantiated without all the other Nodes being ready
			//Therefore all the Constructor code moved to the _Ready override.
		}
		//------------//
		
		//Functions//
		public bool CanPlayCard(Card toThrow){
			// Check if the card is a Wild or Wild Draw Four
			if (toThrow.GetColor() == DeckOfCards.COLORS.Black)
			{
				return true;
			}
			//Rule Checking
			if(toThrow.GetColor() == thePile.GetCurrentCard().GetColor() 
			|| toThrow.GetValue() == thePile.GetCurrentCard().GetValue() )
				{
					return true;
				}
			else return false;
			}
			
			public void SpecialCardResult(Card thrown){
				//Card Checking
				DeckOfCards.VALUES specialvalue = thrown.GetValue();
				switch(specialvalue){
				case DeckOfCards.VALUES.reverse:
					// Reverse the order of play
			is_order_reversed = !is_order_reversed;
					break;
			
				case DeckOfCards.VALUES.skip:
					// Skip the next player's turn
					NextPlayer();
					break;
			
				case DeckOfCards.VALUES.two:
					// The next player must draw two cards
					NextPlayer();
					//players[currentPlayerIndex].myHand.AddCards(2, theDeck);
					for(int i = 0; i<1; i++) DealCardToPlayer(theDeck, players[currentPlayerIndex]);
					break;
			
				case DeckOfCards.VALUES.wild:
					// The player who played the card chooses the color
					// For an AI player, you could choose the color that it has the most of
					// For a human player, you could prompt them to choose a color
					players[currentPlayerIndex].getsToChooseColor = true;
					break;
			
				case DeckOfCards.VALUES.four:
					// The player who played the card chooses the color
					// The next player must draw four cards
					players[currentPlayerIndex].getsToChooseColor = true;
					NextPlayer();
					//players[currentPlayerIndex].myHand.AddCards(4, theDeck);
					for(int i = 0; i<3; i++) DealCardToPlayer(theDeck, players[currentPlayerIndex]);
					break;
				}
		}
		
		public bool IsGameWon(){
			// Check if any player has won the game
			foreach (Player player in players)
			{
				if (player.myHand.my_cards.Count == 0)
				{
					GD.Print(player.GetPlayerName() + " has won the game!");
					
					// Update the scores of the players
					foreach (Player player_i in players)
					{
						foreach( Card card in player_i.myHand.my_cards)
						player_i.AddPoints(card.scoreValue);
					}
					//EndGame();
					return true;
				}
				else return false;
			}
			return false;
		}
		public void NextPlayer(){
			//i += 1;
			//if (i % 1000 == 0) GD.Print("Awaiting Input: ");
			
			//if(!(currentPlayer is CPUplayer)){
			//	//Placeholder
			//	GD.Print("Human Played!");
			//	
			//}
			//else{
			//	Card playCard = currentPlayer.ChooseCard();
			//	if (CanPlayCard(playCard)){
			//		thePile.SetCurrentCard(playCard);
			//	}
			//	else{//Draw one card
			//		currentPlayer.myHand.AddCard(theDeck.GiveCards(1)[0]);
			//	}	
			//}
			
			previousPlayerIndex = currentPlayerIndex;
			
			// Update the index of the current player
			if (is_order_reversed)
			{
				currentPlayerIndex--;
				if (currentPlayerIndex < 0)
				{
					currentPlayerIndex = players.Count - 1;
				}
			}
			else
			{
				currentPlayerIndex++;
				if (currentPlayerIndex >= players.Count)
				{
					currentPlayerIndex = 0;
				}
			}
			// Update the current and previous player variables
			currentPlayer = players[currentPlayerIndex];
			previousPlayer = players[previousPlayerIndex];
		}
	//----------//
		public void OnCardPlayed(Card card)
		{
			// The player has played a card, continue the game loop...
		}
		// Called when the node enters the scene tree for the first time//
		public override void _Ready()
		{
			//GD.Print("F off!"); //DEBUG
			//EmitSignal(nameof(MySignal));
			//GD.Print("5 i expect them set!");
			//playerCount = (int)global.Get("numPlayers");
			//diffLevel = (int)global.Get("isDifficult");
			if(!has_received_params){
				playerCount = 2; //DEBUG
				diffLevel = DIFF_LVL.HARD; //DEBUG
			}
			
			GD.Print("GameHandler Is Ready to Boot!\n");	//Is Running
			
			//Get the Nodes
			theDeck = GetNode<Deck>("../Deck");
			thePile = GetNode<Pile>("../Pile");
			GD.Print(theDeck.Name);
			GD.Print(thePile.Name);
			
			//Determine Players
			players = new List<Player>(playerCount);
			
			Player human = new Player("Player One");	//Human Player
			players.Add(human);
			GD.Print("Added Human!\n");
			
			
			int p = 1;
			while(p<playerCount){
				CPUplayer plr = new CPUplayer();
				players.Add(plr);
				
				p++;
			}
			GD.Print($"Added {p-1} CPUs of Difficulty: {diffLevel}!\n");
			
			foreach (Player player in players)
			{
				player.Connect(nameof(Player.CardPlayed), this, nameof(OnCardPlayed));
			}
			//GD.Print(players);
			
			for (int player_i = 0; player_i < players.Count; player_i++){
				GD.Print($"Player \'{players[player_i].GetPlayerName()}\' is here.\n");
			}
			
			//Add players to thePlayers Grouping Node 
			thePlayers =  GetNode<Node>("../thePlayers");
			if (thePlayers != null)
			{
				GD.Print("thePlayers Node was found and is called: ", thePlayers.Name);
				
				if (players.Count > 0 && players[0] != null)
				{
					var par = players[0].GetParent();
					if (par != null)
						GD.Print("my parent is: ", par.Name);
					else GD.Print("free to parent");
					
					var isin = players[0].IsInsideTree();
					if (isin == false)
						GD.Print("im not here");
					else GD.Print("im already here");
					
					GD.Print("players is ready to receive");
					foreach(Player plyr in players){
						thePlayers.AddChild(plyr);
					}
				}
				else
				{
					GD.Print("players[0] is NULL");
				}
			}
			else
			{
				GD.Print("thePlayers is NULL");
			}
			
			
			//PrintTree(GetTree().Root, "");
			
			GD.Print("GameHandler Is Ready to Go!\n");	//Everything is initialized, time to start!
			GD.Print("Starting Game Loop!\n");
			StartGame();
		}
		
		private void PrintTree(Node node, string indentation)
		{
			GD.Print(indentation + node.Name);
			foreach (Node child in node.GetChildren())
			{
				PrintTree(child, indentation + "--");
			}
		}
		
		public async void DealCardToPlayer(Deck deck, Player player)
		{
			PackedScene cardScene = (PackedScene)GD.Load("res://Card.tscn");
			Card thisCardNode = (Card)cardScene.Instance(); //The template
			Card thisCard = deck.GiveCards(1)[0]; //The Top 'Card'
			thisCardNode.SetColor(thisCard.GetColor()); //Imprint it
			thisCardNode.SetValue(thisCard.GetValue());
			thisCardNode.is_special = thisCard.is_special;
			
			
			player.myHand.AddChild(thisCardNode);
			
			// Get the start and end positions
			Vector3 startPos = deck.GlobalTransform.origin;
			Vector3 endPos = player.myHand.GlobalTransform.origin;
			//Vector3 endPos = new Vector3(0, 4, 0);
		
			// Calculate the direction from the start position to the end position
			Vector3 direction = (endPos - startPos).Normalized();
		
			// Set the duration of the movement
			float duration = 2.0f; // Adjust this value as needed
			
			// Use a tween to interpolate the card's position
			Tween tween = new Tween();
			AddChild(tween);
			tween.InterpolateProperty(thisCardNode, "transform", thisCardNode.Transform, new Transform(thisCardNode.Transform.basis, endPos), duration);
			tween.Start();
		
			// Wait for the tween to finish before continuing
			await ToSignal(tween, "tween_all_completed");
		
			// Add the card to the player's hand in the game logic
			player.myHand.AddCard(thisCard, player);
			tween.QueueFree();
		}
	
		
		public void StartGame()
		{
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
					DealCardToPlayer(theDeck, plr);
					//plr.myHand.AddCards(1, theDeck);	//Deal the right way
					//plr.myHand.RevealHand();
				}
			}
			GD.Print("Dealing Cards...Done!");
			GD.Print("The players were each dealt a Hand of 7 Cards.");
			
			GD.Print("...");
			PackedScene cardScene = (PackedScene)GD.Load("res://Card.tscn");
			Card firstCard = (Card)cardScene.Instance();
			do{		//Ensure that 1st card is not a special.
				Card dummy = (Card)theDeck.GiveCards(1)[0];
				firstCard.SetColor( dummy.GetColor() );
				firstCard.SetValue( dummy.GetValue() );
				firstCard.is_special = dummy.is_special;
				GD.Print("First Card is: "+ firstCard.ToString());
			}
			while(firstCard.is_special);
			thePile.SetCurrentCard(firstCard);
			
			GD.Print("First Card Placed");
			
			
			currentPlayer = players[0];	//human goes first
			
			GD.Print("...");
			GD.Print("Ready to Play");
		}
		//---------------------------------------------------------------//
		
		public async void StartGameLoop()
		{
			while (!IsGameWon())
			{
				// Get the current player
				Player currentPlayer = players[currentPlayerIndex];
				
				Card chosenCard;
				
				GD.Print($"Awaiting {currentPlayer.GetName()}'s Move!");
				
				if (currentPlayer is Players.CPUplayer){
					// If the current player is a CPU player, use the ChooseCard function
	  				chosenCard = (Card)currentPlayer.ChooseCard();
				}
				else {// Wait for the player to play a card
					await ToSignal(currentPlayer, nameof(Player.ChooseCard));
					chosenCard = currentPlayer.chosenThatCard;
				}
				
				bool stuck_here = !CanPlayCard(chosenCard);
					
				if(stuck_here && currentPlayer is Players.CPUplayer){//Draw one card
					Card passCard = theDeck.GiveCards(1)[0];
					currentPlayer.myHand.AddCard(passCard, currentPlayer);
				}
				else if (!CanPlayCard(chosenCard)){
					while(stuck_here){
						GD.Print("Choose Another Card!");
						await ToSignal(currentPlayer, nameof(Player.ChooseCard));
						chosenCard = currentPlayer.chosenThatCard;
						stuck_here = !CanPlayCard(chosenCard);
					}
				}
				else{
					GD.Print("That was a fast turn");
				}
				thePile.SetCurrentCard(chosenCard);
				
				GD.Print("Calling Next Player!");
				NextPlayer();
			}
		}
		// Called every frame (delta is elapsed time since previous frame)
		public override void _Process(float delta)
		{
	//	  //Game Loop
	//	if(players.Count == 1) {
	//		Update();
	//		return;	//Ends Session
	//	}
	//
	//	NextPlayer();
	//
	//	if (currentPlayer.myHand.hand.Count == 0){
	//		players.Remove(currentPlayer);
	//		GD.Print("Player {0} has won this round", currentPlayer.GetName() );
	//	}
	//
		}
	}
//-----------------------------------------------------------------//
}
