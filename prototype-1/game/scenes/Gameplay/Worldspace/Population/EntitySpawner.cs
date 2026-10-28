using Godot;
using System;

public static class EntitySpawner
{
    public static BaseEntity SpawnThis(string Name, Vector3 pos)
    {
        var entity = new BaseEntity();
        entity.TemplateStr = Name;
        entity.Position = pos;
        return entity;
    }
}