using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildMenu : MonoBehaviour
{
    public GameObject BuilderMenu;
    public BuildSelectionTower buildSelectionTower;

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
            buildSelectionTower.setIsinPreview(false);
            buildSelectionTower.destroyPreview();
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void selectedTowerIcon()
    {

    }

}
