using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private Collision2D collision2D;


    public float jumpY = 5f; 
    public float jumpX = -1f;
    private bool isCenter;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Centering(collision2D);
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
        if(other.gameObject.tag == "Ground" || other.gameObject.tag =="NormalPlatform" 
        || other.gameObject.tag =="AppearingPlatform"|| other.gameObject.tag =="BrokenPlatform")
        {
            isJumping = false;
            Centering(other);
        }
    }

    private void Centering(Collision2D other)
    {
        isCenter=false;
            if(!isCenter)
            {
                Vector3 characterPos = transform.position; // karakterin pozisyonunu al
                Vector3 centerPos = other.transform.position; // hedef nesnenin pozisyonunu al
                characterPos.x = centerPos.x; // karakterin x pozisyonunu hedef nesnenin x pozisyonuna eşitle
                transform.position = characterPos; // karakterin pozisyonunu güncelle
                isCenter = true;
            }
            else
            {
                isCenter = false;
            }
    }
}
