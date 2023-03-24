using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsScreenManager : MonoBehaviour
{
    [SerializeField] private Transform settingsPopup, settingsFilter;
    [SerializeField] private Button toggleMusicButton, toggleSoundButton, toggleVibrationButton;

    void Start(){
        toggleMusicButton.onClick.AddListener(ToggleMusic);
        toggleSoundButton.onClick.AddListener(ToggleSound);
        toggleVibrationButton.onClick.AddListener(ToggleVibration);
    }
    void ToggleMusic(){
        Debug.Log("toggle music");
    }
    void ToggleSound(){
        Debug.Log("toggle sound");
    }
    void ToggleVibration(){
        Debug.Log("toggle vibration");
    }
}
