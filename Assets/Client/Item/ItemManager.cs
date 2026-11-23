using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab = null;
    // 아이템이 생성될 발판들 (비워두면 Environments의 Floor를 자동으로 찾음)
    [SerializeField] private Collider[] _platforms = null;
    [SerializeField] private int _maxItemCount = 3;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _edgeMargin = 1f;       // 발판 가장자리에서 띄울 거리
    [SerializeField] private float _heightOffset = 0.5f;   // 발판 표면 위로 띄울 높이

    private List<Item> itemList = null;

    private void Awake()
    {
        itemList = new List<Item>();

        if (_platforms == null || _platforms.Length == 0)
        {
            GameObject floor = GameObject.Find("Floor");
            if (floor != null) _platforms = new[] { floor.GetComponent<Collider>() };
        }
    }

    public void Init()
    {
        StartCoroutine(CoSpawnItems());
    }

    private IEnumerator CoSpawnItems()
    {
        while (true)
        {
            if (itemList.Count < _maxItemCount)
            {
                SpawnItem();
            }
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnItem()
    {
        if (_itemPrefab == null || _platforms == null || _platforms.Length == 0) return;

        Collider platform = _platforms[Random.Range(0, _platforms.Length)];
        if (!TryGetRandomPointOnPlatform(platform, out Vector3 spawnPosition)) return;

        GameObject go = Instantiate(_itemPrefab, spawnPosition, Quaternion.identity);
        Item item = go.GetComponent<Item>();
        if (item != null)
        {
            item.Init(OnItemUsed);
            itemList.Add(item);
        }
    }

    // 발판 범위 안의 무작위 XZ를 고르고, 위에서 아래로 레이를 쏴서 발판 표면 위치를 구함
    private bool TryGetRandomPointOnPlatform(Collider platform, out Vector3 point)
    {
        Bounds bounds = platform.bounds;
        float marginX = Mathf.Min(_edgeMargin, bounds.extents.x);
        float marginZ = Mathf.Min(_edgeMargin, bounds.extents.z);

        float x = Random.Range(bounds.min.x + marginX, bounds.max.x - marginX);
        float z = Random.Range(bounds.min.z + marginZ, bounds.max.z - marginZ);

        Ray ray = new Ray(new Vector3(x, bounds.max.y + 10f, z), Vector3.down);
        if (platform.Raycast(ray, out RaycastHit hit, bounds.size.y + 20f))
        {
            point = hit.point + Vector3.up * _heightOffset;
            return true;
        }

        point = Vector3.zero;
        return false;
    }

    private void OnItemUsed(Item item)
    {
        itemList.Remove(item);
    }
}
