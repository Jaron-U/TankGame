using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{   
    private SpriteRenderer sr;//拿到组件
    public Sprite BrokenSprite; //破坏图片
    public GameObject explosionPrefab; //爆炸效果

    public AudioClip dieAudio; //死亡声音
    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
    }
    
    //家被摧毁
    public void HomeDie(){
        sr.sprite = BrokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;

        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
