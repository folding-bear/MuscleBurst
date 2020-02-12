using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAction : MonoBehaviour
{
    #region 面板可見的區域變數
    [Header("移動速度:"), SerializeField]
    private float speed = 0.25f;
    [Header("最大跳躍力:"), SerializeField]
    private float maxJumpPower = 15;
    [Header("最小跳躍力:"), SerializeField]
    private float minJumpPower = 2;
    [Header("跳躍最大高度:"), SerializeField]
    private float maxHigh = 5.5f;
    [Header("二段跳權限:"), SerializeField]
    private bool doubleJump;
    [Header("閃躲移動距離:"), SerializeField]
    private float dodgeDis;
    #endregion

    #region 面板隱藏的區域變數
    //[Header("腳底Transform:"), SerializeField]
    private Transform buttomPos;
    //[Header("腳底Collider:"), SerializeField]
    private BoxCollider2D sole;
    
    private Rigidbody2D rd2D;
    private Animator animator;
    private Collider2D platform;
    public int state;//0為無移動鎖定狀態，1為攻擊狀態，2為閃躲狀態
    public bool jumping,maxHeight,canJumpDown,squat;//跳躍狀態，抵達跳躍最大高度，在平台上可下躍狀態，蹲下狀態
    public bool controlLock,actionLock,moveLock;//控制鎖，動作鎖，移動鎖
    private float lookX;
    private float FightWaitTime;//進入戰鬥狀態後的等待時間
    private float starthigh, jumphigh,nowhigh; //起跳位置 相差高度 現在高度
    #endregion

    #region 內建方法
    void Start()
    {
        lookX = transform.localScale.x;//存取自身的scale
        animator = GetComponent<Animator>();//存取Animator
        rd2D = GetComponent<Rigidbody2D>();//存取Rigidbody2D
        buttomPos = transform.GetChild(5).GetComponent<BoxCollider2D>().transform;
        sole = transform.GetChild(5).GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        Move();
        //Debug.Log(rd2D.velocity.x);
        Squat();
        Attack();
        //Debug.Log(jumping);
        if (animator.GetBool("戰鬥待機"))
        {
            FightWaitTime += Time.deltaTime;
            if (FightWaitTime >= 3)//在戰鬥狀態待機超過3秒，則恢復一般站立動作
            {
                Fighting(1);
            }
        }
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
        if (collision.gameObject.CompareTag("地面") || collision.gameObject.CompareTag("可下躍平台"))
        {
            float offset = collision.collider.bounds.size.y / 2 - 0.3f;
            float collisionY = collision.transform.position.y;
            //Debug.Log(offset);
            //Debug.Log("腳底的Y"+buttomPos.position.y);
            //Debug.Log("碰撞到的Y" +( collisionY + offset));
            //Debug.Log(buttomPos.position.y > collision.transform.position.y + offset);
            if (buttomPos.position.y > collision.transform.position.y + offset /*&& animator.GetBool("跳躍")*/) 
            {
                
                animator.SetBool("跳躍", false);
                animator.SetBool("落下", false); 
                jumping = false;
                maxHeight = false;

                starthigh = rd2D.transform.position.y; //重設起跳位置
                nowhigh = starthigh; //重設當前高度
                jumphigh = 0; //重設高度

                actionLock = false;
                
                Invoke("SoleUnShow",0.1f);
            }
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("可下躍平台") )
        {
           
            platform = collision.collider;
            
            if (animator.GetBool("蹲下"))
            {
                canJumpDown = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("可下躍平台")) 
        {
            platform = null;
            canJumpDown = false;
        } 
    }

    #endregion
    #region 自訂方法
    //控制鎖時間 輕攻擊0.05s，重攻擊0.3s，閃躲0.3s
    #region 玩家動作
    private void Idle()
    {
        if (rd2D.velocity.x == 0)//停止移動就回到站立動畫
        {
            animator.SetBool("跑步", false);
        }
    }
    private void Move()
    {
        if (moveLock || squat) return;
        if (Input.GetKey(KeyCode.D))
        {
            Fighting(1);
            animator.SetBool("跑步", true);
            transform.Translate(transform.right * speed);
            //rd2D.AddForce(transform.right * speed);
            transform.localScale = new Vector2(lookX, 0.25f);//角色面向右
        }
        if (Input.GetKey(KeyCode.A))
        {
            Fighting(1);
            animator.SetBool("跑步", true);
            transform.Translate(transform.right * -speed);
            //rd2D.AddForce(transform.right * -speed);
            transform.localScale = new Vector2(-lookX, 0.25f);//角色面向左
        }
       
    }
    private void Squat()
    {
        if (jumping ) return;
        if (squat)
        {
            animator.SetBool("蹲下", true);
            moveLock = true;
            if (Input.GetKey(KeyCode.D)) transform.localScale = new Vector2(lookX, 0.25f);//角色面向右;
            else if (Input.GetKey(KeyCode.A)) transform.localScale = new Vector2(-lookX, 0.25f);//角色面向左
        }   
        else if (Input.GetKey(KeyCode.S))
        {
            if (controlLock || actionLock) return;
            animator.SetBool("蹲下", true);
            Fighting(1);
            moveLock = true;
            
            if (Input.GetKey(KeyCode.D)) transform.localScale = new Vector2(lookX, 0.25f);//角色面向右;
            else if (Input.GetKey(KeyCode.A)) transform.localScale = new Vector2(-lookX, 0.25f);//角色面向左
        }

        else if (Input.GetKeyUp(KeyCode.S) || !squat)
        {
            if (state > 0) 
            {
                moveLock = true;
            }
            else
            {
                StartCoroutine(MoveUnLock(0));
            }
            animator.SetBool("蹲下", false);
          
        }
    }
    /// <summary>
    /// 該方法須配合Event Trigger ; index=0 為 pointer down時使用，index=1 為 update selected時使用，index=2 為 pointer up時使用。
    /// </summary>
    /// <param name="index"></param>
    public void Jump(int index)
    {
        //Debug.Log(index);
        #region Mobile
        
        if(index ==0 && canJumpDown && platform != null)//平台下躍判定
        {
            platform.isTrigger = true;
            //Debug.Log("1");
            animator.SetBool("落下", true);
            SoleShow();
            Invoke("EndJumpDown", 0.5f);
            
        }    
        else if (index==0 && !jumping)
        {
           
            if (controlLock || animator.GetBool("蹲下")) return;
            actionLock = true;
            /// 2019/12/21 20-22 by wen
            jumping = true;
            starthigh = rd2D.transform.position.y; //紀錄起跳位置
            nowhigh = starthigh; //紀錄當前高度
            jumphigh = 0; //紀錄跳躍高度
            ///
            Fighting(1);
            if (state != 0) return;
           
            animator.SetBool("跳躍", true);
            
            Invoke("SoleShow",0.2f);
            //rd2D.gravityScale = 0;
            //if(!animator.GetBool("跳躍")) 
            
                rd2D.velocity = transform.up * maxJumpPower;
            //animator.SetBool("落下", true);
            //rd2D.AddForce(transform.up * maxJumpPower, ForceMode2D.Impulse);

        }
        else if(index==1 && jumping && !maxHeight)
        {
            //if (rd2D.velocity.y > maxHigh|| rd2D.velocity.y <0)
            //{
            //    maxHeight = true;
            //}
            nowhigh = rd2D.transform.position.y; //紀錄當前高度
            jumphigh = nowhigh - starthigh; //紀錄跳躍高度
           
            //rd2D.gravityScale = 0;

            //if (jumphigh >= maxHigh )
            //{
            //    maxHeight = true;
                
            //    rd2D.gravityScale = 10;
            //    //Debug.Log("2");
            //    animator.SetBool("落下", true);
            //}
            
            rd2D.velocity = transform.up * maxJumpPower;
            //rd2D.velocity += Vector2.up * minJumpPower;
            //rd2D.AddForce(transform.up * minJumpPower, ForceMode2D.Impulse);  
        }
        if (index == 2 && jumping)
        {
            maxHeight = true;
            
            rd2D.gravityScale = 10;
            //Debug.Log("3");
            //animator.SetBool("跳躍", false);
        }

        //放到外面-----------------------------------------
        nowhigh = rd2D.transform.position.y; //紀錄當前高度
        jumphigh = nowhigh - starthigh; //紀錄跳躍高度
        if ((jumphigh > maxHigh || rd2D.velocity.y < 0) && jumping)
        {
            maxHeight = true;
            //Debug.Log("4");
            //animator.SetBool("跳躍", false);
        }

       
        //-------------------------------------------------
        #endregion

    }
    private void Jump()
    {
        #region PC
        //if (Input.GetKeyDown(KeyCode.W) && !jumping)
        //{

        //    animator.SetBool("跳躍", true);
        //    jumping = true;
        //    rd2D.AddForce(transform.up * maxJumpPower, ForceMode2D.Impulse);

        //}
        //else if (Input.GetKey(KeyCode.W) && jumping && !maxHeight)
        //{

        //    if (rd2D.velocity.y > maxHigh)
        //    {
        //        maxHeight = true;
        //    }
        //    rd2D.AddForce(transform.up * minJumpPower, ForceMode2D.Impulse);
        //}
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    maxHeight = true;
        //}
        #endregion
    }
    /// <summary>
    /// index=0 為輕攻擊，index=1 為重攻擊。
    /// </summary>
    /// <param name="index"></param>
    public void Attack(int index)
    {
        if (controlLock) return;
        controlLock = true;
        state = 1;

        switch (index)
        {
            case 0:
                animator.SetTrigger("輕拳");
                break;
            case 1:
                animator.SetTrigger("重拳");
                break;
        }
        
        if(!animator.GetBool("蹲下")) Fighting(0);

    }
    public void Attack()
    {
        if (controlLock) return;
      
       

        if (Input.GetKeyDown(KeyCode.J))
        {
            state = 1;
            controlLock = true;
            animator.SetTrigger("輕拳"); 
            if (!animator.GetBool("蹲下")) Fighting(0);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            state = 1;
            controlLock = true;
            animator.SetTrigger("重拳");
            
            if (!animator.GetBool("蹲下")) Fighting(0);
        }       

        

    }
    /// <summary>
    /// index=0，代表進入戰鬥待機狀態；index=1，離開戰鬥待機狀態。
    /// </summary>
    /// <param name="index"></param>
    private void Fighting(int index)
    {
        
        if (index == 0)
        {
            animator.SetBool("戰鬥待機", true);
            FightWaitTime = 0;
        }
        else
        {
            animator.SetBool("戰鬥待機", false);
            FightWaitTime = 0;
        }
       
    }
    public void Dodge()
    {
        if (controlLock || actionLock|| jumping) return;
        controlLock = true;
        state = 2;
        animator.SetTrigger("閃躲");
  
        if (transform.localScale.x > 0 || Input.GetKey(KeyCode.D)) 
        {
            rd2D.AddForceAtPosition(transform.right * dodgeDis, transform.position,ForceMode2D.Impulse);
        }
        else if(transform.localScale.x < 0 || Input.GetKey(KeyCode.A))
        {
            rd2D.AddForceAtPosition(-transform.right * dodgeDis, transform.position,ForceMode2D.Impulse);
        }
    }
    #endregion
    #region 控制開關
    private void SoleShow()
    {
        sole.enabled = true;
        
    }
    private void SoleUnShow()
    {
        sole.enabled = false;
    }
    private void EndAttacking()
    {
        state = 0;
    }
    private void EndJumpDown()
    {
        platform.isTrigger = false;
        canJumpDown = false;
        //animator.SetBool("落下", false);

    }
    public void Ray()
    {
        RaycastHit2D hit= Physics2D.Raycast(buttomPos.position, transform.up, 5, 256);
        if (hit)
        {
            squat = true;
            
            //print(hit.collider);
        }
        else
        { 
            //print(hit.collider);
            squat = false;
            state = 0;
        }

    }
    /// <summary>
    /// locknumber 0-2分別代表:controlLock、actionLock、moveLock；3-5分別代表:0和1、0和2、1和2；6則代表0-2全部。
    /// lockswitch true/false 為locknumber的值；time為延遲時間。
    /// </summary>
    /// <returns></returns>
    public IEnumerator LockSwitch(int locknumber,bool lockswitch,float time)
    {
        yield return new WaitForSeconds(time);
        switch (locknumber)
        {
            case 0:
                controlLock = lockswitch;
                break;
            case 1:
                actionLock = lockswitch;
                break;
            case 2:
                moveLock = lockswitch;
                break;
            case 3:
                controlLock = lockswitch;
                actionLock = lockswitch;
                break;
            case 4:
                controlLock = lockswitch;
                moveLock = lockswitch;
                break;
            case 5:
                actionLock = lockswitch;
                moveLock = lockswitch;
                break;
            case 6:
                controlLock = lockswitch;
                actionLock = lockswitch;
                moveLock = lockswitch;
                break;
        }
    }
    public IEnumerator MoveUnLock(float time)
    {
        yield return new WaitForSeconds(time);
        moveLock = false;
    }
    public IEnumerator ContorlUnLock(float time)
    {
        yield return new WaitForSeconds(time);
        controlLock = false;
    }
    #endregion
    #endregion

}
