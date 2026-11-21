using UnityEngine;

public sealed class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        GameSession.Instance.FinishLevel(LampController.Instance == null ? 0f : LampController.Instance.Energy);
    }
}
