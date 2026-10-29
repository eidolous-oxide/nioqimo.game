using Godot;
using System;

//Core handles most universal processes for the game, such as startup/shutdown and saving.
//Core calls other scenes as needed.
public partial class Core : Node
{

    public override void _Ready()
    {

        //register bindings
        InputSettings.RegisterDefaultBinds();

        //physics
        ProjectSettings.SetSetting("physics/3d/default_gravity", UniversalPhysics.Gravity);
    }

}
