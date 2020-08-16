using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{

    public Button gameStart;
    public Button tutorialStart;
    public Button creditStart;
    public Button exitStart;

    
    // Start is called before the first frame update
    void Start()
    {
        this.gameStart.onClick.AddListener(CommonFunction.Instance.ReStart);
        this.tutorialStart.onClick.AddListener(CommonFunction.Instance.TutorialScene);
        this.creditStart.onClick.AddListener(CommonFunction.Instance.CreditScene);
        this.exitStart.onClick.AddListener(CommonFunction.Instance.ExitGame);
    }


    
}
