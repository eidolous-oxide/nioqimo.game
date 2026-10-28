using Godot;
using System;

//this aint earth
public static class UniversalPhysics
{
    public const float Gravity = 10.58f; //m/s²
    
    //SCALE
        //voxels are locked to one unit = 1m
        //content is instead scaled up by 4 to treat one unit as .25m
        //i guess its all made up anyway
        //but math needs to take this into account
    public const float VoxScale = 4f;
}