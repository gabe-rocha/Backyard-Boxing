using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{
    Player player;
    OpponentAI opponent;

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> Data.player != null && Data.opponent != null);
        player = Data.player;
        opponent = Data.opponent;
    }
    public void OnMovementButtonPressed(bool left){
        player.Move(left);
        StartCoroutine(MoveOpponent(left));
    }

    private IEnumerator MoveOpponent(bool left)
    {
        yield return new WaitForSeconds(0.25f);
        opponent.Move(left);
    }
}