using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatformController : MonoBehaviour
{

    // Private Variables
    private PlayerController playerController;
    private Animator anim;
    private bool bouncingPlatformIsActive;
    public float fixedScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.Instance;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Communicate with the animator
        anim.SetBool("bouncingPlatformIsActive", bouncingPlatformIsActive);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerController.IncreaseJumpForce();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
    if(other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
            other.transform.localScale = new Vector3(fixedScale / transform.localScale.x, fixedScale / transform.localScale.y, fixedScale / transform.localScale.z);
            bouncingPlatformIsActive = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            bouncingPlatformIsActive = false;
            playerController.DecreaseJumpForce();
        }
    }
}
