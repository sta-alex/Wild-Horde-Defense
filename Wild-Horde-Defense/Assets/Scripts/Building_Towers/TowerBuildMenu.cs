using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildMenu : MonoBehaviour
{
    public BuildSelectionTower selectionTower;
    public GameObject BuilderMenu;

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
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            GameObject.Destroy(selectionTower.getPreviewTower());
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }


}
