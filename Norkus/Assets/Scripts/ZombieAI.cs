 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    private Transform target;
    private Animator zombieAnim;
    private bool acceptedDistance = false;
    private bool canMove = true;
    private AudioSource shotSound;
    [SerializeField] private AudioSource attack;

    public float timeToKill = 2;
    private Transform GazeManager; 

    private Transform joints;
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        shotSound = target.GetChild(0).GetChild(3).GetComponent<AudioSource>();
        GazeManager = target.GetChild(0).GetChild(0);
        joints = gameObject.transform.GetChild(0);
    }


    void Start()
    {
        zombieAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {

        if (!target || !canMove) return;

        var step =  speed * Time.deltaTime; 
        var distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.position.x,2) + Mathf.Pow(transform.position.y - target.position.y,2) + Mathf.Pow(transform.position.z - target.position.z,2));
        
        if (distance >= 20){
            zombieAnim.Play("Idle_Look");
            acceptedDistance = false;
        }else{
            if(!acceptedDistance){
                zombieAnim.Play("Run");
                acceptedDistance = true;
            }
        }
        if (acceptedDistance){
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            joints.LookAt(target.position, Vector3.up);   
        }
    }



    public void KillZombie(){
        canMove = false;
        zombieAnim.Play("Death");
        shotSound.Play();
        gameObject.tag = "Dead";
        StartCoroutine(DestroyGameObject());
    }

    public void HitPlayer(){
        canMove = false;
        zombieAnim.Play("Attack1");
        attack.Play();
        target?.SendMessage("KillPlayer", null, SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerEnter(){
        GazeManager?.SendMessage("SetUpGaze", timeToKill, SendMessageOptions.DontRequireReceiver);
    }
    public void OnPointerExit(){
        GazeManager?.SendMessage("SetUpGaze", 2.0, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator DestroyGameObject(){
        yield return new WaitForSeconds(5);
        //Destroy(gameObject);
    }

    
}
