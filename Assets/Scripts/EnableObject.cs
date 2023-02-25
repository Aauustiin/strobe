using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    public void enableObject()
    {
        obj.SetActive(true);
    }
}
