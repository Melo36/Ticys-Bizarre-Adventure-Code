using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlatforms : MonoBehaviour
{
    [SerializeField]
    private GameObject colorPrefab;
    [SerializeField]
    private GameObject breakPrefab;
    [SerializeField]
    private float emptyChance;
    [SerializeField]
    private float colorChance;
    private Vector3[,] positions;

    private void Awake()
    {
        //Fange unten rechts an zu zaehlen
        //X geht nach oben
        //Z geht nach links
        positions = new Vector3[9, 9];
        for (int a = 0; a < 9; a++)
        {
            for (int b = 0; b < 9; b++)
            {
                positions[a, b] = new Vector3((a - 4) * 14, 10, (b - 4) * 14);
            }
        }
    }

    private void Start()
    {
        generateRandomLevel();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(PlayerPhysicMa.onStart)
            {
                generateRandomLevel();
            }
            else
            {
                StartCoroutine(notOnStart());
            }
        }
    }

    private IEnumerator notOnStart()
    {
        GameData.showText("You can only reset the level while standing on the starting platform!");
        yield return new WaitForSeconds(4);
        GameData.showText("");
    }

    public void generateRandomLevel()
    {
        //ES FUNKTIONIERT
        //POG
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int colorPlatforms = 0;
        for(int a = 0; a < 9; a++)
        {
            for(int b = 0; b < 9; b++)
            {
                if (a!=4 || b!=4)
                {
                    if(Random.value > (emptyChance / 100))
                    {
                        if (Random.value > (colorChance / 100))
                        {
                            Instantiate(breakPrefab, positions[a, b], Quaternion.identity, transform);
                        }
                        else
                        {
                            Instantiate(colorPrefab, positions[a, b], Quaternion.identity, transform);
                            colorPlatforms++;
                        }
                    }
                }
            }
        }
        PlatformColor.numOfPlatforms = colorPlatforms;
        PlatformColor.countActivated = 0;
        PlatformColor.allActivated = false;
    }
}
