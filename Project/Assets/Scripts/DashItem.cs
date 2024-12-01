using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : ItemBase
{
    protected override void Start()
    {
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
