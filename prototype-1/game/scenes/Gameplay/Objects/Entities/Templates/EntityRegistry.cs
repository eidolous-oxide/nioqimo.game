using Godot;
using System;
//entity template library
public static class EntityRegistry
{
    public static EntityTemplate Get(string name)
    {
        return name switch
        {
            #region entity names
            "TestPlayer" => testPlayer,
            #endregion
            
            _ => throw new System.Exception($"Unknown entity template: {name}")
        };
    }

    #region Entity Template List
    public static readonly TestPlayerTemp testPlayer = new TestPlayerTemp();
    #endregion
}