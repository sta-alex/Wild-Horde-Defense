using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask placementLayerMask; // LayerMask für den Platzierungsbereich

    private GameObject selectedTowerPrefab; // Der ausgewählte Turm

    // Update is called once per frame
    void Update()
    {
        // Überprüfe, ob der Spieler eine Mausaktion durchführt
        if (Input.GetMouseButtonDown(0))
        {
            // Führe den Raycast durch
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
            {
                // Der Ray hat etwas getroffen
                // Überprüfe, ob es sich um den Platzierungsbereich handelt
                if (hit.collider.CompareTag("PlacementArea"))
                {
                    // Der Spieler kann hier einen Turm platzieren
                    // Zeige eine visuelle Rückmeldung oder platziere den Turm an dieser Position
                    PlaceTower(hit.point);
                }
            }
        }
    }

    void PlaceTower(Vector3 position)
    {
        // Überprüfe, ob ein Turm ausgewählt wurde
        if (selectedTowerPrefab != null)
        {
            // Platzieren Sie den Turm an der gewünschten Position
            Instantiate(selectedTowerPrefab, position, Quaternion.identity);
        }
    }

    // Methode zum Setzen des ausgewählten Turms
    public void SelectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
    }
}