using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

  private Player playerScript;


  // Use this for initialization
  void Start()
  {
    playerScript = GameObject.Find("Player").GetComponent<Player>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Enemy"))
    {
      playerScript.DamagePlayer();
    }

    if (collision.CompareTag("DeadArea"))
    {
      playerScript.DamageWater();
    }

    if (collision.CompareTag("Coin"))
    {
      playerScript.PlayerGetCurrency(collision.GetComponent<Coin>().coinValue);
      Destroy(obj: collision.gameObject);
    }

    if (collision.CompareTag("Portal"))
    {
      playerScript.PlayerGetCurrency(1);
      Destroy(obj: collision.gameObject);
      playerScript.PlayerNextLevel();

    }
  }
}
