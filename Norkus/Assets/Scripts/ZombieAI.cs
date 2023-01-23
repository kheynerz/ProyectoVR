 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieAI : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    public int visionDistance = 20;
    private Transform target;
    private Transform player;
    private Animator zombieAnim;
    private bool acceptedDistance = false;
    private bool canMove = true;
    private AudioSource shotSound;
    [SerializeField] private AudioSource attack;

    [SerializeField] private AudioSource walkSound;
    
    [SerializeField] private AudioSource groan;

    
/*    [SerializeField] private GameObject AK;
    
    [SerializeField] private GameObject SN;
    
    [SerializeField] private GameObject BA;*/

    public float timeToKill = 2;
    private Transform GazeManager; 

    private Transform joints;

    private Rigidbody rigid;
 
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        GazeManager = player.GetChild(0).GetChild(0);
        joints = gameObject.transform.GetChild(0);
        target = player.GetChild(0).GetChild(2);

    }


    void Start()
    {
        zombieAnim = transform.GetChild(0).GetComponent<Animator>();
        rigid = transform.GetComponent<Rigidbody>();
        shotSound = player.GetChild(0).GetChild(2 + PlayerPrefs.GetInt("player", 1)).GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!target || !canMove) return;

        var step =  speed * Time.deltaTime; 
        var distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.position.x,2) + Mathf.Pow(transform.position.y - target.position.y,2) + Mathf.Pow(transform.position.z - target.position.z,2));
        
        if (distance >= visionDistance){
            zombieAnim.Play("Idle_Look");
            acceptedDistance = false;
            walkSound.Stop();
        }else{
            if(!acceptedDistance){
                zombieAnim.Play("Run");
                groan.Play();
                acceptedDistance = true;
                walkSound.loop = true;
                walkSound.Play();
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
        walkSound.Stop();
        groan.Stop();
        StartCoroutine(DestroyGameObject());

        player.SendMessage("CheckKills", null, SendMessageOptions.DontRequireReceiver);
    }

    public void HitPlayer(){
        canMove = false;
        zombieAnim.Play("Attack1");
        attack.Play();
        player?.SendMessage("KillPlayer", null, SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerEnter(){
        GazeManager?.SendMessage("SetUpGaze", timeToKill, SendMessageOptions.DontRequireReceiver);
    }
    public void OnPointerExit(){
        GazeManager?.SendMessage("SetUpGaze", 2.0, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator DestroyGameObject(){
        rigid.useGravity = true;
        yield return new WaitForSeconds(3);
        rigid.useGravity = false;
        rigid.isKinematic = true;
        //Destroy(gameObject);
    }

}
