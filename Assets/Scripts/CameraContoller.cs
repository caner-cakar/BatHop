using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float damping;

    private Vector3 velocity = Vector3.zero;

    private bool isCameraWork=true;
    public bool turnCamera=false;

    private Coroutine rotateCameraCoroutine;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    



    void FixedUpdate()
    {
        if(isCameraWork)
        {
            Vector3 movePosition = player.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
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
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        yield return new WaitForSeconds(5f);

        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void StopCamera()
    {
        isCameraWork = false;
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    public void RunCamera()
    {
        isCameraWork = true;
    }


}
