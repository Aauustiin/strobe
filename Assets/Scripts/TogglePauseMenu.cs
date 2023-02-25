using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePauseMenu : MonoBehaviour
{
    private bool paused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Puzzle puzzle;

    void Awake()
    {
        paused = false;
    }

    public void TogglePause()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);
        puzzle.enabled = !paused;
    }
}
