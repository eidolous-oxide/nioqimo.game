using Godot;
using System;

public partial class CreateWorld : Control
{
    public override void _Ready()
    {
        var backButton = GetNode<Button>("BackButton");
        backButton.Pressed += onBackPressed;        
    }

    private void onBackPressed()
    {
        var worldSelect = GD.Load<PackedScene>("res://game/scenes/GUI/Main Menus/Worlds/WorldSelect.tscn");
        var instance = worldSelect.Instantiate();
        
        var gui = GetTree().Root.GetNode<Node>("Core/GUI");
        gui.AddChild(instance);

        QueueFree();
    }

    
}
