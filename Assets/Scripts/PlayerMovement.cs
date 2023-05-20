using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpY = 17f; 
    public float jumpX = -2.9f;
    private bool isCenter;
    private bool isJumping = false;
    private bool isFalling = false;
    private float fallTime = 0f;
    [SerializeField] float maxFallTime = 0.3f;
    private bool onBrokenPlatform;


    Vector3 characterPos;
    Vector3 centerPos;
    Vector2 touchPos;    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TouchControl();
        if(isFalling && rb.velocity.y <0.1f)
        {
            fallTime += Time.deltaTime;
            if(fallTime>=maxFallTime)
            {
                FindObjectOfType<GameSceneController>().DeathLogic();
            }
        }
    }

    private void TouchControl()
    {
        if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began && gameObject.tag !="SettingsButton")
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Jump();
        }
    }
    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag =="NormalPlatform" 
        || other.gameObject.tag =="Appear"|| other.gameObject.tag =="BrokenPlatform")
        {
            isJumping = false;
            isFalling = false;
            fallTime = 0f;
            Centering(other);
            if(other.gameObject.tag =="NormalPlatform"|| other.gameObject.tag =="BrokenPlatform")
                FindObjectOfType<Score>().UpdateScore();
                
        }
        if(other.gameObject.tag=="CloudPlatform")
        {
            FindObjectOfType<GameSceneController>().DeathLogic();
        }
        if(other.gameObject.tag=="DeathPlatform")
        {
            FindObjectOfType<GameSceneController>().DeathLogic();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "BrokenPlatform")
        {
            onBrokenPlatform = true;
            StartCoroutine(BrokenPlatform(other));
        }
        if(other.gameObject.tag == "Star")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Health")
        {
            FindObjectOfType<Timer>().ExtraTime(5);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Reverse")
        {
            FindObjectOfType<CameraContoller>().StartCoroutine(FindObjectOfType<CameraContoller>().RotateCamera());
            FindObjectOfType<Timer>().ExtraTime(7);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "BrokenPlatform")
        {
            onBrokenPlatform = false;
        }
    }
    
    IEnumerator BrokenPlatform(Collider2D other)
    {
        yield return new WaitForSeconds(1f);
        if(onBrokenPlatform)
        {
            Destroy(other.gameObject);
            FindObjectOfType<GameSceneController>().DeathLogic();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    private void Centering(Collision2D other)
    {
        isCenter=false;
            if(!isCenter)
            {
                characterPos = transform.position;
                centerPos = other.transform.position;
                characterPos.x = centerPos.x; 
                transform.position = characterPos;
                isCenter = true;
            }
            else
            {
                isCenter = false;
            }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Appear" && rb.velocity.y<0.1f)
        {
            FindObjectOfType<GameSceneController>().DeathLogic();
        }
    }


    private void Jump()

    {
        if(transform.position.x > touchPos.x && !isJumping && !isFalling)
        {
            rb.velocity = new Vector2(jumpX, jumpY);
            isJumping = true;
            isFalling = true;
        }
        if(transform.position.x < touchPos.x && !isJumping && !isFalling)
        {
            rb.velocity = new Vector2(-jumpX, jumpY);
            isJumping = true;
            isFalling = true;
        }
    }

}
