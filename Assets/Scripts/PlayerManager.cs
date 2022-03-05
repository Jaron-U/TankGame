using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;//判定游戏结束

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text lifeValText;


    //单例 
    private static PlayerManager instance;

    public static PlayerManager Instance{
        get{
            return instance;
        }
        set{
            instance = value;
        }
    }

    private void Awake(){
        instance = this;
    }

    //复活
    private void Recover(){
        if(lifeValue<0){
            isDefeat = true;
            return;
            //游戏失败，返回主界面
        }else{
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-3, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().creatPlayer = true;
            isDead = false;
        }
    }

    void Update(){
        if(isDead){
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        lifeValText.text = lifeValue.ToString();
    }

}
