using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject characterPanel;
    public GameObject[] skins;
    public int selectedCharacter;

    public Character[] characters;
    public Button unlockButton;
    public Text coinScoreText;
    public GameObject Price;

    StartSceneController startSceneController;
    private void Awake()
    {
        skins[selectedCharacter].SetActive(true);
        startSceneController = FindObjectOfType<StartSceneController>();
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
        {
            player.SetActive(false);
        }
        skins[selectedCharacter].SetActive(true);

        foreach(Character c in characters)
        {
            if (c.price == 0)
                c.isUnlocked = true;
            else
            {
                c.isUnlocked = PlayerPrefs.GetInt(c.name, 0) == 0 ? false : true;
            }
        }
        UpdateUI();
    }

    public void ChangeNext()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter == skins.Length)
            selectedCharacter = 0;

        skins[selectedCharacter].SetActive(true);
        if(characters[selectedCharacter].isUnlocked)
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

        UpdateUI();
    }

    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter == -1)
            selectedCharacter = skins.Length -1;

        skins[selectedCharacter].SetActive(true);
        if (characters[selectedCharacter].isUnlocked)
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        UpdateUI();
    }

    public void ExitPanel()
    {
        for(int i=0; i<skins.Length;i++)
        {
            skins[i].SetActive(false);
        }
        characterPanel.SetActive(false);
        startSceneController.characters.SetActive(false);
        startSceneController.startButton.SetActive(true);
        startSceneController.characterButton.SetActive(true);
        startSceneController.settingsPanel.SetActive(false);
        startSceneController.settingsButton.interactable = true;
        Time.timeScale = 1;
    }

    public void UpdateUI()
    {
        coinScoreText.text = ""+ PlayerPrefs.GetInt("MoneyScore",0).ToString();
        if(characters[selectedCharacter].isUnlocked == true)
            unlockButton.gameObject.SetActive(false);
        else
        {
            Price.GetComponentInChildren<TextMeshProUGUI>().text =""+characters[selectedCharacter].price;
            if(PlayerPrefs.GetInt("MoneyScore",0)< characters[selectedCharacter].price)
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = false;
            }
            else
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = true;
            }
        }
    }

    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("MoneyScore",0);
        int price = characters[selectedCharacter].price;
        PlayerPrefs.SetInt("MoneyScore",coins-price);
        PlayerPrefs.SetInt(characters[selectedCharacter].name,1);
        PlayerPrefs.SetInt("SelectedCharacter",selectedCharacter);
        characters[selectedCharacter].isUnlocked = true;
        UpdateUI();
    }
}
