﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public static GameManager GMInstance;
    private static bool sceneChanged = false;

    private GameObject[] teleporters;

    public static int stageCompleted = 0;
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
            PlayerPrefs.SetFloat("PlayerCheckpointY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerCheckpointZ", player.transform.position.z);
            print("Saved!");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointX") + " X axis");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointY") + " Y axis");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointZ") + " Z axis");
        }
        if (teleporters == null)
        {
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Resetting playerprefs. Restart for changes");
            PlayerPrefs.DeleteAll();
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            SaveLevelCompletion(2);
            print("Set two level completions. You will no longer partake in those first two levels anymore."); 
        }
    }
    private Vector3 LoadPosition()
    {
        float playerPosX = PlayerPrefs.GetFloat("PlayerCheckpointX");
        float playerPosY = PlayerPrefs.GetFloat("PlayerCheckpointY");
        float playerPosZ = PlayerPrefs.GetFloat("PlayerCheckpointZ");
        Vector3 playerPosition = new Vector3(playerPosX, playerPosY, playerPosZ);

        if (PlayerPrefs.HasKey("PlayerCheckpointX"))
        {
            return player.transform.position = playerPosition;
        }
        else
            return player.transform.position;
    }

    public void SaveLevelCompletion(int levelNum)
    {
        PlayerPrefs.SetInt("Stage", levelNum); //This will set the key "Stage" with the value from stage.
    }

    public void SavePosition(String sceneName)
    {
        print(PlayerPrefs.GetInt("LevelScene"));
        PlayerPrefs.SetFloat("PlayerCheckpointX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerCheckpointY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerCheckpointZ", player.transform.position.z);
        PlayerPrefs.SetFloat("Health", player.GetComponent<Character_Status>().healthHit);
        PlayerPrefs.SetInt("Capacity", player.GetComponent<Character_Status>().curCapacity);
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
        float loadHealth = PlayerPrefs.GetFloat("Health");
        int loadCapacity = PlayerPrefs.GetInt("Capacity");
        if (loadHealth != 0 || loadCapacity != 0)
        {
            player.GetComponent<Character_Status>().curCapacity = loadCapacity;
            player.GetComponent<Character_Status>().healthHit = loadHealth;
        }

        string getLevelScene = PlayerPrefs.GetString("LevelScene");
        if (getLevelScene != SceneManager.GetActiveScene().name && !sceneChanged)
        {
            sceneChanged = true;
            SceneManager.LoadScene(getLevelScene);
        }

        sceneChanged = false; //This allows changing scenes again.
        LoadPosition(); //Load the save keys from playerprefs.

        if (PlayerPrefs.HasKey("Stage")) //If it has an existing key called Stage.
        {
            stageCompleted = PlayerPrefs.GetInt("Stage"); //Grab the value from stage loaded from previous gameplay.
            PopUp_TC.completedStages = stageCompleted;
            print("it works? " + " Levels completed: " + PopUp_TC.completedStages);
        }
    }

    void OnDisable()
    {
        print("Disable the script and terminate.");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
