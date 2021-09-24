using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionButton : MonoBehaviour {

    Transform itemTransform;
    bool isActive = false;
    WardrobeManager wardrobeManager;
    ScrollviewContentBuilder scrollViewBuilder;
    Image imgSelected;

    private void Awake() {
        imgSelected = transform.Find("Image Selected").GetComponent<Image>();
    }

    public void Initialize(ScrollviewContentBuilder scrollViewBuilder, WardrobeManager wardrobeManager, Transform itemTransform, bool isActive) {
        this.scrollViewBuilder = scrollViewBuilder; //this is mommy
        this.wardrobeManager = wardrobeManager;
        this.itemTransform = itemTransform;
        this.isActive = isActive;

        imgSelected.enabled = isActive;
    }

    public void OnButtonPressed() {

        if(itemTransform.name.Substring(0, 2).Equals(Data.typeAccessories)) {
            isActive = !isActive;
            wardrobeManager.SetPlayerTransformActive(itemTransform, isActive);
            imgSelected.enabled = isActive;
            return;
        }

        if(isActive) {
            return;
        } else {
            isActive = true;
            wardrobeManager.SetPlayerTransformActive(itemTransform, isActive); //Activate item
            SetButtonSelectedState(isActive);
            scrollViewBuilder.DeselectOtherButtons(transform.name);
        }
    }

    //this is used to show/hide the selection border on the button, and inform the button its item is no longer active
    public void SetButtonSelectedState(bool active) {
        isActive = active;
        imgSelected.enabled = isActive;
    }
}