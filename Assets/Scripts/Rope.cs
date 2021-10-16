using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float shotSpeed;
    private void Update()
    {
        transform.Translate(Vector3.up * shotSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        Destroy(gameObject);
    }
}
