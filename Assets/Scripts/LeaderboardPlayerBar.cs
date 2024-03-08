using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPlayerBar : MonoBehaviour
{
    public GameObject targetObject;
    private Camera mainCamera;

    public GameObject playerView;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.transform.position;

            Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetPosition);

            if (screenPosition.x >= 0 && screenPosition.x <= Screen.width && screenPosition.y >= 0 && screenPosition.y <= Screen.height)
            {
                Debug.Log("Hedef GameObject ekranin içinde.");
                playerView.SetActive(false);
            }
            else
            {
                playerView.SetActive(true);
                Debug.Log("Hedef GameObject ekranin dişinda.");
            }

            if(PlayerPrefs.GetInt("HighScore") == 0)
            {
                playerView.SetActive(false);
            }
        }
        else
        {
            if(PlayerPrefs.GetInt("HighScore") == 0)
            {
                playerView.SetActive(false);
            }
        }
    }
}
