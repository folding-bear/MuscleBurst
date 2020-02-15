using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("敵人資料:"),SerializeField]
    protected EnemyData data;
    [Header("眼睛:"), SerializeField]
    private Transform eye;

    protected Animator ani;
    

    protected virtual void Start()
    {
        ani = GetComponent<Animator>();
       
    }
    protected virtual void Update()
    {
        
    }

    protected virtual IEnumerator Partrol()
    {
        yield return new WaitForSeconds(0);
    }

    protected virtual void Wait()
    {
        ani.SetBool("移動", false);
    }

    protected virtual void Move()
    {
        ani.SetBool("移動", true);
    }

    protected virtual void Search()
    {

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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255,0,0,255);//紅色
        Gizmos.DrawRay(eye.position, -transform.right * data.attackRange);
        Gizmos.color = new Color32(0, 0, 255, 100);//藍色
        Gizmos.DrawRay(eye.position, -transform.right * data.searchDis);
    }
}
