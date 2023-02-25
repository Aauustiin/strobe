using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] private GameObject tooltipText;
    [SerializeField] private GameObject canvas;

    private GameObject processorName;
    private GameObject[] inputTooltips;
    private GameObject[] outputTooltips;

    public void CreateProcessorTooltip(AbstractProcessor p)
    {
        processorName = Instantiate(tooltipText, canvas.transform);
        processorName.transform.position = Camera.main.WorldToScreenPoint(p.transform.position);
        processorName.GetComponent<RectTransform>().anchoredPosition = new Vector2(processorName.GetComponent<RectTransform>().anchoredPosition.x, processorName.GetComponent<RectTransform>().anchoredPosition.y + 75);
        processorName.GetComponent<TextMeshProUGUI>().text = p.name;
        Input[] inputs = p.GetComponentsInChildren<Input>();
        Output[] outputs = p.GetComponentsInChildren<Output>();
        Vector3[] inputValues = p.GetInputValues();
        Vector3[] outputValues = p.GetOutputValues();
        inputTooltips = new GameObject[inputs.Length];
        outputTooltips = new GameObject[outputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            inputTooltips[i] = Instantiate(tooltipText, canvas.transform);
            inputTooltips[i].transform.position = Camera.main.WorldToScreenPoint(inputs[i].transform.position);
            inputTooltips[i].GetComponent<TextMeshProUGUI>().text = inputValues[i].ToString();
            if ((inputTooltips[i].transform.position.x - Camera.main.WorldToScreenPoint(p.transform.position).x) < 0)
                inputTooltips[i].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
            else if ((inputTooltips[i].transform.position.x - Camera.main.WorldToScreenPoint(p.transform.position).x) > 0)
                inputTooltips[i].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
        }
        for (int i = 0; i < outputs.Length; i++)
        {
            outputTooltips[i] = Instantiate(tooltipText, canvas.transform);
            outputTooltips[i].transform.position = Camera.main.WorldToScreenPoint(outputs[i].transform.position);
            outputTooltips[i].GetComponent<TextMeshProUGUI>().text = outputValues[i].ToString();
            if ((outputTooltips[i].transform.position.x - Camera.main.WorldToScreenPoint(p.transform.position).x) < 0)
                outputTooltips[i].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
            else if ((outputTooltips[i].transform.position.x - Camera.main.WorldToScreenPoint(p.transform.position).x) > 0)
                outputTooltips[i].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
        }
    }

    public void ClearProcessorTooltips()
    {
        if (processorName != null)
        {
            Destroy(processorName);
        }
        if (inputTooltips != null)
        {
            foreach (GameObject i in inputTooltips)
            {
                Destroy(i);
            }
        }
        if (outputTooltips != null)
        {
            foreach (GameObject i in outputTooltips)
            {
                Destroy(i);
            }
        }
    }

    public void drawRecieverTarget(Reciever r)
    {
        GameObject recieverTarget = Instantiate(tooltipText, canvas.transform);
        recieverTarget.transform.position = Camera.main.WorldToScreenPoint(r.transform.position);
        recieverTarget.GetComponent<TextMeshProUGUI>().text = "Target: " + r.GetTargetValues()[0].ToString();
    }
}
