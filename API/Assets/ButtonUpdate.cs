using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class ButtonUpdate : MonoBehaviour
{
    [SerializeField] Button btnUpdate;
    [SerializeField] Text username;
    [SerializeField] Text password;
    [SerializeField] Text fullname;
    [SerializeField] Text textNotice;
    string uri = "https://localhost:44350/api/users/18";
    // Start is called before the first frame update
    void Start()
    {
        textNotice.text = "";
        btnUpdate.onClick.AddListener(() => StartCoroutine(Upload()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Upload()
    {
        string usernameTxt = username.text.Trim();
        string passwordTxt = password.text.Trim();
        string fullnameTxt = fullname.text.Trim();
        User user = new User(18,usernameTxt,passwordTxt,fullnameTxt);
        string jsonUser = JsonUtility.ToJson(user);
        Debug.Log(jsonUser);
        using (UnityWebRequest www = UnityWebRequest.Put(uri.Trim(), jsonUser))
        {
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Content-Type", "application/json");
            Debug.Log(jsonUser);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                textNotice.text  = www.error.ToString();
            }
            else
            {
                textNotice.text = "Upload complete!";
            }
        }
    }
}
