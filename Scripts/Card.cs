using Godot;
using System;

namespace DeckOfCards
{
	
	public enum COLORS : byte	//Color
	{
		Red,
		Green,
		Blue,
		Yellow,
		Black
	}
	
	public enum VALUES : byte	//Value
	{
		cero,
		uno,
		dos,
		tres,
		cuatro,
		cinco,
		seis,
		siete,
		ocho,
		nueve,
		reverse,
		skip,
		two,
		wild,
		four
	}
	
	public class Card : RigidBody		//Card Objects Class
	{
		//Attributes//
		[Export]
		private COLORS color;	//R, G, B, Y or K(blacK)
		[Export]
		private VALUES val;	//15 total values, (0-9, R_everse, S_kip, draw T_wo, W_ild, draw F_our)
		public bool is_special;	//is black or R,S,T
		//private byte score;	//The Card's worth
		public int scoreValue => CalcScore();
		
		
		private MeshInstance mesh;
		//[Export]
		public Texture myTex;
		
		[Signal]
		public delegate void CardClicked(Card clckdcrd);
		//----------//
		
		//Constructors//
		public Card()
		{
			//is_special = false;
		}
		public Card(COLORS clr, VALUES vl)
		{
			color = clr;
			val = vl;
			is_special = (int)val > 9;
		}
		//-----------//
		
		//GetSetFunctions//
		public COLORS GetColor()
		{
			return color;
		}
		public void SetColor(COLORS c)
		{
			color = c;
		}
		
		public VALUES GetValue(){
			return val;
		}
		public void SetValue(VALUES v)
		{
			val = v;
		}
		//-----------//
		
		//----------------//
		//Helper Functions//
		//----------------//
		
		//Calculates score from value
		public byte CalcScore()
		{
			byte score;
			if ( (int)val <=9)
				score = (byte)val;
			else if((int)val <=12 && (int)val >10)
				score = 20;
			else
				score = 50;		return score;
		}
		
		//Helper for easy printout, also used for texture name path
		public override string ToString()
		{
			return string.Format("{0}{1}", this.GetColor(), (int)this.GetValue());
		}
		//----------------//
		
		//---------------//
		//Class Functions//
		//---------------//
		
		// Called when the node's children are ready, it's ready too.
		public override void _Ready()
		{
			base._Ready();
			//GD.Print("Hello, I'm card :"+ this.Name);
			GetNode<RigidBody>(".").Sleeping = true;
			//var node = (Node)GetNode("this");
			
			//var spriteNode = GetNode<TextureButton>("Card");
			//spriteNode.TextureNormal = myTex;
			mesh = GetNode<MeshInstance>("Face_Front");
			
			//Enable Input Events for Mouse Click
			this.SetProcessInput(true);
			//Connect the Mouse Click Event signal to this object
		//	this.Connect("input_event", this, nameof(OnInputEvent));
			
			Node parent = GetParent();
			
			GD.Print("Card has Entered...");
			if (parent != null && parent.Name.StartsWith("Hand_P") ){
				GD.Print(" ...in Hand!");
				this.RevealCard();
			}
			else GD.Print(" ...Elsewhere!");
		}
		
		//*Only after ready*//
		
		//Texture it
		public void RevealCard()
		{
			myTex = (Texture)ResourceLoader.Load("res://Card PNGs/OYNO_FrontFace"+ ToString()+".png");
			var cardFaceMat = (SpatialMaterial)GD.Load("res://Materials/mat_cardFrontFace(MULTI).tres");
			cardFaceMat = (SpatialMaterial)cardFaceMat.Duplicate(true);//Make it Unique
			cardFaceMat.SetupLocalToScene();
			cardFaceMat.AlbedoTexture = myTex;
			GD.Print( this.ToString());
			
			mesh.MaterialOverride = cardFaceMat;
		}
		
		public override void _UnhandledInput(InputEvent @event) //Process that Detects when a card is Selected
		{
			if (@event is InputEventMouseButton eventMouseButton)
			{
				if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == (int)ButtonList.Left)
				{
					// Get the mouse position in the viewport
					var mousePos = GetViewport().GetMousePosition();
	
					// Create a ray from the camera to the mouse position
					var spaceState = GetWorld().DirectSpaceState;
					var from = GetViewport().GetCamera().ProjectRayOrigin(mousePos);
					var to = from + GetViewport().GetCamera().ProjectRayNormal(mousePos) * 1000;
					var result = spaceState.IntersectRay(from, to);
	
					// Check if this card was clicked
					if (result.Count > 0 && result["collider"] == this)
					{
						GD.Print("Card selected: " + this.GetColor() + this.GetValue());
						GD.Print("Hello, I'm card :"+ this.Name);
						//flip or select card logic here
						Node parent = GetParent();
						
						GD.Print("Card was Selected...");
						if (parent != null && parent.Name.StartsWith("Hand_P1") ){
							GD.Print(" ...from Hand!");
							EmitSignal(nameof(CardClicked), this);
						}
						else GD.Print("...from Elsewhere!");
					}
				}
			}
		}
	}
}
