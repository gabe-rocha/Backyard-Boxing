using System.ComponentModel;
using UMA;
using UMA.AssetBundles;
using UMA.CharacterSystem;
using UMA.Dynamics;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cosmetic Item")]
public class CosmeticItemSO : ScriptableObject {
    public string prefabName;
    public Sprite itemSprite;
    public float itemPrice;
    public bool isLocked = true;

    public enum slots {
        Hair,
        Head, //goggles, tattoos
        Beard,
        Chest, //shirts
        Waist, //belts
        Legs, //pants
        Hands, //gloves
        Feet //shoes
    }
    public slots slot;
}