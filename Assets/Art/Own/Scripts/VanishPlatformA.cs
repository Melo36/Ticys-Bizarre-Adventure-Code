using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatformA : MonoBehaviour
{
    enum State
    {
        Invisible,
        Visible,
        Blinking,
    }

    private State state = State.Visible;
    public float timeInvisible = 2;
    public float timeVisible = 3;
    private float currTimer;
    public float blinkRate = 0.2f;
    public int blinkCount = 5;
    private int currBlinkCount = 0;
    private MeshRenderer meshRenderer;
    private Collider[] colliders;
    private MovingPlatform platformMover;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliders = GetComponents<Collider>();
        platformMover = GetComponent<MovingPlatform>();
    }

    private void Update()
    {
        if (state != State.Blinking)
        {
            currTimer += Time.deltaTime;
            if (state == State.Visible)
            {
                if (currTimer > timeVisible)
                {
                    StartCoroutine(BlinkToInvisible());
                }
            }
            if (state == State.Invisible)
            {
                if (currTimer > timeInvisible)
                {
                    InvisibleToVisible();
                }
            }
        }
    }

    private void InvisibleToVisible()
    {
        meshRenderer.enabled = true;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
        state = State.Visible;
        currTimer = 0;
    }

    public IEnumerator BlinkToInvisible()
    {
        state = State.Blinking;
        currTimer = 0;
        meshRenderer.enabled = false;
        while (currBlinkCount < blinkCount)
        {
            yield return new WaitForSeconds(blinkRate);
            meshRenderer.enabled = !meshRenderer.enabled;
            if (!meshRenderer.enabled)
            {
                currBlinkCount++;
            }
        }
    }

    
  
    
   
}
    