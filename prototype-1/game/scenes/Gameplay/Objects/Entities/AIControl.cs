using Godot;
using System;

//AI control module
public class AIControl : IController
{
    //do later

    private BaseEntity baseEntity;
    public AIControl(BaseEntity entity)
    {
        baseEntity = entity;
    }

    public ControlInput GetControlInput()
    {
        //do later
        return new ControlInput
        {
            //do later
        };
    }
}