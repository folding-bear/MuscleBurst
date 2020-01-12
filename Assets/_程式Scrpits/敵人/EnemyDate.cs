using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="敵人資料",menuName ="自訂/敵人資料")]
public class EnemyDate : ScriptableObject
{
    [Header("等級:"),Range(1,99)]
    public int level;
    [Header("血量:")]
    public float hp;
    [Header("霸體:")]
    public float superArmour;
    [Header("攻擊力:")]
    public float damage;
    [Header("碰撞傷害:")]
    public float contactDamage;
    [Header("防禦:")]
    public float defense;
    [Header("攻擊速度:")]
    public float attackSpeed;
    [Header("移動速度:")]
    public float moveSpeeed;
    [Header("經驗值:")]
    public float exp;
    [Header("抗性:"),Range(-2,1),Tooltip("抗性依序分別為火、水、風、地、光、暗。")]
    public float[] elementResistances = new float[6];

    public enum Rank{N,R,S};
    [Header("強度分級:")]
    public Rank rank;
}
