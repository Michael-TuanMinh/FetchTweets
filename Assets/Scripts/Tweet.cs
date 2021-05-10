using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Tweet : MonoBehaviour
{
    [SerializeField] private Image avatar;

    [SerializeField] private TextMeshProUGUI publicName;

    [SerializeField] private TextMeshProUGUI id;

    [SerializeField] private TextMeshProUGUI tweet;

    public void LoadTweet(string _imageURL, string _publicName, string _id, string _tweet)
    {
        if(!string.IsNullOrEmpty(_imageURL)) StartCoroutine(LoadImage(_imageURL));
        publicName.text = _publicName;
        id.text = _id;
        tweet.text = _tweet;
    }
    
    IEnumerator LoadImage(string url) 
    {
        using(var unityWebRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(unityWebRequest.error);
            }
            else
            {
                if (unityWebRequest.isDone)
                {
                    var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
                    var rect = new Rect(0, 0, texture.width, texture.height);
                    var _sprite = Sprite.Create(texture,rect,new Vector2(0.5f,0.5f));
                    avatar.sprite = _sprite;
                }
            }
        }
    }
}
