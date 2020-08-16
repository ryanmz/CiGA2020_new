using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text mainTime;
    public float recordTime;

    private PlayerController playerA;
    private PlayerController playerB;

    public Text playerAbox;
    public Text playerBbox;

    public Text playerAframe;
    public Text playerBframe;


    public Text playerARecord;
    public Text playerBRecord;

    public Text playerAResult;
    public Text playerBResult;

    public GameObject panel;

    public Button mainMenu;
    public Button reStart;
    // Start is called before the first frame update
    void Start()
    {
        this.playerA = GameObject.Find("PlayerA").GetComponent<PlayerController>();
        this.playerB = GameObject.Find("PlayerB").GetComponent<PlayerController>();

        this.reStart.onClick.AddListener(CommonFunction.Instance.ReStart);
        this.mainMenu.onClick.AddListener(CommonFunction.Instance.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        this.CalcuateTime();
        this.CalcuateBox();
    }

    void CalcuateTime()
    {
        if (this.recordTime >= 0.0f)
        {
            int minutes;
            int seconds;
            if (this.recordTime > 60)
            {
                minutes = (int)(this.recordTime / 60.0f);
                seconds = (int)(this.recordTime % 60.0f);
            }
            else
            {
                minutes = 0;
                seconds = (int)this.recordTime;
            }
            this.mainTime.text = minutes.ToString() + ":" + seconds.ToString();
            this.recordTime-=Time.deltaTime;
        }
        else
        {
            this.mainTime.text = "0:00";
            Time.timeScale = 0.0f;
            this.panel.SetActive(true);
            this.CalcuateResult();
        }
    }

    void CalcuateBox()
    {
        this.playerAbox.text = playerA.boxCollections.ToString();
        this.playerBbox.text = playerB.boxCollections.ToString();

        this.playerAframe.text = playerA.frameCollections.ToString();
        this.playerBframe.text = playerB.frameCollections.ToString();
    }

    void CalcuateResult()
    {
        if(playerA.frameCollections > playerB.frameCollections)
        {
            this.playerAResult.text = "Win!";
            this.playerBResult.text = "Lose...";
        }
        else if(playerA.frameCollections < playerB.frameCollections)
        {
            this.playerBResult.text = "Win!";
            this.playerAResult.text = "Lose...";
        }
        else
        {
            this.playerBResult.text =  "Tie";
            this.playerAResult.text =  "Tie";
        }
        this.playerBRecord.text = "X" +" "+ this.playerB.frameCollections.ToString();
        this.playerARecord.text = "X" +" "+ this.playerA.frameCollections.ToString();
    }


}
