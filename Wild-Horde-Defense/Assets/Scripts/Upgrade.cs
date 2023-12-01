using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public GameManager gameManager;

    public List<GameObject> availableTowers;
    private GameObject towerToUpgrade;
    public BuildSelectionTower buildSelectionTower;
    private Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementDictionary;
    public RangePowerPlacement rangeBooster;
    public SpeedPowerPlacement speedBooster;
    public FirePowerPlacement powerBooster;

    private TowerPlacement towerPlacement;
    private bool transaction;

    public void Start()
    {
        towersPlacedOnPlacementDictionary = buildSelectionTower.getTowersPlacedOnPlacementDictionary();
    }
    public void UpgradeCrossbowLvl1()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(300);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject crossbowTowerLvL2 = Instantiate(availableTowers[1], snapPosition, Quaternion.identity);
            crossbowTowerLvL2.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, crossbowTowerLvL2);
            rangeBooster.BoostZone(crossbowTowerLvL2, towerPlacement);
            speedBooster.BoostSpeed(crossbowTowerLvL2, towerPlacement);
            powerBooster.BoostPower(crossbowTowerLvL2, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }
    public void UpgradeCrossbowLvl2()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(900);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject crossbowTowerLvL3 = Instantiate(availableTowers[2], snapPosition, Quaternion.identity);
            crossbowTowerLvL3.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, crossbowTowerLvL3);
            rangeBooster.BoostZone(crossbowTowerLvL3, towerPlacement);
            speedBooster.BoostSpeed(crossbowTowerLvL3, towerPlacement);
            powerBooster.BoostPower(crossbowTowerLvL3, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }
    public void UpgradePoisonLvl1()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(480);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject poisonTowerLvL2 = Instantiate(availableTowers[4], snapPosition, Quaternion.identity);
            poisonTowerLvL2.transform.localScale = new Vector3(14f, 14f, 14f);
            
            replaceFromDictionary(towerToUpgrade, poisonTowerLvL2);
            rangeBooster.BoostZone(poisonTowerLvL2, towerPlacement);
            speedBooster.BoostSpeed(poisonTowerLvL2, towerPlacement);
            powerBooster.BoostPower(poisonTowerLvL2, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }
    public void UpgradePoisonLvl2()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(1440);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject poisonTowerLvL3 = Instantiate(availableTowers[5], snapPosition, Quaternion.identity);
            poisonTowerLvL3.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, poisonTowerLvL3);
            rangeBooster.BoostZone(poisonTowerLvL3, towerPlacement);
            speedBooster.BoostSpeed(poisonTowerLvL3, towerPlacement);
            powerBooster.BoostPower(poisonTowerLvL3, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }

    public void UpgradeCannonLvl1()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(360);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject cannonTowerLvL2 = Instantiate(availableTowers[7], snapPosition, Quaternion.identity);
            cannonTowerLvL2.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, cannonTowerLvL2);
            rangeBooster.BoostZone(cannonTowerLvL2, towerPlacement);
            speedBooster.BoostSpeed(cannonTowerLvL2, towerPlacement);
            powerBooster.BoostPower(cannonTowerLvL2, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }
    public void UpgradeCannonLvl2()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(1080);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject cannonTowerLvL3 = Instantiate(availableTowers[8], snapPosition, Quaternion.identity);
            cannonTowerLvL3.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, cannonTowerLvL3);
            rangeBooster.BoostZone(cannonTowerLvL3, towerPlacement);
            speedBooster.BoostSpeed(cannonTowerLvL3, towerPlacement);
            powerBooster.BoostPower(cannonTowerLvL3, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }
    public void UpgradeFireLvl1()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(600);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject fireTowerLvL2 = Instantiate(availableTowers[10], snapPosition, Quaternion.identity);
            fireTowerLvL2.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, fireTowerLvL2);
            rangeBooster.BoostZone(fireTowerLvL2, towerPlacement);
            speedBooster.BoostSpeed(fireTowerLvL2, towerPlacement);
            powerBooster.BoostPower(fireTowerLvL2, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }
    public void UpgradeFireLvl2()
    {
        loadCurrentSelectedTower();
        transaction = gameManager.updateCurrency(1200);
        if (transaction)
        {
            Vector3 snapPosition = new Vector3(towerToUpgrade.transform.position.x, towerToUpgrade.transform.position.y, towerToUpgrade.transform.position.z);
            GameObject fireTowerLvL3 = Instantiate(availableTowers[11], snapPosition, Quaternion.identity);
            fireTowerLvL3.transform.localScale = new Vector3(14f, 14f, 14f);
            replaceFromDictionary(towerToUpgrade, fireTowerLvL3);
            rangeBooster.BoostZone(fireTowerLvL3, towerPlacement);
            speedBooster.BoostSpeed(fireTowerLvL3, towerPlacement);
            powerBooster.BoostPower(fireTowerLvL3, towerPlacement);
            GameObject.Destroy(towerToUpgrade);
        }
    }

    private void loadCurrentSelectedTower()
    {
        this.towerToUpgrade = gameManager.getCurrentSelectedTower();
    }

    private void replaceFromDictionary(GameObject towerToReplace, GameObject newTower)
    {
        Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementDictionary = buildSelectionTower.getTowersPlacedOnPlacementDictionary();
        if(towersPlacedOnPlacementDictionary != null)
        {
            if (towersPlacedOnPlacementDictionary.ContainsValue(towerToReplace))
            {
                TowerPlacement foundKey = null;
                foreach (var kvp in towersPlacedOnPlacementDictionary)
                {
                    if (kvp.Value == towerToReplace)
                    {
                        foundKey = kvp.Key;
                        towerPlacement = foundKey;
                        break;
                    }
                }

                if(foundKey != null)
                {
                    towersPlacedOnPlacementDictionary[foundKey] = newTower;
                    buildSelectionTower.updateDictionary(towersPlacedOnPlacementDictionary);
                }
            }
        }

        Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementNONSpecialDictionary = buildSelectionTower.getTowersPlacedOnPlacementDictionaryNONSpecial();
        if (towersPlacedOnPlacementNONSpecialDictionary != null)
        {
            if (towersPlacedOnPlacementNONSpecialDictionary.ContainsValue(towerToReplace))
            {
                TowerPlacement foundKey = null;
                foreach (var kvp in towersPlacedOnPlacementNONSpecialDictionary)
                {
                    if (kvp.Value == towerToReplace)
                    {
                        foundKey = kvp.Key;
                        towerPlacement = foundKey;
                        break;
                    }
                }

                if (foundKey != null)
                {
                    towersPlacedOnPlacementNONSpecialDictionary[foundKey] = newTower;
                    buildSelectionTower.updateDictionaryNONSpecialDictionary(towersPlacedOnPlacementNONSpecialDictionary);
                }
            }
        }

    }

}
