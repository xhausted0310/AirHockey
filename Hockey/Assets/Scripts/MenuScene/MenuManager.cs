using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle MultiplayerToggle;

    public GameObject DifficultyToggles;

    private void Start()
    {
        MultiplayerToggle.onValueChanged
            .AddListener(IsMultiplayerOn => DifficultyToggles.SetActive(!IsMultiplayerOn));
        MultiplayerToggle.isOn = GameValues.isMultiplayer;
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SetMultiplayer(bool isOn)
    {
        GameValues.isMultiplayer = isOn;
    }

    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if(isOn)
        {
            GameValues.Difficulty = GameValues.Difficulties.Easy;
        }
    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValues.Difficulty = GameValues.Difficulties.Medium;
        }
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            GameValues.Difficulty = GameValues.Difficulties.Hard;
        }
    }
    #endregion
}
