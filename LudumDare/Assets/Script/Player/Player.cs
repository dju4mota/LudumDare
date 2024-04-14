using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool paused;
    public bool IsPaused { get => paused; set => paused = value; }

    private Rigidbody2D rb2d;
    private CircleCollider2D col;
    private AnimationController anim;
    private float horizontal;
    [SerializeField] public float speed;
    [SerializeField] public GameObject SummonedPlatform;
    [SerializeField] public float slidingSpeed;
    [SerializeField] public float jumpForce;
    [SerializeField] public int Maxenergy;
    [SerializeField] public int energy;
    private bool isRight;
    public bool isSliding;
    public bool isBouncy;
    private float bounceForce;
    private bool isDead;
    public float cooldown;

    [SerializeField] BlockManager.BlockEnum Platform = BlockManager.BlockEnum.Block;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<AnimationController>();
        transform.position = GameManager.Instance.checkpoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
        Mathf.Clamp(rb2d.velocity.x, -15f, 15f);
        if(!isSliding)
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);

        if(!isRight && horizontal > 0){
            Flip();
        }else if(isRight && horizontal < 0){
            Flip();
        }
        if(transform.position.y < -10){
            StartCoroutine(Die());
        }

        cooldown = Mathf.Min(cooldown, 1f);
        cooldown += Time.deltaTime;
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed && IsGrounded()){
            if(!isBouncy){
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                Debug.Log("jump");
            }
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

    public void GetGround(InputAction.CallbackContext context){
        if(context.performed){
            Platform = GridBlock.Instance.changeBlock();
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

    public void SlidingMovement(){
            if(isRight){
                rb2d.AddForce(Vector2.right * 10f);
            }else{
            rb2d.AddForce(Vector2.left * 10f);
            }
    }

    public void Summon(InputAction.CallbackContext context){
        if(context.performed && cooldown >= 1f){
            CheckEnergy();
            BlockManager.Instance.addBlock(Platform, SummonedPlatform.transform.position);
            cooldown = 0f;
        }
    }

    public void OnCollisionStay2D(Collision2D col){

        if(col.gameObject.CompareTag("Bounce")){
            isBouncy = true;
        }else{
            isBouncy = false;
        }

         if(col.gameObject.CompareTag("Slide")){
            isSliding = true;
            SlidingMovement();
        }else{
            isSliding = false;
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
        if(col.gameObject.CompareTag("Slide")){
            SlidingMovement();
        }
        if(col.gameObject.CompareTag("Checkpoint")){
            energy = Maxenergy;
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Bounce")){
            bounceForce = -rb2d.velocity.y/2;
        }
    }

    public IEnumerator Die(){
        isDead = true;
        rb2d.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.6f);
        isDead = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        transform.position = GameManager.Instance.checkpoint.position;
    }

    public bool CheckEnergy(){
        int custo = BlockManager.Instance.GetEnergy(Platform);
        if(custo < energy){
            energy -= custo;
            return true;
        }return false;
    }

    void Animations(){
        if(isDead){
            anim.ChangeAnimationState("Dead");
        }
        else if(horizontal != 0 && IsGrounded()){
            anim.ChangeAnimationState("Running");
        }
        else if(!IsGrounded()){
            if(rb2d.velocity.y < 0){
                anim.ChangeAnimationState("Falling");
            }else{
                anim.ChangeAnimationState("Jumping");
            }
        }
        else{
            anim.ChangeAnimationState("Idle");
        }   
    }

}
