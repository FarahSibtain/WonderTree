using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject settingsPanel;

    bool toggle = false;

    int colorIndex = 0;    

    public void OnRedButtonClicked()
    {
        colorIndex = 0;
    }

    public void OnBlueButtonClicked()
    {
        colorIndex = 1;
    }

    public void OnGreenButtonClicked()
    {
        colorIndex = 2;
    }

    public void OnSettingsButtonClicked()
    {
        toggle = !toggle;
        settingsPanel.SetActive(toggle);
    }

    public void OnBtnPlayGameClicked()
    {
        PlayerPrefs.SetInt("colorIndex", colorIndex);
        SceneManager.LoadScene("Game");
    }
}
