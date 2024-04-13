using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool paused;
    public bool IsPaused { get => paused; set => paused = value; }

    private Rigidbody2D rb2d;
    private CircleCollider2D col;
    private float horizontal;
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float energy;
    private bool isRight;
    public bool isSliding;
    public bool isBouncy;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
        if(!isRight && horizontal > 0){
            Flip();
        }else if(isRight && horizontal < 0){
            Flip();
        }
        if(isSliding){
            Sliding();
        }
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed && IsGrounded()){
            if(!isBouncy)
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            else{
                rb2d.velocity = new Vector2(rb2d.velocity.x, 1.5f*jumpForce);
            }
        }

        if(context.canceled && rb2d.velocity.y > 0){
            if(!isBouncy)
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            else{
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0.75f*rb2d.velocity.y);
            }
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

    public void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.CompareTag("Slide")){
            isSliding = true;
        }else{
            isSliding = false;
        }

        if(col.gameObject.CompareTag("Bounce")){
            isBouncy = true;
        }else{
            isBouncy = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Bounce")){
            Debug.Log(rb2d.velocity.y/2);
            rb2d.AddForce(new Vector2(0, rb2d.velocity.y/2));
        }
    }

    public void Sliding(){
        if(rb2d.velocity.x != 0){
            rb2d.velocity = new Vector2();
        }
    }
}
