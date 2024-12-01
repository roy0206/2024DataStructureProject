using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : ItemBase
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
        return playerController.Jump();
    }
}
