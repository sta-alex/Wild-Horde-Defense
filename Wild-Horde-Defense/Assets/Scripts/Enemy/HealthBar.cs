using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image healthbarSprite;
    [SerializeField] private Image healthbarSpritewhite;
    [SerializeField] private float reduceSpeed = 2f;
    private Camera maincamera;
    private float target = 1;
    private float dmg = 0;

    private Color fullHealthColor = new Color(85f / 255f, 255f / 255f, 85f / 255f);     // Light Green
    private Color midHealthColor = new Color(255f / 255f, 255f / 255f, 85f / 255f);     // Yellow
    private Color LowmidHealthColor = new Color(255f / 255f, 165f / 255f, 0f);          // Orange
    private Color lowHealthColor = new Color(255f / 255f, 85f / 255f, 85f / 255f);      // Light Red

    private Coroutine smoothHealthCoroutine;
    private float despawntimer = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color lerpedColor = Color.Lerp(lowHealthColor, fullHealthColor, target);
        healthbarSprite.color = lerpedColor;
        //transform.rotation = Quaternion.LookRotation(transform.position - maincamera.transform.position);
        //healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount,target,reduceSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {   
        target = currentHealth / maxHealth;
        if (target >= 0 && target <=1)
        {
            gameObject.SetActive(true);

            if (smoothHealthCoroutine != null)
            {
                StopCoroutine(smoothHealthCoroutine);
            }

            smoothHealthCoroutine = StartCoroutine(SmoothlyUpdateHealthBar(target));

            healthbarSpritewhite.fillAmount = Mathf.Lerp(dmg, target, reduceSpeed * Time.deltaTime);

            if (target > 0.7f)
            {
                healthbarSprite.color = Color.Lerp(midHealthColor, fullHealthColor, (target - 0.5f) * 2f);
            }
            else if (target < 0.7f && target > 0.4)
            {
                healthbarSprite.color = Color.Lerp(LowmidHealthColor, midHealthColor, target * 2f);
            }
            else
            {
                healthbarSprite.color = Color.Lerp(lowHealthColor, LowmidHealthColor, target * 2f);
            }
            dmg = target;
        }
    }

    private IEnumerator SmoothlyUpdateHealthBar(float targetValue)
    {
        float currentTime = 0f;
        float initialFillAmount = healthbarSprite.fillAmount;

        while (currentTime < reduceSpeed)
        {
            currentTime += Time.deltaTime;
            healthbarSprite.fillAmount = Mathf.Lerp(initialFillAmount, targetValue, currentTime / reduceSpeed);

            yield return null;
        }

        healthbarSprite.fillAmount = targetValue;
        healthbarSpritewhite.fillAmount = 0f;
        StartCoroutine(Despawntimer());


    }

    private IEnumerator Despawntimer()
    {
        yield return new WaitForSeconds(despawntimer);
        gameObject.SetActive(false);
    }


}
