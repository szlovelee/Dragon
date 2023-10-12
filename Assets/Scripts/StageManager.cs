using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    Player player;
    Dvalin dvalin;

    int phase;
    float playerMaxHealth;
    float playerHealth;
    float dragonMaxHealth;
    float dragonHealth;
    float dragonMaxShield;
    float dragonShield;

    WaitForSeconds waitForSeconds = new WaitForSeconds(6f);

    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    public event Action<float> OnPlayerHealthChange;
    public event Action<float> OnShieldHealthChange;
    public event Action<float> OnDragonHealthChange;
    public event Action OnWin;
    public event Action OnGameOver;

    public bool IsGameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        IsGameOver = false;
        phase = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dvalin = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Dvalin>();

        player.Health.OnDie += GameOver;
        dvalin.OnDie += Win;

        playerMaxHealth = player.Health.GetMaxHealth();
        playerHealth = player.Health.GetHealth();

        dragonMaxHealth = 100;
        dragonHealth = dragonMaxHealth;

        dragonMaxShield = dvalin.shieldMaxHealth;
        dragonShield = dragonMaxShield;

        StartCoroutine(Phase1());
    }

    private void Update()
    {
        
    }

    public void ApplyPlayerDamage(float damage)
    {
        player.Health.TakeDamage((int)damage);
        playerHealth = player.Health.GetHealth();
        Flash();
        OnPlayerHealthChange?.Invoke(playerHealth / playerMaxHealth);
    }

    public void ApplyShieldDamage(float shieldHealth)
    {
        dragonShield = shieldHealth;
        OnShieldHealthChange?.Invoke(dragonShield / dragonMaxShield);
    }

    public void ApplyDragonDamage(float dragonHealth)
    {
        this.dragonHealth = dragonHealth;
        OnDragonHealthChange?.Invoke(dragonHealth / dragonMaxHealth);
    }

    void Win()
    {
        OnWin?.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; player.Input.InputActions.Disable();
        IsGameOver = true;
        phase = 0;
    }

    void GameOver()
    {
        OnGameOver?.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsGameOver = true;
        phase = 0;
    }


    private void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color32(255, 170, 170, 255);
        coroutine = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float startAlpha = 1f;
        float a = startAlpha;

        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            byte alphaByte = (byte)(a * 255);
            image.color =  new Color32(255, 170, 170, alphaByte);;
            yield return null;
        }

        image.enabled = false;
    }

    IEnumerator Phase1()
    {
        while (phase == 1)
        {
            dvalin.StartAnimation(1);
            yield return waitForSeconds;
            dvalin.StartAnimation(2);
            yield return waitForSeconds;
            yield return waitForSeconds;
            dvalin.StartAnimation(3);
            yield return waitForSeconds;
            yield return waitForSeconds;

        }
        yield return null;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
