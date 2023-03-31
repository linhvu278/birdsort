using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New scenery item", menuName = "ShopItem/ShopItem_scenery")]
public class ShopItem_scenery : ScriptableObject
{
    public int itemID;
    public Sprite itemIcon;
    public int itemPrice;
}
