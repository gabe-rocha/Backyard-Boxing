using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> listUMAS;
    [SerializeField] Transform UMASpawnPosition;

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>GameData.player != null);
        GameObject defaultChar = listUMAS[0];
        InstantiateCharacter(defaultChar);
    }

    public void OnButtonCharacterPressed(int buttonIndex){
        InstantiateCharacter(listUMAS[buttonIndex]);
    }

    private void InstantiateCharacter(GameObject newChar)
    {
        GameData.player.DestroyUMA();
        var currentUMA = Instantiate(newChar, UMASpawnPosition, false);
        GameData.player.SetUMA(currentUMA);
    }
}
