using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {


    public float speed;
    public int jumpForce;
    public int health;
    public int coins;
    public Transform groundCheck;

    private bool invunerable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    private Animator anim;

    public float attackRate;
    public Transform spawnAttack;
    public GameObject attackPrefab;
    public GameObject crown;

    public AudioClip fxJump;
    public AudioClip fxAttack;
    public AudioClip fxHurt;

    private float nextAttack = 0f;

    private CameraScript cameraScript;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        CoinManager.instance.SetCoinText(coins);
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }
	
	// Update is called once per frame
	void Update () {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded) {
            jumping = true;
            SoundManager.instance.PlaySound(fxJump);
        }
        SetAnimations();

        if (Input.GetButton("Fire1") && grounded && Time.time > nextAttack) {
            Attack();
            SoundManager.instance.PlaySound (fxAttack);
        }

	}

    void FixedUpdate() {

        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if ((move < 0f && facingRight) || (move > 0f && facingRight == false)) {
            Flip();
        }

        if (jumping) {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }

    }

    void Flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void SetAnimations() {
        anim.SetFloat("VelY", rb2d.velocity.y);
        anim.SetBool("JumpFall", !grounded);
        anim.SetBool("Walk", rb2d.velocity.x != 0f && grounded);
    }

    void Attack() {
        anim.SetTrigger("Shot");
        nextAttack = Time.time + attackRate;

        GameObject cloneAttack = Instantiate(attackPrefab, spawnAttack.position, spawnAttack.rotation);

        if (!facingRight) {
            cloneAttack.transform.eulerAngles = new Vector3(180, 0, 180);
        }
    }

    IEnumerator DamageEffect() {

        cameraScript.ShakeCamera(0.5f, 0.2f);

        for (float i = 0f; i < 1f; i += 0.1f) {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invunerable = false;

    }

    public void PlayerGetCurrency(int value) {
        this.coins += value;
        CoinManager.instance.SetCoinText(this.coins);
    }

    public void DamagePlayer() {
        if (!invunerable) {
            invunerable = true;
            health--;
            StartCoroutine(DamageEffect());

            SoundManager.instance.PlaySound (fxHurt);
            Hud.instance.RefreshLife(health);

            if (health < 1) {
                Debug.Log("Morreu");
                KingDeath();
                Invoke("ReloadLevel", 3f);
                gameObject.SetActive(false);
            }
        }
    }

    public void DamageWater() {
        health = 0;
        Hud.instance.RefreshLife(health);
        KingDeath();
        Invoke("ReloadLevel", 3f);
        gameObject.SetActive(false);
    }

    void KingDeath() {

        GameObject cloneCrown = Instantiate(crown, transform.position, Quaternion.identity);
        Rigidbody2D rb2dCrown = cloneCrown.GetComponent<Rigidbody2D>();
        rb2dCrown.AddForce(Vector3.up * 300);

    }

    void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PlayerNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DontDestroyOnLoad(this);
    }

}
