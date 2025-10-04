using System;
using UnityEngine;

public static class ObjectFactory
{
    public static GameObject CreateObject<T>(T objectType, Vector2 position) where T : ISpawnable
    {
        return objectType.Spawn(position);
    }
}

public interface ISpawnable
{
    GameObject Spawn(Vector2 position);
    void Despawn();

    void Initialize(Vector2 force);
}