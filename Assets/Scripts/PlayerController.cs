using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            return _instance;
        }
    }

    // Public Variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private LayerMask whatIsHazard;

    [Header("UI objects")]
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private Text winGameText;

    // Private Variables
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isClimbingLadder = false;
    private bool isFacingRight = true;
    private bool isHurt = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Fixed update is called once per frame
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        isGrounded = GroundCheck();
        isClimbingLadder = LadderCheck();
        isHurt = HazardCheck();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Jump code goes here
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
            //GetComponent<AudioSource>().Play();
        }

        // Climbing ladder code
        if (isClimbingLadder)
        {
            rBody.velocity = new Vector2(horiz * speed, vert * speed);
        }

        // Player hurt code 
        if (isHurt)
        {
            LoseGame();
        }

        rBody.velocity = new Vector2(horiz * speed, (rBody.velocity.y));

        // Check if the sprite needs to be flipped
        if((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }

        // Communicate with the animator
        anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("ySpeed", (rBody.velocity.y));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isClimbingLadder", isClimbingLadder);
        anim.SetBool("isHurt", isHurt);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }

    private bool LadderCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsLadder);
    }

    private bool HazardCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsHazard);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

    private void WinGame()
    {
        speed = 0;
        jumpForce = 0;
        winGameText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    private void LoseGame()
    {
        speed = 0;
        jumpForce = 0;
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    // Bouncing Platform Methods
    public void IncreaseJumpForce()
    {
        jumpForce *= 2;
    }

    public void DecreaseJumpForce()
    {
        jumpForce /= 2;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            rBody.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            rBody.gravityScale = 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingObject"))
        {
            transform.parent = other.transform;
        }


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingObject"))
        {
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("Hazard"))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndDoor"))
        {
            WinGame();
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Level 3");
            }
        }
    }
}
