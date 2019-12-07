﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    [Header("移動速度:"), SerializeField]
    private float speed = 0.25f;
    [Header("最大跳躍力:"), SerializeField]
    private float maxJumpPower = 10;
    [Header("最小跳躍力:"), SerializeField]
    private float minJumpPower = 3;
    [Header("跳躍最大高度:"), SerializeField]
    private float maxHigh = 20;
    [Header("腳底Transform:"), SerializeField]
    private Transform buttomPos;
    [Header("腳底Collider:"), SerializeField]
    private BoxCollider2D sole;



    private Rigidbody2D rd2D;
    private Animator animator;
    private bool jumping,maxHeight;//跳躍狀態，抵達跳躍最大高度
    private float lookX;
    
    
    void Start()
    {
        lookX = transform.localScale.x;//存取自身的scale
        animator = GetComponent<Animator>();//存取Animator
        rd2D = GetComponent<Rigidbody2D>();//存取Rigidbody2D
    }

    
    void Update()
    {
        Move();
        //Debug.Log(rd2D.velocity.x);
        Squat();
        //Debug.Log(jumping);
    }
    private void FixedUpdate()
    {
        
        Idle();
        Jump();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("down");
        if (!sole.IsTouching(collision.collider)) { return; }
        if (collision.gameObject.CompareTag("地面"))
        {
            float offset = collision.collider.bounds.size.y / 2 - 0.2f;
            float collisionY = collision.transform.position.y;
            //Debug.Log(offset);
            //Debug.Log("腳底的Y"+buttomPos.position.y);
            //Debug.Log("碰撞到的Y" +( collisionY + offset));
            //Debug.Log(buttomPos.position.y > collision.transform.position.y + offset);
            if (buttomPos.position.y > collision.transform.position.y + offset) 
            {
                animator.SetBool("跳躍", false);
                jumping = false;
                maxHeight = false;
                Time.timeScale = 1;
            }
            
        }
    }
   
    private void Idle()
    {
        if (rd2D.velocity.x == 0)//停止移動就回到站立動畫
        {
            animator.SetBool("跑步", false);
        }
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("跑步", true);
            transform.Translate(transform.right * speed);
            //rd2D.AddForce(transform.right * speed);
            transform.localScale = new Vector2(lookX, 0.25f);//角色面向右
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("跑步", true);
            transform.Translate(transform.right * -speed);
            //rd2D.AddForce(transform.right * -speed);
            transform.localScale = new Vector2(-lookX, 0.25f);//角色面向左
        }
       
    }
    private void Squat()
    {
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("蹲下", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("蹲下", false);
        }
    }
    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.W) && !jumping)
        {

            animator.SetBool("跳躍", true);
            jumping = true;
            rd2D.AddForce(transform.up * maxJumpPower, ForceMode2D.Impulse);

        }
        else if (Input.GetKey(KeyCode.W) && jumping && !maxHeight)
        {

            if (rd2D.velocity.y > maxHigh)
            {
                maxHeight = true;
            }
            rd2D.AddForce(transform.up * minJumpPower, ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            maxHeight = true;
        }
             
    }
}
