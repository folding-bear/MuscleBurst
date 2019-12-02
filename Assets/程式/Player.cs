using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("移動速度:")]
    [SerializeField]private float speed;
    [Header("跳躍力:")]
    public float jumpPower;

    //public Raycast raycast;
    public float high;
    public Transform buttomPos;
    public BoxCollider2D sole;
    private bool jumping;
    private float lookX;
    private Animator animator;
    void Start()
    {
        lookX = transform.localScale.x;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {

        //if (GetComponent<Rigidbody2D>().velocity.y < 0)
        //{
        //    //Debug.Log("掉落中");
        //}
        //if (GetComponent<Rigidbody2D>().velocity.y > 0)
        //{
        //    //Debug.Log("上跳中");
        //}
        
        if (Input.GetKey(KeyCode.D))
        {
            Move(0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("蹲下", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("蹲下", false);
        }
        
        //Debug.Log(jumping);
    }
    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.x <= 0)
        {
            animator.SetBool("走路", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            Jump();
            animator.SetBool("跳躍", true);
            jumping = true;
        }
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        //Debug.DrawRay(buttomPos.position, -Vector2.up, Color.red, 10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("down");
        if (!sole.IsTouching(collision.collider)) { return; }
        if (collision.gameObject.CompareTag("地面"))
        {
            float offset = collision.collider.bounds.size.y / 2 - 0.1f;
            float collisionY = collision.transform.position.y;
            //Debug.Log(offset);
            //Debug.Log("自身的Y"+buttomPos.position.y);
            //Debug.Log("碰撞到的Y" +( collisionY + offset));
            //Debug.Log(buttomPos.position.y > collision.transform.position.y + offset);
            if (buttomPos.position.y > collision.transform.position.y + offset) 
            {
                animator.SetBool("跳躍", false);
                jumping = false;
            }
            
        }
    }
   

    private void Move(int index)
    {
        if (index == 0)
        {
            animator.SetBool("走路", true);
            transform.Translate(transform.right * Time.deltaTime * speed);
            transform.localScale = new Vector2(lookX, 0.25f);
        }
        else
        {
            animator.SetBool("走路", true);
            transform.Translate(transform.right * Time.deltaTime * -speed);
            transform.localScale = new Vector2(-lookX, 0.25f);
        }
    }
    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * Time.deltaTime * jumpPower * jumpPower,ForceMode2D.Impulse);
        
        //raycast.fireRay = true;
        //RaycastHit2D hit = Physics2D.Raycast(buttomPos.position, -Vector2.up);
        
    }
}
