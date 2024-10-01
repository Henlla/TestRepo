using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sig")]
public class Signal : ScriptableObject
{
    public List<ReceiverManager> receivers = new();

    public void Trigger()
    {
        foreach (var receiver in receivers)
        {
            receiver.Raise();
        }
    }
}
