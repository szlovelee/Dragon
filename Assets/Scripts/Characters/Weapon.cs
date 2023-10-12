using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private ParticleSystem bubble;

    private int damage;
    private bool isActive;
    private Coroutine currentCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }

    public void Appear()
    {
        if (isActive) return;
        isActive = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;

        if (currentCoroutine == null) return;
        StopCoroutine(currentCoroutine);
        bubble.Stop();
    }

    public void DisappearOnIdle()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(DisappearCoroutine(1f));
    }

    public void DisappearOnMove()
    {
        if (!isActive) return;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(DisappearCoroutine(0f));
    }

    IEnumerator DisappearCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isActive) yield break;

        isActive = false;
        bubble.Play();
        StartCoroutine(BubbleOff());

        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }

    IEnumerator BubbleOff()
    {
        yield return new WaitForSeconds(1f);
        bubble.Stop();
    }
    
}
