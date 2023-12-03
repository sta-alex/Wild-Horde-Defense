using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

    public GameObject textComponent;
    private float blinkInterval = 0.5f;
    private bool isTextVisible = true;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isTextVisible = !isTextVisible;
            textComponent.SetActive(isTextVisible);
            timer = blinkInterval;
        }

    }

    public void loadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameMenu");
    }

    public void restartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
    }
}
