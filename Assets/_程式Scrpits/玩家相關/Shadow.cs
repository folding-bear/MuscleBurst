using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [Header("貼圖:"), SerializeField]
    private SpriteRenderer[] sprites;
    [Header("顏色:"), SerializeField]
    private Color changeColor;

    void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = changeColor;
        }
    }

    
    void Update()
    {
        
    }
}
