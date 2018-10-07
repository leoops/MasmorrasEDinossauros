using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

    private Player playerScript;

    public AudioClip fxCoin;
	// Use this for initialization
	void Start () {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            playerScript.DamagePlayer();
        }

        if (collision.CompareTag("Water")) {
            playerScript.DamageWater();
        }

        if (collision.CompareTag("Coin")) {
            Debug.Log("Coletou");
            SoundManager.instance.PlaySound (fxCoin);
            Destroy(collision.gameObject);
        }
    }
}
