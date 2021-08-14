using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject umaHolder;
    
    private GameObject UMA;
    private DynamicCharacterAvatar avatar;
    public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

    private IEnumerator Start(){
        yield return new WaitUntil(()=> GameData.SLOT_NAME_CHEST != null);
        GameData.playerTransform = gameObject.transform;
        GameData.player = this;
    }
    
    public void SetWardrobeSlotItem(string slot, string recipe){         
        if(Avatar != null){
            Avatar.SetSlot(slot, recipe);
            Avatar.BuildCharacter();
        }
    }

    internal void DestroyUMA()
    {
        if(umaHolder.transform.childCount == 0) return;
        Destroy(umaHolder.transform.GetChild(0).gameObject);
        UMA = null;
    }

    public void SetUMA(GameObject newUma){
        UMA = newUma;
        Avatar = UMA.GetComponent<DynamicCharacterAvatar>();
        newUma.transform.parent = umaHolder.transform;
    }
}