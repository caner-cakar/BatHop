using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject characterPanel;
    public GameObject[] skins;
    public int selectedCharacter;

    StartSceneController startSceneController;
    private void Start() {
        startSceneController = FindObjectOfType<StartSceneController>();
    }
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
        for(int i=0; i<skins.Length;i++)
        {
            skins[i].SetActive(false);
        }
        characterPanel.SetActive(false);
        startSceneController.startButton.SetActive(true);
        startSceneController.characterButton.SetActive(true);
        startSceneController.settingsPanel.SetActive(false);
        startSceneController.settingsButton.interactable = true;
        Time.timeScale = 1;
    }
}
