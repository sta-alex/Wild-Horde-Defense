using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text currencyText;
    private int currency;
    private GameObject previousTower;
    private GameObject currentSelectedTower;
    // Start is called before the first frame update
    void Start()
    {
        currency = 220;
        currencyText.text = "$ " + currency;
        Debug.Log(currencyText);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            TowerUi(ray);
        }
    }

    private void TowerUi(Ray ray)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Tower");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            {
                if (hit.transform != null)
                {
                    GameObject clickedTower = hit.transform.gameObject;
                    currentSelectedTower = clickedTower;
                    Debug.Log("Spieler hat auf Turm geklickt: " + clickedTower.name);
                    if (previousTower == null)
                    {
                        HUD_Tower(clickedTower, true);
                        Debug.Log("HUD turned ON" + clickedTower.name);
                    }
                    else
                    {
                        if (previousTower.name.Equals(clickedTower.name) && ReferenceEquals(previousTower, clickedTower))
                        {
                            GameObject UpgradeAndSellObject = clickedTower.transform.Find("Tower_Ui")?.gameObject;
                            if (UpgradeAndSellObject.activeSelf)
                            {
                                HUD_Tower(clickedTower, false);
                                Debug.Log("HUD turned OFF" + clickedTower.name);
                            }
                            else
                            {
                                HUD_Tower(clickedTower, true);
                                Debug.Log("HUD turned ON" + clickedTower.name);
                            }
                        }
                        else
                        {
                            if (previousTower.activeSelf)
                            {
                                HUD_Tower(previousTower, false);
                            }
                            Debug.Log("HUD turned OFF" + previousTower.name);
                            HUD_Tower(clickedTower, true);
                            Debug.Log("HUD turned ON" + clickedTower.name);
                        }
                    }
                    previousTower = clickedTower;
                }
            }
        }
        else
        {
            int layerMask2 = 1 << LayerMask.NameToLayer("Placements");
            RaycastHit hit2;
            if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layerMask2))
            {

            }
            else
            {
                HUD_Tower(previousTower, false);
            }
        }
    }
    private void ShowTowerZone(GameObject clickedTower, bool hudTowerOn = false)
    {

        GameObject towerUI = clickedTower.transform.Find("Tower_Ui").gameObject;

        towerUI.SetActive(true);

        if (towerUI != null)
        {
            GameObject canvas = towerUI.transform.Find("Canvas").gameObject;

            if (canvas != null)
            {
                GameObject towerUI_Upgrade_Sell = canvas.transform.Find("Buttons").gameObject;
                towerUI_Upgrade_Sell.SetActive(true);

                GameObject rangeIndicator = canvas.transform.Find("RangeIndicator").gameObject;
                if (rangeIndicator != null)
                {
                    GameObject image = rangeIndicator.transform.Find("Image").gameObject;
                    enableOrDisableRange(image, hudTowerOn);
                }
            }
        }
    }

    private void ShowTowerUpgradeAndSell(GameObject clickedTower, bool hudTowerOn = false)
    {
        {
            GameObject UpgradeAndSellObject = clickedTower.transform.Find("Tower_Ui")?.gameObject;
            if (UpgradeAndSellObject != null)
            {
                if (hudTowerOn)
                {
                    UpgradeAndSellObject.SetActive(true);
                }
                else
                {
                    UpgradeAndSellObject.SetActive(false);
                }
            }
        }
    }

    private void enableOrDisableRange(GameObject image, bool hudTowerOn)
    {
        if (hudTowerOn)
        {
            image.SetActive(true);
        }
        else
        {
            image.SetActive(false);
        }
    }

    private void HUD_Tower(GameObject clickedTower, bool hudTowerOn = false)
    {
        ShowTowerZone(clickedTower, hudTowerOn);
        ShowTowerUpgradeAndSell(clickedTower, hudTowerOn);
    }


    public bool updateCurrency(int decreaseCurrency)
    {
        bool transaction = false;
        int oldCurrency = this.currency;
        this.currency = this.currency - decreaseCurrency;
        transaction = currencyAvailable(oldCurrency);
        if (transaction)
        {
            this.currencyText.text = "$ " + this.currency;
        }
        else
        {
            this.currencyText.text = "$ " + oldCurrency;
        }

        return transaction;
    }

    public void increaseCurrency(int increaseCurrency)
    {
        this.currency = this.currency + increaseCurrency;
        this.currencyText.text = "$ " + this.currency;
    }

    private bool currencyAvailable(int oldCurrency)
    {
        if (this.currency < 0)
        {
            this.currency = oldCurrency;
            return false;
        }
        else
        {
            return true;
        }
    }

    public GameObject getCurrentSelectedTower()
    {
        return this.currentSelectedTower;
    }

}
