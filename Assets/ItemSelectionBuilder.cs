using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using UMA;

public class ItemSelectionBuilder : MonoBehaviour
{
    [SerializeField] private ItemListSO listOfItemsSO;
    [SerializeField] private GameObject scrollviewContents;
    [SerializeField] private GameObject itemButtonTemplate;
    
    private Player player;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>Data.player != null);
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize(){
        List<ItemSO> listOfItems = new List<ItemSO>();
        player = Data.player;

        yield return new WaitUntil(()=>player.Avatar != null); //it may take a while to get the avatar
        
        if(player.Avatar.activeRace.racedata.raceName == Data.RACE_NAME_MALE)
            listOfItems = listOfItemsSO.listOfItemsMale;
        else
            listOfItems = listOfItemsSO.listOfItemsFemale;

        foreach (var item in listOfItems)
        {
            //instantiate a button
            var button = Instantiate(itemButtonTemplate, Vector3.zero, Quaternion.identity, scrollviewContents.transform);
            button.GetComponent<ItemSelectionButton>().Initialize(item);
        }
        itemButtonTemplate.SetActive(false);
    }

    public void Hide(){
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void Show(){
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
}
