using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game_Play"); 
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