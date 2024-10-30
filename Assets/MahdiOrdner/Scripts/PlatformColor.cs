using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColor : MonoBehaviour
{
    public static int numOfPlatforms = 0;
    public static int countActivated = 0;
    public static bool allActivated = false;
    private bool done;
    private Renderer render;
    [SerializeField]
    private Material unactivated;
    [SerializeField]
    private Material activated;
    [SerializeField]
    private Material finished;
    private bool toggle;

    private void Awake()
    {
        toggle = false;
        done = false;
        //bringt nur beim ersten Aufruf ohne generateLevel etwas
        numOfPlatforms++;
    }

    private void Start()
    {
        render = GetComponent<Renderer>();
        render.material = unactivated;
    }

    private void Update()
    {
        if (!done && allActivated)
        {
            render.material = finished;
            done = true;
        }
    }

    public void toggleMaterial()
    {
        if(!allActivated)
        {
            if (toggle)
            {
                render.material = unactivated;
                countActivated--;
            }
            else
            {
                render.material = activated;
                countActivated++;
            }
            toggle = !toggle;
            allActivated = numOfPlatforms == countActivated;
        }
    }
}
