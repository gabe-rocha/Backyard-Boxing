using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCustomizationManager : MonoBehaviour
{

    [SerializeField] private GameObject mainCharacterCustomizationScreen;
    [SerializeField] private ItemSelectionBuilder hairSelectionScreenBuilder;
    [SerializeField] private ItemSelectionBuilder beardSelectionScreen;
    [SerializeField] private ItemSelectionBuilder accessoriesSelectionScreen;
    [SerializeField] private ItemSelectionBuilder skinColorSelectionScreen;
    [SerializeField] private ItemSelectionBuilder shirtSelectionScreen;
    [SerializeField] private ItemSelectionBuilder shortsSelectionScreen;
    [SerializeField] private ItemSelectionBuilder glovesSelectionScreen;
    [SerializeField] private ItemSelectionBuilder shoesSelectionScreen;

    private void Start()
    {
        HideAllScreens();
        mainCharacterCustomizationScreen.SetActive(true);
    }

    private void HideAllScreens(){
        mainCharacterCustomizationScreen.SetActive(false);
        shirtSelectionScreen.Hide();
        hairSelectionScreenBuilder.Hide();
        beardSelectionScreen.Hide();
        shortsSelectionScreen.Hide();
        // accessoriesSelectionScreen.Hide();
        // skinColorSelectionScreen.Hide();
        // glovesSelectionScreen.Hide();
        // shoesSelectionScreen.Hide();

        return;
    }

    public void OnButtonBackPressed(){
        HideAllScreens();
        mainCharacterCustomizationScreen.SetActive(true);
    }

    public void OnButtonHairPressed(){
        HideAllScreens();
        hairSelectionScreenBuilder.Show();
    }

    public void OnButtonBeardPressed(){
        HideAllScreens();
        beardSelectionScreen.Show();
    }
    
    public void OnButtonHatsPressed(){
        HideAllScreens();
        accessoriesSelectionScreen.Show();
    }
    
    public void OnButtonSkinColorPressed(){
        HideAllScreens();
        skinColorSelectionScreen.Show();
    }
    
    public void OnButtonShirtPressed(){
        HideAllScreens();
        shirtSelectionScreen.Show();
    }
    
    public void OnButtonShortsPressed(){
        HideAllScreens();
        shortsSelectionScreen.Show();
    }
    
    public void OnButtonGlovesPressed(){
        HideAllScreens();
        glovesSelectionScreen.Show();
    }
    
    public void OnButtonShoesPressed(){
        HideAllScreens();
        shoesSelectionScreen.Show();
    }

    public void OnButtonPlayPressed(){
        GameData.gameState = GameData.GameStates.Loading;
        SceneManager.LoadScene(1);
    }

}
