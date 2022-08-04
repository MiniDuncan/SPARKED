using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed;
    public float jumpForce, gravityScale;
    private float yStore;

    public CharacterController charCon;

    private CameraController cam;

    private Vector3 moveAmount;

    public float rotateSpeed = 10f;

    public Animator anim;

    public GameObject jumpParticle, landingParticle;
    private bool lastGrounded;

    public float bounceForce;

    [HideInInspector]
    public bool stopMoving;


    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();

        lastGrounded = true;

        charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
    }

    private void FixedUpdate()
    {
        if (!charCon.isGrounded)
        {
            moveAmount.y = moveAmount.y + (Physics.gravity.y * gravityScale * Time.fixedDeltaTime);
        } else
        {
            moveAmount.y = Physics.gravity.y * gravityScale * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f && !LevelManager.instance.levelComplete && !stopMoving)
        {

            yStore = moveAmount.y;

            moveAmount = (cam.transform.forward * Input.GetAxisRaw("Vertical")) + (cam.transform.right * Input.GetAxisRaw("Horizontal"));
            moveAmount.y = 0f;
            moveAmount = moveAmount.normalized;

            if (moveAmount.magnitude > .1f)
            {
                if (moveAmount != Vector3.zero)
                {
                    Quaternion newRot = Quaternion.LookRotation(moveAmount);

                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
                }
            }

            moveAmount.y = yStore;

            if (charCon.isGrounded)
            {
                jumpParticle.SetActive(false);

                if (!lastGrounded)
                {
                    landingParticle.SetActive(true);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    moveAmount.y = jumpForce;

                    jumpParticle.SetActive(true);

                    AudioManager.instance.PlaySFXPitched(11);
                }
            }

            lastGrounded = charCon.isGrounded;

            charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);


            float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;

            anim.SetFloat("speed", moveVel);
            anim.SetBool("isGrounded", charCon.isGrounded);
            anim.SetFloat("yVel", moveAmount.y);
        }


    }

    public void Bounce()
    {
        moveAmount.y = bounceForce;

        charCon.Move(Vector3.up * bounceForce * Time.deltaTime);
    }
}
