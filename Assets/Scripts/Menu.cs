using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button loadMenu;

    void Start()
    {
        if (PlayerPrefs.GetInt("LastLevelIndex") == 0)
        {
            loadMenu = GameObject.Find("LoadGame").GetComponent<Button>();
            loadMenu.interactable = false;
        }
    }

    public void NewGame()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        PlayerPrefs.SetInt("LastLevelIndex", level);
        PlayerPrefs.SetInt("PlayerCoins", 0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevelIndex"), LoadSceneMode.Single);
    }
    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                  Application.Quit();
        #endif
    }
}
