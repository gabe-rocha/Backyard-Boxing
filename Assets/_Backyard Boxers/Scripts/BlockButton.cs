using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButton : MonoBehaviour {

    private void Start() { }

    public void OnButtonHeld() {
        Player.Instance.Block(true);
    }

    public void OnButtonReleased() {
        Player.Instance.Block(false);
    }
}