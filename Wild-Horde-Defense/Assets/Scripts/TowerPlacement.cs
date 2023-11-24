using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask placementLayerMask; // LayerMask f�r den Platzierungsbereich

    private GameObject selectedTowerPrefab; // Der ausgew�hlte Turm

    // Update is called once per frame
    void Update()
    {
        // �berpr�fe, ob der Spieler eine Mausaktion durchf�hrt
        if (Input.GetMouseButtonDown(0))
        {
            // F�hre den Raycast durch
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
            {
                // Der Ray hat etwas getroffen
                // �berpr�fe, ob es sich um den Platzierungsbereich handelt
                if (hit.collider.CompareTag("PlacementArea"))
                {
                    // Der Spieler kann hier einen Turm platzieren
                    // Zeige eine visuelle R�ckmeldung oder platziere den Turm an dieser Position
                    PlaceTower(hit.point);
                }
            }
        }
    }

    void PlaceTower(Vector3 position)
    {
        // �berpr�fe, ob ein Turm ausgew�hlt wurde
        if (selectedTowerPrefab != null)
        {
            // Platzieren Sie den Turm an der gew�nschten Position
            Instantiate(selectedTowerPrefab, position, Quaternion.identity);
        }
    }

    // Methode zum Setzen des ausgew�hlten Turms
    public void SelectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
    }
}