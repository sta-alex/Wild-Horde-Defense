using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerPlacement : MonoBehaviour
{
    public BuildSelectionTower buildSelectionTower;
    private GameObject currentPlacedTower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPlacedTower = buildSelectionTower.getCurrentPlacedTower();
        if (currentPlacedTower == null)
        {

        }
        else
        {

        }
    }



}
