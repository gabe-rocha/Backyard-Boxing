using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacterPrefab, opponentCharacterPrefab;
    public Transform playerSpawnPosition, opponentSpawnPosition, ringCenter;

    
    private static FightManager instance;
    public static FightManager Instance { get { return instance; } }
    private Player player;
    private OpponentAI opponent;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(()=>GameData.player != null);
        player = GameData.player;
        opponent = GameData.opponent;
        
        //instantiate player
        InstantiatePlayer();
        //intantiate opponent
        InstantiateOpponent();

        //start fight
        StartCoroutine(StartFight());
        
    }

    private IEnumerator StartFight()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Fight!");
        GameData.gameState = GameData.GameStates.Fighting;
    }

    private void InstantiateOpponent()
    {
        var opponentChar = Instantiate(opponentCharacterPrefab, opponentSpawnPosition.position, Quaternion.identity);
        opponent.SetUMA(opponentChar);
    }

    private void InstantiatePlayer()
    {
        var playerChar = Instantiate(playerCharacterPrefab, playerSpawnPosition.position, Quaternion.identity);
        player.SetUMA(playerChar);
    }
}
