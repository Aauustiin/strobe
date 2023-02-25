using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : AbstractProcessor
{
    [SerializeField] private Vector3[] targetValues;

    override protected void processValues()
    {
        bool flag = true;

        for(int i = 0; i < inputValues.Length; i++)
        {
            flag = flag && (inputValues[i] == targetValues[i]);
        }

        if (flag == true)
        {
            EventManager.Instance.TriggerRecieverSatisfied(this);
        }
        else
        {
            EventManager.Instance.TriggerRecieverUnsatisfied(this);
        }
    }

    public Vector3[] GetTargetValues()
    {
        return targetValues;
    }

    public override void Reset()
    {
        EventManager.Instance.TriggerRecieverUnsatisfied(this);
        base.Reset();
    }
}
