using Godot;
using System;

public partial class DebugWorld : Node3D
{
    public override void _Ready()
    {
        //load player scene
        var playerScene = GD.Load<PackedScene>("res://game/scenes/Gameplay/Player/player.tscn");
        var player = playerScene.Instantiate<PlayerManager>();
        AddChild(player);
        //spawn player
        var entity = EntitySpawner.SpawnThis("TestPlayer", new Vector3(0,100,0));
        AddChild(entity);
        //transfer control
        player.PossessEntity(entity);
    }

}
