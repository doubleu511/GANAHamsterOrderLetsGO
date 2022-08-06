using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerItem : ModuleItem
{
    public override void OnEnter()
    {
        base.OnEnter();

        SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();
        subtitle.ShowSubtitle(subtitle.onObtainPropeller);
    }
}
