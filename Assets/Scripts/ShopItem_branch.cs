using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New branch item", menuName = "ShopItem/ShopItem_branch")]
public class ShopItem_branch : ScriptableObject
{
    public int itemID;
    public Sprite itemIcon;
    public int itemPrice;
}
