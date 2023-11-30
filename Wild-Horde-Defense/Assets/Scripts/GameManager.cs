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
        currency = 2000;
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
    }
    private void ShowTowerZone(GameObject clickedTower, bool hudTowerOn = false)
    {
        Transform scaledTransform = clickedTower.transform.Find("SCALED");
        if (scaledTransform != null)
        {
            GameObject zoneObject = scaledTransform.Find("Zone")?.gameObject;
            if (zoneObject == null)
            {
                zoneObject = scaledTransform.Find("Zone2")?.gameObject;
            }
            if (zoneObject == null)
            {
                zoneObject = scaledTransform.Find("Zone3")?.gameObject;
            }
            if (zoneObject != null)
            {
                Behaviour halo = (Behaviour)zoneObject.GetComponent("Halo");
                enableOrDisableRange(halo, hudTowerOn);
            }
        }
        else
        {
            scaledTransform = clickedTower.transform.Find("Zone");

            GameObject zoneObject = clickedTower.transform.Find("Zone")?.gameObject;

            if (zoneObject == null)
            {
                zoneObject = clickedTower.transform.Find("Zone2")?.gameObject;
            }
            if (zoneObject == null)
            {
                zoneObject = clickedTower.transform.Find("Zone3")?.gameObject;
            }

            if (zoneObject != null)
            {
                Behaviour halo = (Behaviour)zoneObject.GetComponent("Halo");
                enableOrDisableRange(halo, hudTowerOn);
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

    private void enableOrDisableRange(Behaviour halo, bool hudTowerOn)
    {
        if (hudTowerOn)
        {
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
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
