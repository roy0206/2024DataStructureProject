using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject clearPanel;

/*    [SerializeField] Text timer;
    float curTime = 0;*/

    void Start()
    {
        
    }

    void Update()
    {
        DetectEnding();
    }

    private void FixedUpdate()
    {
/*        curTime = Time.fixedDeltaTime;
        timer.text = $"{(int)curTime / 60} : {(int)curTime % 60}";*/

    }

    void DetectEnding()
    {
        if((player.transform.position - endPoint.position).magnitude < 1f)
        {
            clearPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingPanel?.SetActive(false);
    }

    public void LeaveForOutGame()
    {
        SceneManager.LoadScene("OutGame");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
