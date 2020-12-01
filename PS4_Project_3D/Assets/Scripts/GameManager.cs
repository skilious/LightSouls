using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public static GameManager GMInstance;
    private static bool sceneChanged = false;
    private void Awake()
    {
        GMInstance = this;
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerCheckpointX") && !PlayerPrefs.HasKey("PlayerCheckpointY")) //If it doesn't exist, setup one.
        {
            PlayerPrefs.SetFloat("PlayerCheckpointX", player.transform.position.x); //Saves X and Y axis from the player's starting position.
            PlayerPrefs.SetFloat("PlayerCheckpointY", player.transform.position.z);
            print("Saved!");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointX") + " X axis");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointY") + " Z axis");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Resetting playerprefs. Restart for changes");
            PlayerPrefs.DeleteAll();
        }
    }
    private Vector3 LoadPosition()
    {
        float playerPosX = PlayerPrefs.GetFloat("PlayerCheckpointX");
        float playerPosY = PlayerPrefs.GetFloat("PlayerCheckpointY");
        Vector3 playerPosition = new Vector3(playerPosX, player.transform.position.y, playerPosY);
        if (PlayerPrefs.HasKey("PlayerCheckpointX"))
        {
            return player.transform.position = playerPosition;
        }
        else
            return player.transform.position;
    }

    public void SavePosition(String sceneName)
    {
        print(PlayerPrefs.GetInt("LevelScene"));
        PlayerPrefs.SetFloat("PlayerCheckpointX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerCheckpointY", player.transform.position.z);
        PlayerPrefs.SetString("LevelScene", sceneName);
    }

    public Vector3 SetPosition(Vector3 playerPos)
    {
        return player.transform.position = playerPos;
    }
    public static GameObject GetPlayer()
    {
        return player;
    }

    void OnEnable()
    {
        print("On enable! - Enable the OnSceneLoaded script and save position");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string getLevelScene = PlayerPrefs.GetString("LevelScene");
        if (getLevelScene != SceneManager.GetActiveScene().name && !sceneChanged)
        {
            sceneChanged = true;
            SceneManager.LoadScene(getLevelScene);
        }
        sceneChanged = false;
        LoadPosition();
    }

    void OnDisable()
    {
        print("Disable the script and terminate.");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
