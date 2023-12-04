using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePowerPlacement : MonoBehaviour
{

    private string hexCode = "9F1008";
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

                            GameObject towerUi = FindTowerUi(towerToBoost);

                            if (towerUi != null)
                            {

                                Color newColor;
                                if (ColorUtility.TryParseHtmlString("#" + hexCode, out newColor))
                                {
                                    towerUi.gameObject.GetComponent<Image>().color = newColor;
                                }

                                Tower tower = towerToBoost.GetComponent<Tower>();
                                if (tower != null)
                                {
                                    tower.dmg += 30;
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

    }


