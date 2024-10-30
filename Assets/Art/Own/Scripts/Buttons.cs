using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ButtonPressDied()
    {
        GameData.lives = 3;
        GameData.LoadScene();
    }

    public void ButtonPressWin()
    {
        GameData.lives = 3;
        GameData.firstScene();
    }

    public void ButtonPressMain()
    {
        GameData.lives = 3;
        GameData.LoadScene();
    }

}
