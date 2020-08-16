using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReturn : MonoBehaviour
{
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(CommonFunction.Instance.MainMenu);
    }

    
}
