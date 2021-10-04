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
        FightEnd,
        Training
    }

    public static GameStates gameState = GameStates.Loading;

    public static string SLOT_NAME_HAIR = "Hair";
    public static string SLOT_NAME_CHEST = "Chest";
    public static string RACE_NAME_MALE = "HumanMale";
    public static string RACE_NAME_FEMALE = "HumanFemale";

    public static float tiltMinX = 0.5f;
    public static float movementStepDelay = 1f; //max 1 step every movementStepRate
    public static float fightSlipDelay = 1f;
    public static float fightJabDelay = 0.5f;
    public static float fightCrossDelay = 1f;
    public static float fightHookDelay = 1f;
    public static float fightUppercutDelay = 1.5f;
    public const int roundDuration = 60;
    public const string typeHairs = "H_";
    public const string typeBeards = "B_";
    public const string typeAccessories = "X_";
    public const string typeTops = "T_";
    public const string typePants = "P_";
    public const string typeGloves = "G_";
    public const string typeShoes = "S_";
    public const string typeUnderwears = "U_";

    public const string NoHairNeverDeactivate = "H_NoHairNeverDeactivate";
    public const string NoBeardNeverDeactivate = "B_NoBeardNeverDeactivate";
}