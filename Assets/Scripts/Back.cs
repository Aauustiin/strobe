using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public void BackToLS()
    {
        ProgressionManager.Instance.LoadLevelSelectFromPuzzle();
    }

    public void BackToMM()
    {
        ProgressionManager.Instance.LoadMainMenuFromPuzzle();
    }
}
