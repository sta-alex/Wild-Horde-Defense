using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip meinSoundClip;
    public GameManager gameManager;
    public GameObject closedChest;
    public GameObject openedChest;
    public GameObject coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openChest()
    {
        closedChest.SetActive(false);
        openedChest.SetActive(true);
        coins.SetActive(true);
        audioSource.PlayOneShot(meinSoundClip);
        gameManager.increaseCurrency(100);
    }

}
