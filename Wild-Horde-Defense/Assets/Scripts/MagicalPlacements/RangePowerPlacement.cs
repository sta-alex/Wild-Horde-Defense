using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePowerPlacement : MonoBehaviour
{

    private Dictionary<TowerPlacement, GameObject> towersInDictionary;
    private GameObject towerToBoost;
    public BuildSelectionTower buildSelectionTower;

    private string previousTowerName;
    private bool boostOnce;
    // Start is called before the first frame update
    void Start()
    {
        previousTowerName = "nothing";
        boostOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        loadDictionary();
    }

    private void loadDictionary()
    {
        towersInDictionary = buildSelectionTower.getTowersPlacedOnPlacementDictionary();

        if(towersInDictionary != null)
        {
            foreach (TowerPlacement towerPlacement in towersInDictionary.Keys)
            {
                if(towerPlacement != null)
                {
                    if (towerPlacement.name.Equals("TowerPlacement01 (2)"))
                    {
                        GameObject tower = towersInDictionary[towerPlacement];
                        towerToBoost = tower;

                        if (!towerToBoost.name.Equals(previousTowerName))
                        {
                            boostOnce = false;
                        }
                        GameObject zone = findZone(towerToBoost);
                        GameObject towerUi = FindTowerUi(towerToBoost);

                        if (zone != null && towerUi != null)
                        {
                            SphereCollider sphereCollider = zone.GetComponent<SphereCollider>();
                            if (sphereCollider != null && !boostOnce)
                            {
                                boostOnce = true;
                                sphereCollider.radius += 0.2f;
                                towerUi.transform.localScale = new Vector3(towerUi.transform.localScale.x + 1f, towerUi.transform.localScale.y +1f, towerUi.transform.localScale.z);
                                Debug.LogError("Radius boostes from Tower: " + tower.name);
                            }
                        }
                    }
                    previousTowerName = towersInDictionary[towerPlacement].name;
                }
            }
        }
    }

    private GameObject FindTowerUi(GameObject tower)
    {
        GameObject towerUI = tower.transform.Find("Tower_Ui").gameObject;

        if (towerUI != null)
        {
            GameObject canvas = towerUI.transform.Find("Canvas").gameObject;

            if (canvas != null)
            {
                GameObject rangeIndicator = canvas.transform.Find("RangeIndicator").gameObject;
                if (rangeIndicator != null)
                {
                    GameObject imageRange = rangeIndicator.transform.Find("Image").gameObject;
                    return imageRange;
                }
            }
        }
        return null;
    }

    private GameObject findZone(GameObject tower)
    {
        Transform scaledTransform = tower.transform.Find("SCALED");

        if (scaledTransform != null)
        {
            GameObject zoneObject = scaledTransform.transform.Find("Zone")?.gameObject;

            if (zoneObject == null)
            {
                zoneObject = scaledTransform.transform.Find("Zone2")?.gameObject;
            }
            if (zoneObject == null)
            {
                zoneObject = scaledTransform.transform.Find("Zone3")?.gameObject;
            }

            if (zoneObject != null)
            {
                //toDO Range Indicator
                return zoneObject;
            }
        }
        else
        {

            if (scaledTransform == null)
            {
                GameObject zoneObject = tower.transform.Find("Zone")?.gameObject;

                if (zoneObject == null)
                {
                    zoneObject = tower.transform.Find("Zone2")?.gameObject;
                }
                if (zoneObject == null)
                {
                    zoneObject = tower.transform.Find("Zone3")?.gameObject;
                }

                if (zoneObject != null)
                {
                    //toDO Range Indicator
                    return zoneObject;
                }
            }
        }
            return null;
    }
}
