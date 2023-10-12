using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle_1;
    [SerializeField] private ParticleSystem particle_2;
    [SerializeField] private ParticleSystem particle_3;
    [SerializeField] private ParticleSystem particle_4;
    [SerializeField] private ParticleSystem particle_5;


    public void AppearWeapon()
    {
        gameObject.GetComponent<Player>().Weapon.gameObject.SetActive(true);
        gameObject.GetComponent<Player>().Weapon.Appear();
    }

    public void DisappearOnMove()
    {
        gameObject.GetComponent<Player>().Weapon.DisappearOnMove();
    }

    public void ThirdComboEffect()
    {
        Debug.Log("Effect");
        StopCoroutine(StopEffect());
        particle_1.Play();
        particle_2.Play();
        particle_3.Play();
        particle_4.Play();
        particle_5.Play();
        StartCoroutine(StopEffect());
    }

    IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(2.5f);
        particle_1.Stop();
        particle_2.Stop();
        particle_3.Stop();
        particle_4.Stop();
        particle_5.Stop();
    }
}

