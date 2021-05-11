using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TwitterManager twitterManager;
    
    [Header("UI")]
    [SerializeField] 
    private TMP_InputField keyword;

    [SerializeField] 
    private TMP_Dropdown languageDropdown;
    
    [SerializeField] 
    private TMP_Dropdown searchTypeDropdown;
    
    [SerializeField] 
    private TMP_Dropdown searchLimitDropdown;

    [SerializeField] private TextMeshProUGUI prompText;
    
    private enum Language
    {
        English, Spanish, French, Chinese
    }

    private void Start()
    {
        languageDropdown.AddOptions(Enum.GetNames(typeof(Language)).ToList());
        searchTypeDropdown.AddOptions(Enum.GetNames(typeof(SearchResultType)).ToList());
    }

    public void Search()
    {
        if(string.IsNullOrEmpty(keyword.text) || string.IsNullOrWhiteSpace(keyword.text)) return;
        
        string keyWord = keyword.text;
        int searchLimit = Int32.Parse(searchLimitDropdown.options[searchLimitDropdown.value].text);

        string language = "";
        switch ((Language)Enum.Parse(typeof(Language),languageDropdown.options[languageDropdown.value].text))
        {
            case Language.English:
                language = "en";
                break;
            case Language.Chinese:
                language = "zh";
                break;
            case Language.Spanish:
                language = "es";
                break;
            case Language.French:
                language = "fr";
                break;
        }

        twitterManager.Search(keyWord, searchLimit , language, searchTypeDropdown.options[searchTypeDropdown.value].text);
    }

    public void OnReceivedError(string _error)
    {
        prompText.enabled = true;
        prompText.text = "Failed to load data. " + _error;
        StartCoroutine(DisablePrompText());
    }

    private IEnumerator DisablePrompText()
    {
        yield return new WaitForSeconds(1.5f);
        prompText.enabled = false;
    }
}
