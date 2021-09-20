using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionButton : MonoBehaviour {
    [SerializeField] private Image itemImage;
    private string slotName;
    private string wardrobeRecipeName;

    public void Initialize(CosmeticItemSO item) {
        // wardrobeRecipeName = item.recipeName;
        // itemImage.sprite = item.itemImage;
        slotName = item.slot.ToString();
    }

    public void OnButtonPressed() {
        if(string.IsNullOrEmpty(wardrobeRecipeName)) {
            Debug.Log("Wardrobe Recipe not set");
            return;
        }

        // player.Instance.SetWardrobeSlotItem(slotName, wardrobeRecipeName);
    }
}