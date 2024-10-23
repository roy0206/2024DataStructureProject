using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public void OnClicked()
    {
        GameObject.Find("OutGameManager").GetComponent<OutGameManager>().LoadGame(gameObject.name);
    }
}
