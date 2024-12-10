using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : ItemBase
{
    public Sprite spr;
    protected override void Start()
    {
        base.Start();
        spr = Resources.Load<Sprite>("Assets/MYR/Resource/jump.png");

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
