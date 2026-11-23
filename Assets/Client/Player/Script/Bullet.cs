using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 소환됐을 때, 앞으로 날라가기만 하면 됨
    private Rigidbody _rb = null;
    [SerializeField] private GameObject _effectPrefab = null;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.AddForce(transform.forward * 20, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(20f);
            // 이펙트 플레이
            Vector3 detectedPosision = other.ClosestPoint(transform.position);
            Instantiate(_effectPrefab, detectedPosision, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
