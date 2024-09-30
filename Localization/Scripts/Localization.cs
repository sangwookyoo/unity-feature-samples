using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public enum LanguageOption
{ 
    DeviceLanguage, kr, en, jp,
}

public class Localization
{
    private const string LOCALIZATION_FILE_NAME = "LOCALIZATION_COMMON";
    
    private TextAsset _localSheet;
    private LanguageOption _language = LanguageOption.DeviceLanguage;
    private readonly Dictionary<string, string> _localizedTextDics = new();
    
    public event Action LanguageOptionChanged;
    public string CurrentLanguageKey { get; private set; }
    
    public void Init()
    {
        CurrentLanguageKey = _language == LanguageOption.DeviceLanguage ? GetSystemLanguageKey() : _language.ToString();
        Debug.Log($"[Localization] Current Language is set to {CurrentLanguageKey}");
        LoadLocalizationData();
    }
    
    public void ChangeLanguageOption(LanguageOption targetLanguage)
    {
        if (_language == targetLanguage) return;
        
        _language = targetLanguage;
        Init();
        LanguageOptionChanged?.Invoke();
    }

    public void ApplyLocalization(TextMeshProUGUI text, string key) => text.text = GetLocalizedText(key);
    
    public void ApplyLocalizationWithParam(TextMeshProUGUI text, string key, params object[] args) => text.text = GetLocalizedTextWithParam(key, args);

    public string GetLocalizedText(string key)
    {
        return _localizedTextDics.TryGetValue(key, out string text) ? text : $"[Missing: {key}]";
    }

    public string GetLocalizedTextWithParam(string key, params object[] args)
    {
        string text = GetLocalizedText(key);
        return string.IsNullOrEmpty(text) ? text : string.Format(text, args);
    }
    
    private void LoadLocalizationData()
    {
        _localizedTextDics.Clear();

        LoadFromCSV(Resources.Load<TextAsset>(LOCALIZATION_FILE_NAME));

        if (_localSheet != null)
        {
            LoadFromCSV(_localSheet);
        }
    }

    private void LoadFromCSV(TextAsset textAsset)
    {
        if (textAsset == null) return;
        
        string[] lines = textAsset.text.Split('\n');
        if (lines.Length < 2)
        {
            Debug.LogWarning("[Localization]: CSV file is empty or contains only headers.");
            return;
        }

        string[] headers = lines[0].Split(',');
        int targetLanguageIndex = Array.IndexOf(headers, CurrentLanguageKey);

        if (targetLanguageIndex == -1)
        {
            Debug.LogWarning($"[Localization]: Language key \"{CurrentLanguageKey}\" not found in CSV.");
            return;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) continue;

            string[] columns = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            if (columns.Length <= targetLanguageIndex) continue;

            string key = columns[0].Trim('"', ' ');
            string value = columns[targetLanguageIndex].Trim('"', ' ').Replace("<br>", "\n");

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                _localizedTextDics[key] = value;
            }
        }
    }

    private string GetSystemLanguageKey()
    {
        return Application.systemLanguage switch
        {
            SystemLanguage.Korean => nameof(LanguageOption.kr),
            SystemLanguage.English => nameof(LanguageOption.en),
            SystemLanguage.Japanese => nameof(LanguageOption.jp),
            _ => nameof(LanguageOption.en)
        };
    }
}