using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTile : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectCard;
    [SerializeField] private GameObject victoryTag;

    public void StartLevel()
    {
        ProgressionManager.Instance.LoadLevel(int.Parse(this.name));
        levelSelectCard.SetActive(false);
    }

    public void EnableVictoryTag()
    {
        victoryTag.SetActive(true);
    }
}
