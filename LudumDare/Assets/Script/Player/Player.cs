using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool paused;
    public bool IsPaused { get => paused; set => paused = value; }

    private Rigidbody2D rb2d;
    private CircleCollider2D col;
    private float input;
    private float horizontal;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private bool isRight;
    public bool isSliding;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSliding){
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
        }
        if(!isRight && horizontal > 0){
            Flip();
        }else if(isRight && horizontal < 0){
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed && IsGrounded()){
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        if(context.canceled && rb2d.velocity.y > 0){
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded(){
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, GameManager.Instance.groundMask);
        return hit.collider != null;
    }

    private void Flip(){
        isRight = !isRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
    }
}
