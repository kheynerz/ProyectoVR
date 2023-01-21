using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Transform player;

    void Start(){
        player = GameObject.FindWithTag("Player").transform;
    }

    public void GivePowerToPlayer(){
    }
}
