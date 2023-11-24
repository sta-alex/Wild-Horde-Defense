using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelectionTower : MonoBehaviour
{
    public Camera cam;
    public GameObject tower;
    private GameObject previewTower;

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
                mousePosition.y = 0f;

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
        Debug.Log("test");
    }

    void SetPreviewCannonScale()
    {
        if (previewTower != null)
        {
            previewTower.transform.localScale = new Vector3(20f, 20f, 20f);
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
            GameObject newCannon = Instantiate(tower, previewTower.transform.position, Quaternion.identity);
            newCannon.transform.localScale = new Vector3(20f, 20f, 20f);
            inPreviewMode = false;
            Debug.Log("Placed " + tower.name + " on terrain at " + previewTower.transform.position);
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