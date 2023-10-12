using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Image playerHealth;
    [SerializeField] private Image playerHealthDecrease;
    [SerializeField] private Image dragonHealth;
    [SerializeField] private Image dragonHealthDecrease;
    [SerializeField] private Image dragonShield;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        StageManager.instance.OnPlayerHealthChange += playerHealthChange;
        StageManager.instance.OnShieldHealthChange += dragonShieldChange;
        StageManager.instance.OnDragonHealthChange += dragonHealthChange;
        StageManager.instance.OnWin += OpenWinPanel;
        StageManager.instance.OnGameOver += OpenGameOverPanel;
    }

    private void playerHealthChange(float rate)
    {
        playerHealth.fillAmount = rate;
        StartCoroutine(HealthDecrease(playerHealthDecrease, rate));
    }

    private void dragonHealthChange(float rate)
    {
        dragonHealth.fillAmount = rate;
        StartCoroutine(HealthDecrease(dragonHealthDecrease, rate));
    }

    private void dragonShieldChange(float rate)
    {
        dragonShield.fillAmount = rate;
    }

    private void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    IEnumerator HealthDecrease(Image img, float rate)
    {
        yield return new WaitForSeconds(0.5f);
        float curAmount = img.fillAmount;
        float decreaseSpeed = 0.05f;

        while (curAmount > rate)
        {
            curAmount -= decreaseSpeed * Time.deltaTime;
            curAmount = Mathf.Max(curAmount, rate);
            img.fillAmount = curAmount;
            yield return null;
        }
    }

}
