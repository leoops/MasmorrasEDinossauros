using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {

    public Text coinText;
    public int coinValue;

    public static CoinManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(instance);
        }
    } 

    public void SetCoinText(int valueOfCoins) {
        coinText.text = valueOfCoins.ToString();
    }

}
