using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{
    private  Dictionary<string, string> Translations = null;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void trad(string lang)
    {
        var data = Resources.Load<TextAsset>($"Translations/{lang}");
        if (data != null)
            ParseFile(data.text);
    }
    
    public  void ParseFile(string data)
    {
        using (var stream = new StringReader(data))
        {
            var line = stream.ReadLine();
            var temp = new string[2];
            var key = string.Empty;
            var value = string.Empty;
            while (line != null)
            {
                if (line.StartsWith(";") || line.StartsWith("["))
                {
                    line = stream.ReadLine();
                    continue;
                }

                temp = line.Split('=');
                if (temp.Length == 2)
                {
                    key = temp[0].Trim();
                    value = temp[1].Trim();
                    if (value == string.Empty)
                        continue;
                    if (Translations.ContainsKey(key))
                        Translations[key] = value;
                    else
                        Translations.Add(key, value);
                }

                line = stream.ReadLine();
            }
        }
    }
}
