using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    //子弹移动速度
    public float moveSpeed = 10;
    //是否是玩家的子弹  bool默认是false
    public bool isPlayerBullect;

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

     //触发器
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag){
            case "Tank":
                //如果不是自己的子弹
                if(!isPlayerBullect){
                    collision.SendMessage("TankDie");
                }
                break;
            case "Enemy":
                
                break;
            case "Block":
                Destroy(collision.gameObject); //被撞物体被销毁
                Destroy(gameObject);//自己被销毁
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            case "Home":
                collision.SendMessage("HomeDie");
                Destroy(gameObject);//自己被销毁
                break;
            default:
                break;
        }
    }
}
