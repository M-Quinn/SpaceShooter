using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] [Tooltip("This allows for pausing of the game but not needed in the main menu")] bool _isMainMenu;
    [SerializeField] Animator _anim;
    [Header("TextBoxes")]
    [SerializeField] TextMeshProUGUI _normalHighScore_Text;
    [SerializeField] TextMeshProUGUI _pacifistHighScore_Text;
    [SerializeField] TextMeshProUGUI _musicVolume_Text;
    [SerializeField] TextMeshProUGUI _soundEffectsVolume_Text;
    [Header("Sliders")]
    [SerializeField] Slider _musicVolume_Slider;
    [SerializeField] Slider _soundEffectVolume_Slider;

    bool _isSettingsMenuActive = false;

    public static Action UpdateMusicVolume;

    private void Start()
    {
        PullIntFromPlayerPrefs("HighScore", _normalHighScore_Text, true);
        PullIntFromPlayerPrefs("PacifistHighScore", _pacifistHighScore_Text, true);
        SetSlidersFromStart(_musicVolume_Slider, "MusicVolume", _musicVolume_Text);
        SetSlidersFromStart(_soundEffectVolume_Slider, "SoundEffectVolume", _soundEffectsVolume_Text);
    }

    private int PullIntFromPlayerPrefs(string keyName, TextMeshProUGUI textBox, bool isScore)
    {
        int num;
        if (isScore)
            num = 0;
        else
        {//is volume
            num = 100;
        }
        if (PlayerPrefs.HasKey(keyName))
            num = PlayerPrefs.GetInt(keyName);
        textBox.text = num.ToString();
        return num;
    }
    private void SetSlidersFromStart(Slider slider, string keyname, TextMeshProUGUI textbox) {
        int num = PullIntFromPlayerPrefs(keyname, textbox, false);
        slider.value = num;
    }

    public void ToggleSettingsMenu() {
        if (!_isMainMenu && !_isSettingsMenuActive)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
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

    public void ChangeMusicVolume() {
        Single x = _musicVolume_Slider.value;
        PlayerPrefs.SetInt("MusicVolume", (int)x);
        _musicVolume_Text.text = ((int)x).ToString();
        UpdateMusicVolume?.Invoke();
    }
    public void ChangeSoundEffectVolume() {
        Single x = _soundEffectVolume_Slider.value;
        PlayerPrefs.SetInt("SoundEffectVolume", (int)x);
        _soundEffectsVolume_Text.text = ((int)x).ToString();
        SoundEffects.UpdateSoundEffectVolume?.Invoke();
    }

    public void BackToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Quit() {
        Application.Quit();
    }



}
