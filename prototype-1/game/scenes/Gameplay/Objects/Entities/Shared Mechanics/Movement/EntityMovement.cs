using Godot;
using System;

//shared entity movement logic
public class Locomotor
{
    private readonly CharacterBody3D entity;
    private readonly MoveStats moveStats;
    public Locomotor(CharacterBody3D entity, MoveStats moveStats)
    {
        this.entity = entity;
        this.moveStats = moveStats;
    }

    #region Allowance states
        private bool isFlying;
        private bool isSwim;
            private bool isSwimFloat;
            private bool isSwimSub;
        private bool isClimb;
        private bool isFall;
    #endregion
    private Vector3 v3locity;

    public void ProcessLoco(ControlInput input, double delta)
    {
        Vector3 dir = input.MoveDirection;
        

        #region verticality
        if (!entity.IsOnFloor())    //falling
            v3locity.Y -= UniversalPhysics.Gravity * (float)delta;

        if (input.wantJmp && entity.IsOnFloor())    //jumping
            v3locity.Y = Mathf.Sqrt(2 * UniversalPhysics.Gravity * moveStats.JumpHeight);
        #endregion
        
        #region allowance branches
        if (isFlying)   ProcessFly(input, delta);
        else if (isSwim)    ProcessSwim(input, delta);
        else if (isClimb) ProcessClimb(input, delta);
        #endregion
        else
        {
            #region ground movement logic

                if (dir.Length() > 0)
                {
                    float speed = moveStats.speedBase;

                    #region multipliers

                        if (input.wantCrch) speed *= moveStats.smulCrouch;
                        if (input.wantRun)  speed *= moveStats.smulRun;

                        Vector3 localDir = entity.GlobalTransform.Basis.Inverse() * dir;
                       // GD.Print($"localDir: ({localDir.X:F2}, {localDir.Z:F2})  dir: ({dir.X:F2}, {dir.Z:F2})");


                        if (localDir.Z > 0)  speed *= moveStats.smulBack;                       
                        if (input.firstPOV)
                        {
                            if (Mathf.Abs(localDir.X) > 0)   speed *= moveStats.smulStrafe;
                        }
                        
                    #endregion
                    
                    v3locity.X = dir.X * speed;
                    v3locity.Z = dir.Z * speed;

                    //add autojump later
                }
                else
                {
                    #region no input slowdown
                        float slow = 10f;   //might change later
                        v3locity.X = Mathf.Lerp(v3locity.X, 0, slow * (float)delta);
                        v3locity.Z = Mathf.Lerp(v3locity.Z, 0, slow * (float)delta);
                    #endregion
                }

            #endregion
        }

        #region REORIENTATION
            if (input.firstPOV)
            {
                entity.Rotation = new Vector3(0, input.cameraYaw, 0); 
                //GD.Print($"Set entity yaw: {Mathf.RadToDeg(entity.Rotation.Y)}");
                //GD.Print($"Actual Basis Z: {entity.GlobalTransform.Basis.Z}");
            }     
            else    //third pov
            {
                if (input.turning3 != 0)
                {
                    float turnSpeed = 2.5f; //radians per sec
                    float currentYaw = entity.Rotation.Y;
                    float newYaw = currentYaw + (input.turning3 * turnSpeed * (float)delta);
                    entity.Rotation = new Vector3(0, newYaw, 0);
                }
            }
        
        #endregion
        
        //send it to godot
        entity.Velocity = v3locity;
        entity.MoveAndSlide();
        v3locity = entity.Velocity;
    }

    #region alternate locomotion states
    private void ProcessFly(ControlInput input, double delta) {}
    private void ProcessSwim(ControlInput input, double delta) {}
    private void ProcessClimb(ControlInput input, double delta) {}
    #endregion

}

