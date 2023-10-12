using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dvalin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject attack1area;
    [SerializeField] private GameObject attack2area;
    [SerializeField] private GameObject attack3area;

    [SerializeField] private Health shieldSource;
    [SerializeField] private Health myHealth;

    [HideInInspector]
    public float shieldMaxHealth;

    public event Action OnDie;

    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        shieldMaxHealth = shieldSource.GetMaxHealth();
    }

    private void Start()
    {
        shieldSource.OnHit += ApplyShieldDamage;
        shieldSource.OnDie += PassedOut;
        myHealth.OnHit += ApplyDragonDamage;
        myHealth.OnDie += Die;
    }

    private void PassedOut()
    {
        if (isDead) return;
        int hash = Animator.StringToHash("IsPassedOut");
        animator.SetBool(hash, true);
        hash = Animator.StringToHash("KnockDown");
        animator.SetTrigger(hash);
        StartCoroutine(PassedOutTimeCount());
    }

    public void StartAnimation(int num)
    {
        int hash = Animator.StringToHash($"Attack{num}");
        animator.SetTrigger(hash);
    }

    private void Die()
    {
        isDead = true;
        OnDie?.Invoke();
        int hash = Animator.StringToHash("IsDead");
        animator.SetBool(hash, true);
        hash = Animator.StringToHash("KnockDown");
        animator.SetTrigger(hash);
    }

    private void ApplyShieldDamage()
    {
        float shieldHealth = shieldSource.GetHealth();
        StageManager.instance.ApplyShieldDamage(shieldHealth);
    }

    private void ApplyDragonDamage()
    {
        float health = myHealth.GetHealth();
        StageManager.instance.ApplyDragonDamage(health);
    }

    public void FirstAttackArea()
    {
        attack1area.SetActive(true);
    }

    public void SecondAttackArea()
    {
        attack2area.SetActive(true);
    }

    public void ThirdAttackArea()
    {
        attack3area.SetActive(true);
    }


    public void TurnOffAttackArea()
    {
        attack1area.SetActive(false);
        attack2area.SetActive(false);
        attack3area.SetActive(false);
    }

    IEnumerator PassedOutTimeCount()
    {
        yield return new WaitForSeconds(20f);
        int hash = Animator.StringToHash("IsPassedOut");
        animator.SetBool(hash, false);
        shieldSource.Heal((int)shieldMaxHealth);
        float shieldHealth = shieldSource.GetHealth();
        StageManager.instance.ApplyShieldDamage(shieldHealth);
    }
}
