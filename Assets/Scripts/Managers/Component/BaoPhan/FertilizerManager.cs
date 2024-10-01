using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FertilizerManager : MonoBehaviour
{

    void Update()
    {
        Vector3 position = ClickManager.UniversalPosition();
        position.z = 0f;
        transform.position = position;
    }

   
}


