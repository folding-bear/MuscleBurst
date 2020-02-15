using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [Header("貼圖:"), SerializeField]
    private SpriteRenderer[] sprites;
   
    public Material material_BySprites;

    public bool flicker;

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].material = material_BySprites;
        }
        if (flicker)
        {
            StartCoroutine(Flicker());
        }
    }

    private void Update()
    {

    }

    IEnumerator Flicker()
    {
        while (true)
        {
            material_BySprites.color = new Color32(150, 150, 150, 100);
            yield return new WaitForSeconds(0.1f);
            material_BySprites.color = new Color32(200, 200, 200, 200);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
