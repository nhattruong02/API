using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] Text username;
    [SerializeField] Text password;
    [SerializeField] Text textNotice;
    [SerializeField] Button btnLogin;   
    // Start is called before the first frame update
    void Start()
    {
        textNotice.text = "";
        btnLogin.onClick.AddListener(() => StartCoroutine(GetRequest("https://localhost:44350/api/users/18")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    User user = JsonUtility.FromJson<User>(webRequest.downloadHandler.text);
                    string usernameUser = username.text.ToString();
                    string passwordUser = password.text.ToString();    
                    if(usernameUser.Trim().Equals(user.username.Trim()) && passwordUser.Trim().Equals(user.password.Trim()))
                    {

                        textNotice.text = "Dang nhap thanh cong";
                    }
                    else
                    {
                        textNotice.text = "UserName && Password khong chinh xac";
                    }
                    break;
            }
        }
    }
}

[SerializeField]
public class User
{

    // not use {get;set} instead of [SerializeField]
    public int id;
    public string fullname;
    public string username;
    public string phone;
    public string password;

    public User()
    {
    }
    public User(int id,string username, string password,string fullname)
    {
        this.id = id;
        this.username = username;
        this.password = password;
        this.fullname = fullname;
    }
}
