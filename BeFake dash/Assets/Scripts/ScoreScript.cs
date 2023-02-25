using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public Text MyscoreText;
    private int ScoreNum = 0;
    void Start()
    {

    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coin)
    {
        if(coin.tag == "MyCoin")
        {
            ScoreNum+= 1;
            Destroy(coin.gameObject);
            MyscoreText.text = "" + ScoreNum;
        } 
    }

}
