using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LoseTxt;

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;

    public PuckScript puckScript;
    public PlayerMovement playerMovement;
    public AIScript aIScript;

    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if(didAiWin)
        {
            audioManager.PlayLostGame();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(true);
        }
        else
        {
            audioManager.PlayWonGame();
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScore();
        puckScript.CenterPuck();
        playerMovement.ResetPosition();
        aIScript.ResetPosition(); 
    }

    public void ShowMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
