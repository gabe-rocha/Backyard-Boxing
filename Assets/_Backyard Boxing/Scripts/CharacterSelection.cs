using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> listCharacters;
    [SerializeField] Transform characterSpawnPosition;

    private GameObject currentChar;

    private void Start()
    {
        GameObject defaultChar = listCharacters[0];
        currentChar = Instantiate(defaultChar, characterSpawnPosition, false);
    }

    public void OnButtonCharacterPressed(int buttonIndex){
        InstantiateCharacter(listCharacters[buttonIndex]);
    }

    private void InstantiateCharacter(GameObject newChar)
    {
        Destroy(currentChar);
        currentChar = Instantiate(newChar, characterSpawnPosition, false);
    }
}
