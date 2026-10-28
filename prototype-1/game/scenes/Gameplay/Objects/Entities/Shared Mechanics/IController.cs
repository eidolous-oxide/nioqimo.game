using Godot;
using System;

//interface for entity possession from player or ai
public interface IController
{
    ControlInput GetControlInput();
}

public struct ControlInput
{
    #region Movements
        public Vector3 MoveDirection;
        public bool wantJmp;
        public bool wantCrch;
        public bool wantRun;
    #endregion
    #region camera
        public bool firstPOV;
        public float cameraYaw;
        public float turning3;  //x values
    #endregion
}