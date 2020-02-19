using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyNear : Enemy
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Partrol());
    }
    protected override IEnumerator Partrol()
    {
        yield return new WaitForSeconds(data.reactionTime);
        CheckMove(0);
        Move();
        yield return new WaitForSeconds(moveTime);
        ResetTimer();
        Wait();
        while (true)
        {
            yield return new WaitForSeconds(data.reactionTime);
            CheckMove(1);
            Move();
            yield return new WaitForSeconds(moveTime * 2);
            ResetTimer();
            Wait();
            yield return new WaitForSeconds(data.reactionTime);
            CheckMove(2);
            Move();
            yield return new WaitForSeconds(moveTime * 2);
            ResetTimer();
            Wait();
        }
    }
    /// <summary>
    /// 0為初始位置移動至巡邏點最左側、1為最左側移動至最右側、2則與1相反。
    /// </summary>
    /// <param name="index"></param>
    private void CheckMove (int index)
    {
        switch (index) 
        {
            case 0:
                transform.DOMoveX(transform.position.x - data.patrolRange, moveTime);
                break;
            case 1:
                transform.localScale = new Vector2(-lookX, transform.localScale.y);
                transform.DOMoveX(transform.position.x + data.patrolRange * 2, moveTime * 2);
                break;
            case 2:
                transform.localScale = new Vector2(lookX, transform.localScale.y);
                transform.DOMoveX(transform.position.x - data.patrolRange * 2, moveTime * 2);
                break;
        }

    }
    
   
}
