using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCustomizationManager : MonoBehaviour {

    [SerializeField] private GameObject mainCharacterCustomizationScreen;
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject accessoriesSelectionScreen;
    [SerializeField] private GameObject hairSelectionScreen;
    [SerializeField] private GameObject beardSelectionScreen;
    [SerializeField] private GameObject topsSelectionScreen;
    [SerializeField] private GameObject pantsSelectionScreen;
    [SerializeField] private GameObject glovesSelectionScreen;
    [SerializeField] private GameObject shoesSelectionScreen;
    [SerializeField] private TextMeshProUGUI textCurrentScreen;
    // [SerializeField] private GameObject skinColorSelectionScreen;

    private IEnumerator Start() {
        yield return new WaitForSeconds(Time.deltaTime);
        ShowMainScreen();
    }

    public void OnButtonBackPressed() {
        ShowMainScreen();
    }

    private void ShowMainScreen() {
        HideAllScreens();
        mainCharacterCustomizationScreen.SetActive(true);
        textCurrentScreen.text = "";
        buttonBack.SetActive(false);
    }

    private void HideAllScreens() {
        mainCharacterCustomizationScreen.SetActive(false);
        topsSelectionScreen.SetActive(false);
        hairSelectionScreen.SetActive(false);
        beardSelectionScreen.SetActive(false);
        pantsSelectionScreen.SetActive(false);
        accessoriesSelectionScreen.SetActive(false);
        glovesSelectionScreen.SetActive(false);
        shoesSelectionScreen.SetActive(false);
        // skinColorSelectionScreen.SetActive(false);

        return;
    }

    public void OnButtonHairPressed() {
        HideAllScreens();
        hairSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Hairs";
        buttonBack.SetActive(true);
    }

    public void OnButtonBeardPressed() {
        HideAllScreens();
        beardSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Beards";
        buttonBack.SetActive(true);
    }

    public void OnButtonHatsPressed() {
        HideAllScreens();
        accessoriesSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Accessories";
        buttonBack.SetActive(true);
    }

    public void OnButtonSkinColorPressed() {
        // HideAllScreens();
        // skinColorSelectionScreen.SetActive(true);
    }

    public void OnButtonShirtPressed() {
        HideAllScreens();
        topsSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Tops";
        buttonBack.SetActive(true);
    }

    public void OnButtonShortsPressed() {
        HideAllScreens();
        pantsSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Bottoms";
        buttonBack.SetActive(true);
    }

    public void OnButtonGlovesPressed() {
        HideAllScreens();
        glovesSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Gloves";
        buttonBack.SetActive(true);
    }

    public void OnButtonShoesPressed() {
        HideAllScreens();
        shoesSelectionScreen.SetActive(true);
        textCurrentScreen.text = "Shoes";
        buttonBack.SetActive(true);
    }
}