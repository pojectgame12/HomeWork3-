using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class CrystalPickup : MonoBehaviour
{
    public float energyReward = 25f;
    private Vector3 startPosition;
    private void Start() { startPosition = transform.position; }
    private void Update()
    {
        float bob = Mathf.Sin(Time.time * 2.8f) * 0.12f;
        transform.position = startPosition + new Vector3(0, bob, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        GameSession.Instance.AddCrystal();
        if (LampController.Instance != null) LampController.Instance.Recharge(energyReward);
        Destroy(gameObject);
    }
}
