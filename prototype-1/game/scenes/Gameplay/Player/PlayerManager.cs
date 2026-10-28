using Godot;
using System;


//handles player possession between entities
public partial class PlayerManager : Node   //need to extend node for _ready()
{
    private BaseEntity ctrlEntity;
    private PlayerControl player;
    private Camera3D cam1;
    public ThirdPOVCam pivot3;
    public override void _Ready()
    {
        cam1 = GetNode<Camera3D>("playerCamera");
        pivot3 = GetNode<ThirdPOVCam>("3rdPOVCam");

        player = new PlayerControl(cam1, pivot3);    
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _Input(InputEvent @event)
    {
        player.MouseCamera(@event);
    }
    public override void _Process(double delta)
    {
        player.JoyCamera(delta);
        player.ZoomCamera(delta);
    }
    public void PossessEntity(BaseEntity entity)
    {
        ctrlEntity?.SetController(new AIControl(ctrlEntity)); //if null go to ai
        ctrlEntity = entity;
        entity.SetController(player);   

        player.SetTargetEntity(entity);

        var camPos = entity.Template.CameraPosition;
        cam1.Reparent(entity);
        cam1.Position = camPos.first; //adjust cam in templates later
        pivot3.Reparent(entity);
        pivot3.Position = camPos.thirdAnchor;

    }
}