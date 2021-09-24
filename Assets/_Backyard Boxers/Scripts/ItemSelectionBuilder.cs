using System.Collections;
using System.Collections.Generic;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;

public class ItemSelectionBuilder : MonoBehaviour {
    [SerializeField] private CosmeticItemListSO listOfItemsSO;
    [SerializeField] private GameObject scrollviewContents;
    [SerializeField] private GameObject itemButtonTemplate;

    private Player player;

    // Start is called before the first frame update
    private void Start() {
        player = Player.Instance;
        Initialize();
    }

    private void Initialize() {
        List<CosmeticItemSO> listOfItems = new List<CosmeticItemSO>();

        // if(player.Avatar.activeRace.racedata.raceName == Data.RACE_NAME_MALE)
        //     listOfItems = listOfItemsSO.listOfItemsMale;
        // else
        //     listOfItems = listOfItemsSO.listOfItemsFemale;

        foreach (var item in listOfItems) {
            //instantiate a button
            var button = Instantiate(itemButtonTemplate, Vector3.zero, Quaternion.identity, scrollviewContents.transform);
            // button.GetComponent<ItemSelectionButton>().Initialize(item);
        }
        itemButtonTemplate.SetActive(false);
    }

    public void Hide() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }
    public void Show() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }

}