using Godot;
using System;

namespace DeckOfCards{
	
	public enum COLORS : byte	//Color
	{
		RED,
		GREEN,
		BLUE,
		YELLOW,
		BLACK
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
	
public class Card : Control		//Card Objects Class
{
	//Attributes//
	private COLORS color;	//R, G, B, Y or K(blacK)
	private VALUES val;	//15 total values, (0-9, R_everse, S_kip, draw T_wo, W_ild, draw F_our)
	
	public bool is_special;	//is black or R,S,T
	
	[Export]
	public Texture myTex = (Texture)ResourceLoader.Load("res://Card PNGs/OYNO_FrontFace"+"Blue0"+".png");
	//----------//
	
	//Constructor//
	public Card(){
		is_special = false;
	}
	public Card(COLORS clr, VALUES vl){
		is_special = false;
		
		color = clr;
		val = vl;
	}
	//-----------//
	
	//Functions//
	public COLORS GetColor(){
		return color;
	}
	public void SetColor(COLORS c){
		color = c;
	}
	
	public VALUES GetValue(){
		return val;
	}
	public void SetValue(VALUES v){
		val = v;
	}
	
	public override string ToString(){
		return string.Format("{0} {1}", this.GetColor(), this.GetValue());
	}
	//-----------//

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		GD.Print("Hello");
		//var node = (Node)GetNode("this");
		
		
		var spriteNode = GetNode<TextureButton>("Card");
		spriteNode.TextureNormal = myTex;
	}
	//----------//

}
}
