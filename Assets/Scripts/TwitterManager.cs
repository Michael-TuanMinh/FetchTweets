using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Web.Twitter.DataStructures;

public class TwitterManager : MonoBehaviour
{
    [SerializeField]
    private string twitterApiConsumerKey;
    
    [SerializeField]
    private string twitterApiConsumerSecret;
    
    [SerializeField] 
    private GameEvent OnFinishedSearching;
    
    [SerializeField] 
    private GameEvent OnRecievedError;

    private Token token;

    public SearchResults results; // TODO: Set it to private in production
    

    public void Search(string _keyWord, int _maxTweet, string _lang, string _seachType)
    {
        StartCoroutine(SearchForTweets(_keyWord, _maxTweet, _lang, (SearchResultType)Enum.Parse(typeof(SearchResultType), _seachType)));
    }

    
    // Language code: https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
    // _timeLimit: Don't search for any tweet after this time
    private IEnumerator SearchForTweets(string _keyWord, int? _maxTweetsToReturn = null, string _lang = null, 
        SearchResultType _searchType = SearchResultType.recent, DateTime? _timeLimit = null)
    {
        yield return StartCoroutine(GetTwitterApiAccessToken(twitterApiConsumerKey, twitterApiConsumerSecret));
        
        string url = "https://api.twitter.com/1.1/search/tweets.json?q=" + _keyWord;

        if (_maxTweetsToReturn != null) url += "&count=" + _maxTweetsToReturn;
        if (_lang != null) url += "&lang=" + _lang;
        if (_timeLimit != null) url += "&until=" + _timeLimit.Value.Year + _timeLimit.Value.Month + _timeLimit.Value.Day;

        url += "&result_type=" + _searchType;
        url += "&include_entities=true";

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("Authorization", "Bearer " + token.access_token);
        
        yield return webRequest.SendWebRequest();
        HandleWebRequestResult(webRequest, () =>
        {
            results = JsonUtility.FromJson<SearchResults>(webRequest.downloadHandler.text);
            OnFinishedSearching.Raise(results.statuses.Length);
        });
    }
    
    private IEnumerator GetTwitterApiAccessToken(string consumerKey, string consumerSecret)
    {
        string url = "https://api.twitter.com/oauth2/token";

        WWWForm body = new WWWForm ();
        body.AddField("grant_type", "client_credentials");

        string encodeKey = consumerKey.Trim() + ":" + consumerSecret.Trim();
        string URL_ENCODED_KEY_AND_SECRET = Convert.ToBase64String(Encoding.UTF8.GetBytes(encodeKey));
        
        UnityWebRequest webRequest = UnityWebRequest.Post(url, body);
        webRequest.SetRequestHeader("Authorization", "Basic " + URL_ENCODED_KEY_AND_SECRET);

        yield return webRequest.SendWebRequest();
        HandleWebRequestResult(webRequest, () =>
        {
            token = JsonUtility.FromJson<Token>(webRequest.downloadHandler.text);
        });
    }

    private void HandleWebRequestResult(UnityWebRequest _webRequest, Action _onSuceed)
    {
        switch (_webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                OnRecievedError.Raise("Error: " + _webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                OnRecievedError.Raise("HTTP Error: " + _webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                _onSuceed?.Invoke();
                Debug.Log(_webRequest.downloadHandler.text);
                break;
        }
    }
        
}

