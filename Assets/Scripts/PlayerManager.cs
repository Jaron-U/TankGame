using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

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
    public GameObject gameOverUI;


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
        if(lifeValue<=0){
            isDefeat = true;
            // Invoke("PauseGame", 0.5f);//延迟一秒暂停
            gameOverUI.SetActive(true);
            // Thread.Sleep(2000);
            ReturnToMain();
        }else{
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-3, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().creatPlayer = true;
            isDead = false;
        }
    }

    private void PauseGame(){
        Time.timeScale = 0; //暂停画面
    }

    void Update(){
        if(isDefeat){
            //游戏失败，返回主界面
            Invoke("PauseGame", 0.5f);//延迟一秒暂停
            gameOverUI.SetActive(true);
            Time.timeScale = 1; 
            Thread.Sleep(1000);
            Invoke("ReturnToMain", 0.1f);
        }
        if(playerScore == 5){
            //游戏失败，返回主界面
            Invoke("PauseGame", 0.5f);//延迟一秒暂停
            gameOverUI.SetActive(true);
            Time.timeScale = 1; 
            Thread.Sleep(1000);
            Invoke("ReturnToMain", 0.1f);
        }
        if(isDead){
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        lifeValText.text = lifeValue.ToString();
    }

    private void ReturnToMain(){
        SceneManager.LoadScene(0);
    }

    private void StartGame(){
        Time.timeScale = 1; 
    }
}
