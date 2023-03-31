using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsScreenManager : MonoBehaviour
{
    public static SettingsScreenManager instance;

    [SerializeField] private Transform settingsPopup, settingsFilter;
    [SerializeField] private Button closeButton/*, restorePurchasesButton*/, shopButton, removeAdsButton;
    [SerializeField] private Toggle toggleMusicButton, toggleSfxButton, toggleVibrationButton;

    private AudioManager audioManager;
    private MainGameManager mainGameManager;
    private ShopScreenManager shopScreenManager;

    private float settingsPopupSpeed = 1f;
    private Vector3 ogPos = new Vector3(0, 10f, 0);

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        audioManager = AudioManager.instance;
        mainGameManager = MainGameManager.instance;
        shopScreenManager = ShopScreenManager.instance;

        closeButton.onClick.AddListener(delegate{
            EnableSettingsScreen(false);
        });

        toggleMusicButton.onValueChanged.AddListener(delegate{
            ToggleMusic(toggleMusicButton);
        });
        toggleSfxButton.onValueChanged.AddListener(delegate{
            ToggleSfx(toggleSfxButton);
        });
        toggleVibrationButton.onValueChanged.AddListener(delegate{
            ToggleVibration(toggleVibrationButton);
        });

        // restorePurchasesButton.onClick.AddListener(RestorePurchases);
        shopButton.onClick.AddListener(OpenShopMenu);
        removeAdsButton.onClick.AddListener(RemoveAds);
        
        EnableSettingsScreen(false);
    }
    void ToggleMusic(Toggle toggle){
        audioManager.ToggleBgMusic(toggleMusicButton.isOn);
    }
    void ToggleSfx(Toggle toggle){
        audioManager.ToggleSfx(toggleSfxButton.isOn);
    }
    void ToggleVibration(Toggle toggle){
        // Debug.Log("toggle vibration: " + toggleVibrationButton.isOn);
        mainGameManager.ToggleVibration(toggleVibrationButton.isOn);
    }
    public void EnableSettingsScreen(bool value){
        settingsFilter.gameObject.SetActive(value);
        switch (value){
            case true:
                settingsPopup.DOMove(Vector3.zero, settingsPopupSpeed).SetEase(Ease.OutBack);
                break;
            case false:
                settingsPopup.position = ogPos;
                break;
        }
    }
    // void RestorePurchases(){
    //     Debug.Log("purchases restored");
    // }
    void OpenShopMenu(){
        shopScreenManager.EnableShopScreen(true);
        EnableSettingsScreen(false);
    }
    void RemoveAds(){
        Debug.Log("ads removed");
    }
}