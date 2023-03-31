using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New bird item", menuName = "ShopItem/ShopItem_bird")]
public class ShopItem_bird : ScriptableObject
{
    public int itemID;
    public Sprite itemIcon;
    public int itemPrice;
}
