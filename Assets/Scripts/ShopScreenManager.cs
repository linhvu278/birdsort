using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopScreenManager : MonoBehaviour
{
    public static ShopScreenManager instance;

    private ShopManager shopManager;

    [SerializeField] private Transform shopPopup, shopFilter;
    [SerializeField] private Button closeButton;

    [Header ("User money")]
    [SerializeField] private Transform shopUserMoney;
    [SerializeField] private TextMeshProUGUI shopUserMoneyText;

    // [SerializeField] ToggleGroup toggleGroup;
    [Header ("Shop buttons")]
    [SerializeField] private Toggle toggleScenery;
    [SerializeField] private Toggle toggleBird;
    [SerializeField] private Toggle toggleBranch;

    [Header ("Shop panels")]
    [SerializeField] private RectTransform sceneryPanel;
    [SerializeField] private RectTransform birdPanel;
    [SerializeField] private RectTransform branchPanel;

    [Header ("Shop scroll view")]
    [SerializeField] private Transform shopScrollView;
    private ScrollRect shopScrollRect;

    private ShopPanelManager sceneryPanelManager, birdPanelManager, branchPanelManager;

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        shopManager = ShopManager.instance;
        shopManager.onMoneyChanged += GetMoneyText;

        shopScrollRect = shopScrollView.GetComponent<ScrollRect>();
        // shopScrollRect.GetComponentInChildren<Scrollbar>().value = 1;
        
        closeButton.onClick.AddListener(delegate{
            EnableShopScreen(false);
        });

        toggleScenery.onValueChanged.AddListener(delegate{
            ToggleSceneryPanel(toggleScenery);
        });
        toggleBird.onValueChanged.AddListener(delegate{
            ToggleBirdPanel(toggleBird);
        });
        toggleBranch.onValueChanged.AddListener(delegate{
            ToggleBranchPanel(toggleBranch);
        });

        // toggleScenery.isOn = true;
        // ToggleSceneryPanel(toggleScenery);
        
        EnableShopScreen(false);
    }
    public void EnableShopScreen(bool value){
        shopFilter.gameObject.SetActive(value);
        shopPopup.gameObject.SetActive(value);
        shopUserMoney.gameObject.SetActive(value);
        GetMoneyText();
        // if (value) ToggleSceneryPanel(toggleScenery);
        if (value) toggleScenery.isOn = true;
        if (value) ToggleSceneryPanel(toggleScenery);
    }
    void ToggleSceneryPanel(Toggle toggle){
        sceneryPanel.gameObject.SetActive(toggleScenery.isOn);
        shopScrollRect.content = sceneryPanel;
        // shopScrollRect.GetComponentInChildren<Scrollbar>().value = 1;
    }
    void ToggleBirdPanel(Toggle toggle){
        birdPanel.gameObject.SetActive(toggleBird.isOn);
        shopScrollRect.content = birdPanel;
        // shopScrollRect.GetComponentInChildren<Scrollbar>().value = 1;
    }
    void ToggleBranchPanel(Toggle toggle){
        branchPanel.gameObject.SetActive(toggleBranch.isOn);
        shopScrollRect.content = branchPanel;
        // shopScrollRect.GetComponentInChildren<Scrollbar>().value = 1;
    }
    void GetMoneyText(){
        shopUserMoneyText.text = shopManager.GetMoney().ToString();
    }
    // void OnEnable(){
    //     GetMoneyText();
    // }
}
