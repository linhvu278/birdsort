using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New shop item", menuName = "ShopItemObj")]
public class ShopItemObj : ScriptableObject
{
    public int itemID;
    public ShopItemType shopItemType;
    public Sprite itemIcon;
    public int itemPrice;
    public Sprite itemSpriteData;
}

public enum ShopItemType { item_scenery, item_bird, item_branch }