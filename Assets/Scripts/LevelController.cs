using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour
{
    // Singleton pattern
    private static LevelController _instance;
    public static LevelController Instance 
    { get 
        { 
            return _instance; 
        } 
    }

    [SerializeField] private Text itemUIText;
    [SerializeField] private PlayableDirector pdirector;

    // Private variables
    private int totalItemsQuantity = 0, itemsCollectedQuantity = 0;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        pdirector.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalItemsQuantity = GameObject.FindGameObjectsWithTag("Item").Length;
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        itemUIText.text = itemsCollectedQuantity + " / " + totalItemsQuantity;
    }

    public void PickupItem()
    {
        itemsCollectedQuantity++;
        UpdateItemUI();
    }

    public void CheckLevelEnd()
    {
        if(itemsCollectedQuantity == totalItemsQuantity)
        {
            // Play animation of the player jumping up and down

            // Play level end audio
            Camera.main.gameObject.GetComponent<AudioSource>().Stop();

            //Show level end UI
            pdirector.enabled = true;
        }
    }
}
