using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adder : AbstractProcessor
{
    override protected void processValues()
    {
        outputValues = new Vector3[outputs.Length];

        for (int i = 0; i < inputValues.Length; i++)
        {
            outputValues[0] += inputValues[i];
        }
        
        if (outputValues[0].x > 255) outputValues[0].x = 255;
        if (outputValues[0].y > 255) outputValues[0].y = 255;
        if (outputValues[0].z > 255) outputValues[0].z = 255;
    }
}
