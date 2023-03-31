using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    private ShopManager shopManager;

    [Header ("Item object")]
    [SerializeField] private ShopItemObj shopItemObj;
    
    [Header ("Select")]
    [SerializeField] private Toggle toggleUse;
    [SerializeField] private TextMeshProUGUI toggleText;

    [Header ("Buy")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;

    private const string useText = "USE", selectedText = "SELECTED";

    // void OnEnable(){
        
    // }
    // void Awake(){}
    void Start(){
        shopManager = ShopManager.instance;

        buyButton.onClick.AddListener(BuyItem);
        buyButton.gameObject.SetActive(!IsItemOwned());

        if (IsItemSelected()) SelectItem(toggleUse);

        toggleUse.onValueChanged.AddListener(delegate{ SelectItem(toggleUse); });
    }
    public void SetItem(ShopItemObj item, ToggleGroup group){
        shopItemObj = item;
        GetComponent<Image>().sprite = shopItemObj.itemIcon;
        priceText.text = shopItemObj.itemPrice.ToString();
        toggleUse.group = group;
    }
    void BuyItem(){
        if (shopManager.GetMoney() >= shopItemObj.itemPrice){
            shopManager.AddShopItem(shopItemObj);
            shopManager.SetMoney(-shopItemObj.itemPrice);
            buyButton.gameObject.SetActive(!IsItemOwned());
            toggleText.text = useText;
        } else Debug.Log("not enough money");
    }
    void SelectItem(Toggle toggle){
        switch (shopItemObj.shopItemType){
            case ShopItemType.item_scenery:
                shopManager.SelectSceneryItem(shopItemObj);
                break;
            case ShopItemType.item_bird:
                shopManager.SelectBirdItem(shopItemObj);
                break;
            case ShopItemType.item_branch:
                shopManager.SelectBranchItem(shopItemObj);
                break;
        }
        if (toggleUse.isOn) toggleText.text = selectedText;
        else toggleText.text = useText;
    }
    private bool IsItemOwned(){
        return shopManager.IsItemOwned(shopItemObj);
    }
    private bool IsItemSelected(){
        return shopManager.IsItemSelected(shopItemObj);
    }
}
