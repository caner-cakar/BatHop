using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experimental : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    public float jumpTime = 0.5f;

    private bool isJumping = false;
    private float jumpTimeCounter;
    private float moveDirection = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Karakterin zıplama yönü mouse ile belirleniyor
        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            moveDirection = Input.mousePosition.x > Screen.width / 2f ? 1f : -1f;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
        }

        if (isJumping)
        {
            // Zıplayan karakter yerçekimi etkisinde hareket eder
            rb.velocity += Vector2.down * Time.deltaTime * Physics2D.gravity.y;

            jumpTimeCounter -= Time.deltaTime;
            if (jumpTimeCounter <= 0)
            {
                isJumping = false;
            }
        }
        else
        {
            // Zıplamayan karakter sadece sağa/sola hareket edebilir
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }
}
