using System;
using UnityEngine;

public class BaseObjectSO : ScriptableObject
{
    protected string Id { get; private set; }
    public string Name;
    public Sprite Sprite;
    public ObjectPlaceable Prefab;
    [SerializeField] protected ObjectType ObjectType;

    public BaseObjectSO CopyObject()
    {
        BaseObjectSO instance = Instantiate(this);
        return instance;
    }

    public string GetId()
    {
        return Id;
    }

    public ObjectType GetObjectType()
    {
        return ObjectType;
    }

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }
}

public enum ObjectType
{
    Enclosure,
    Factory,
    Shop
}
