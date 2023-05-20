using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float damping;

    private Vector3 velocity = Vector3.zero;

    private bool isCameraWork=true;

    



    void FixedUpdate()
    {
        if(isCameraWork)
        {
            Vector3 movePosition = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        }
    }

    public IEnumerator RotateCamera()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        yield return new WaitForSeconds(5f);

        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //Physics2D.gravity = -Physics2D.gravity;
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
