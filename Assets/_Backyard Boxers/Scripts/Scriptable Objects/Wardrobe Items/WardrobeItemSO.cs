using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Wardrobe Item")]
public class WardrobeItemSO : ScriptableObject {
    public string itemName;
    public Sprite itemSprite;
    public float itemPrice;
    public bool isLocked = true;
    public string transformName;
}