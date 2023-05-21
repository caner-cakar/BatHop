using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    int characterIndex;

    private void Awake() 
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter",0);
        Instantiate(playerPrefabs[characterIndex], new Vector3(0f,-2.63f,0f),Quaternion.identity);   
    }
}
