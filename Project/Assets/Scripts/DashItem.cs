using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DashItem : ItemBase
{
    private Sprite spr;
    protected override void Start()
    {
        spr = Resources.Load<Sprite>("Assets/MYR/Resource/dash.png");
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
