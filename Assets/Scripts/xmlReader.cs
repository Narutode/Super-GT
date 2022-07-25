using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class xmlReader : MonoBehaviour
{
    [SerializeField] private scriptableObjectGenerator generalProps;
    // Start is called before the first frame update
    public TextAsset dictionary;
    
    
    public int currentLanguage;
    
 
    string graphic;
    string langue5;
    private string quit;
    private string play;

    public TextMeshProUGUI textgraphic;
    public TextMeshProUGUI textlangue;
    public TextMeshProUGUI textplay;
    public TextMeshProUGUI textquit;
    
    public Dropdown selectDropdown;

    private List<Dictionary<String, string>> languages = new List<Dictionary<string, string>>();
    private Dictionary<string, string> obj;

    private void Awake()
    {
        selectDropdown.value = selectDropdown.options.FindIndex(option => option.text == generalProps.Langue);
        Reader();
    }

    private void Update()
    {
        languages[currentLanguage].TryGetValue("langue", out langue5);
        languages[currentLanguage].TryGetValue("graphic", out graphic);
        languages[currentLanguage].TryGetValue("play", out play);
        languages[currentLanguage].TryGetValue("quit", out quit);
        
        textgraphic.text = graphic;
        textlangue.text = langue5;
        textplay.text = play;
        textquit.text = quit;
    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList LanguageList = xmlDoc.GetElementsByTagName("language");
        
        foreach (XmlNode languageValue in LanguageList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            { 
                obj.Add(value.Name , value.InnerText);
            }
            languages.Add(obj);
        }
    }

    public void ValueChangeCheck()
    {
        generalProps.Langue = selectDropdown.options[selectDropdown.value].text;
        currentLanguage = selectDropdown.value;
    }
}
