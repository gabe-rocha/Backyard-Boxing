using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable Objects/Item List")]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> listOfItemsMale;
    public List<ItemSO> listOfItemsFemale;
}
