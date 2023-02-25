using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : AbstractProcessor
{
    override protected void processValues()
    {
        outputValues[0] = new Vector3(inputValues[0].x, inputValues[1].y, inputValues[2].z);
    }
}
