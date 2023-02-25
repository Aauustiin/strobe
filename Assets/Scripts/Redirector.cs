using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirector : AbstractProcessor
{
    override protected void processValues()
    {
        for (int i = 0; i < inputValues.Length; i++)
        {
            outputValues[i] = inputValues[i];
        }
    }
}
