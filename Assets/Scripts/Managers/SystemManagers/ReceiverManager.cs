using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReceiverManager : MonoBehaviour
{
    public Signal signal;
    public UnityEvent unityEvent;

    public void Raise()
    {
        unityEvent.Invoke();
    }

    void OnEnable()
    {
        signal.receivers.Add(this);
    }

    void OnDisable()
    {
        signal.receivers.Remove(this);
    }
}
