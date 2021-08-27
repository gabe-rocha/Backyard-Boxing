using System.Collections;
using System.Collections.Generic;

public static class Data
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

    public static float tiltMinX = 0.5f;
    public static float movementStepDelay = 3f; //max 1 step every movementStepRate
    public static float fightJabDelay = 0.5f;
    public static float fightSlipDelay = 1f;
}