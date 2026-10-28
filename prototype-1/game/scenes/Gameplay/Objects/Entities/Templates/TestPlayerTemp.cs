using Godot;
using System;

public partial class TestPlayerTemp : EntityTemplate
{
    public TestPlayerTemp()
    {
        #region movement
            MoveStats = new MoveStats
            {
                smulBack = 0.000001f,   //temporary
            };
            MoveAbility = new MoveAbility
            {
                canFly = false
            };
        #endregion

        #region visuals
            EntityModel = new EntityModel
            {
                modelPath = "res://game/assets/gameplaceholderplayer/gameplaceholderplayer.glb",    //export as 256
                modelScale = 1f,
            };
            ModelCollision = new ModelCollision
            {
                colShape = new CapsuleShape3D
                {
                    Height = 1.7f,
                    Radius = 1.2f,
                }
            };
            CameraPosition = new CameraPosition
            {
                first = new Vector3(0, 6.5f, -0.625f),
                thirdAnchor = new Vector3(0, 3f, 0f)  //figure this out later              
            };
        #endregion
    }
}