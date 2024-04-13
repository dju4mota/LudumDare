using System.Collections;
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
    private float bounceForce;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        transform.position = GameManager.Instance.checkpoint.position;
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
        if(transform.position.y < -10){
            StartCoroutine(Die());
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
        if(!isSliding || rb2d.velocity.x == 0){
            horizontal = context.ReadValue<Vector2>().x;
        }
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

        if(col.gameObject.CompareTag("Spikes")){
            StartCoroutine(Die());
        }
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Bounce") && bounceForce > 2f){
            Debug.Log(bounceForce);
            rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
        }
    }

    public void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Slide") && IsGrounded()){
           horizontal = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Bounce")){
            bounceForce = -rb2d.velocity.y/2;
        }
    }

    IEnumerator Die(){
        isDead = true;
        rb2d.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        isDead = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        transform.position = GameManager.Instance.checkpoint.position;
    }

}
