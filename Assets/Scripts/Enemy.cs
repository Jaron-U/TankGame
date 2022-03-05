using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    //属性值
    public float moveSpeed = 4;
    //子弹旋转角度 控制子弹的贴图方向
    private Vector3 bullectEulerAngles;
    // private float defendTimeVal = 3;//无敌时间
    // private bool isDefended = true;//判断玩家是否是无敌状态
    private float v = -1;
    private float h;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bulletPrefab; //得到子弹预制体的引用
    public GameObject explosionPrefab; //爆炸预制体的引用
    // public GameObject defendEffectPrefab; //保护预制体的引用

    //计时器
    private float timeVal;//子弹冷却时间  
    private float timeValChangeDirection;//改变方向的计时器

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        //攻击CD如果大于0.8秒才能发射 
        if(timeVal>=0.8f){
            Attack();
        }else{
            timeVal+=Time.deltaTime;
        }
        
    }
    
    //固定时间和帧率
    private void FixedUpdate() {
        Move();
        // Attack();
    }

    //敌人攻击方法
    private void Attack(){
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bullectEulerAngles));
        timeVal = 0;
    }

    private void Move()
    {   
        //Tankmove function
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            //如果向左，就变为向左的图片 
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, 180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        ChangeRandomDirection();
    }

    //change direction
    private void ChangeRandomDirection(){
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0;
                h = -1;
            }
            else if (num > 2 && num <= 4)
            {
                v = 0;
                h = 1;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
    }

    //Tank 死亡方法
    private void TankDie(){
        // if (isDefended){
        //     return;
        // }
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
        //玩家得分
        PlayerManager.Instance.playerScore++;
    }

    //如果碰到敌人或者墙改变方向
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || 
        collision.gameObject.tag == "River"){
            timeValChangeDirection = 4;
            v *= -1;
            h *= -1;
        }
    }
}
