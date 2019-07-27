using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidBody2D;
    public float playerVelocity = 10;
    public Transform jumpPosition;
    public float raycastRadius;
    public LayerMask mask;
    bool canJump;

    public static int nextInt = 2;
    public string nextScene = "";
    public GameObject spawn;
    public GameObject Player;
    public GameObject deadSlime;
    //public GameObject deadSlimeCrakled;
    public bool isWait = false;
    public float timerWait = 1;
    public int nbBloc = 3;
    public int nbBlocCrakled = 3;
    private bool crakledIsSelected;
    public Text textVar1;
    //public Text textVar2;
    public bool canPlay = true;

    public int numLeaves;
    public Animator animator;
    private AudioSource audiosource;
    public AudioClip clipmarcher;
    public AudioClip clipsauter;
    public AudioClip clipatterire;
    public AudioClip clipmort;
    public AudioClip clipfreeze;
    void Start () {
        audiosource=GetComponent<AudioSource>();
        rigidBody2D = GetComponent<Rigidbody2D> ();
        spawn = GameObject.FindWithTag ("spawn");
        Player = GameObject.FindWithTag("Player");

    }

    void Update () {
        textVar1.text = (" " + nbBloc.ToString () + " ");

        if (!crakledIsSelected) {
            textVar1.color = Color.white;
           // textVar2.color = Color.white;
        }
        //if (crakledIsSelected) {
            textVar1.color = Color.white;
            //textVar2.color = Color.red;
        //}

        if (!isWait && canPlay) {
            float horizontalInput = Input.GetAxis("Horizontal");
            bool nowCanJump = Physics2D.OverlapCircle(jumpPosition.position, raycastRadius, mask);

            if(nowCanJump && !canJump)
            {
                audiosource.clip = clipatterire;
                audiosource.Play();
            }
            canJump = nowCanJump;
           
            Vector2 velocity = new Vector2(horizontalInput * playerVelocity, rigidBody2D.velocity.y);
            if (!canJump) {
                velocity.x = velocity.x / Mathf.Sqrt(2);
            }
            rigidBody2D.velocity = velocity;

            animator.SetFloat("velocityY", velocity.y);

            if (velocity.x != 0) 
            {
                animator.SetBool("isMoving", true);
                if(!audiosource.isPlaying)
                {
                    audiosource.clip = clipmarcher;
                    audiosource.Play();
                }
            }
            else
            {
                animator.SetBool("isMoving", false);
                if(audiosource.clip==clipmarcher)
                {
                    audiosource.Stop();
                }
            }
            Vector3 scale = transform.localScale;
            if (velocity.x > 0) {
                scale.x = Mathf.Abs (scale.x);
            } else if (velocity.x < 0) {
                scale.x = -Mathf.Abs (scale.x);
            }
            transform.localScale = scale;

            if (canJump && Input.GetButtonDown ("Jump")) {
                animator.SetTrigger("isJumping");
                audiosource.clip = clipsauter;
                audiosource.Play();
            }

            if (Input.GetButtonDown ("Fire2")) {
              
                Player = GameObject.FindWithTag ("Player");
                spawn = GameObject.FindWithTag ("spawn");

               // if (crakledIsSelected) {
                    //if (nbBlocCrakled > 0) {
                       // animator.SetTrigger("Freeze");
                        //Instantiate (deadSlimeCrakled, Player.transform.position, Quaternion.identity);

                       // nbBlocCrakled--;
                    // }
                    if (nbBloc > 0)
                    {
                        animator.SetTrigger("Freeze");
                        Instantiate(deadSlime, Player.transform.position, Quaternion.identity);
                        nbBloc--;
                    audiosource.clip = clipfreeze;
                    audiosource.Play();
                }
            }
        }

        //if (Input.GetButtonDown ("Submit")) {
         //   crakledIsSelected = !crakledIsSelected;
        //}
        if (isWait) {
            rigidBody2D.gravityScale = 0;
            rigidBody2D.velocity = new Vector2();
            timerWait -= Time.deltaTime;
            if (timerWait <= 0) {
                rigidBody2D.velocity = new Vector2 ();
                rigidBody2D.gravityScale = 0;
                Player.transform.position = spawn.transform.position;
                isWait = false;
                rigidBody2D.gravityScale = 5;
                timerWait = 1;
            }
        }
        if (Input.GetButtonDown ("Fire3") && !isWait) {
            Player = GameObject.FindWithTag ("Player");
            spawn = GameObject.FindWithTag ("spawn");

            Player.transform.position = spawn.transform.position;
        }
        if (Input.GetButtonDown ("Cancel")) {
            nextInt--;
            nextScene = nextInt.ToString ();
            SceneManager.LoadScene (nextScene);
            nextInt++;
            nextScene = nextInt.ToString ();
        }
    }
    void OnTriggerEnter2D (Collider2D collision) {
        if (collision.tag == "Door") {
            nextScene = nextInt.ToString ();
            SceneManager.LoadScene (nextScene);
            nextInt++;
            nextScene = nextInt.ToString ();
        }

        if (collision.tag == "Spike") {
            isWait = true;
            canPlay = false;
            animator.SetTrigger("Death");
                audiosource.clip = clipmort;
                audiosource.Play();
                animator.SetFloat("velocityY", 0);
            animator.SetBool("isMoving", false);
        }
    }

    void OnCollisionEnter2D (Collision2D col) {
   
            if (col.gameObject.tag == "deadSlimeCrakled") {
            Destroy (col.gameObject, 3);
        }
    }

 }
