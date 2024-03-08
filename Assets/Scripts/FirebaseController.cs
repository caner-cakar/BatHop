using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using UnityEngine.UI;
using Google;
using Unity.VisualScripting;
using Firebase.Database;
using System.Data.Common;
using System.Linq;

public class FirebaseController : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public GameObject loadingPanel;
    public bool alreadyNickname;
    public bool containsForeign;

    public DatabaseReference DBreference;
    [NonSerialized]public bool isNicknameUsed;
    [NonSerialized]public string nicknameData;

    private GoogleSignInConfiguration configuration;
    [NonSerialized]public string webClientId = "your webid";

    public static FirebaseController instance { get; private set; }

    private void Awake()
    {
        if (instance == null) 
        {
            //First run, set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) 
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        configuration = new GoogleSignInConfiguration 
        { 
            WebClientId = webClientId, 
            RequestEmail = true, 
            RequestIdToken = true
        };
    }

    void InitializeFirebase() 
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start() 
    {   
        loadingPanel.SetActive(false);
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) 
        {
            InitializeFirebase();
        } 
        else 
        {
            UnityEngine.Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        }
        });
        SignInWithGoogle();
    }

    public void SignOut()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        SignInWithGoogle();
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) 
    {
        if (auth.CurrentUser != user) 
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null&& auth.CurrentUser.IsValid();
            if (!signedIn && user != null) 
            {
                Debug.Log("Signed out " + user.UserId);
                SignInWithGoogle();
            }
            user = auth.CurrentUser;
            if (signedIn) 
            {
                Debug.Log("Signed in " + user.UserId);
                GetData();
                GetHighScoreFromDatabase();
            }
        }
        else
        {
            SignInWithGoogle();
        }
    }

    void OnDestroy() 
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    

    public void PostToDatabase(string userMail)
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(user.UserId).Child("Email").SetValueAsync(userMail.ToString());
    }

    public void PostHighScoreToDatabase(float score)
    {
        FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").Child(user.UserId).Child("Nickname").GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if(task.IsFaulted)
            {
                Debug.LogError("Veri Çekme hatası");
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(snapshot.Exists && snapshot.Value.ToString() != "")
                {
                    FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").Child(user.UserId).Child("HighScore").SetValueAsync(score);
                }
                else
                {
                    Debug.LogError("Nickname bulunamadı");
                }
            }
            else
            {
                Debug.LogError("Bilinmeyen hata");
            }
        });
    }

    public void GetHighScoreFromDatabase()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").Child(user.UserId).Child("HighScore").GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if(task.IsFaulted)
            {
                Debug.LogError("Veri Çekme hatası");
                PlayerPrefs.SetInt("HighScore",0);
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(snapshot.Exists)
                {
                    int highScore = int.Parse(snapshot.Value.ToString());
                    PlayerPrefs.SetInt("HighScore",highScore);
                }
                else
                {
                    PlayerPrefs.SetInt("HighScore",0);
                }
            }
            else
            {
                PlayerPrefs.SetInt("HighScore",0);
            }
        });
    }

    public void UpdateNickNameAndPostToDatabase(string nickName)
    {
        string lowerNickname = nickName.ToLower();
        bool containsForeignCharacters = ContainsForeignCharacters(lowerNickname);
        FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").OrderByChild("Nickname").EqualTo(lowerNickname).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result.Exists)
            {
                Debug.LogError("Bu nickname zaten kullanılıyor. Lütfen farklı bir nickname seçin.");
                isNicknameUsed = true;
                containsForeign =false;
            }
            else
            {
                if(lowerNickname.Length >=5 && !lowerNickname.Contains(" ") && lowerNickname.Length <=15 && containsForeignCharacters)
                {
                    Debug.Log("Bu nickname benzersiz. Kullanabilirsiniz!");
                    isNicknameUsed = false;
                    containsForeign =false;
                    FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").Child(user.UserId).Child("Nickname").SetValueAsync(lowerNickname);
                    PlayerPrefs.SetInt(("nickname"),1);
                }
                else 
                {
                    containsForeign =true;
                }
            }
        });  
    }
    public bool ContainsForeignCharacters(string input)
    {   
        foreach (char c in input)
        {
            if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || char.IsDigit(c)))
            {
                return false;
            }
        }
        
        return true;
    } 
    

    public void GetData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted)
            {
                Debug.LogError("Getdata nickname fault");
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(!snapshot.Child(user.UserId).Child("Nickname").Exists)
                {
                    Debug.LogError("Kullaniciya ait nickname yok");
                    PlayerPrefs.SetInt("nickname",0);
                    nicknameData = null;
                }
                else
                {
                    Debug.LogError("Kullaniciya ait nickname var");
                    PlayerPrefs.SetInt("nickname",1);
                    string temp = snapshot.Child(user.UserId).Child("Nickname").Value.ToString();
                    nicknameData = temp;
                }
            }
            else
            {
                GetData();
            }
            
        });
    }

    public void SignInWithGoogle() 
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;  
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnGoogleAuthenticatedFinished);  
    }


    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if(task.IsFaulted)
        {
            Debug.Log("Fault");
        }
        else if(task.IsCanceled)
        {
            Debug.Log("Login Cancel");
        }
        else 
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task=>
            {
                if(task.IsCanceled)
                {
                    Debug.Log("SignInWithCredentialAsync was canceled");
                    return;
                }
                else if(task.IsFaulted)    
                {
                    Debug.Log("SignInWithCredentialAsync encountered an error: "+task.Exception);
                    return;
                }
               
                    user = auth.CurrentUser;
                    PostToDatabase(user.Email);
                    GetHighScoreFromDatabase();
                    GetData();
                    StartCoroutine(LoadingScreen());
                    Debug.LogError("Succesfully logged : "+ user.UserId);
            });
        }
    }

    public IEnumerator LoadingScreen()
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        loadingPanel.SetActive(false);
        if(PlayerPrefs.GetInt("nickname")==0)
        {
            FindObjectOfType<StartSceneController>().NicknamePanel();
        }
        else
        {
            Debug.Log("Nickname var");
        }
    }

    /*public void GetMoneyScoreFromDatabase()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted)
            {
                PlayerPrefs.SetInt("MoneyScore",0);
            }
            else
            {
                DataSnapshot snapshot = task.Result;
                if(!snapshot.Child(user.UserId).Child("MoneyScore").Exists)
                {
                    Debug.LogError("Kullanıcıya ait para yok");
                    PlayerPrefs.SetInt("MoneyScore",0);
                }
                else
                {
                    Debug.LogError("Kullanıcıya ait para var");
                    int temp = int.Parse(snapshot.Child(user.UserId).Child("MoneyScore").Value.ToString());
                    PlayerPrefs.SetInt("MoneyScore",temp);
                }
            }
            
        });
    }*/

    /*public void PostMoneyScoreToDatabase(int moneyScore)
    {
       
    }*/

    public void AllTimeLeaderboard()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Leaderboard").OrderByChild("HighScore").GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Leaderboard verileri çekilirken hata oluştu: " + task.Exception);
                }
                else
                {
                    DataSnapshot snapshot = task.Result;

                    foreach (var childSnapshot in snapshot.Children.Reverse())
                    {
                        string userId = childSnapshot.Key;
                        int score = int.Parse((childSnapshot.Child("HighScore").Value.ToString()));
                        string nickname = childSnapshot.Child("Nickname").Value.ToString();
                        bool meBar;
                        if(userId == auth.CurrentUser.UserId)
                        {
                            meBar = true;
                        }
                        else
                        {
                            meBar = false;
                        }
                        FindObjectOfType<StartSceneController>().GetAllTimePanelData(nickname,score,meBar);
                        Debug.Log("Nickname: "+nickname+" score :"+score);
                    }
                }
            });
    }
}


