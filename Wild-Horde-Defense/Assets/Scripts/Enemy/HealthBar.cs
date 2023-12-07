using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduceSpeed = 1000000f;
    private Camera maincamera;
    private float target = 1f;
    private float dmg = 0f;

    private Color fullHealthColor = new Color(85f / 255f, 255f / 255f, 85f / 255f);     // Light Green
    private Color midHealthColor = new Color(255f / 255f, 255f / 255f, 85f / 255f);     // Yellow
    private Color LowmidHealthColor = new Color(255f / 255f, 165f / 255f, 0f);          // Orange
    private Color lowHealthColor = new Color(255f / 255f, 85f / 255f, 85f / 255f);      // Light Red

    private Coroutine smoothHealthCoroutine;
    private Coroutine destroycoroutine;
    private float despawntimer = 5f;

    private GameObject foreGround;
    private GameObject backGround;


    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
        enableUI(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - maincamera.transform.position);
        
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        if (destroycoroutine != null)
            StopCoroutine(destroycoroutine);
        enableUI(true);
        target = currentHealth / maxHealth;
        if (target > 0f && target <= 1f)
        {

            if (smoothHealthCoroutine != null)
            {
                StopCoroutine(smoothHealthCoroutine);
            }

            smoothHealthCoroutine = StartCoroutine(SmoothlyUpdateHealthBar(target));

            UpdateHealthBarColor(target);
            dmg = target;
        }
        else if (target <= 0f)
        {
            healthbarSprite.fillAmount = 0f;
            UpdateHealthBarColor(target);
          
            
        }
    }

    private void UpdateHealthBarColor(float target)
    {
        if (target > 0.7f)
        {
            healthbarSprite.color = Color.Lerp(midHealthColor, fullHealthColor, (target - 0.5f) * 2f);
        }
        else if (target < 0.7f && target > 0.4f)
        {
            healthbarSprite.color = Color.Lerp(LowmidHealthColor, midHealthColor, target * 2f);
        }
        else
        {
            healthbarSprite.color = Color.Lerp(lowHealthColor, LowmidHealthColor, target * 2f);
        }
    }

    private IEnumerator SmoothlyUpdateHealthBar(float targetValue)
    {
        //StopCoroutine(destroycoroutine);
        float currentTime = 0f;
        float initialFillAmount = healthbarSprite.fillAmount;

        while (currentTime < 1f) 
        {
            currentTime += 0.01f; 
            healthbarSprite.fillAmount = Mathf.Lerp(initialFillAmount, targetValue, currentTime);
            if (healthbarSprite.fillAmount < 0f)
                enableUI(false);

            yield return null;
        }

        healthbarSprite.fillAmount = targetValue;
       destroycoroutine = StartCoroutine(Despawntimer());
    }

    private IEnumerator Despawntimer()
    {
        yield return new WaitForSeconds(despawntimer);
        enableUI(false);
    }
    private void OnDestroy()
    {
        healthbarSprite.fillAmount = 0f;
        
    }

    public void enableUI(bool OfforOn)
    {
            if (OfforOn)
            {
                gameObject.transform.Find("Foreground").gameObject.SetActive(true);
                gameObject.transform.Find("Background").gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.Find("Foreground").gameObject.SetActive(false);
                gameObject.transform.Find("Background").gameObject.SetActive(false);
            }       
    }

}
