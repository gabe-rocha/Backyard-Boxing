using System.Collections;
using System.Collections.Generic;

public static class Data {

    public enum GameStates {
        Loading,
        SelectingCharacter,
        ShowingRoster,
        ShowingRingGirl,
        Fighting,
        ShowingResults,
        FightEnd
    }

    public static GameStates gameState = GameStates.Loading;

    public static string SLOT_NAME_HAIR = "Hair";
    public static string SLOT_NAME_CHEST = "Chest";
    public static string RACE_NAME_MALE = "HumanMale";
    public static string RACE_NAME_FEMALE = "HumanFemale";

    public static float tiltMinX = 0.5f;
    public static float movementStepDelay = 1f; //max 1 step every movementStepRate
    public static float fightJabDelay = 0.5f;
    public static float fightSlipDelay = 1f;
}