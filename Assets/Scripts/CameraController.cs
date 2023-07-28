using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    private GameObject player;
    
    public float followSpeed=5f;
    public Vector3 offset;

    float rotateValue;
    
    private bool isCameraWork = true;

    private Coroutine rotateCameraCoroutine;
    public bool isCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (isCameraWork)
        {
            Vector3 targetPosition = transform.position;
            targetPosition.x = player.transform.position.x;
            
            if(player.transform.position.y>=0.4f)
            {
                targetPosition.y = player.transform.position.y;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }else 
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }
        }
    }

    public void StartRotateCamera()
    {
        if (rotateCameraCoroutine == null)
        {
            rotateCameraCoroutine = StartCoroutine(RotateCameraCoroutine());
        }
    }

    public void StopRotateCamera()
    {
        if (rotateCameraCoroutine != null)
        {
            StopCoroutine(rotateCameraCoroutine);
            rotateCameraCoroutine = null;
        }
    }

    private IEnumerator RotateCameraCoroutine()
    {
        if(PlayerPrefs.GetInt("soundValue",1)==1)
            AudioManager.Instance.sfxSource.Stop();   
        if(isCamera)
        {
            rotateValue = 0f;
            Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, rotateValue);
            isCamera = false;
        }
        else
        {
            rotateValue = 180f;
            Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, rotateValue);
            isCamera = true;
            yield return new WaitForSeconds(5f);
            Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isCamera = false;
        }
       
    }

    public void StopCamera()
    {
        isCameraWork = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void RunCamera()
    {
        isCameraWork = true;
    }
}
