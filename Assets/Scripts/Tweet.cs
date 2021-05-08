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
        //if(!string.IsNullOrEmpty(_imageURL)) StartCoroutine(LoadImage(_imageURL));
        publicName.text = _publicName;
        id.text = _id;
        tweet.text = _tweet;
    }
    
    IEnumerator LoadImage(string url) 
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        var texture = DownloadHandlerTexture.GetContent(webRequest);
        avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }
}
