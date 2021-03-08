using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //outlets
     Rigidbody2D rigidbody;
     CapsuleCollider2D capsuleCollider;
     public Animator animator;
     public SpriteRenderer spriteRenderer;
     public Text coinsText;
     public AudioSource audioSource;
     public AudioClip collectCoinSound;
     public AudioClip jumpSound;
     public AudioClip levelCompleteSound;
     public Text countdownText;

    //state tracking
    public int jumpsLeft;
    public int timeRunning;
    public int numCoins;
    public bool isPaused;
    public bool isMuted;
    public static bool IsInputEnabled = true;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        numCoins = PlayerPrefs.GetInt("Coins");
        audioSource = GetComponent<AudioSource>();
        animator.SetInteger("Character", PlayerPrefs.GetInt("Character"));
        isPaused = false;
        Time.timeScale = 1f;
        StartCoroutine(Countdown(3));
    }
    IEnumerator Countdown(int seconds){
        int count = seconds;
        while (count > 0){
            PlayerController.IsInputEnabled = false;
            print("Starting in " + count + "...");
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countdownText.gameObject.SetActive(false);
        PlayerController.IsInputEnabled = true;
    }

    void FixedUpdate() {
        if(isPaused) {
            return;
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) {
            timeRunning++;
        }
        else {
            timeRunning = 0;
        }
    }

    void Update()
    {
        if(isPaused) {
            return;
        }
        if(PlayerController.IsInputEnabled)
        {
        //move right
        if(Input.GetKey(KeyCode.RightArrow)) {
            spriteRenderer.flipX = false;
            animator.SetBool("IsRunning", true);
            if (timeRunning > 30) {
                transform.position += new Vector3(0.05f, 0, 0);
            }
            else {
                transform.position += new Vector3(0.03f, 0, 0);
            }
        }
        //move left
        else if(Input.GetKey(KeyCode.LeftArrow)) {
            spriteRenderer.flipX = true;
            animator.SetBool("IsRunning", true);
            if (timeRunning > 30) {
                transform.position += new Vector3(-0.05f, 0, 0);
            }
            else {
                transform.position += new Vector3(-0.03f, 0, 0);
            }
        }
        //not moving
        else {
            animator.SetBool("IsRunning", false);
        }

        //jump
        if(Input.GetKeyDown(KeyCode.Space)) {
          if(jumpsLeft > 0) {
              jumpsLeft--;
              rigidbody.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
              audioSource.PlayOneShot(jumpSound);
          }
        }
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);

        //update UI
        coinsText.text = numCoins.ToString();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
          RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 1.7f);
          Debug.DrawRay(transform.position,-transform.up * 1.7f);

          for (int i=0; i<hits.Length; i++) {
              RaycastHit2D hit = hits[i];
              if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                  jumpsLeft = 1;
              }
          }
        }
    }

}
