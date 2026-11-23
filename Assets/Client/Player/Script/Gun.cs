using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // 총을 발사하는 로직을 가지고 있어야 함
    // shootPosition을 찾아서
    // bullet을 shootPosition에 소환

    private ShootPosition _shootPosition = null;
    [SerializeField] private GameObject _bullet = null;
    private AudioSource _audioSource = null;
    private void Awake()
    {
        _shootPosition = GetComponentInChildren<ShootPosition>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        // bullet을 shootPosition에 소환하는 기능
        Instantiate(_bullet, _shootPosition.transform.position, _shootPosition.transform.rotation);
        _audioSource.Play();
    }
}
