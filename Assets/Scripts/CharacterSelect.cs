using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject characterPanel;
    public GameObject[] skins;
    public int selectedCharacter;

    public void ChangeNext()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if(selectedCharacter==skins.Length)
            selectedCharacter=0;
        
        skins[selectedCharacter].SetActive(true);
    }

    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter==-1)
            selectedCharacter=skins.Length-1;
        
        skins[selectedCharacter].SetActive(true);
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void ExitPanel()
    {
        skins[selectedCharacter].SetActive(false);
        characterPanel.SetActive(false);
        FindObjectOfType<StartSceneController>().startButton.SetActive(true);
        FindObjectOfType<StartSceneController>().characterButton.SetActive(true);
        FindObjectOfType<StartSceneController>().settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
