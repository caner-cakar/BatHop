using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    CameraController cameraController;
    Timer timerController;
    GameSceneController gameSceneController;
    Score scoreController;

    SpriteRenderer playerSprite;
    private Rigidbody2D rb;
    private Animator anim;

    public float jumpY = 17f; 
    public float jumpX = -2.9f;

    private bool isCenter;
    private bool isJumping = false;
    private bool isFalling = false;
    public bool isDead = false;

    private float fallTime = 0f;
    [SerializeField] float maxFallTime = 0.3f;

    private bool onBrokenPlatform;

    Vector3 characterPos;
    Vector3 centerPos;
    Vector2 touchPos;    

    float time;
    

    void Start()
    {
        scoreController = FindObjectOfType<Score>();
        gameSceneController = FindObjectOfType<GameSceneController>();
        timerController = FindObjectOfType<Timer>();
        cameraController = FindObjectOfType<CameraController>();
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        if(PlayerPrefs.GetInt("selectedCharacter",0) == 0)
        {
            anim = GetComponent<Animator>();
        }
        
    }

    void Update()
    {
        time += Time.deltaTime;
        TouchControl();
        if(isFalling && rb.velocity.y <0.1f)
        {
            fallTime += Time.deltaTime;
            if(fallTime>=maxFallTime)
            {
                DeathFall();
                StartCoroutine(gameSceneController.DeathLogic());
            }
        }
        if(PlayerPrefs.GetInt("selectedCharacter",0) == 0)
        {
            UpdateAnimation();
        }
    }

    private void TouchControl()
    {
        if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                Jump();
            }
            
        }
    }
    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag =="NormalPlatform" 
        || other.gameObject.tag =="BrokenPlatform" || other.gameObject.tag=="Reverse")
        {
            isJumping = false;
            isFalling = false;
            fallTime = 0f;
            Centering(other);
            if((other.gameObject.tag =="NormalPlatform"|| other.gameObject.tag=="Reverse") && cameraController.isCamera == false)
                scoreController.UpdateScore(10);
            if((other.gameObject.tag =="NormalPlatform"|| other.gameObject.tag=="Reverse") && cameraController.isCamera == true)
                scoreController.UpdateScore(15);
            if (other.gameObject.tag == "BrokenPlatform" && cameraController.isCamera == false)
                scoreController.UpdateScore(15);
            if (other.gameObject.tag == "BrokenPlatform" && cameraController.isCamera == true)
                scoreController.UpdateScore(30);
            if (other.gameObject.tag == "Reverse")
            {
                cameraController.StopRotateCamera();
                cameraController.StartRotateCamera();
                timerController.ExtraTime(7);
            }
        }
        if(other.gameObject.tag=="CloudPlatform")
        {
            DeathFall();
            StartCoroutine(gameSceneController.DeathLogic());
        }
        if(other.gameObject.tag=="DeathPlatform")
        {
            DeathFall();
            StartCoroutine(gameSceneController.DeathLogic());
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
            scoreController.UpdateMoney();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Health")
        {
            timerController.ExtraTime(5);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Garlic")
        {
            DeathFall();
            StartCoroutine(gameSceneController.DeathLogic());
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "BrokenPlatform")
        {
            onBrokenPlatform = false;
            FindObjectOfType<PlatformController>().CallForBreak(other);
        }
    }
    
    IEnumerator BrokenPlatform(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        if(onBrokenPlatform && other != null)
        {
            DeathFall();
            StartCoroutine(gameSceneController.DeathLogic());
            FindObjectOfType<PlatformController>().CallForBreak(other);
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
            DeathFall();
            StartCoroutine(gameSceneController.DeathLogic());
        }
    }


    private void Jump()

    {
        if(transform.position.x > touchPos.x && !isJumping && !isFalling && !isDead)
        {
            //rb.velocity = new Vector2(jumpX, jumpY);
            rb.AddForce(new Vector2(jumpX, jumpY), ForceMode2D.Impulse);
            isJumping = true;
            isFalling = true;
        }
        if(transform.position.x < touchPos.x && !isJumping && !isFalling && !isDead)
        {
            //rb.velocity = new Vector2(-jumpX, jumpY);
            rb.AddForce(new Vector2(-jumpX, jumpY), ForceMode2D.Impulse);
            isJumping = true;
            isFalling = true;
        }
    }

    private void DeathFall()
    {
        isDead = true;
        playerSprite.enabled=true;
        StartCoroutine(RotateForDuration(2f));
        GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 1f;
        rb.velocity = new Vector3(0f, -2f, 0f);
    }

    private IEnumerator RotateForDuration(float duration)
    {
        float startRotation = transform.rotation.eulerAngles.z;
        float targetRotation = startRotation + (360f * duration);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    }

    private void UpdateAnimation()
    {
        if(rb.velocity.x > 0f && rb.velocity.y> 0.1f)
        {
            playerSprite.flipX = true;
            anim.SetBool("isJumping",true);
        }
        if(rb.velocity.x < 0f  && rb.velocity.y> 0.1f)
        {
            playerSprite.flipX = false;
            anim.SetBool("isJumping",true);
        }
        if(rb.velocity.y< 0.1f)
        {
            anim.SetBool("isJumping",false);
        }
    }
}
