using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    //属性值
    public float moveSpeed = 3;
    //子弹旋转角度 控制子弹的贴图方向
    private Vector3 bullectEulerAngles;
    //子弹冷却时间
    private float timeVal;
    private float defendTimeVal = 3;//无敌时间
    private bool isDefended = true;//判断玩家是否是无敌状态

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bulletPrefab; //得到子弹预制体的引用
    public GameObject explosionPrefab; //爆炸预制体的引用
    public GameObject defendEffectPrefab; //保护预制体的引用

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        // float h = Input.GetAxisRaw("Horizontal");
        // transform.Translate(Vector3.right*h*moveSpeed*Time.deltaTime, Space.World);
        // if(h<0){
        //     sr.sprite = tankSprite[3];
        // }else if(h>0){
        //     sr.sprite = tankSprite[1];
        // }

        // float v = Input.GetAxisRaw("Vertical");
        // transform.Translate(Vector3.up*v*moveSpeed*Time.deltaTime, Space.World);
        // if(v<0){
        //     sr.sprite = tankSprite[2];
        // }else if(v>0){
        //     sr.sprite = tankSprite[0];
        // }
        
        //检查无敌状态
        if(isDefended){
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal<=0){
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }


        //攻击CD如果大于0.4秒才能发射 
        if(timeVal>=0.4f){
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

    //攻击方法
    private void Attack(){
        //如果检测到玩家按下一个空格键
        if(Input.GetKeyDown(KeyCode.Space)){
            //实例化子弹   子弹预制体     当前坦克的位置         坦克的旋转角度
            //transform.eulerAngles+
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bullectEulerAngles));
        }
    } 

    private void Move(){ //Tankmove function
        //h = 1 or -1; 1的input是D或者->； -1的input 是A or <-
        float h = Input.GetAxisRaw("Horizontal");
        //vector3.right y轴 .up x轴  .forward z轴 
        //space.world指当前视图
        transform.Translate(Vector3.right*h*moveSpeed*Time.fixedDeltaTime, Space.World);
        if(h<0){
            //如果向左，就变为向左的图片 
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }else if(h>0){
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
        
        //防止玩家同时按下两个按键
        if (h!=0){
            return;
        }
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up*v*moveSpeed*Time.fixedDeltaTime, Space.World);
        if(v<0){
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0,0,180);
        }else if(v>0){
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0,0,0);
        }
    }

    //Tank 死亡方法
    private void TankDie(){
        if (isDefended){
            return;
        }
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
