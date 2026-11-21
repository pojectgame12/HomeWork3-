using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Only a designated obelisk + correct aim + lit lamp activates the bridge.</summary>
public sealed class LampController : MonoBehaviour
{
    public static LampController Instance { get; private set; }
    public Transform designatedObelisk;
    public float energy = 60f;
    public float consumptionPerSecond = 12f;
    public float aimDegrees = -30f;
    public float allowedAngleError = 10f;
    public float interactionRadius = 3.3f;
    public bool isLit;
    private LineRenderer beam;
    private Transform player;
    public float Energy { get { return energy; } }
    public bool BridgeConditionsMet
    {
        get
        {
            if (!isLit || energy <= 0 || designatedObelisk == null) return false;
            Vector2 direction = designatedObelisk.position - transform.position;
            Vector2 aim = new Vector2(Mathf.Cos(aimDegrees * Mathf.Deg2Rad), Mathf.Sin(aimDegrees * Mathf.Deg2Rad));
            return Vector2.Angle(aim, direction) <= allowedAngleError;
        }
    }

    private void Awake()
    {
        Instance = this;
        beam = GetComponent<LineRenderer>();
        if (beam == null) beam = gameObject.AddComponent<LineRenderer>();
        beam.material = new Material(Shader.Find("Sprites/Default"));
        beam.startWidth = 0.12f;
        beam.endWidth = 0.27f;
        beam.positionCount = 2;
        beam.useWorldSpace = true;
        beam.sortingOrder = 7;
    }
    private void Start()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null) player = pc.transform;
    }
    private void Update()
    {
        bool near = player != null && Vector2.Distance(player.position, transform.position) <= interactionRadius;
        bool mobileLampClick = MobileInputState.ConsumeLamp();
        if (near && (Input.GetKeyDown(KeyCode.E) || mobileLampClick) && energy > 0f)
            isLit = !isLit;
        // J/L are rotate-left/right. Mouse aiming is only for non-touch and not UI clicks.
        if (near)
        {
            float rotate = 0f;
            if (Input.GetKey(KeyCode.J) || MobileInputState.rotateLeft) rotate -= 1f;
            if (Input.GetKey(KeyCode.L) || MobileInputState.rotateRight) rotate += 1f;
            aimDegrees = Mathf.Clamp(aimDegrees + rotate * 90f * Time.deltaTime, -75f, 75f);
            bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 0 && !overUI)
            {
                Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 delta = mouse - transform.position;
                if (delta.sqrMagnitude > 0.1f)
                    aimDegrees = Mathf.Clamp(Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg, -75f, 75f);
            }
        }
        if (isLit)
        {
            energy = Mathf.Max(0f, energy - consumptionPerSecond * Time.deltaTime);
            if (energy <= 0f) isLit = false;
        }
        Vector2 dir = new Vector2(Mathf.Cos(aimDegrees * Mathf.Deg2Rad), Mathf.Sin(aimDegrees * Mathf.Deg2Rad));
        beam.enabled = isLit;
        beam.SetPosition(0, transform.position + new Vector3(0.15f, 0.18f, 0));
        beam.SetPosition(1, transform.position + (Vector3)(dir * 5.0f));
        Color glow = BridgeConditionsMet ? new Color(0.2f, 1f, 0.8f, 0.85f) : new Color(1f, 0.8f, 0.3f, 0.8f);
        beam.startColor = glow;
        beam.endColor = new Color(glow.r, glow.g, glow.b, 0.05f);
    }
    public void Recharge(float amount)
    {
        energy = Mathf.Clamp(energy + amount, 0f, 100f);
    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
