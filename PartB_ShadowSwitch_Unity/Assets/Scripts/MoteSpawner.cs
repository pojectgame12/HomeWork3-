using UnityEngine;

/// <summary>Recurring automatic Spawn from a second Prefab while shadow bridge is active.</summary>
public sealed class MoteSpawner : MonoBehaviour
{
    public GameObject motePrefab;
    public ShadowBridge bridge;
    private float nextSpawn;
    private void Update()
    {
        if (motePrefab == null || bridge == null || !bridge.IsSolid || Time.time < nextSpawn) return;
        nextSpawn = Time.time + 0.25f;
        Vector3 p = bridge.transform.position;
        float halfWidth = bridge.GetComponent<BoxCollider2D>().size.x * 0.47f;
        p.x += Random.Range(-halfWidth, halfWidth);
        p.y += 0.18f;
        Instantiate(motePrefab, p, Quaternion.identity);
    }
}
