using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapReacting : MonoBehaviour
{
    public void getFloorInfo(SpatialIndex.FLOOR_STATUS state)
    {
        if (CharacterMove.checkWithFloor((int)state, (int)SpatialIndex.FLOOR_STATUS.LAVA))
        {
            characterMove.speed *= 0.5f;
        }
        if (CharacterMove.checkWithFloor((int)state, (int)SpatialIndex.FLOOR_STATUS.WATER))
        {
            characterMove.speed *= 2f;
        }
    }

    [SerializeField]
    SpatialIndex spatial;

    [SerializeField]
    CharacterMove characterMove;

    // Update is called once per frame
    void Update()
    {
        spatial.getFloorState(transform.position.x, transform.position.z, gameObject);
    }
}
