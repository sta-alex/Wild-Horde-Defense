using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangePowerPlacement : MonoBehaviour
{
    private string hexCode = "17197D";
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

    public void BoostZone(GameObject towerToBoost, TowerPlacement towerPlacement)
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
                        if (towerFromDictionary.name.Equals(towerToBoost.name) && towerPlacement.name.Equals("TowerPlacement01 (2)"))
                        {
                            GameObject zone = findZone(towerToBoost);
                            GameObject towerUi = FindTowerUi(towerToBoost);

                            if (zone != null && towerUi != null)
                            {
                                SphereCollider sphereCollider = zone.GetComponent<SphereCollider>();
                                if (sphereCollider != null)
                                {
                                    Color newColor;
                                    if (ColorUtility.TryParseHtmlString("#" + hexCode, out newColor))
                                    {
                                        towerUi.gameObject.GetComponent<Image>().color = newColor;
                                    }
                                    sphereCollider.radius += 0.2f;
                                    towerUi.transform.localScale = new Vector3(towerUi.transform.localScale.x + 1f, towerUi.transform.localScale.y + 1f, towerUi.transform.localScale.z);
                                    Debug.Log("Radius boostes from Tower: " + towerToBoost.name);
                                }
                            }
                        }
                    }
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
