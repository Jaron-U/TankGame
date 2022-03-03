using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //                  沿x轴移动                                      沿世界坐标系
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }
}
