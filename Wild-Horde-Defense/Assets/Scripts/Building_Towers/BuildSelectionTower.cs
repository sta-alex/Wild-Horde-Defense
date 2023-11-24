using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelectionTower : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> availableTowers;
   
    public GameObject tower;

    private GameObject previewTower;

    private GameObject newTower;

    public List<TowerPlacement> towerPlacementList;

    public bool inPreviewMode = false;
    private bool oneTowerAtTime = false;

    void Update()
    {
        if (inPreviewMode)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mousePosition = hit.point;
                mousePosition.y = 14.99725f;

                if (previewTower != null && tower != null && oneTowerAtTime)
                {
                    oneTowerAtTime = false;
                    previewTower = Instantiate(tower, mousePosition, Quaternion.identity) as GameObject;
                    SetPreviewCannonScale();
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
                    if (!towerPlacement.towerPlaced)
                    {
                        Vector3 snapPosition = new Vector3(towerPlacement.transform.position.x, towerPlacement.transform.position.y, towerPlacement.transform.position.z);
                        newTower = Instantiate(tower, snapPosition, Quaternion.identity);
                        newTower.transform.localScale = new Vector3(14f, 14f, 14f);
                        GameObject.Destroy(previewTower);
                        inPreviewMode = false;
                        towerPlacement.towerPlaced = true;
                        Debug.Log("Placed " + tower.name + " on terrain at " + previewTower.transform.position);
                    }
                    else
                    {
                        Debug.Log("Tower allready placed!");
                    }
                }
                else
                {
                    Debug.Log("Das Objekt kann nicht platziert werden.");
                }
            }

        }
    }

    public void loadFireTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[3];
        this.previewTower = tower;
    }
    public void loadCannonTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[2];
        this.previewTower = tower;
    }
    public void loadPoisionTower()
    {
        GameObject.Destroy(previewTower);
        oneTowerAtTime = true;
        inPreviewMode = true;
        Debug.Log("in Preview");
        this.tower = availableTowers[1];
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


    /*
    float GetTerrainHeightAt(Vector3 position)
    {
        Terrain terrain = Terrain.activeTerrain;
        float terrainHeight = terrain.SampleHeight(position);

        return terrainHeight;
    }
    */
}