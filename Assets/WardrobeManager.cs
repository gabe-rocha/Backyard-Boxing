using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeManager : MonoBehaviour {

#region Public Fields
#endregion

#region Private Serializable Fields

#endregion

#region Private Fields    

    private Dictionary<string, int> dicWardrobe = new Dictionary<string, int>();

#endregion

#region MonoBehaviour CallBacks
    void Start() {

        ReadWardrobeFromDiskAndApply();

    }
#endregion

#region Private Methods

    private void ReadWardrobeFromDiskAndApply() {

        dicWardrobe = GetPlayerWardrobeFromHisBody();

        Dictionary<string, int> dicToChange = new Dictionary<string, int>(dicWardrobe);
        foreach (var d in dicWardrobe) {
            dicToChange[d.Key] = PlayerPrefs.GetInt(d.Key, 0);
        }

        if(PlayerPrefs.GetInt("Wardrobe Ever Saved Before") == 0) {
            //We need to activate basic parts for the first time playing
            Debug.Log("Wardrobe Never Saved Before, Activating basic parts...");
            transform.FindDeepChild("H_NoHairNeverDeactivate").gameObject.SetActive(true);
            dicToChange["H_NoHairNeverDeactivate"] = 1;
            transform.FindDeepChild("B_NoBeardNeverDeactivate").gameObject.SetActive(true);
            dicToChange["B_NoBeardNeverDeactivate"] = 1;
            transform.FindDeepChild("T_No Shirt").gameObject.SetActive(true);
            dicToChange["T_No Shirt"] = 1;
            transform.FindDeepChild("G_No Gloves").gameObject.SetActive(true);
            dicToChange["G_No Gloves"] = 1;
            transform.FindDeepChild("P_No Pants").gameObject.SetActive(true);
            dicToChange["P_No Pants"] = 1;
            transform.FindDeepChild("S_No Shoes").gameObject.SetActive(true);
            dicToChange["S_No Shoes"] = 1;
        }
        dicWardrobe = dicToChange;

        //Now activate the parts
        foreach (var d in dicWardrobe) {
            Transform playerPiece = transform.FindDeepChild(d.Key);
            if(playerPiece != null) {
                playerPiece.gameObject.SetActive(d.Value == 1);
            }
        }

        Debug.Log($"Wardrobe Read from PlayerPrefs and applied");
        EventManager.Instance.TriggerEvent(EventManager.Events.WardrobeApplied);
    }

    private void SaveWardrobeToDisk() {
        dicWardrobe = GetPlayerWardrobeFromHisBody();
        foreach (var d in dicWardrobe) {
            PlayerPrefs.SetInt(d.Key, d.Value);
        }

        PlayerPrefs.SetInt("Wardrobe Ever Saved Before", 1);
        Debug.Log($"Wardrobe Saved to PlayerPrefs");
    }

    public Dictionary<string, int> GetPlayerWardrobeFromHisBody() {
        Dictionary<string, int> dic = new Dictionary<string, int>();

        bool includeInactive = true;
        var allTransforms = GetComponentsInChildren<Transform>(includeInactive);

        foreach (Transform t in allTransforms) {
            string pieceName = t.name;
            string pieceType = t.name.Substring(0, 2);

            if(pieceType != Data.typeHairs &&
                pieceType != Data.typeBeards &&
                pieceType != Data.typeAccessories &&
                pieceType != Data.typeTops &&
                pieceType != Data.typePants &&
                pieceType != Data.typeGloves &&
                pieceType != Data.typeShoes &&
                pieceType != Data.typeUnderwears) {

            } else {
                dic.Add(pieceName, t.gameObject.activeSelf ? 1 : 0);
            }
        }

        return dic;
    }
#endregion

#region Public Methods
    public void SetPlayerTransformActive(Transform trans, bool isActive) {

        var allTransforms = GetComponentsInChildren<Transform>(true);

        switch (trans.name.Substring(0, 2)) {

            case Data.typeAccessories:
                trans.gameObject.SetActive(isActive);

                //we will not deactivate the others
                break;

            case Data.typeHairs:
                trans.gameObject.SetActive(isActive);
                //deactivate all others
                foreach (Transform t in allTransforms) {
                    string pieceName = t.name;
                    string pieceType = t.name.Substring(0, 2);
                    if(pieceName != trans.name && pieceType == Data.typeHairs) {
                        t.gameObject.SetActive(!isActive);
                    }
                    if(pieceName == Data.NoHairNeverDeactivate) {
                        t.gameObject.SetActive(true);
                    }
                }
                break;

            case Data.typeBeards:
                trans.gameObject.SetActive(isActive);
                //deactivate all others
                foreach (Transform t in allTransforms) {
                    string pieceName = t.name;
                    string pieceType = t.name.Substring(0, 2);
                    if(pieceName != trans.name && pieceType == Data.typeBeards) {
                        t.gameObject.SetActive(!isActive);
                    }
                    if(pieceName == Data.NoBeardNeverDeactivate) {
                        t.gameObject.SetActive(true);
                    }
                }
                break;

            case Data.typeTops:
                trans.gameObject.SetActive(isActive);
                //deactivate all others
                foreach (Transform t in allTransforms) {
                    string pieceName = t.name;
                    string pieceType = t.name.Substring(0, 2);
                    if(pieceName != trans.name && pieceType == Data.typeTops) {
                        t.gameObject.SetActive(!isActive);
                    }
                }
                break;

            case Data.typePants:
                trans.gameObject.SetActive(isActive);
                //deactivate all others
                foreach (Transform t in allTransforms) {
                    string pieceName = t.name;
                    string pieceType = t.name.Substring(0, 2);
                    if(pieceName != trans.name && pieceType == Data.typePants) {
                        t.gameObject.SetActive(!isActive);
                    }
                }
                break;

            case Data.typeGloves:
                trans.gameObject.SetActive(isActive);
                //deactivate all others
                foreach (Transform t in allTransforms) {
                    string pieceName = t.name;
                    string pieceType = t.name.Substring(0, 2);
                    if(pieceName != trans.name && pieceType == Data.typeGloves) {
                        t.gameObject.SetActive(!isActive);
                    }
                }
                break;

            case Data.typeShoes:
                trans.gameObject.SetActive(isActive);
                //deactivate all others
                foreach (Transform t in allTransforms) {
                    string pieceName = t.name;
                    string pieceType = t.name.Substring(0, 2);
                    if(pieceName != trans.name && pieceType == Data.typeShoes) {
                        t.gameObject.SetActive(!isActive);
                    }
                }
                break;

            default:
                break;
        }

        // var trans = transform.FindDeepChild(transformName.name);

        SaveWardrobeToDisk();
    }
#endregion
}