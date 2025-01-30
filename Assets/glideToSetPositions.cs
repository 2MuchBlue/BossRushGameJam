using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glideToSetPositions : GlideToPosition
{

    public int index = 0;
    [SerializeField]
    Vector3[] positions;

    public void setIndex(int i){
        index = i;
        setTargetPos(positions[index]);
    }
}
