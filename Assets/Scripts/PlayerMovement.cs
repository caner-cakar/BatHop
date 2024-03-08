using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    bool isDeadSong;

    private float fallTime = 0f;
    [SerializeField] float maxFallTime = 0.3f;

    private bool onBrokenPlatform;

    Vector3 characterPos;
    Vector3 centerPos;
    Vector2 touchPos;    
    private Coroutine rotateForDuration;

    float time;

    public Vector3 playerLastPos;
    bool isPlayerDeadBefore;
    bool lastPlatformBroken;
    Vector3 lastBrokenPlatformPos;
    [SerializeField] GameObject brokenPlatformPreFab;
    Transform lastBrokenPlatformTransform;

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
    
    public void StartFromLastPos()
    {   
        isPlayerDeadBefore = true;
        if(lastPlatformBroken)
        {
            GameObject newBrokenPlatform = Instantiate(brokenPlatformPreFab, lastBrokenPlatformTransform);
            newBrokenPlatform.transform.position = lastBrokenPlatformPos;
        }

        isDead = false;
        GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 10f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StopRotation();
        transform.position = playerLastPos;
        time = 0f;
        rb.gravityScale = 10f;
        GetComponent<BoxCollider2D>().enabled = true;
        cameraController.RunCamera();
        onBrokenPlatform = false;
        playerSprite.enabled=true;
        gameSceneController.panels.SetActive(false);
        isFalling = false;
        fallTime =0f;
        gameSceneController.alreadyCalled = false;
        scoreController.KeepScore();
        scoreController.currentScore = scoreController.currentScore -10;
        timerController.isTime = true;
        if(FindObjectOfType<BatsMovement>() != null)
            FindObjectOfType<BatsMovement>().isPlayerDead = true;

        isPlayerDeadBefore = false;
        isDeadSong = false;
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
            {
                lastPlatformBroken = false;
                scoreController.UpdateScore(10);
            }
            if((other.gameObject.tag =="NormalPlatform"|| other.gameObject.tag=="Reverse") && cameraController.isCamera == true)
            {
                lastPlatformBroken = false;
                scoreController.UpdateScore(15);
            }
            if (other.gameObject.tag == "BrokenPlatform" && cameraController.isCamera == false)
            {
                lastPlatformBroken = true;
                scoreController.UpdateScore(15);
                lastBrokenPlatformPos = other.gameObject.transform.position;
                lastBrokenPlatformTransform = other.gameObject.transform.parent;
            }
            if (other.gameObject.tag == "BrokenPlatform" && cameraController.isCamera == true)
            {
                lastPlatformBroken = true;
                scoreController.UpdateScore(30);
                lastBrokenPlatformPos = other.gameObject.transform.position;
                lastBrokenPlatformTransform = other.gameObject.transform.parent;
            }
            if (other.gameObject.tag == "Reverse")
            {
                lastPlatformBroken = false;
                cameraController.StopRotateCamera();
                cameraController.StartRotateCamera();
                SfxSounds("Reverse");
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
            SfxSounds("Coin");
        }
        if(other.gameObject.tag == "Health")
        {
            Destroy(other.gameObject);
            SfxSounds("Health");
            timerController.ExtraTime(5);
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
                if(other.gameObject.tag =="NormalPlatform" || other.gameObject.tag=="Reverse" )
                    playerLastPos = gameObject.transform.position;
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
        SfxSounds("Dead");
        isDead = true;
        playerSprite.enabled=true;
        StartRotation();
        GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 1f;
        rb.velocity = new Vector3(0f, -2f, 0f);
        
        
    }

    private IEnumerator RotateForDuration()
    {
        float startRotation = transform.rotation.eulerAngles.z;
        float targetRotation = startRotation + (360f * 1f);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float t = elapsedTime / 1f;
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    }

    public void StartRotation()
    {
        if (rotateForDuration == null)
        {
            rotateForDuration = StartCoroutine(RotateForDuration());
        }
    }

    public void StopRotation()
    {
        if (rotateForDuration != null)
        {
            StopCoroutine(rotateForDuration);
            rotateForDuration = null;
        }
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

    public void DeadSong()
    {
        if(isDeadSong == false)
        {
            AudioManager.Instance.PlaySFX("Dead");
            isDeadSong =true;
        }
    }

    public void SfxSounds(string name)
    {
        if(PlayerPrefs.GetInt("soundValue",1)==1)
        {
            if(name == "Coin")
            {
                AudioManager.Instance.PlaySFX("Coin");
            }   
            if(name == "Health")
            {
                AudioManager.Instance.PlaySFX("Health");
            }
            if(name == "Reverse")
            {
                AudioManager.Instance.PlaySFX("Reverse");
            }
            if(name == "Dead")
            {
                DeadSong();
            }
        }
        else if(PlayerPrefs.GetInt("soundValue",1)==0)
        {
            AudioManager.Instance.sfxSource.Stop();
        }
    }
}
