using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerPlacement : MonoBehaviour
{


    private Dictionary<TowerPlacement, GameObject> towersInDictionary;
    public BuildSelectionTower buildSelectionTower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BoostPower(GameObject towerToBoost, TowerPlacement towerPlacement)
    {
        towersInDictionary = buildSelectionTower.getTowersPlacedOnPlacementDictionary();

        if (towersInDictionary != null)
        {
            foreach (TowerPlacement towerPlacementInDictionary in towersInDictionary.Keys)
            {
                if (towerPlacement != null)
                {
                    if (towerPlacementInDictionary.name.Equals(towerPlacement.name))
                    {
                        GameObject towerFromDictionary = towersInDictionary[towerPlacementInDictionary];
                        if (towerFromDictionary.name.Equals(towerToBoost.name) && towerPlacement.name.Equals("TowerPlacement01 (1)"))
                        {
                            Tower tower = towerToBoost.GetComponent<Tower>();
                            if(tower != null)
                            {
                                tower.dmg += 5;
                            }
                        }
                    }
                }
            }
        }
    }


}
