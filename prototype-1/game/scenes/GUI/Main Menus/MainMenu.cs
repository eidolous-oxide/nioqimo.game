using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        //bring up saves screen
        var playButton = GetNode<Button>("PlayButton");
        playButton.Pressed += onPlayPressed;

        //settings will do nothing rn
        var settingsButton = GetNode<Button>("SettingsButton");
        settingsButton.Pressed += onSettingsPressed;

        var quitButton = GetNode<Button>("QuitButton");
        quitButton.Pressed += onQuitPressed;
    }

    #region play
    private void onPlayPressed()
    {
        var worldSelect = GD.Load<PackedScene>("res://game/scenes/GUI/Main Menus/Worlds/WorldSelect.tscn");
        var instance = worldSelect.Instantiate();

        var gui = GetTree().Root.GetNode<Node>("Core/GUI");
        gui.AddChild(instance);

        QueueFree();
    }
    #endregion

    #region settings
    private void onSettingsPressed()
    {
        
    }
    #endregion

    #region quit
    private void onQuitPressed()
    {
        GetTree().Quit();
    }
    #endregion

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
