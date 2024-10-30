using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class GameData
{
    //Verstehe nicht warum man diesen komischen Kram mit der instance machen soll, wenn es auch einfach mit statischen Attributen und Methoden geht
    private static Sprite fullHeart, emptyHeart;
    private static Text middleText;
    private static Image[] lifeImages;
    private static bool initialized = false;

    //Shared attributes across scenes
    public static int money;
    public static int bosslive = 5;
    public static int score = 0;
    public static int lives = 3;
    public static int currentScene = 0;

    public static void initialize()
    {
        if (!initialized)
        {
            lifeImages = new Image[3];
            middleText = GameObject.Find("MiddleText").GetComponent<Text>();
            lifeImages[0] = GameObject.Find("Lives1").GetComponent<Image>();
            lifeImages[1] = GameObject.Find("Lives2").GetComponent<Image>();
            lifeImages[2] = GameObject.Find("Lives3").GetComponent<Image>();
            //Ich weiss, ziemlich unschoen die Loesung
            fullHeart = lifeImages[0].sprite;
            emptyHeart = lifeImages[1].sprite;
            resetLives();
            score = 0;
            lives = 3;
            initialized = true;
        }
        else
        {
            middleText.text = "";
            resetLives();
            lives = 3;
        }
    }

    public static void firstScene()
    {
        currentScene = 0;
        SceneManager.LoadScene(currentScene);
    }

    public static void LoadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public static void loadNextScene()
    {
        lives = 3;
        SceneManager.LoadScene(++currentScene);
    }

    public static void locked(double time)
    {
        if (time > 0)
        {
            string text = time.ToString();
            if (text.Length > 7)
                text = text.Substring(0, 7);
            middleText.text = "Start in: " + text;
        }
        else
            middleText.text = "";
    }

    public static void showText(string t)
    {
        middleText.text = t;
    }

    public static void resetLives()
    {
        lives = 3;
        for(int i = 0; i < 3; i++)
        {
            lifeImages[i].sprite = fullHeart;
        }
    }

    public static void die()
    {
        lifeImages[--lives].sprite = emptyHeart;
    }
}
