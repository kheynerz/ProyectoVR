using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
          
    void OnTriggerEnter(Collider other) {
        other?.SendMessage("HitPlayer",null, SendMessageOptions.DontRequireReceiver);        
    }
}
