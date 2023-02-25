using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProcessor : MonoBehaviour
{
    /* The index of an input in inputs should be the same as the index of that input in inputValues,
     * the same is true for output and outputValues. */

    protected Input[] inputs;
    protected Output[] outputs;
    protected Vector3[] inputValues;
    protected Vector3[] outputValues;

    void OnEnable()
    {
        EventManager.Instance.ResetProcessors += Reset;
    }

    void OnDisable()
    {
        EventManager.Instance.ResetProcessors -= Reset;
    }

    public void Compute(Vector3 inputValue, Input input, List<AbstractProcessor> processorHistory)
    {
        if (!processorHistory.Contains(this))
        {
            GetInputValues();
            GetOutputValues();
            acceptInput(inputValue, input);
            processValues();
            processorHistory.Add(this);
            passOutput(processorHistory);
        }
        else
        {
            Debug.Log("Error: Loop Detected");
        }
    }

    public Vector3[] GetInputValues()
    {
        if ( (inputs == null) || (inputValues == null) )
        {
            inputs = GetComponentsInChildren<Input>();
            inputValues = new Vector3[inputs.Length];
        }

        return inputValues;
    }

    public Vector3[] GetOutputValues()
    {
        if ((outputs == null) || (outputValues == null))
        {
            outputs = GetComponentsInChildren<Output>();
            outputValues = new Vector3[outputs.Length];
        }

        return outputValues;
    }

    private void acceptInput(Vector3 inputValue, Input input)
    {
        if (input != null)
        {
            int index = System.Array.FindIndex(inputs, x => x == input);
            inputValues[index] = inputValue;
        }
    }
    
    virtual protected void processValues()
    {
        Debug.Log("Warning: processValues not implemented");
    }

    private void passOutput(List<AbstractProcessor> processorHistory)
    {
        for(int i = 0; i < outputValues.Length; i++)
        {
            outputs[i].ProduceOutput(outputValues[i], processorHistory);
        }
    }

    virtual public void Reset()
    {
        inputs = GetComponentsInChildren<Input>();
        inputValues = new Vector3[inputs.Length];
        outputs = GetComponentsInChildren<Output>();
        outputValues = new Vector3[outputs.Length];

        foreach (Output o in outputs)
        {
            o.ResetBeam();
        }
    }
}
