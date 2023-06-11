using Godot;
using System;

namespace DeckOfCards{
	
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
	private COLORS color;	//R, G, B, Y or K(blacK)
	private VALUES val;	//15 total values, (0-9, R_everse, S_kip, draw T_wo, W_ild, draw F_our)
	private byte score;	//The Card's worth
	
	public bool is_special;	//is black or R,S,T
	
	private MeshInstance mesh;
	
	[Export]
	public Texture myTex = (Texture)ResourceLoader.Load("res://Card PNGs/OYNO_FrontFace"+"Blue0"+".png");
	//----------//
	
	//Constructor//
	public Card()
	{
		is_special = false;
	}
	public Card(COLORS clr, VALUES vl)
	{
		is_special = false;
		
		color = clr;
		val = vl;
	}
	//-----------//
	
	//Functions//
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
	
	public byte GetScore()
	{
		return score;
	}
	public void SetScore(byte sc)
	{
		score = sc;
	}
	
	public override string ToString()
	{
		return string.Format("{0}{1}", this.GetColor(), (int)this.GetValue());
	}
	//-----------//

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		GD.Print("Hello");
		GetNode<RigidBody>(".").Sleeping = true;
		//var node = (Node)GetNode("this");
		
		//var spriteNode = GetNode<TextureButton>("Card");
		//spriteNode.TextureNormal = myTex;
		mesh = GetNode<MeshInstance>("Face_Front");
	}
	//----------//
	
	public void RevealCard()
	{
		//Texture it
		var cardFaceMat = (SpatialMaterial)GD.Load("res://Materials/mat_cardFrontFace(MULTI).tres");
		cardFaceMat.SetupLocalToScene();
		cardFaceMat.AlbedoTexture = (Texture)GD.Load("res://Card PNGs/OYNO_FrontFace"+ ToString()+".png");
		//GD.Print(GetCurrentCard().ToString());
		
		mesh.MaterialOverride = cardFaceMat;
		
	}
}
}
