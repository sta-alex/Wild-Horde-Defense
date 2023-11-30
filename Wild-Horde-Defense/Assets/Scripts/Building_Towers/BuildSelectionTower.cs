using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSelectionTower : MonoBehaviour
{
    public GameManager gameManager;
    public Camera cam;
    public List<GameObject> availableTowers;

    public GameObject tower;

    private GameObject previewTower;
    private GameObject placedTower;

    public List<TowerPlacement> towerPlacementList;

    private Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementDictionary = new Dictionary<TowerPlacement, GameObject>();
    private Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementDictionaryNONSpecial = new Dictionary<TowerPlacement, GameObject>();

    public bool inPreviewMode = false;
    private bool oneTowerAtTime = false;
    private bool transaction;


    private void Start()
    {
        gameManager.enabled = false;
    }
    void Update()
    {
        if (inPreviewMode)
        {
            gameManager.enabled = false;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mousePosition = hit.point;
                mousePosition.y = 18.19725f;

                if (previewTower != null && tower != null && oneTowerAtTime)
                {
                    oneTowerAtTime = false;
                    previewTower = Instantiate(tower, mousePosition, Quaternion.identity) as GameObject;
                    SetPreviewCannonScale();
                    ShowTowerZone(previewTower);
                    DisableCollider(previewTower);
                }

                SetPreviewCannonPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && previewTower != null)
                {
                    PlaceTower();
                }
            }
        }
    }

    void SetPreviewCannonScale()
    {
        if (previewTower != null)
        {
            previewTower.transform.localScale = new Vector3(14f, 14f, 14f);
        }
    }

    void SetPreviewCannonPosition(Vector3 position)
    {
        if (previewTower != null)
        {
            previewTower.transform.localPosition = position;
        }

    }

    void DisableCollider(GameObject obj)
    {
        Collider collider = obj.GetComponent<Collider>();
        if (collider)
        {
            collider.enabled = false;
        }
    }

    void PlaceTower()
    {
        if (previewTower != null)
        {

            foreach (TowerPlacement towerPlacement in towerPlacementList)
            {
                if (previewTower.transform.position.x > towerPlacement.transform.position.x - 15f && previewTower.transform.position.x < towerPlacement.transform.position.x + 15f &&
                previewTower.transform.position.y > towerPlacement.transform.position.y - 15f && previewTower.transform.position.y < towerPlacement.transform.position.y + 15f &&
                previewTower.transform.position.z > towerPlacement.transform.position.z - 15f && previewTower.transform.position.z < towerPlacement.transform.position.z + 15f)
                {
                    GameObject selectedTower = gameManager.getCurrentSelectedTower();


                        if (!towerPlacement.towerPlaced)
                        {
                            DecreaseMoneyFromPlayer();
                            if (transaction)
                            {
                                Vector3 snapPosition = new Vector3(towerPlacement.transform.position.x, towerPlacement.transform.position.y + 3.5f, towerPlacement.transform.position.z);
                                GameObject placedTower = Instantiate(tower, snapPosition, Quaternion.identity);
                                setTowerToDictionary(placedTower, towerPlacement);
                                placedTower.transform.localScale = new Vector3(14f, 14f, 14f);
                                GameObject.Destroy(previewTower);
                                inPreviewMode = false;
                                towerPlacement.towerPlaced = true;
                                gameManager.enabled = true;
                                Debug.Log("Placed " + tower.name + " on terrain at " + previewTower.transform.position);
                            }
                            else
                            {
                                Debug.Log("Kein Geld verfügbar!");
                            }
                        }
                    else
                    {
                        Debug.Log("Tower allready placed!");
                    }
                }
                else
                {
                    Debug.Log("Das Objekt kann nicht platziert werden!");
                }
            }

        }
    }

    private void setTowerToDictionary(GameObject placedTower, TowerPlacement towerplacement)
    {
        GameObject specialPlacement = towerplacement.transform.Find("FirePower")?.gameObject;

        if (specialPlacement == null)
        {
            specialPlacement = towerplacement.transform.Find("RangePower")?.gameObject;
        }

        if (specialPlacement == null)
        {
            specialPlacement = towerplacement.transform.Find("SpeedPower")?.gameObject;
        }

        Dictionary<TowerPlacement, GameObject> targetDictionary = (specialPlacement == null) ? towersPlacedOnPlacementDictionaryNONSpecial : towersPlacedOnPlacementDictionary;

        if (!targetDictionary.ContainsKey(towerplacement))
        {

            targetDictionary.Add(towerplacement, placedTower);

            Debug.Log((specialPlacement == null) ? "Turm auf normales Placement platziert" : "Turm auf SpecialPlacement platziert");
        }
        else
        {
            Debug.LogWarning("Platz ist bereits im Dictionary vorhanden. Der Turm wurde nicht erneut hinzugefügt.");
        }
    }

    public void loadFireTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[9];
        this.previewTower = tower;
    }
    public void loadCannonTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[6];
        this.previewTower = tower;
    }
    public void loadPoisionTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[3];
        this.previewTower = tower;
    }
    public void loadCrossbowTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[0];
        this.previewTower = tower;
    }

    public void ShowTowerZone(GameObject towerObject)
    {
        GameObject towerUI = towerObject.transform.Find("Tower_Ui").gameObject;

        towerUI.SetActive(true);

        if (towerUI != null)
        {
            GameObject canvas = towerUI.transform.Find("Canvas").gameObject;

            if(canvas != null)
            {
                GameObject towerUI_Upgrade_Sell = canvas.transform.Find("Buttons").gameObject;
                towerUI_Upgrade_Sell.SetActive(false);

                GameObject imageArrow = canvas.transform.Find("Image").gameObject;
                imageArrow.SetActive(false);

                GameObject rangeIndicator = canvas.transform.Find("RangeIndicator").gameObject;
                if(rangeIndicator != null)
                {
                    GameObject imageRange = rangeIndicator.transform.Find("Image").gameObject;
                    imageRange.SetActive(true);
                }
            }
        }
    }

    private void DecreaseMoneyFromPlayer()
    {
        if (tower.name.Equals("HM_crossbow_1"))
        {
            transaction = gameManager.updateCurrency(100);
        }
        if (tower.name.Equals("HM_cannon_1"))
        {
            transaction = gameManager.updateCurrency(120);
        }
        if (tower.name.Equals("HM_poison_1"))
        {
            transaction = gameManager.updateCurrency(160);
        }
        if (tower.name.Equals("HM_fire_1"))
        {
            transaction = gameManager.updateCurrency(200);
        }
    }

    public bool getIsinPreview()
    {
        return this.inPreviewMode;
    }
    public void setIsinPreview(bool inPreviewMode)
    {
        this.inPreviewMode = inPreviewMode;
    }

    public void destroyPreview()
    {
        GameObject.Destroy(this.previewTower);
    }

    public GameObject getPreviewTower()
    {
        return this.previewTower;
    }

    public GameObject getCurrentPlacedTower()
    {
        return this.placedTower;
    }
    public Dictionary<TowerPlacement, GameObject> getTowersPlacedOnPlacementDictionaryNONSpecial()
    {
        return this.towersPlacedOnPlacementDictionaryNONSpecial;
    }

    public Dictionary<TowerPlacement, GameObject> getTowersPlacedOnPlacementDictionary()
    {
        return this.towersPlacedOnPlacementDictionary;
    }

    public void updateDictionary(Dictionary<TowerPlacement, GameObject> dictionary)
    {
        this.towersPlacedOnPlacementDictionary = dictionary;
    }
    public void updateDictionaryNONSpecialDictionary(Dictionary<TowerPlacement, GameObject> nonSpecialDictionary)
    {
        this.towersPlacedOnPlacementDictionaryNONSpecial = nonSpecialDictionary;
    }

    /*
    float GetTerrainHeightAt(Vector3 position)
    {
        Terrain terrain = Terrain.activeTerrain;
        float terrainHeight = terrain.SampleHeight(position);

        return terrainHeight;
    }
    */
}