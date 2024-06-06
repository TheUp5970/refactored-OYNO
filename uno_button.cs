using Godot;
using System;

public class uno_button : MeshInstance
{
	AnimationPlayer animationPlayer;
	AudioStreamPlayer3D audioPlayer;
	
	public override void _Ready()
	{
		SetProcessInput(true);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		audioPlayer = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
		
		animationPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished)); //Signal when done playing
	}
	
	public void OnAnimationFinished(String anim_name)
	{
		// Re-enable input processing
		SetProcessInput(true);
	}
	
	public override void _Input(InputEvent @event)
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
	
				// Check if the collider is the StaticBody node
				if (result.Count > 0 && result["collider"] == GetNode("StaticBody"))
				{
					GD.Print("Uno Declared!");
					animationPlayer.Play("button_push_release");
					audioPlayer.Play();
					SetProcessInput(false);	//Prevent spam clicks until anim is done!
				}
			}
		}
	}

	
	private bool IsAParentOf(Node node)
	{
		while (node != null)
		{
			if (node == this)
			{
				return true;
			}
			node = node.GetParent();
		}
		return false;
	}
	}

