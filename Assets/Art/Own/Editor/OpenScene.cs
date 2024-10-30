using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;




public class OpenScene : MonoBehaviour
{
    [MenuItem("OpenScene/MainMenu")]
    static void MainMenu()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Own/Scenes/MainMenu.unity");
        }
    }

    [MenuItem("OpenScene/Level1")]
    static void Level1()
    {
       if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Own/Scenes/Level1.unity");
        }
        

    }
    [MenuItem("OpenScene/Level2")]
    static void Level2()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Own/Scenes/Level2.unity");
        }


    }
    [MenuItem("OpenScene/DeathScene")]
    static void DeathScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Own/Scenes/DeathScene.unity");
        }
    }

    [MenuItem("OpenScene/WinScene")]
    static void WinScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Own/Scenes/WinScene.unity");
        }
    }
}

