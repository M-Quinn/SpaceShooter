using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [Header("TextBoxes")]
    [SerializeField] TextMeshProUGUI _normalHighScore_Text;
    [SerializeField] TextMeshProUGUI _pacifistHighScore_Text;
    [SerializeField] TextMeshProUGUI _musicVolume_Text;
    [SerializeField] TextMeshProUGUI _soundEffectsVolume_Text;

    bool _isSettingsMenuActive = false;

    public static Action UpdateMusicVolume;
    public static Action UpdateSoundEffectVolume;

    private void Start()
    {
        PullIntFromPlayerPrefs("HighScore", _normalHighScore_Text, true);
        PullIntFromPlayerPrefs("PacifistHighScore", _pacifistHighScore_Text, true);
        PullIntFromPlayerPrefs("MusicVolume", _musicVolume_Text, false);
        PullIntFromPlayerPrefs("SoundEffectVolume", _soundEffectsVolume_Text, false);
    }

    private static void PullIntFromPlayerPrefs(string keyName, TextMeshProUGUI textBox, bool isScore)
    {
        int num;
        if (isScore)
            num = 0;
        else//is volume
            num = 100;
        if (PlayerPrefs.HasKey(keyName))
            num = PlayerPrefs.GetInt(keyName);
        textBox.text = num.ToString();
    }

    public void ToggleSettingsMenu() {
        _anim.SetTrigger("Toggle");
        _isSettingsMenuActive = !_isSettingsMenuActive;
    }

    public void ResetNormalHighScore() {
        PlayerPrefs.SetInt("HighScore", 0);
        _normalHighScore_Text.text = "0";
    }

    public void ResetPacifistHighScore() {
        PlayerPrefs.SetInt("PacifistHighScore", 0);
        _pacifistHighScore_Text.text = "0";
    }

}
