using System.ComponentModel;
using UnityEngine;
using UMA.CharacterSystem;
using UMA;
using UMA.AssetBundles;
using UMA.Dynamics;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public string recipeName;
    public Sprite itemImage;
    public float itemPrice;

    public enum slots{
        Hair,
        Head,
        Beard,
        Chest,
        Legs,
        Hands
    }
    public slots slot;
}
