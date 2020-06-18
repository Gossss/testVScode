using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private BoxCollider2D m_boxCollider2D;
    private Animator m_animator;

    [Header("Pengaturan Jalan")]
    [Range(300, 700)]
    [SerializeField] private int speed = 350;
    [SerializeField] private bool  facingRgiht = false;

    [Header("Pengaturan Lompat")]
    [Range(300, 700)]
    [SerializeField] private int jumpForce = 500;
    [SerializeField] private float jumpTimer = .2f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //Lompat
    const float radius = .3f;
    private float jumpTimeCounter;

    //Jalan
    private float dirX;
    private SpriteRenderer m_spriteRenderer;

    private void Awake() {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        jumpTimeCounter = jumpTimer;
    }

    private void Update() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
        JumpWithTimer();
    }

    private void FixedUpdate() {
        //Movement();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
    }

    private void Movement() {
        m_rigidbody2D.velocity = new Vector2(dirX * speed * Time.deltaTime, m_rigidbody2D.velocity.y);
        dirX = Input.GetAxisRaw("Horizontal");
        if(m_spriteRenderer != null) {

        }
    }

    private void Animasi() {

    }

    private void JumpWithTimer() {
        if(isGrounded) {
            jumpTimeCounter = jumpTimer;
        }

        if(Input.GetKeyDown(KeyCode.X) && isGrounded) {
            isJumping = true;
        } else if(Input.GetKeyUp(KeyCode.X)){
            isJumping = false;
        }

        if(jumpTimeCounter > 0 && isJumping) {
            m_rigidbody2D.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
            jumpTimeCounter -= Time.fixedDeltaTime;
        } else {
            isJumping = false;
        }
    }
}
    