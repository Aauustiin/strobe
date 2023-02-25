using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SelectionHotbar : MonoBehaviour
{
    [SerializeField] private GameObject[] selections;
    [SerializeField] private GameObject highlight;

    private SelectionManager selectionManager;

    void Awake()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    void OnEnable()
    {
        selectionManager.OnChangeSelection += OnSelectionChange;
        selectionManager.InventoryUpdate += OnInventoryUpdate;
    }

    void OnDisable()
    {
        selectionManager.OnChangeSelection -= OnSelectionChange;
        selectionManager.InventoryUpdate -= OnInventoryUpdate;
    }

    public void OnSelectionChange(int selection)
    {
        highlight.GetComponent<RectTransform>().sizeDelta = selections[selection].GetComponent<TextMeshProUGUI>().GetRenderedValues(true);
        highlight.transform.position = selections[selection].transform.position - new Vector3((200f - highlight.GetComponent<RectTransform>().sizeDelta.x) / 2, (highlight.GetComponent<RectTransform>().sizeDelta.y - 50f) / 2, 0f);
    }

    public void OnInventoryUpdate()
    {
        IDictionary<int, int> inventory = selectionManager.GetInventory();
        for (int i = 0; i < selections.Length; i++)
        {
            selections[i].GetComponent<TextMeshProUGUI>().text = selections[i].name + " x " + inventory[i];
        }
    }
}
