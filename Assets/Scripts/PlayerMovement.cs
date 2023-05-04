using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpY = 5f; 
    public float jumpX = -1f;

    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TouchControl();
    }

    private void TouchControl()
    {
        if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            if(transform.position.x > touchPos.x && !isJumping)
            {
                rb.velocity = new Vector2(jumpX, jumpY);
                isJumping = true;
            }
            if(transform.position.x < touchPos.x && !isJumping)
            {
                rb.velocity = new Vector2(-jumpX, jumpY);
                isJumping = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
