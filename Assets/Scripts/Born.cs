using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{   
    public GameObject playerPrefab;
    //敌人的列表
    public GameObject[] enemyPrefabList;
    //判断产生玩家还是敌人
    public bool creatPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //延时调用
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank(){
        //true == 产生玩家
        if(creatPlayer){
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }else{
            int num = Random.Range(0,2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }

    }
}
