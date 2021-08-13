using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationManager : MonoBehaviour
{

    [SerializeField] private GameObject mainCharacterCustomizationScreen;
    [SerializeField] private GameObject hairSelectionScreen;
    [SerializeField] private GameObject beardSelectionScreen;
    [SerializeField] private GameObject accessoriesSelectionScreen;
    [SerializeField] private GameObject skinColorSelectionScreen;
    [SerializeField] private GameObject shirtSelectionScreen;
    [SerializeField] private GameObject shortsSelectionScreen;
    [SerializeField] private GameObject glovesSelectionScreen;
    [SerializeField] private GameObject shoesSelectionScreen;

    private void Start()
    {
        HideAllScreens();
        mainCharacterCustomizationScreen.SetActive(true);
    }

    private void HideAllScreens(){
        mainCharacterCustomizationScreen.SetActive(false);
        hairSelectionScreen.SetActive(false);

        return;
        beardSelectionScreen.SetActive(false);
        accessoriesSelectionScreen.SetActive(false);
        skinColorSelectionScreen.SetActive(false);
        shirtSelectionScreen.SetActive(false);
        shortsSelectionScreen.SetActive(false);
        glovesSelectionScreen.SetActive(false);
        shoesSelectionScreen.SetActive(false);
    }

    public void OnButtonBackPressed(){
        HideAllScreens();
        mainCharacterCustomizationScreen.SetActive(true);
    }

    public void OnButtonHairPressed(){
        HideAllScreens();
        hairSelectionScreen.SetActive(true);
    }

    public void OnButtonBeardPressed(){
        HideAllScreens();
        beardSelectionScreen.SetActive(true);
    }
    
    public void OnButtonHatsPressed(){
        HideAllScreens();
        accessoriesSelectionScreen.SetActive(true);
    }
    
    public void OnButtonSkinColorPressed(){
        HideAllScreens();
        skinColorSelectionScreen.SetActive(true);
    }
    
    public void OnButtonShirtPressed(){
        HideAllScreens();
        shirtSelectionScreen.SetActive(true);
    }
    
    public void OnButtonShortsPressed(){
        HideAllScreens();
        shortsSelectionScreen.SetActive(true);
    }
    
    public void OnButtonGlovesPressed(){
        HideAllScreens();
        glovesSelectionScreen.SetActive(true);
    }
    
    public void OnButtonShoesPressed(){
        HideAllScreens();
        shoesSelectionScreen.SetActive(true);
    }

}
