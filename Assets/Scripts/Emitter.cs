using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : AbstractProcessor
{
    [SerializeField] private Vector3[] values;

    private void Start()
    {
        StartCoroutine(fireAfterSeconds(0.1f));
    }

    void OnEnable()
    {
        EventManager.Instance.FireEmitters += Fire;
        Fire();
    }

    void OnDisable()
    {
        EventManager.Instance.FireEmitters -= Fire;
    }

    override protected void processValues()
    {
        for (int i = 0; i < outputValues.Length; i++)
        {
            outputValues[i] = values[i];
        }
    }

    public void Fire()
    {
        Compute(Vector3.zero, null, new List<AbstractProcessor>() { });
    }

    private IEnumerator fireAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Fire();
    }
}
