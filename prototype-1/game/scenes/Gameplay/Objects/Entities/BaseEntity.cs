using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

//base entity instance
public partial class BaseEntity : CharacterBody3D
{
    //import templates
    private EntityTemplate template;
    public EntityTemplate Template => template;
    public override void _Ready()
    {
        template = EntityRegistry.Get(TemplateStr);
        moveStats = template.MoveStats.Clone();
        locomotor = new Locomotor(this, moveStats);

        //create model
        if (template.ModelCollision?.colShape != null)
        {
            var collision = new CollisionShape3D();
            collision.Shape = template.ModelCollision.colShape;
            AddChild(collision);
        }
        if (!string.IsNullOrEmpty(template.EntityModel?.modelPath))
        {
            var modelScene = GD.Load<PackedScene>(template.EntityModel.modelPath);
            var modelInstance = modelScene.Instantiate<Node3D>();
            modelInstance.Scale = Vector3.One * template.EntityModel.modelScale;
            AddChild(modelInstance);
        }

        controller = new AIControl(this);   //default to ai control
    }


    #region Entity Template
        [Export] public string TemplateStr;
        private MoveStats moveStats;
        private MoveAbility moveAbility;
        private EntityModel entityModel;
        private ModelCollision modelCollision;
        private CameraPosition cameraPosition;


    #endregion

    #region Player or AI control
    private IController controller;
    private Locomotor locomotor;    
    public void SetController(IController entityCtrl)
    {
        controller = entityCtrl;
    }
    public override void _PhysicsProcess(double delta)
    {
        var input = controller.GetControlInput();
        locomotor.ProcessLoco(input, delta);
    }
    #endregion
}