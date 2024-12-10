using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    StageManager stageManager;
    private void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            stageManager.Restart();
        }
    }
}
