using UnityEngine;

[RequireComponent(typeof(BoxCollider2D),typeof(SpriteRenderer))]
public sealed class ShadowBridge : MonoBehaviour
{
    public LampController lamp;
    public bool IsSolid { get; private set; }
    private BoxCollider2D bridgeCollision;
    private SpriteRenderer visual;
    private void Awake()
    {
        bridgeCollision = GetComponent<BoxCollider2D>();
        visual = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        IsSolid = lamp != null && lamp.BridgeConditionsMet;
        bridgeCollision.enabled = IsSolid;
        visual.color = IsSolid ? Color.white : new Color(0.65f, 0.55f, 0.95f, 0.17f);
    }
}
