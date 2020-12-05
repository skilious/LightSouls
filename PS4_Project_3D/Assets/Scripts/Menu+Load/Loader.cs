using System.Collections;
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
        CreditsScene,
        Tutorial,
        Stage_01,
        Stage_02,
        Stage_03,
        Stage_04,
        Level_Skull,
        SoulGame,
        Level_Select,
        L1_Boss,
        L2_Boss
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
