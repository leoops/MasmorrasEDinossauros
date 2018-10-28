using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalGame : MonoBehaviour {

    public GameObject finishMessage;
    public Text coinsFinal;
    private bool finished = false;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && finished)
        {
            var level = 0;
            PlayerPrefs.SetInt("PlayerCoins", 0);
            SceneManager.LoadScene(level, LoadSceneMode.Single);
            PlayerPrefs.SetInt("LastLevelIndex", level);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            finishMessage.SetActive(true);
            coinsFinal.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
            finished = true;
        }
    }
}
