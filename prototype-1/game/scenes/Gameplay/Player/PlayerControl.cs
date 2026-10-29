using Godot;
using System;

public class PlayerControl : IController
{

    //setup
    private BaseEntity playerNtt;
    public void SetTargetEntity(BaseEntity entity)
    {
        playerNtt = entity;   
    }

    #region player camera
        private Camera3D cam1;
        private Vector2 camRotate;
        private float mouseSensitivity = 0.002f;
        private float stickSensitivity = 2.0f;

        private Camera3D cam3;
        private ThirdPOVCam pivot3;
        private enum povMode
        {
            first,
            third
        }
        public PlayerControl(Camera3D cam1, ThirdPOVCam pivot3)
        {
            this.cam1 = cam1;
            this.pivot3 = pivot3;
            cam3 = pivot3.GetNode<Camera3D>("3rdPOVSpring/player3rdCamera");
        }
        public void MouseCamera(InputEvent @event)
        {
            if (@event is InputEventMouseMotion mouseMotion)
            {
                camRotate.Y -= mouseMotion.Relative.X * mouseSensitivity;
                camRotate.X -= mouseMotion.Relative.Y * mouseSensitivity;
                camRotate.X = Mathf.Clamp(camRotate.X, -Mathf.Pi/2, Mathf.Pi/2);

                if (mode == povMode.first)
                    cam1.Rotation = new Vector3(camRotate.X, 0, 0);
                else
                    pivot3.Rotation = new Vector3(camRotate.X, camRotate.Y, 0);
            }
        }
        public void JoyCamera(double delta)
        {
            float joyX = Input.GetJoyAxis(0, JoyAxis.RightX);   //0 is first controller
            float joyY = Input.GetJoyAxis(0, JoyAxis.RightY);

            joyX = -joyX;
            joyY = -joyY;
            
            if (Mathf.Abs(joyX) > 0.1f || Mathf.Abs(joyY) > 0.1f)   //tiny deadzone
            {
                camRotate.Y += joyX * stickSensitivity * (float)delta;
                camRotate.X += joyY * stickSensitivity * (float)delta;
                camRotate.X = Mathf.Clamp(camRotate.X, -Mathf.Pi/2, Mathf.Pi/2);
                
                if (mode == povMode.first)
                    cam1.Rotation = new Vector3(camRotate.X, 0, 0);
                else
                    pivot3.Rotation = new Vector3(camRotate.X, camRotate.Y, 0);
            }
        }
        public void ZoomCamera(double delta)
        {
            if (mode == povMode.third)
            {
                float zoomD = 0f;
                if (Input.IsActionPressed(InputIDs.ZoomIn3)) zoomD -= 10f * (float)delta;
                if (Input.IsActionPressed(InputIDs.ZoomOut3)) zoomD += 10f * (float)delta;
                if (zoomD != 0) pivot3.Zoom3(zoomD);
            }
        }
    #endregion

    //player input
    private povMode mode = povMode.first;
    public ControlInput GetControlInput()
    {
        #region base loco/cam
            float turning3 = 0f;
            var dir = Vector3.Zero;

            #region movement
                if (Input.IsActionPressed(InputIDs.MoveFwd)) dir.Z -= 1;    //forward -z
                if (Input.IsActionPressed(InputIDs.MoveBk)) dir.Z += 1;     //back +z
                if (Input.IsActionPressed(InputIDs.MoveL)) dir.X -= 1;       //left -x
                if (Input.IsActionPressed(InputIDs.MoveR)) dir.X += 1;      //right +x
            #endregion

            #region camera
                if (Input.IsActionJustPressed(InputIDs.CamPOV))
                {
                    mode = mode == povMode.first ? povMode.third : povMode.first;
                    cam1.Current = (mode == povMode.first);
                    cam3.Current = (mode == povMode.third);
                }

                if (dir.Length() > 0)
                {
                    if (mode == povMode.third)
                    {
                        turning3 = -dir.X;   

                        var nttOrigin = playerNtt.GlobalTransform.Basis;
                        var fwd = new Vector3(nttOrigin.Z.X, 0, nttOrigin.Z.Z).Normalized();
                        dir = fwd * dir.Z;  //only use z axis, x is for turning
                    }
                    else    //first pov
                    {
                        var camOrigin = cam1.GlobalTransform.Basis;
                        var fwd = new Vector3(camOrigin.Z.X, 0, camOrigin.Z.Z).Normalized();
                        var right = new Vector3(camOrigin.X.X, 0, camOrigin.X.Z).Normalized();
                        dir = (fwd * dir.Z + right * dir.X).Normalized();
                    }
                }
            #endregion

        #endregion

        //GD.Print($"Entity Basis Z: {playerNtt.GlobalTransform.Basis.Z}");
        //GD.Print($"Camera Global Rotation: {cam1.GlobalRotation}");
        
        return new ControlInput
        {
            MoveDirection = dir,
            wantJmp = Input.IsActionJustPressed(InputIDs.MoveJmp),
            wantRun = Input.IsActionPressed(InputIDs.MoveRun),   //make toggle later
            wantCrch = Input.IsActionPressed(InputIDs.MoveCrch),    //make toggle later
            firstPOV = (mode == povMode.first),
            cameraYaw = camRotate.Y,
            turning3 = turning3,
        };
    }


}
