using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "自訂/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("玩家姓名:")]
    public string playerName = "菲蕾姆";
    [Header("等級:"), Range(1, 50)]
    public int level = 1;
    [Header("經驗值:"), Range(0, 999999)]
    public int exp;
    [Header("最大生命值:"),Range(0,99999)]
    public float maxHP;
    [Header("當前生命值:"),Range(0,99999)]
    public float curHP;
    [Header("攻擊力:"),Range(1,9999)]
    public float attack;
    [Header("防禦:"),Range(1,9999)]
    public float defense;
    [Header("痠痛值上限:"), Range(0, 100)]
    public float pang;
    [Header("當前痠痛值:"), Range(0, 100)]
    public float curPang;
    [Header("痠痛恢復值/每秒:"), Range(0, 100)]
    public float pangRestore;
    [Header("興奮值上限:"), Range(0, 200)]
    public float maxExcited;
    [Header("當前興奮值:"), Range(0, 200)]
    public float curExcited;
    [Header("霸體:")]
    public float superArmour;
    [Header("攻擊速度:")]
    public float attackRate;
    [Header("移動速度:")]
    public float moveSpeed;
    
}
