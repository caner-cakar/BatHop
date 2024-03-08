using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public GameObject panels;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject highScorePanel;
    [SerializeField] GameObject yourScorePanel;

    public bool alreadyCalled;

    public AdsManager adsManager;

    public bool isHighScore;
    public bool isYourScore;
    private string panelName;

    private int adCounter;
    private Coroutine deathPanelCoroutine;

    public void Awake()
    {
        adsManager = FindObjectOfType<AdsManager>();
        panels.SetActive(false);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
    }

    private void Start() 
    {
        alreadyCalled = false;
        Time.timeScale = 1;
        adCounter = 0;
    }

    public void StartDeathPanelCoroutine(string panelName)
    {
        if (deathPanelCoroutine == null)
        {
            deathPanelCoroutine = StartCoroutine(DeathPanels(panelName));
        }
    }

    public void StopDeathPanelCoroutine()
    {
        if (deathPanelCoroutine != null)
        {
            StopCoroutine(DeathPanels(null));
            deathPanelCoroutine = null;
        }
    }

    public void Settings()
    {
        panels.SetActive(true);
        settingsPanel.SetActive(true);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
        Time.timeScale = 0;
    }
    public void ExitSettings()
    {
        panels.SetActive(false);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        highScorePanel.SetActive(false);
        yourScorePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public IEnumerator DeathPanels(string panelname)
    {
        Time.timeScale = 1;
        if(panelname == "LosePanel")
        {
            if(adsManager.adForRewarded && adCounter<3)
            {
                panels.SetActive(true);
                settingsPanel.SetActive(false);
                losePanel.SetActive(true);
                highScorePanel.SetActive(false);
                yourScorePanel.SetActive(false);
                losePanel.transform.Find("ContinueText").gameObject.SetActive(true);
                losePanel.transform.Find("AdsButton").gameObject.SetActive(true);
                losePanel.transform.Find("PlayButton").gameObject.SetActive(false);
                losePanel.transform.Find("HomeButton").gameObject.SetActive(false);
                losePanel.transform.Find("ExitButton").gameObject.SetActive(false);

                yield return new WaitForSeconds(6f);
                losePanel.transform.Find("ExitButton").gameObject.SetActive(true);
            }
            else
            {
                panels.SetActive(true);
                settingsPanel.SetActive(false);
                losePanel.SetActive(true);
                highScorePanel.SetActive(false);
                yourScorePanel.SetActive(false);
                losePanel.transform.Find("ContinueText").gameObject.SetActive(false);
                losePanel.transform.Find("AdsButton").gameObject.SetActive(false);
                losePanel.transform.Find("PlayButton").gameObject.SetActive(true);
                losePanel.transform.Find("HomeButton").gameObject.SetActive(true);
                losePanel.transform.Find("ExitButton").gameObject.SetActive(false);
                yield return null;
            }
        }
        else if (panelname == "YourScorePanel")
        {
            if(adsManager.adForRewarded && adCounter<3)
            {
                panels.SetActive(true);
                settingsPanel.SetActive(false);
                yourScorePanel.SetActive(true);
                highScorePanel.SetActive(false);
                losePanel.SetActive(false);
                yourScorePanel.transform.Find("ContinueText").gameObject.SetActive(true);
                yourScorePanel.transform.Find("AdsButton").gameObject.SetActive(true);
                yourScorePanel.transform.Find("PlayButton").gameObject.SetActive(false);
                yourScorePanel.transform.Find("HomeButton").gameObject.SetActive(false);
                yourScorePanel.transform.Find("ExitButton").gameObject.SetActive(false);

                yield return new WaitForSeconds(6f);
                yourScorePanel.transform.Find("ExitButton").gameObject.SetActive(true);
            }
            else
            {
                panels.SetActive(true);
                settingsPanel.SetActive(false);
                yourScorePanel.SetActive(true);
                highScorePanel.SetActive(false);
                losePanel.SetActive(false);
                yourScorePanel.transform.Find("ContinueText").gameObject.SetActive(false);
                yourScorePanel.transform.Find("AdsButton").gameObject.SetActive(false);
                yourScorePanel.transform.Find("PlayButton").gameObject.SetActive(true);
                yourScorePanel.transform.Find("HomeButton").gameObject.SetActive(true);
                yourScorePanel.transform.Find("ExitButton").gameObject.SetActive(false);
                yield return null;
            }
        }
        else if (panelname == "HighScorePanel")
        {
            if(adsManager.adForRewarded && adCounter<3)
            {
                panels.SetActive(true);
                settingsPanel.SetActive(false);
                losePanel.SetActive(false);
                highScorePanel.SetActive(true);
                yourScorePanel.SetActive(false);
                highScorePanel.transform.Find("ContinueText").gameObject.SetActive(true);
                highScorePanel.transform.Find("AdsButton").gameObject.SetActive(true);
                highScorePanel.transform.Find("PlayButton").gameObject.SetActive(false);
                highScorePanel.transform.Find("HomeButton").gameObject.SetActive(false);
                highScorePanel.transform.Find("ExitButton").gameObject.SetActive(false);

                yield return new WaitForSeconds(6f);
                highScorePanel.transform.Find("ExitButton").gameObject.SetActive(true);
            }
            else
            {
                panels.SetActive(true);
                settingsPanel.SetActive(false);
                losePanel.SetActive(false);
                highScorePanel.SetActive(true);
                yourScorePanel.SetActive(false);
                highScorePanel.transform.Find("ContinueText").gameObject.SetActive(false);
                highScorePanel.transform.Find("AdsButton").gameObject.SetActive(false);
                highScorePanel.transform.Find("PlayButton").gameObject.SetActive(true);
                highScorePanel.transform.Find("HomeButton").gameObject.SetActive(true);
                highScorePanel.transform.Find("ExitButton").gameObject.SetActive(false);
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Panel not found");
            yield return null;
        }
    }

    public void ExitAdsPanel()
    {
        if(panelName == "LosePanel")
        {
            panels.SetActive(true);
            settingsPanel.SetActive(false);
            yourScorePanel.SetActive(false);
            highScorePanel.SetActive(false);
            losePanel.SetActive(true);
            losePanel.transform.Find("ContinueText").gameObject.SetActive(false);
            losePanel.transform.Find("AdsButton").gameObject.SetActive(false);
            losePanel.transform.Find("PlayButton").gameObject.SetActive(true);
            losePanel.transform.Find("HomeButton").gameObject.SetActive(true);
            losePanel.transform.Find("ExitButton").gameObject.SetActive(false);
        }
        else if (panelName == "YourScorePanel")
        {
            panels.SetActive(true);
            settingsPanel.SetActive(false);
            yourScorePanel.SetActive(true);
            highScorePanel.SetActive(false);
            losePanel.SetActive(false);
            yourScorePanel.transform.Find("ContinueText").gameObject.SetActive(false);
            yourScorePanel.transform.Find("AdsButton").gameObject.SetActive(false);
            yourScorePanel.transform.Find("PlayButton").gameObject.SetActive(true);
            yourScorePanel.transform.Find("HomeButton").gameObject.SetActive(true);
            yourScorePanel.transform.Find("ExitButton").gameObject.SetActive(false);        
        }
        else if (panelName == "HighScorePanel")
        {
            panels.SetActive(true);
            settingsPanel.SetActive(false);
            losePanel.SetActive(false);
            highScorePanel.SetActive(true);
            yourScorePanel.SetActive(false);
            highScorePanel.transform.Find("ContinueText").gameObject.SetActive(false);
            highScorePanel.transform.Find("AdsButton").gameObject.SetActive(false);
            highScorePanel.transform.Find("PlayButton").gameObject.SetActive(true);
            highScorePanel.transform.Find("HomeButton").gameObject.SetActive(true);
            highScorePanel.transform.Find("ExitButton").gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel not found");
        }
        StopAllCoroutines();
    }


    public void AdsButton()
    {
        AudioManager.Instance.StopSFX();
        adCounter++;
        adsManager.ShowRewardedAd();    
    }

    public void PlayAgain()
    {
        AudioManager.Instance.sfxSource.Stop();
        Score score1 = gameObject.AddComponent<Score>();
        score1.KeepScore();
        PlayerPrefs.SetInt("GameCounter", (PlayerPrefs.GetInt("GameCounter",0))+1);
        Debug.Log("GameCounter: "+ PlayerPrefs.GetInt("GameCounter",0));
        if(PlayerPrefs.GetInt("GameCounter",0)<5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            adsManager.ShowInterstitialAd();
            PlayerPrefs.SetInt("GameCounter",0);  
        }
    }

    

    public void Menu()
    {
        SceneManager.LoadSceneAsync("StartScene");
    }


    public IEnumerator DeathLogic()
    {
        if(!alreadyCalled)
        {
            if(FindObjectOfType<BatsMovement>() != null)
            FindObjectOfType<BatsMovement>().isPlayerDead = true;
            FindObjectOfType<PlayerMovement>().isDead = true;
            FindObjectOfType<CameraController>().StopCamera();
            FindObjectOfType<Timer>().isTime = false;
            FindObjectOfType<Score>().StopScore();
            yield return new WaitForSeconds(0.5f);
            if(!isHighScore && !isYourScore)
            {
                StopDeathPanelCoroutine();
                StartDeathPanelCoroutine("LosePanel");
                panelName = "LosePanel";
            }  
            if(isYourScore)
            {
                StopDeathPanelCoroutine();
                StartDeathPanelCoroutine("YourScorePanel");
                panelName = "YourScorePanel";
            } 
            if(isHighScore)
            {
                StopDeathPanelCoroutine();
                StartDeathPanelCoroutine("HighScorePanel");
                panelName = "HighScorePanel";
                
            }
            alreadyCalled = true;
        }
    }
}
    



