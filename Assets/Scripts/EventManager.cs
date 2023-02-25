using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get { if (_instance == null) { _instance = FindObjectOfType<EventManager>(); } return _instance; } }

    private static EventManager _instance;

    public event System.Action FireEmitters, ResetProcessors;

    public delegate void OnRecieverSatisfied(Reciever r);
    public event OnRecieverSatisfied RecieverSatisfied;

    public void TriggerRecieverSatisfied(Reciever r)
    {
        RecieverSatisfied?.Invoke(r);
    }

    public delegate void OnRecieverUnsatisfied(Reciever r);
    public event OnRecieverUnsatisfied RecieverUnsatisfied;

    public void TriggerRecieverUnsatisfied(Reciever r)
    {
        RecieverUnsatisfied?.Invoke(r);
    }

    public void TriggerResetProcessors()
    {
        ResetProcessors?.Invoke();
    }

    public void TriggerFireEmitters()
    {
        FireEmitters?.Invoke();
    }
}
