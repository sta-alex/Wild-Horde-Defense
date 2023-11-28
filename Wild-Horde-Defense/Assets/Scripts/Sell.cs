using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour
{
    public GameManager gameManager;
    private GameObject currentTower;

    public void SellTower()
    {
        loadCurrentSelectedTower();
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
}