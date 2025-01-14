using System;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        } 
    }

    private void Start()
    {
        /*InitListeners();
        SceneManager.sceneLoaded += (_, _) => { InitListeners(); };*/
    }

    private void InitListeners()
    {
        /*// Music
        EventManager.instance.onPlayMusic.AddListener(PlayMusic);
        EventManager.instance.onPauseMusic.AddListener(PauseMusic);
        EventManager.instance.onStopMusic.AddListener(StopMusic);

        // Sfx
        EventManager.instance.onPlaySfx.AddListener(PlaySfx);
        EventManager.instance.onPauseSfx.AddListener(PauseSfx);
        EventManager.instance.onStopSfx.AddListener(StopSfx);*/
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