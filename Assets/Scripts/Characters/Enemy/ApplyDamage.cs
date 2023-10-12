using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float damageRate;

    private float _lastDamagedTime;

    private void OnEnable()
    {
        _lastDamagedTime = damageRate;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _lastDamagedTime += Time.deltaTime;
            if (_lastDamagedTime >= damageRate)
            {
                _lastDamagedTime = 0f;

                StageManager.instance.ApplyPlayerDamage(damage);
            }
        }
    }
}
