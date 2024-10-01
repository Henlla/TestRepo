using System.Collections.Generic;
using UnityEngine;

public class BaseRecordSO<T> : ScriptableObject
{
    public List<T> ListObject = new List<T>();

    public virtual void SaveList(T obj)
    {
        ListObject.Add(obj);
    }

    public virtual List<T> GetList()
    {
        return ListObject;
    }
}
