using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // 대미지를 받을 수 있는 녀석
    // 체력, 최대 체력
    // 죽으면 얼마나 점수를 부여할지

    [SerializeField] private float _hp = 0f;
    [SerializeField] private float moveSpeed = 3f;
    private float _maxHp = 100f;
    private int _score = 10;
    private Player _player = null;
    private AudioSource _audioSource = null;

    private Action<Enemy, int> onEnemyDie = null;
    
    private void Awake()
    {
        _hp = _maxHp;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(Player player, Action<Enemy, int> onEnemyDie)
    {
        _player = player;
        this.onEnemyDie = onEnemyDie;
        StartCoroutine(CoChasePlayer());
    }

    private IEnumerator CoChasePlayer()
    {
        while (true)
        {
            // 플레이어의 위치 쫓아가는 로직
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            float distance = (_player.transform.position - transform.position).magnitude;

            if (distance >= 3f)
            {
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                _player.TakeDamage(10f);
                yield return new WaitForSeconds(2f);
            }
            yield return null;
        }
    }
    public void TakeDamage(float damage)
    {
        _hp -= damage;
        _audioSource.Play();
        if (_hp <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        onEnemyDie?.Invoke(this, _score);
        Destroy(gameObject);
    }
    
}
