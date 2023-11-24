using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelectionTower : MonoBehaviour
{
    public Camera cam;
    public GameObject tower;
    private GameObject previewTower;

    private GameObject newTower;

    public TowerPlacement towerPlacement;

    public bool inPreviewMode = false;

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

                if (previewTower == null)
                {
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

    public void OnClick()
    {
        inPreviewMode = true;
        Debug.Log("in Preview");
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

            if (previewTower.transform.position.x > towerPlacement.transform.position.x - 15f && previewTower.transform.position.x < towerPlacement.transform.position.x + 15f &&
                previewTower.transform.position.y > towerPlacement.transform.position.y - 15f && previewTower.transform.position.y < towerPlacement.transform.position.y + 15f &&
                previewTower.transform.position.z > towerPlacement.transform.position.z - 15f && previewTower.transform.position.z < towerPlacement.transform.position.z + 15f)
            {

                if(newTower == null)
                {
                    Vector3 snapPosition = new Vector3(towerPlacement.transform.position.x, towerPlacement.transform.position.y, towerPlacement.transform.position.z);
                    newTower = Instantiate(tower, snapPosition, Quaternion.identity);
                    newTower.transform.localScale = new Vector3(14f, 14f, 14f);
                    GameObject.Destroy(previewTower);
                    inPreviewMode = false;
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


    /*
    float GetTerrainHeightAt(Vector3 position)
    {
        Terrain terrain = Terrain.activeTerrain;
        float terrainHeight = terrain.SampleHeight(position);

        return terrainHeight;
    }
    */
}