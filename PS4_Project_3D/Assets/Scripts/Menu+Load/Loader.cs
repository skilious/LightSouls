﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu,
        LoadingScene,
        ControlsScene,
        SoulGame,
        CreditsScene,

        // UNUSED
        // Add Level Scenes Here
    }

    private static Scene targetScene;

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

        targetScene = scene;
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
