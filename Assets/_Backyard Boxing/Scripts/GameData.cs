using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public enum GameStates{
        Loading,
        SelectingCharacter,
        Fighting
    }

    public static GameStates gameState = GameStates.Loading;
    public static Player player;
    public static OpponentAI opponent;


    public static string SLOT_NAME_HAIR = "Hair";
    public static string SLOT_NAME_CHEST = "Chest";
    public static string RACE_NAME_MALE = "HumanMale";
    public static string RACE_NAME_FEMALE = "HumanFemale";
}
