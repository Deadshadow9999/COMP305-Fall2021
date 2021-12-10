using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        // Retro scrolling background
        //InvokeRepeating("MoveBG", scrollSpeed, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.mainTextureOffset = new Vector2(Time.time * scrollSpeed, 0);
    }

    private void MoveBG()
    {
        renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x + 0.05f, 0);
    }
}
