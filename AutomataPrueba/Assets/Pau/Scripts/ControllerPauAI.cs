using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPauAI : MonoBehaviour
{
    public AI_Agent agent; // pointer in c++

    // Update is called once per frame
    void Update()
    {
        //ASSERT
        agent.updateAgent();
    }
}
