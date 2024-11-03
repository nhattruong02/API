using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Get : MonoBehaviour
{
    [SerializeField] Text textNotice;
    [SerializeField] Button btnGet;
    // Start is called before the first frame update
    void Start()
    {
        btnGet.onClick.AddListener(() => StartCoroutine(GetRequest("https://localhost:44350/api/users/18")));
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
                    textNotice.text = $"Username:\t{user.username.Trim()} \n Password:\t{user.password.Trim()} \n Fullname:\t{user.fullname.Trim()}";
                    break;
            }
        }
    }
}
