using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildMenu : MonoBehaviour
{
    public GameManager gameManager;
    public BuildSelectionTower selectionTower;
    private GameObject lighting;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTowerMenu()
    {
        lighting = GameObject.Find("Lightning");
        disableLighting();

        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            gameManager.enabled = true;
            GameObject.Destroy(selectionTower.getPreviewTower());
        }
        else
        {
            this.gameObject.SetActive(true);
        }

    }

    private void disableLighting()
    {
        if(lighting != null)
        {
            lighting.SetActive(false);
        }
    }


}
