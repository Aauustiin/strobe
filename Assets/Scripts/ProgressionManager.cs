using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    private int currentLevel;
    [SerializeField] private string[] levels;
    private IDictionary<string, bool> progressionStatus;

    [SerializeField] private GameObject mainMenuCard;
    [SerializeField] private GameObject levelSelectCard;
    [SerializeField] private GameObject victoryMessage;

    public static ProgressionManager Instance { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        progressionStatus = new Dictionary<string, bool> { };
        foreach(string l in levels)
        {
            progressionStatus.Add(l, false);
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(levels[level], LoadSceneMode.Additive);
        currentLevel = level;
    }

    public void LevelComplete()
    {
        progressionStatus[levels[currentLevel]] = true;
        LoadLevelSelectFromPuzzle();
    }

    public void LoadLevelSelectFromPuzzle()
    {
        SceneManager.UnloadSceneAsync(levels[currentLevel]);
        levelSelectCard.SetActive(true);
        bool flag = true;
        foreach(KeyValuePair<string, bool> x in progressionStatus)
        {
            flag = flag && x.Value;
            if (x.Value == true)
            {
                GameObject.Find(System.Array.FindIndex(levels, y => y == x.Key).ToString()).GetComponent<LevelSelectTile>().EnableVictoryTag();
            }
        }
        if (flag == true)
        {
            victoryMessage.SetActive(true);
        }
    }

    public void LoadMainMenuFromPuzzle()
    {
        SceneManager.UnloadSceneAsync(levels[currentLevel]);
        mainMenuCard.SetActive(true);
    }
}
