using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //用来装饰初始化地图所需物品的数组
    //0:home, 1:Block, 2:Wall, 3:Born, 4: River, 5: Grass, 6:AirWall
    public GameObject[] item;
    //已经占用的位置
    private List<Vector3> itemUsedPosition = new List<Vector3>();

    private void Awake(){
        InstanHome();
        CreateAirWall();
        CreateElements();
        GameObject player = Instantiate(item[3], new Vector3(-3, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().creatPlayer = true;
        CreateEnemies();

    }

    //实例化home
    private void InstanHome(){
        //实例化home
        //Quaternion.identity无旋转角度
        CreatItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用砖把家围起来
        CreatItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreatItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for(int i=-1; i<2; i++){
            CreatItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }

    private void CreatItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation){
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemUsedPosition.Add(createPosition);
    }

    //产生随机位置方法
    private Vector3 CreatRandomPosition(){
        //不生成x=-10, 10的两列， y=-8, 8这两行位置
        while(true){
            Vector3 createPosition = new Vector3(Random.Range(-10, 11), Random.Range(-7, 8), 0);
            if(!HasThisPosition(createPosition)){
                return createPosition;
            }
        }
    }

    //用来判断位置列表中是否有这个位置
    private bool HasThisPosition(Vector3 position1){
        // return itemUsedPosition.Contains(position1);
        for(int i=0; i<itemUsedPosition.Count; i++){
            if(position1 == itemUsedPosition[i]){
                return true;
            }
        }
        return false;
    }

    //产生空气墙
    private void CreateAirWall(){
        Instantiate(item[6], new Vector3(0,-9,0), Quaternion.identity);
        Instantiate(item[6], new Vector3(0,9,0), Quaternion.identity);
        Instantiate(item[6], new Vector3(-11,0,0), Quaternion.Euler(0,0,90));
        Instantiate(item[6], new Vector3(11,0,0), Quaternion.Euler(0,0,90));
    }

    //生产地图其他元素
    private void CreateElements(){
        for(int i=0; i<60; i++){
            //block
            CreatItem(item[1], CreatRandomPosition(), Quaternion.identity);
        }
        for(int k=0; k<20; k++){
            CreatItem(item[2], CreatRandomPosition(), Quaternion.identity);
            CreatItem(item[4], CreatRandomPosition(), Quaternion.identity);
            CreatItem(item[5], CreatRandomPosition(), Quaternion.identity);
        } 
    }

    //产生敌人
    private void CreateEnemies(){
        CreatItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreatItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreatItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemies2", 4, 5);
    }
    //每隔一段时间产生敌人
    private void CreateEnemies2(){
        int num = Random.Range(0,3);
        Vector3 EnemyPos = new Vector3();
        switch (num)
        {
            case 0:
                EnemyPos = new Vector3(-10, 8, 0);
                break;
            case 1:
                EnemyPos = new Vector3(0, 8, 0);
                break;
            case 2:
                EnemyPos = new Vector3(10, 8, 0);
                break;
            default:
                break;
        }
        CreatItem(item[3], EnemyPos, Quaternion.identity);  
    }

}
