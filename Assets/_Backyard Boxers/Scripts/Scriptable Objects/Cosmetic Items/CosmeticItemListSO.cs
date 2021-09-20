using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cosmetic Item List")]
public class CosmeticItemListSO : ScriptableObject {
    public List<CosmeticItemSO> listOfItemsMale;
    public List<CosmeticItemSO> listOfItemsFemale;
}