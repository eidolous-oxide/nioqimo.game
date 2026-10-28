using Godot;
using System;

//template for static entity info

//DONT FORGET TO WRITE
    // [Export]
    // {get; set;}

[GlobalClass]
public partial class EntityTemplate : Resource
{
    [Export] public MoveAbility MoveAbility {get; set;}
    [Export] public MoveStats MoveStats {get; set;}
    [Export] public EntityModel EntityModel {get; set;}
    [Export] public ModelCollision ModelCollision {get; set;}
    [Export] public CameraPosition CameraPosition {get; set;}
}

//template parts

#region movement
    [GlobalClass]
    public partial class MoveAbility : Resource
    {
        [Export] public bool canFly { get; set; } = false;
        [Export]  public bool canDive { get; set; } = true;
    }
    [GlobalClass]
    public partial class MoveStats : Resource
    {
        //methods
        public MoveStats Clone()
        {
            return new MoveStats
            {
                #region base movements
                speedBase = this.speedBase,
                smulRun = this.smulRun,
                smulBack = this.smulBack,
                smulStrafe = this.smulStrafe,
                smulSwim = this.smulSwim,
                smulCrouch = this.smulCrouch,
                smulProne = this.smulProne,
                JumpHeight = this.JumpHeight
                #endregion
            };
        }

        //values below
        #region Values
            //speed notes
                //1.0f stands for '1 voxel per second'. Each voxel is 0.25m. Scale accordingly. see universalphysics.voxscale for more
            #region Base Movements
            [Export] public float speedBase { get; set;} = 6f;  //~1.5m/sec
            [Export] public float smulRun {get; set;} = 1.125f;   //leave mults be
            [Export] public float smulBack {get; set;} = 0.5f;
            [Export] public float smulStrafe {get; set;} = 0.8f;
            [Export] public float smulSwim {get; set;} = 0.4f;
            [Export] public float smulCrouch {get; set;} = 0.6f;
            [Export] public float smulProne {get; set;} = 0.5f;        
            [Export] public float JumpHeight {get; set;} = 2f;  //.5m
            #endregion

        #endregion
    }
#endregion

#region visuals
    [GlobalClass]
    public partial class EntityModel : Resource
    {
        //temp mesh thingie
        [Export] public Mesh modelMesh {get; set;}
        [Export] public string modelPath {get; set;}
        [Export] public float modelScale {get; set;}
        [Export] public float modelRotate {get; set;}
    }
    [GlobalClass]
    public partial class ModelCollision : Resource
    {
        [Export] public Shape3D colShape {get; set;}
    }
    [GlobalClass]
    public partial class CameraPosition : Resource
    {
        [Export] public Vector3 first {get; set;}
        [Export] public Vector3 thirdAnchor {get; set;}
    }

#endregion


