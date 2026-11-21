using UnityEngine;

public sealed class PitTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        GameSession.Instance.FailLevel(LampController.Instance == null ? 0f : LampController.Instance.Energy);
    }
}
