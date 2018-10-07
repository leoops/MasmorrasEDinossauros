using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public int health;
    public float speed;
    public Transform wallCheck;

    private bool touchedWall = false;
    private bool facingRight = true;
    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Start () {

        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        touchedWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (touchedWall) {
            Flip();
        }

    }

    private void FixedUpdate() {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    void Flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed = speed * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Colidiu player");
        }

        if (collision.CompareTag("Attack")) {
            DamageEnemy();
        }
    }

    IEnumerator DamageEffect() {
        float actualSpeed = speed;
        speed = speed * -1;
        sprite.color = Color.red;
        rb2d.AddForce(new Vector2(0f, 200f));
        yield return new WaitForSeconds(0.1f);
        speed = actualSpeed;
        sprite.color = Color.white;
    }

    void DamageEnemy() {
        health--;
        StartCoroutine(DamageEffect());

        if (health < 1) {
            Destroy(gameObject);
        }

    }

}
