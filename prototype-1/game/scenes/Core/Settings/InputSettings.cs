using Godot;
using System;
using System.Collections.Generic;

public static class InputIDs
{
#region gameplay
    #region player movements
        #region terrestrial
        public const string MoveFwd = "keyMoveFwd";
        public const string MoveBk = "keyMoveBk";
        public const string MoveL = "keyMoveL";
        public const string MoveR = "keyMoveR";
        public const string MoveJmp = "keyMoveJmp";
        public const string MoveCrch = "keyMoveCrch";
        public const string MoveCrwl = "keyMoveCrwl";
        public const string MoveRun = "keyMoveRun";
        #endregion
    #endregion

    //add later:
    //idle, nautical, aerial

    #region camera
        public const string CamPOV = "keyCamPOV";
        public const string ZoomIn3 = "keyZoomIn3";
        public const string ZoomOut3 = "keyZoomOut3";
    #endregion

#endregion
}
public static class InputSettings
{
    private static readonly Dictionary<string, InputEvent[]> DefaultBindingsList = new()
    {

    #region gameplay
        #region player movements
            #region terrestrial
            {InputIDs.MoveFwd, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.W},
                new InputEventJoypadMotion
                {
                    Axis = JoyAxis.LeftY,   //push left joystick up
                    AxisValue = -1.0f
                }
            }},
            {InputIDs.MoveBk, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.S},
                new InputEventJoypadMotion
                {
                    Axis = JoyAxis.LeftY,
                    AxisValue = 1.0f
                }    
            }},
            {InputIDs.MoveL, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.A},
                new InputEventJoypadMotion
                {
                    Axis = JoyAxis.LeftX,
                    AxisValue = -1.0f
                }
            }},
            {InputIDs.MoveR, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.D},
                new InputEventJoypadMotion
                {
                    Axis = JoyAxis.LeftX,
                    AxisValue = 1.0f
                }
            }},
            {InputIDs.MoveJmp, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.Space},
                new InputEventJoypadButton
                {
                    ButtonIndex = JoyButton.A   //bottom face button
                }
            }},
            {InputIDs.MoveRun, new InputEvent[]
            {
                new InputEventKey { Keycode = Key.Shift},
                new InputEventJoypadButton
                {
                    ButtonIndex = JoyButton.LeftStick   //make toggle
                }
            }},
            {InputIDs.MoveCrch, new InputEvent[]
            {
                new InputEventKey { Keycode = Key.Ctrl},
                new InputEventJoypadButton
                {
                    ButtonIndex = JoyButton.B   //is this the right one?? right face button
                }
            }},
            #endregion
        #endregion

        #region camera
            {InputIDs.CamPOV, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.Key0},
                new InputEventJoypadButton
                {
                    ButtonIndex = JoyButton.RightStick
                }
            }},
            {InputIDs.ZoomIn3, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.Equal}
            }},
            {InputIDs.ZoomOut3, new InputEvent[]
            {
                new InputEventKey {Keycode = Key.Minus}
            }}
        #endregion

    #endregion

        //add others later
    };
    public static void RegisterDefaultBinds()
    {
        foreach (var input in DefaultBindingsList)
        {
            RegisterGDAction(input.Key, input.Value);
        }
    }
    private static void RegisterGDAction(string action, InputEvent[] events)
    {
        if (!InputMap.HasAction(action))
            InputMap.AddAction(action);
        
        //InputMap.ActionEraseEvents(action);
        foreach (var ev in events)
        {
            InputMap.ActionAddEvent(action, ev);
        }
    }
}