using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private int[] inventoryCapacity;
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject[] mirroredChoices;
    private int selection;
    private Vector3 rotation;
    private bool mirror;
    private IDictionary<int, int> inventory;

    public delegate void ChangeSelection(int s);
    public event ChangeSelection OnChangeSelection;
    public event System.Action InventoryUpdate;

    void Awake()
    {
        rotation = Vector3.zero;
        selection = 0;
        mirror = false;
    }

    void Start()
    {
        SetInventory(new Dictionary<int, int> {
            { 0, 0 },
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 }
        });

        for (int i = 0; i < inventoryCapacity.Length; i++)
        {
            UpdateInventory(i, inventoryCapacity[i]);
        }
    }

    public void SetInventory(IDictionary<int, int> newInventory)
    {
        inventory = newInventory;
        InventoryUpdate?.Invoke();
    }

    public void UpdateInventory(int s, int jump)
    {
        inventory[s] += jump;
        InventoryUpdate?.Invoke();
    }

    public int GetInventory(int s)
    {
        return inventory[s];
    }
    
    public IDictionary<int, int> GetInventory()
    {
        return inventory;
    }

    private void traverseSelection(int jump)
    {
        selection = (selection + choices.Length + jump) % choices.Length;
        OnChangeSelection?.Invoke(selection);
    }

    private void setSelection(AbstractProcessor choice)
    {
        selection = System.Array.IndexOf(choices, choice);
        OnChangeSelection?.Invoke(selection);
    }

    public GameObject GetSelection()
    {
        if (mirror)
        {
            return mirroredChoices[selection];
        }
        else
        {
            return choices[selection];
        }
    }

    public int GetSelectionInt()
    {
        return selection;
    }

    public int SelectionToInt(GameObject p)
    {
        int result = 0;
        for(int i = 0; i < choices.Length; i++)
        {
            if (p.name.Split('(')[0] == choices[i].name)
                result = i;
        }
        return result;
    }

    public GameObject IntToSelection(int i)
    {
        return choices[i];
    }

    private void rotateSelection(Vector3 addedRotation)
    {
        rotation += addedRotation;
    }

    private void setRotation(Vector3 newRotation)
    {
        rotation = newRotation;
    }

    public Vector3 GetRotation()
    {
        return rotation;
    }

    public void OnMirror(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            mirror = !mirror;
        }
    }

    public bool GetMirror()
    {
        return mirror;
    }

    public void OnIncrement(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            traverseSelection(1);
        }  
    }

    public void OnDecrement(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            traverseSelection(-1);
        }
    }

    public void OnRotateClockwise(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            rotateSelection(new Vector3(0, 90f, 0));
        }
    }

    public void OnRotateCounterClockwise(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            rotateSelection(new Vector3(0, -90f, 0));
        }
    }
}
