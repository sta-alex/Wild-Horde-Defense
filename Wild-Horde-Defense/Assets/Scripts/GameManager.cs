using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text currencyText;
    private int currency;
    private bool towerInScope;
    // Start is called before the first frame update
    void Start()
    {
        towerInScope = false;
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
            RaycastHit hit;
            ShowTowerUpgradeAndSell(ray);

        }
    }

    private void ShowTowerUpgradeAndSell(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.parent != null && hit.transform.parent.parent != null &&
                hit.transform.parent.parent.CompareTag("Tower"))
            {
                GameObject clickedTower = hit.transform.parent.parent.gameObject;
                ShowTowerZone(clickedTower);
                Debug.Log("Spieler hat auf Turm geklickt: " + clickedTower.name);
            }
            else if (hit.transform.parent != null &&
                hit.transform.parent.CompareTag("Tower"))
            {
                GameObject clickedTower = hit.transform.parent.gameObject;
                ShowTowerZone(clickedTower);
                Debug.Log("Spieler hat auf Turm geklickt: " + clickedTower.name);
            }
        }
    }

    private void ShowTowerZone(GameObject clickedTower)
    {
        Transform scaledTransform = clickedTower.transform.Find("SCALED");
        if (scaledTransform != null)
        {
            GameObject zoneObject = scaledTransform.Find("Zone")?.gameObject;

            Behaviour halo = (Behaviour)zoneObject.GetComponent("Halo");
            enableOrDisableRange(halo);
        }
        else
        {
            scaledTransform = clickedTower.transform.Find("Zone");

            if (scaledTransform != null)
            {
                GameObject zoneObject = clickedTower.transform.Find("Zone")?.gameObject;
                if (zoneObject != null)
                {
                    Behaviour halo = (Behaviour)zoneObject.GetComponent("Halo");
                    enableOrDisableRange(halo);
                }
            }
        }
    }

    private void enableOrDisableRange(Behaviour halo)
    {
        if (!towerInScope)
        {
            towerInScope = true;
            halo.enabled = true;
        }
        else
        {
            towerInScope = false;
            halo.enabled = false;
        }
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

    private bool currencyAvailable(int oldCurrency)
    {
        if(this.currency < 0)
        {
            this.currency = oldCurrency;
            return false;
        }
        else
        {
            return true;
        }
    }

}
