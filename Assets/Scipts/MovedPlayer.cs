using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovedPlayer : MonoBehaviour {

    public float speed;
    public int health;
    public int jumpForce;
    public Transform groudCheck;

    private bool grounded = false;
    private bool jumping = false;
    private bool facingRigth = true;

    private new Rigidbody2D rigidbody2D;
    private new Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groudCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }
        SetAnimations();
    }

    public void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);
        if ((move < 0f && facingRigth) || (move > 0f && !facingRigth))
        {
            Flip();
        }
        if (jumping)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }
    }

    void Flip()
    {
        facingRigth = !facingRigth;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void ReloadLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    void SetAnimations()
    {
        animator.SetFloat("VelY", rigidbody2D.velocity.y);
        animator.SetBool("JumpFall", !grounded);
        animator.SetBool("Run", rigidbody2D.velocity.x != 0f && grounded);
    }
    public void PlayerNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1; ;
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        PlayerPrefs.SetInt("LastLevelIndex", nextLevel);
    }
}
