using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public sealed class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.3f;
    public float jumpSpeed = 9.8f;
    private Rigidbody2D body;
    private BoxCollider2D hitbox;
    private SpriteRenderer sprite;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private bool Grounded()
    {
        Bounds b = hitbox.bounds;
        var feet = new Vector2(b.center.x, b.min.y - 0.06f);
        Collider2D[] contacts = Physics2D.OverlapBoxAll(feet, new Vector2(b.size.x * 0.72f, 0.14f), 0f);
        foreach (Collider2D c in contacts)
            if (c != hitbox && !c.isTrigger && c.enabled) return true;
        return false;
    }

    private void Update()
    {
        bool jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
        jump |= MobileInputState.ConsumeJump();
        if (jump && Grounded()) body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        if (body.position.y < -8f) GameSession.Instance.FailLevel(LampController.Instance == null ? 0f : LampController.Instance.Energy);
    }

    private void FixedUpdate()
    {
        float move = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || MobileInputState.left) move -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || MobileInputState.right) move += 1;
        body.linearVelocity = new Vector2(Mathf.Clamp(move, -1, 1) * moveSpeed, body.linearVelocity.y);
        if (move != 0 && sprite != null) sprite.flipX = move < 0;
    }
}
