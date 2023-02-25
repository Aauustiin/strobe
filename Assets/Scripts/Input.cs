using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour
{
    private AbstractProcessor processor;

    void Awake()
    {
        processor = GetComponentInParent<AbstractProcessor>();
    }

    public void AcceptInput(Vector3 value, List<AbstractProcessor> processorHistory)
    {
        if (processor != null)
            processor.Compute(value, this, processorHistory);
    }
}