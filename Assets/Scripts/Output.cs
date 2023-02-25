using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MonoBehaviour
{
    private LineRenderer beam;

    void Awake()
    {
        beam = GetComponentInChildren<LineRenderer>();
        beam.enabled = false;
    }

    public void ProduceOutput(Vector3 value, List<AbstractProcessor> processorHistory)
    {
        if (value != Vector3.zero)
        {
            RaycastHit hitData;
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hitData, Mathf.Infinity))
            {
                visualiseBeam(value, hitData);

                Input input = hitData.collider.GetComponent<Input>();

                if (input != null)
                    input.AcceptInput(value, new List<AbstractProcessor>(processorHistory));
            }
            else
            {
                beam.enabled = false;
            }
        }
        else
        {
            beam.enabled = false;
        }
    }

    private void visualiseBeam(Vector3 value, RaycastHit hitData)
    {
        beam.enabled = true;
        Vector3[] beamPositions = { Vector3.zero, transform.InverseTransformPoint(hitData.point) };
        beam.SetPositions(beamPositions);
        beam.startColor = new Color(value.x / 255f, value.y / 255f, value.z / 255f);
        beam.endColor = new Color(value.x / 255f, value.y / 255f, value.z / 255f);
    }

    public void ResetBeam()
    {
        beam.enabled = false;
    }
}
