using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="敵人資料",menuName ="自訂/敵人資料")]
public class EnemyData : ScriptableObject
{
    public enum Rank { N, R, S };
    [Header("強度分級:")]
    public Rank rank;
    [Header("敵人ID:")]
    public string id;
    [Header("等級:"),Range(1,99)]
    public int level;
    [Header("最大血量:"),Range(0,9999999)]
    public float maxHp;
    [Header("霸體:")]
    public float superArmour;
    [Header("攻擊力:")]
    public float attack;
    [Header("防禦:")]
    public float defense;
    [Header("攻擊間隔:")]
    public float attackRate;
    [Header("移動速度:"),Tooltip("")]
    public float moveSpeed;
    [Header("碰撞傷害:")]
    public float touchDamage; 
    [Header("抗性:"),Range(-2,1),Tooltip("抗性依序為 0 = 水、1 = 火、2 = 地、3 = 風、4 = 光、5 = 暗。(PS:數值 1 = 100%)")]
    public float[] elementResistances = new float[6];
    [Header("經驗值:")]
    public float exp;
    [Header("掉落物:"), Tooltip("此處為物品ID。")]
    public int[] dropItemID;

    [Header("反應時間:")]
    public float reactionTime;
    [Header("警戒範圍:")]
    public float alertRange;
    [Header("巡邏範圍:"),Tooltip("")]
    public float patrolRange;
    [Header("攻擊範圍:")]
    public float attackRange;
}
