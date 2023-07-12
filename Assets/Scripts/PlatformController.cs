using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] GameObject breakThePlatform;
    public void CallForBreak(Collider2D other)
    {
        Instantiate(breakThePlatform, other.transform.position,Quaternion.identity);
        Destroy(other.gameObject);
    }
}
