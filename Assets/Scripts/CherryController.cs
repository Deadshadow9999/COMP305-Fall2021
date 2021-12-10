using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject itemFeedback;

    private LevelController levelController;

    void Start()
    {
        levelController = LevelController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Item pickup animation
            Instantiate(itemFeedback, transform.position, transform.rotation);
            Destroy(this.gameObject);

            // Increase player item counter
            levelController.PickupItem();
        }
    }
}
