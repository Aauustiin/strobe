using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTemplate : MonoBehaviour
{
    public void loadLevelTemplate()
    {
        SceneManager.LoadScene("LevelTemplate", LoadSceneMode.Additive);
    }
}
