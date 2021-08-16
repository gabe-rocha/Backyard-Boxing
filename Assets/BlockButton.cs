using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButton : MonoBehaviour
{
    Player player;

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>GameData.player != null);
        player = GameData.player;
    }
    
    public void OnButtonHeld(){
        player.Block(true);
    }

    public void OnButtonReleased(){
        player.Block(false);
    }

}
