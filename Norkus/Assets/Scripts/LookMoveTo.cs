using System.Collections;
using UnityEngine;

public class LookMoveTo : MonoBehaviour
{
    public float speed = 5.0f;

    public float lookangledown = 15.0f;

    public Transform vrPlayer;

    public bool moveForward;

    private CharacterController _miCC;

    [SerializeField] private AudioSource increaseSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _miCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    void Mover()
    {
        if (vrPlayer.eulerAngles.x >= lookangledown && vrPlayer.eulerAngles.x < 90.0f)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if (moveForward)
        {
            Vector3 _forward = vrPlayer.TransformDirection(Vector3.forward);
            _miCC.SimpleMove(_forward * speed);
        }
    }

    public void PowerUp(){
        increaseSpeed.Play();
        StartCoroutine(IncreaseSpeed());
    }

    IEnumerator IncreaseSpeed(){
        float tmpSpeed = speed;
        speed *= 2;
        yield return new WaitForSeconds(10);
        speed = tmpSpeed;
    }

}
