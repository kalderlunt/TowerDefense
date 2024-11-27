using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game_Play"); 
    }
    
    public void ChangeSceneBy(string sceneName)
    {
        SceneManager.LoadScene($"Game_{sceneName}"); 
    }

    public void OpenTowerSelection()
    {
        SceneManager.LoadScene("TowerSelectionScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}