using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public GameManager gameManager;

    private GameObject towerLvl1;
    private GameObject towerLvl2;
    private GameObject towerLvl3;

    private List<GameObject> towersAlreadyUpgraded = new List<GameObject>();
    private bool transaction;

    public void UpgradeCrossbowLvl1()
    {
        loadCrossbowTowers();
        transaction = gameManager.updateCurrency(300);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(true);
            towerLvl3.SetActive(false);
        }
    }
    public void UpgradeCrossbowLvl2()
    {
        loadCrossbowTowers();
        transaction = gameManager.updateCurrency(900);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(false);
            towerLvl3.SetActive(true);
        }
    }
    public void UpgradeCannonLvl1()
    {
        loadCannonTowers();
        transaction = gameManager.updateCurrency(360);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(true);
            towerLvl3.SetActive(false);
        }
    }
    public void UpgradeCannonLvl2()
    {
        loadCannonTowers();
        transaction = gameManager.updateCurrency(1080);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(false);
            towerLvl3.SetActive(true);
        }
    }
    public void UpgradePoisonLvl1()
    {
        loadPoisonTowers();
        transaction = gameManager.updateCurrency(480);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(true);
            towerLvl3.SetActive(false);
        }
    }
    public void UpgradePoisonLvl2()
    {
        loadPoisonTowers();
        transaction = gameManager.updateCurrency(1440);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(false);
            towerLvl3.SetActive(true);
        }
    }
    public void UpgradeFireLvl1()
    {
        loadFireTowers();
        transaction = gameManager.updateCurrency(600);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(true);
            towerLvl3.SetActive(false);
        }
    }
    public void UpgradeFireLvl2()
    {
        loadFireTowers();
        transaction = gameManager.updateCurrency(1200);
        if (transaction)
        {
            towerLvl1.SetActive(false);
            towerLvl2.SetActive(false);
            towerLvl3.SetActive(true);
        }
    }

    private void loadCrossbowTowers()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_crossbow_1(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl1 = obj;
                towersAlreadyUpgraded.Add(towerLvl1);
            }
            if(obj.tag.Equals("Tower") && obj.name.Contains("HM_crossbow_2(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl2 = obj;
                towersAlreadyUpgraded.Add(towerLvl2);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_crossbow_3(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl3 = obj;
                towersAlreadyUpgraded.Add(towerLvl3);
            }
        }
    }

    private void loadCannonTowers()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_cannon_1(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl1 = obj;
                towersAlreadyUpgraded.Add(towerLvl1);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_cannon_2(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl2 = obj;
                towersAlreadyUpgraded.Add(towerLvl2);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_cannon_3(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl3 = obj;
                towersAlreadyUpgraded.Add(towerLvl3);
            }
        }
    }

    private void loadPoisonTowers()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_poison_1(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl1 = obj;
                towersAlreadyUpgraded.Add(towerLvl1);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_poison_2(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl2 = obj;
                towersAlreadyUpgraded.Add(towerLvl2);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_poison_3(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl3 = obj;
                towersAlreadyUpgraded.Add(towerLvl3);
            }
        }
    }

    private void loadFireTowers()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_fire_1(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl1 = obj;
                towersAlreadyUpgraded.Add(towerLvl1);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_fire_2(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl2 = obj;
                towersAlreadyUpgraded.Add(towerLvl2);
            }
            if (obj.tag.Equals("Tower") && obj.name.Contains("HM_fire_3(Clone)") && !towersAlreadyUpgraded.Contains(obj))
            {
                towerLvl3 = obj;
                towersAlreadyUpgraded.Add(towerLvl3);
            }
        }
    }

}
