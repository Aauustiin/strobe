using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : AbstractProcessor
{
    override protected void processValues()
    {
        for (int i = 0; i < inputValues.Length; i++)
        {
            outputValues[0] = new Vector3(inputValues[i].x, 0, 0);
            outputValues[1] = new Vector3(0, inputValues[i].y, 0);
            outputValues[2] = new Vector3(0, 0, inputValues[i].z);
        }
    }
}
