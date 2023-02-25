using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    public void disableObject()
    {
        obj.SetActive(false);
    }
}
