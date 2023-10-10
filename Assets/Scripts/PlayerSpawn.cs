using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    [SerializeField] private PlayerOne keyboardPlayer;
    [SerializeField] private PlayerTwo controllerPlayer;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        InputScript.CStart += SpawnControllerPlayer;
        InputScript.KStart += SpawnKeyboardPlayer;
        if (playerInputManager.playerCount > 1)
            playerInputManager.splitScreen = true;
    }

    private void SpawnKeyboardPlayer()
    {
        try
        {
            playerInputManager.playerPrefab = keyboardPlayer.gameObject;
        }
        catch (NullReferenceException nullRef)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("No player prefab assigned on keyboard player" + nullRef);
  #endif
        }
        
        playerInputManager.JoinPlayer();
        InputScript.KStart -= SpawnKeyboardPlayer;
        if (playerInputManager.playerCount > 1)
            playerInputManager.splitScreen = true;

    }
    
    private void SpawnControllerPlayer()
    {
        try
        {
            playerInputManager.playerPrefab = controllerPlayer.gameObject;
        }
        catch (NullReferenceException nullRef)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("No player prefab assigned on controller player" + nullRef);
  #endif
        }
        
        playerInputManager.JoinPlayer();
        InputScript.CStart -= SpawnControllerPlayer;
        if (playerInputManager.playerCount > 1)
            playerInputManager.splitScreen = true;
    }

    

    
}
