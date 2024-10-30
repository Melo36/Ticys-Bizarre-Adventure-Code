using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fadeSeconds; 
    [SerializeField]
    private float shrinkSeconds;
    private float secondsPassed = 0;
    private bool breaking = false;
    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    //break ist aus irgendeinem Grund nicht erlaubt als Methodenname
    public void shrink()
    {
        if(!breaking)
        {
            breaking = true;
            StartCoroutine(IEshrink());
        }
    }

    private IEnumerator IEshrink()
    {
        while (secondsPassed < fadeSeconds)
        {
            secondsPassed += Time.deltaTime;
            render.material.color = Color.Lerp(Color.red, Color.black, (secondsPassed / fadeSeconds));
            yield return null;
        }
        secondsPassed = shrinkSeconds;
        while (secondsPassed > 0)
        {
            secondsPassed -= Time.deltaTime;
            transform.localScale = new Vector3(3, (secondsPassed / shrinkSeconds) * 0.5f, 3);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
