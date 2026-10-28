using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        //debug world button very wip will replace
        var debugWorldButton = GetNode<Button>("DebugWorldButton");
        debugWorldButton.Pressed += onDebugButtonPressed;
    }

    //load debug world
    private void onDebugButtonPressed()
    {
        var debugWorld = GD.Load<PackedScene>("res://game/scenes/Gameplay/Worldspace/Interiors/debugWorld.tscn");
        var instance = debugWorld.Instantiate();

        var core = GetTree().Root.GetNode<Core>("Core");
        core.AddChild(instance);

        //remove menu
        QueueFree();
    }

}
