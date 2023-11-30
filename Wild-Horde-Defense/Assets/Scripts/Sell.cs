using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour
{
    public GameManager gameManager;
    private GameObject currentTower;
    public BuildSelectionTower buildSelectionTower;
    private List<TowerPlacement> towerplacements;
    private Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementDictionaryNONSpecial;
    private Dictionary<TowerPlacement, GameObject> towersPlacedOnPlacementDictionary;

    public void SellTower()
    {
        loadCurrentSelectedTower();
        loadAndTurnOffTowerPlacementList();
        int value = loadTextValue();
        gameManager.increaseCurrency(value);
        GameObject.Destroy(currentTower);
    }

    private void loadCurrentSelectedTower()
    {
        this.currentTower = gameManager.getCurrentSelectedTower();
    }

    private int loadTextValue()
    {
        Transform canvasTransform = this.currentTower.transform.Find("Tower_Ui/Canvas");
        if (canvasTransform != null)
        {
            Transform buttonsTransform = canvasTransform.Find("Buttons");
            if (buttonsTransform != null)
            {
                Transform sellTransform = buttonsTransform.Find("Sell");
                if (sellTransform != null)
                {
                    Text sellText = sellTransform.GetComponentInChildren<Text>(true);
                    if (sellText != null)
                    {
                        string sellTextString = sellText.text;
                        Match match = Regex.Match(sellTextString, @"\d+");

                        if (match.Success)
                        {
                            string extractedNumber = match.Value;
                            if (int.TryParse(extractedNumber, out int result))
                            {
                                return result;
                            }
                        }
                    }
                }
            }
        }
        return 0;
    }

    private void loadAndTurnOffTowerPlacementList()
    {
        towersPlacedOnPlacementDictionaryNONSpecial = buildSelectionTower.getTowersPlacedOnPlacementDictionaryNONSpecial();
        towersPlacedOnPlacementDictionary = buildSelectionTower.getTowersPlacedOnPlacementDictionary();

        TurnOffPlacement();
    }

    private void TurnOffPlacement()
    {
        if (towersPlacedOnPlacementDictionaryNONSpecial != null)
        {
            if (towersPlacedOnPlacementDictionaryNONSpecial.ContainsValue(currentTower))
            {
                TowerPlacement foundKey = null;
                foreach (var kvp in towersPlacedOnPlacementDictionaryNONSpecial)
                {
                    if (kvp.Value == currentTower)
                    {
                        foundKey = kvp.Key;
                        break;
                    }
                }
                if (foundKey != null)
                {
                    towersPlacedOnPlacementDictionaryNONSpecial.Remove(foundKey);
                    foundKey.towerPlaced = false;
                }
            }
        }
        if (towersPlacedOnPlacementDictionary != null)
        {
            if (towersPlacedOnPlacementDictionary.ContainsValue(currentTower))
            {
                TowerPlacement foundKey = null;
                foreach (var kvp in towersPlacedOnPlacementDictionary)
                {
                    if (kvp.Value == currentTower)
                    {
                        foundKey = kvp.Key;
                        break;
                    }
                }
                if (foundKey != null)
                {
                    towersPlacedOnPlacementDictionary.Remove(foundKey);
                    foundKey.towerPlaced = false;
                }
            }
        }
    }
}