using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollviewContentBuilder : MonoBehaviour {

#region Public Fields
    public enum ContentTypes {
        Hair,
        Beard,
        Accessories,
        Color,
        Tops,
        Pants,
        Gloves,
        Shoes
    }

    [SerializeField] private ContentTypes contentType;
    [SerializeField] private Transform scrollviewContent;
    [SerializeField] private GameObject goButtonTemplate;
    [SerializeField] private CharacterCustomizationController charCustCont;
#endregion

#region Private Serializable Fields

#endregion

#region Private Fields
    private Dictionary<string, int> dicWardrobe; //everything that's available to him
#endregion

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.WardrobeApplied, BuildScrollviews);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.WardrobeApplied, BuildScrollviews);
    }

#region MonoBehaviour CallBacks
    public void BuildScrollviews() {

        var wardrobeManager = charCustCont.goPlayer.GetComponent<WardrobeManager>();
        dicWardrobe = wardrobeManager.GetPlayerWardrobeFromHisBody();

        string itemType = "";
        switch (contentType) {

            case ContentTypes.Accessories:
                itemType = Data.typeAccessories;
                break;
            case ContentTypes.Hair:
                itemType = Data.typeHairs;
                break;
            case ContentTypes.Beard:
                itemType = Data.typeBeards;
                break;
            case ContentTypes.Tops:
                itemType = Data.typeTops;
                break;
            case ContentTypes.Pants:
                itemType = Data.typePants;
                break;
            case ContentTypes.Gloves:
                itemType = Data.typeGloves;
                break;
            case ContentTypes.Shoes:
                itemType = Data.typeShoes;
                break;
            default:
                break;
        }

        foreach (var d in dicWardrobe) {

            if(d.Key.Substring(0, 2).Equals(itemType)) {
                var itemTransform = charCustCont.goPlayer.transform.FindDeepChild(d.Key);
                var itemData = itemTransform.GetComponent<WardrobeItemData>().wardrobeItemData;

                var goButton = Instantiate(goButtonTemplate, scrollviewContent);
                goButton.name = itemData.itemName;
                goButton.SetActive(true);
                var itemImage = goButton.transform.Find("Image Item").GetComponent<Image>();
                itemImage.sprite = itemData.itemSprite;

                var textItemName = goButton.transform.Find("Text Item Name").GetComponent<TextMeshProUGUI>();
                textItemName.text = itemData.itemName;

                var textValue = goButton.transform.Find("Text Cost").GetComponent<TextMeshProUGUI>();
                Image locked = goButton.transform.Find("Image Locked").gameObject.GetComponent<Image>();
                if(itemData.isLocked) {
                    textValue.text = itemData.itemPrice.ToString();
                    locked.enabled = true;
                } else {
                    textValue.text = "";
                    locked.enabled = false;
                }

                goButton.GetComponent<ItemSelectionButton>().Initialize(this, wardrobeManager, itemTransform, itemTransform.gameObject.activeInHierarchy);

                //deactivate No Hair and No Beard buttons if a hair or beard is active
                string transformName = d.Key;
                if(itemType == Data.typeHairs && transformName != Data.NoHairNeverDeactivate) {
                    scrollviewContent.Find("No Hair").GetComponent<ItemSelectionButton>().SetButtonSelectedState(false);
                }
                if(itemType == Data.typeBeards && transformName != Data.NoBeardNeverDeactivate) {
                    scrollviewContent.Find("No Beard").GetComponent<ItemSelectionButton>().SetButtonSelectedState(false);
                }
            }
        }

    }

    internal void DeselectOtherButtons(string buttonToRemainSelected) {
        foreach (Transform button in scrollviewContent) {
            if(button.name != buttonToRemainSelected && button.name != "Button Template") {
                button.GetComponent<ItemSelectionButton>().SetButtonSelectedState(false);
            }
        }
    }
#endregion

#region Private Methods

#endregion

#region Public Methods

#endregion
}