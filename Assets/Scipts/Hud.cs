using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

    // Use this for initialization
    public Sprite[] sprites;
    public Image lifeBar;

    public static Hud instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(instance);
        }
    }

    public void RefreshLife(int playerHealth) {
        lifeBar.sprite = sprites[playerHealth];
    }
}
