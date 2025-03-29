using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Coinracer");
    }
    
    public  void MainMenu()
    {
        SceneManager.LoadScene("Start_game");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToCredit()
    {
        SceneManager.LoadScene("Credit"); // ต้องตั้งชื่อ Scene เครดิตว่า Credit
    }
}
