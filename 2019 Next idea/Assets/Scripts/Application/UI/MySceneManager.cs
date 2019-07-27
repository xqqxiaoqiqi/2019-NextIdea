using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    private static int StartUpScene = 0;

    private static int LevelSelectionScene = 1;


    public static void JumpToStartUpScene()
    {
        SceneManager.LoadScene(StartUpScene);
    }

    public static void JumpToLevelSelectionScene()
    {
        SceneManager.LoadScene(LevelSelectionScene);
    }


    public static bool JumpToGameScene( int SceneID )
    {
        try
        {
            SceneManager.LoadScene(SceneID);
        }
        catch(System.Exception e)
        {
            Debug.LogError(e.Message);

            return false;
        }

        return true;
    }

}
