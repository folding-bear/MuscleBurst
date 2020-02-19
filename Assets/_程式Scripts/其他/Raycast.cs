using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public bool fireRay;
    [SerializeField]private float longueur;
    void Start()
    {
       
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (fireRay)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, longueur * Time.deltaTime);
            Debug.DrawRay(transform.position, -Vector2.up * longueur * Time.deltaTime, Color.red, 2);
            if (hit.collider.CompareTag("地面"))
            {
                fireRay = false;
            }
        }
    }
   
}
