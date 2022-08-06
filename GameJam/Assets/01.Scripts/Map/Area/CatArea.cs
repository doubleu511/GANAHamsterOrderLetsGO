using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatArea : CollisionArea
{
    public override void OnEnter()
    {
        coll.enabled = false;

        SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();
        subtitle.ShowSubtitle(subtitle.onEnterCatPlace);
    }
}
