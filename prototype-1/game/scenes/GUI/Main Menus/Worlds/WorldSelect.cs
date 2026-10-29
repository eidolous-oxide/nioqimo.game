using Godot;
using System;

public partial class WorldSelect : Control
{
    public override void _Ready()
    {
        var backButton = GetNode<Button>("BackButton");
        backButton.Pressed += onBackPressed;

        var newWorld = GetNode<Button>("NewWorld");
        newWorld.Pressed += onNewWorldPressed;
    }

    #region new world
    private void onNewWorldPressed()
    {
        var createWorld = GD.Load<PackedScene>("res://game/scenes/GUI/Main Menus/Worlds/CreateWorld.tscn");
        GetTree().Root.GetNode<Node>("Core/GUI").AddChild(createWorld.Instantiate());
        QueueFree();
    }
    #endregion

    private void onBackPressed()
    {
        var mainMenu = GD.Load<PackedScene>("res://game/scenes/GUI/Main Menus/MainMenu.tscn");
        var instance = mainMenu.Instantiate();
        
        var gui = GetTree().Root.GetNode<Node>("Core/GUI");
        gui.AddChild(instance);

        QueueFree();
    }

}
