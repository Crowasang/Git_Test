using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float healAmount = 30f;
    [SerializeField] private float rotateSpeed = 90f;

    private Action<Item> onItemUsed = null;

    public void Init(Action<Item> onItemUsed)
    {
        this.onItemUsed = onItemUsed;
    }

    private void Update()
    {
        // 눈에 잘 띄도록 제자리에서 회전
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        player.Heal(healAmount);
        onItemUsed?.Invoke(this);
        Destroy(gameObject);
    }
}
