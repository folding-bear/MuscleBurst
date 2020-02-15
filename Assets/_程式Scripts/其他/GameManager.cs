using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("慢速模式:"), SerializeField]
    private bool slowMotion;
    [Header("撥放速度:"), SerializeField]
    private float playSpeed;

    void Start()
    {
        
    }

   
    void Update()
    {
        if (slowMotion)
        {
          Time.timeScale = playSpeed;
        }
    }
}
