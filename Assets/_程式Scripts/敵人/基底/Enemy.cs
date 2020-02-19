using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [Header("敵人資料:"),SerializeField]
    protected EnemyData data;
    

    protected Animator ani;
    protected Rigidbody2D rb2d;
    protected Transform player;
    protected Vector3 startPos;
    protected float timer = 0;//計時器
    protected float lookX;//面對方向，大於0為面向左，反之為面向右。
    protected float moveTime;//移動至目的所需時間
    protected int state;
    protected virtual void Start()
    {
        lookX = transform.localScale.x;
        startPos = transform.position;
        moveTime = 1 / data.moveSpeed;
        ani = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }
    protected virtual void Update()
    {

        if (timer >= 0)
        {
            timer += Time.deltaTime;
        }
        //Debug.Log(timer);
        //Debug.Log(rb2d.velocity.x);
    }

    protected virtual IEnumerator Partrol()
    {
        yield return new WaitForSeconds(0);
    }

   

    protected virtual void Search()
    {
        float dis = Vector2.Distance(player.position, transform.position);
        if (dis <= data.attackRange)
        {
            Attack();
        }
    }
    protected virtual void Wait()
    {
        ani.SetBool("移動", false);
    }

    protected virtual void Move()
    {
        ani.SetBool("移動", true);
    }

    protected virtual void Attack()
    {
        ani.SetTrigger("攻擊");
    }

    protected virtual void Skill()
    {
        
    }

    protected virtual void Hit()
    {

    }

    protected virtual void BrokenArmour()
    {
        ani.SetTrigger("被擊");
    }

    protected virtual void Dead()
    {
        ani.SetBool("死亡", true);
    }

    protected virtual void DropItem()
    {

    }

    protected virtual void ResetTimer()
    {
        timer = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255,0,0,100);//紅色
        Gizmos.DrawWireSphere(transform.position, data.attackRange);//攻擊範圍
        Gizmos.color = new Color32(0, 0, 255, 100);//藍色
        Gizmos.DrawWireSphere(transform.position, data.alertRange);//警戒範圍
    }
}
