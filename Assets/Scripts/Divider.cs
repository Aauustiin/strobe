using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : AbstractProcessor
{
    override protected void processValues()
    {
        outputValues[0] = new Vector3(Mathf.Ceil(inputValues[0].x/2), Mathf.Ceil(inputValues[0].y/2), Mathf.Ceil(inputValues[0].z/2));
        outputValues[1] = new Vector3(Mathf.Floor(inputValues[0].x/2), Mathf.Floor(inputValues[0].y/2), Mathf.Floor(inputValues[0].z/2));
    }
}
