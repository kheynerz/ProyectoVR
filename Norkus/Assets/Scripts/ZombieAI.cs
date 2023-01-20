using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private GameObject player;
    public int moveSpeed = 2;
    private bool canMove = false;
    private Vector3 moveDirection;  

    [SerializeField] private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerVRwalk");
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (transform.position != player.transform.position)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                controller.Move(moveDirection * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 90);
            }
        }
    }
}
