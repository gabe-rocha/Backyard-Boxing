using UnityEngine;

public class CosmeticItemSO : ScriptableObject {
    public string itemName;
    public Sprite itemSprite;
    public float itemPrice;
    public bool isLocked = true;

    public string slot;
}