using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class StartSceneController : MonoBehaviour
{
    int i=0;
    public Button settingsButton;
    public Button accountChangeButton;
    public GameObject panels;
    public GameObject settingsPanel;
    public GameObject characterPanel;
    public GameObject nicknamePanel;
    public GameObject leaderboardPanel;

    public GameObject startButton;
    public GameObject leaderboardButton;
    public GameObject characterButton;
    public GameObject characters;

    public TextMeshProUGUI nicknamePanelText;
    [SerializeField]TMP_InputField nicknameField;

    public Sprite meBarSprite;
    public Sprite defaultBarSprite;

    public GameObject allTimePreFab;
    public GameObject allTimeContentPanel;

    public void Awake()
    {
        characters.SetActive(false);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        leaderboardButton.SetActive(true);
        characterButton.SetActive(true);
        settingsButton.enabled = true;
        accountChangeButton.enabled = true;
        leaderboardPanel.SetActive(false);
        nicknamePanel.SetActive(false);
        panels.SetActive(false);
    }
  
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void Settings()
    {
        panels.SetActive(true);
        characters.SetActive(false);
        startButton.SetActive(false);
        leaderboardButton.SetActive(false);
        characterButton.SetActive(false);
        settingsButton.interactable = false;
        accountChangeButton.interactable = false;
        settingsPanel.SetActive(true);
        characterPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        nicknamePanel.SetActive(false);
    }
    public void ExitSettings()
    {
        panels.SetActive(false);
        characters.SetActive(false);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        leaderboardButton.SetActive(true);
        characterButton.SetActive(true);
        settingsButton.interactable = true;
        leaderboardPanel.SetActive(false);
        nicknamePanel.SetActive(false);
        accountChangeButton.interactable = true;
    }

    public void ExitLeaderboard()
    {
        SceneManager.LoadScene("StartScene");
        FirebaseController.instance.GetData();
    }

    public void CharacterPanel()
    {
        panels.SetActive(true);
        characters.SetActive(true);
        characterButton.SetActive(false);
        characterPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(false);
        leaderboardButton.SetActive(false);
        settingsButton.interactable = false;
        accountChangeButton.interactable = false;
        FindObjectOfType<CharacterSelect>().selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        FindObjectOfType<CharacterSelect>().skins[FindObjectOfType<CharacterSelect>().selectedCharacter].SetActive(true);
        FindObjectOfType<CharacterSelect>().selectedIcon.SetActive(true);
    }

    public void LeaderboardPanel()
    {
        panels.SetActive(true);
        leaderboardPanel.SetActive(true);
        nicknamePanel.SetActive(false);
        characters.SetActive(false);
        characterButton.SetActive(false);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(false);
        leaderboardButton.SetActive(false);
        settingsButton.interactable = false;
        accountChangeButton.interactable = false;
        Debug.LogError("AllTime opened");
        i=0;
        allTimeContentPanel.SetActive(true);
        FirebaseController.instance.AllTimeLeaderboard();
    }

    public void NicknamePanel()
    {
        FirebaseController.instance.GetData();
        if(PlayerPrefs.GetInt("nickname")==0)
        {
            panels.SetActive(true);
            nicknamePanel.SetActive(true);
            leaderboardPanel.SetActive(false);
            characters.SetActive(false);
            characterPanel.SetActive(false);
            settingsPanel.SetActive(false);
            settingsButton.interactable = false;
            accountChangeButton.interactable = false;
        }
        if(PlayerPrefs.GetInt("nickname") == 1)
        {
            LeaderboardPanel();
        }
    }

    public void ExitNicknamePanel()
    {
        panels.SetActive(false);
        nicknamePanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        characters.SetActive(false);
        characterButton.SetActive(true);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        leaderboardButton.SetActive(true);
        settingsButton.interactable = true;
        accountChangeButton.interactable = true;
    }

    public IEnumerator CheckNickname()
    {
        FirebaseController.instance.UpdateNickNameAndPostToDatabase(nicknameField.text);
        yield return new WaitForSeconds(0.5f);
        if(FirebaseController.instance.isNicknameUsed)
        {
            nicknamePanelText.text = "This nickname is unavailable";
            nicknamePanelText.color = Color.red;
        }
        else if(FirebaseController.instance.containsForeign)
        {
            nicknamePanelText.text = "The nickname must be between 5 and 15 characters and should not contain foreign characters";
            nicknamePanelText.color = Color.red;
        }
        else
        {
            nicknamePanelText.color = Color.green;
            nicknamePanelText.text = "This nickname is available";
            LeaderboardPanel();
        }
    }

    public void OkayButton()
    {
        StartCoroutine(CheckNickname());
    }

    public void GetAllTimePanelData(string nickname, int score, bool meBar)
    {
        if(nickname!=null)
        {
            if(i < 3)
            {
                allTimeContentPanel.transform.Find((i+1).ToString()).Find("Name").GetComponent<TextMeshProUGUI>().text = nickname;
                allTimeContentPanel.transform.Find((i+1).ToString()).Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
                if(meBar)
                {
                    allTimeContentPanel.transform.Find((i+1).ToString()).GetComponent<Image>().sprite = meBarSprite;
                    FindObjectOfType<LeaderboardPlayerBar>().targetObject = allTimeContentPanel.transform.Find((i+1).ToString()).gameObject;
                    FindObjectOfType<LeaderboardPlayerBar>().playerView.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
                    FindObjectOfType<LeaderboardPlayerBar>().playerView.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = nickname;
                    FindObjectOfType<LeaderboardPlayerBar>().playerView.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
                }
                else
                {
                    allTimeContentPanel.transform.Find((i+1).ToString()).GetComponent<Image>().sprite = defaultBarSprite;
                }
                Debug.LogError("i < 3");
            }
            else
            {
                GameObject newObject = Instantiate(allTimePreFab, allTimeContentPanel.transform);
                newObject.transform.localPosition = new Vector3(newObject.transform.localPosition.x,newObject.transform.localPosition.y,0f);
                newObject.transform.localScale = new Vector3(1f,1f,1f);
                newObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
                newObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = nickname;
                newObject.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
                if(meBar)
                {
                    newObject.transform.GetComponent<Image>().sprite = meBarSprite;
                    FindObjectOfType<LeaderboardPlayerBar>().targetObject = newObject;
                    FindObjectOfType<LeaderboardPlayerBar>().playerView.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
                    FindObjectOfType<LeaderboardPlayerBar>().playerView.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = nickname;
                    FindObjectOfType<LeaderboardPlayerBar>().playerView.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
                }
                else
                {
                    newObject.transform.GetComponent<Image>().sprite = defaultBarSprite;
                }
                Debug.LogError("i > 3"); 
            }
            i++;
        }
    }

    /*public void FirstNickname()
    {
        FirebaseController.instance.GetData(); 
        if(PlayerPrefs.GetInt("nickname")==0)
        {
            panels.SetActive(true);
            nicknamePanel.SetActive(true);
            leaderboardPanel.SetActive(false);
            characters.SetActive(false);
            characterPanel.SetActive(false);
            settingsPanel.SetActive(false);
            settingsButton.interactable = false;
            accountChangeButton.interactable = false;
        }
    }*/
}