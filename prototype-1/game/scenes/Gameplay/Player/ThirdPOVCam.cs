using Godot;
using System;

public partial class ThirdPOVCam : Node3D
{
    private SpringArm3D arm;
    private Camera3D cam;
    
    [Export] public float armLength {get; set;} = 8f;  //default value
    public override void _Ready()
    {
        arm = GetNode<SpringArm3D>("3rdPOVSpring");
        cam = GetNode<Camera3D>("3rdPOVSpring/player3rdCamera");

        arm.SpringLength = armLength;
    }

    public void Zoom3(float delta)
    {
        armLength = Mathf.Clamp(armLength + delta, 3f, 15f);    //min/max zoom
        arm.SpringLength = armLength;
    }
}