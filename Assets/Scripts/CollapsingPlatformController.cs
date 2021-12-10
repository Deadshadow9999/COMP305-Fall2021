using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatformController : MonoBehaviour
{
    private Animator anim;
    private bool collapsingPlatformActivated;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        collapsingPlatformActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Communicate with the animator
        anim.SetBool("collapsingPlatformActivated", collapsingPlatformActivated);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            collapsingPlatformActivated = true;
        }
    }
}
