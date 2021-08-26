using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButton : MonoBehaviour
{
    Player player;

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>Data.player != null);
        player = Data.player;
    }
    
    public void OnButtonHeld(){
        player.Block(true);
    }

    public void OnButtonReleased(){
        player.Block(false);
    }

}
