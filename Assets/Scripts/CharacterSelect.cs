using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    public GameObject[] skins;
    public int selectedCharacter;

    public Character[] characters;
    public Button unlockButton;
    public Sprite buyButtonImage;
    public Sprite selectedButtonImage;
    public Sprite selectButtonImage;
    public TextMeshProUGUI coinScoreText;
    public GameObject Price;
    public GameObject selectedIcon;

    StartSceneController startSceneController;
    private void Awake()
    {
        startSceneController = FindObjectOfType<StartSceneController>();
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
        {
            player.SetActive(false);
        }

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
    private void Start() 
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        skins[selectedCharacter].SetActive(true);
        selectedIcon.SetActive(true);
    }

    public void ChangeNext()
    {
        
        skins[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter == skins.Length)
            selectedCharacter = 0;

        skins[selectedCharacter].SetActive(true);
        if(characters[selectedCharacter].isSelected)
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
        if (characters[selectedCharacter].isSelected)
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        UpdateUI();
    }

    public void ExitPanel()
    {
        for(int i=0; i<skins.Length;i++)
        {
            skins[i].SetActive(false);
        }
        shopPanel.SetActive(false);
        startSceneController.panels.SetActive(false);
        startSceneController.characters.SetActive(false);
        startSceneController.startButton.SetActive(true);
        startSceneController.leaderboardButton.SetActive(true);
        startSceneController.characterButton.SetActive(true);
        startSceneController.settingsPanel.SetActive(false);
        startSceneController.leaderboardPanel.SetActive(false);
        startSceneController.settingsButton.interactable = true;
        startSceneController.accountChangeButton.interactable = true;
    }

    public void UpdateUI()
    {

        characters[PlayerPrefs.GetInt("SelectedCharacter",0)].isSelected = true;
        coinScoreText.text = ""+ PlayerPrefs.GetInt("MoneyScore",0).ToString();
        if(characters[selectedCharacter].isUnlocked == true && characters[selectedCharacter].isSelected == false)
        {
            unlockButton.GetComponent<Image>().sprite = selectButtonImage;
            selectedIcon.SetActive(false);
            Price.GetComponentInChildren<TextMeshProUGUI>().text ="Free";
        }
        if(characters[selectedCharacter].isUnlocked == true && characters[selectedCharacter].isSelected == true)
        {
            unlockButton.GetComponent<Image>().sprite = selectedButtonImage;
            selectedIcon.SetActive(true);
            Price.GetComponentInChildren<TextMeshProUGUI>().text ="Free";
        }
        if(characters[selectedCharacter].isUnlocked == false)
        {
            unlockButton.GetComponent<Image>().sprite = buyButtonImage;
            selectedIcon.SetActive(false);
            Price.GetComponentInChildren<TextMeshProUGUI>().text =""+characters[selectedCharacter].price;
        }
    }

    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("MoneyScore",0);
        int price = characters[selectedCharacter].price;
        int selectedPlayer=PlayerPrefs.GetInt("SelectedCharacter", 0);
        
        if(characters[selectedCharacter].isUnlocked == true && characters[selectedCharacter].isSelected == false)
        {
            characters[PlayerPrefs.GetInt("SelectedCharacter", 0)].isSelected = false;
            PlayerPrefs.SetInt(characters[selectedCharacter].name,1);
            PlayerPrefs.SetInt("SelectedCharacter",selectedCharacter);
            characters[selectedCharacter].isSelected = true;
            UpdateUI();
        }
        if(characters[selectedCharacter].isUnlocked == false &&  price <= coins)
        {
            characters[selectedCharacter].isUnlocked = true;
            PlayerPrefs.SetInt(characters[selectedCharacter].name,1);
            PlayerPrefs.SetInt("MoneyScore",coins-price);
            //FirebaseController.instance.PostMoneyScoreToDatabase(PlayerPrefs.GetInt("MoneyScore"));
            UpdateUI();
        }
    }
}
