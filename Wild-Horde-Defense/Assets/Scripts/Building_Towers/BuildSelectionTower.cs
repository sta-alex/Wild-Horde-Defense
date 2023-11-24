using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelectionTower : MonoBehaviour
{
    public Camera cam;
    public GameObject tower;
    private GameObject previewTower;

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
        Debug.Log("test");
    }

    void SetPreviewCannonScale()
    {
        if (previewTower != null)
        {
            previewTower.transform.localScale = new Vector3(10f, 10f, 10f);
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
                Vector3 snapPosition = new Vector3(towerPlacement.transform.position.x, towerPlacement.transform.position.y, towerPlacement.transform.position.z);

                GameObject newTower = Instantiate(tower, snapPosition, Quaternion.identity);
                newTower.transform.localScale = new Vector3(10f, 10f, 10f);
                inPreviewMode = false;
                Debug.Log("Placed " + tower.name + " on terrain at " + previewTower.transform.position);
            }
            else
            {
                // Das Objekt befindet sich nicht im rechteckigen Bereich
                Debug.Log("Das Objekt kann nicht platziert werden.");
            }

        }
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