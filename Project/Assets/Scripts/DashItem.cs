using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DashItem : ItemBase
{
    protected override void Start()
    {
        itemImage = Resources.Load<Sprite>("jump");
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    public override bool OnItemUsed()
    {
        return playerController.PlayerDash();
    }
}
