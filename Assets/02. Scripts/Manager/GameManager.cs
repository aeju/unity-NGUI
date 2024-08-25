using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Application.Quit();
        }
    }
}
