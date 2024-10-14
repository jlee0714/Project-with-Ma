using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlJumpSprint : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    public float move_sp = 500f;
    /*public float Orin_move_sp = 500;
    public Transform GroundCheckPos;
    public float jump_F = 10f;
    public float sprintValue = 3f;
    [SerializeField]
    private bool grounded = false;
    */
    Vector3 moveVec;
    Vector3 moveVecWg;
    //private bool sprint;

    [Header("Rotation")]
    public float mouse_sp = 5f;
    float xRotation, yRotation;
    private readonly float AngleLimit = 45f;

    private Camera cam;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main;
    }

    void Update()
    {
        //grounded = Physics.CheckSphere(GroundCheckPos.position, .1f, LayerMask.GetMask("Ground"));
        //if (grounded && Input.GetKeyDown(KeyCode.Space)) Jump();
        Rotate();
        MoveInput();
    }

    void FixedUpdate()
    {
        Move(); 
    }

    void Rotate()
    {   
        yRotation = Input.GetAxis("Mouse X") * mouse_sp;
        xRotation += -Input.GetAxis("Mouse Y") * mouse_sp;
        xRotation = Mathf.Clamp(xRotation, -AngleLimit, AngleLimit);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, yRotation, 0);
    }

    void MoveInput()
    {
        //if (Input.GetKey(KeyCode.LeftShift)) sprint = true ;
        moveVec = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal"));
    }


    void Move()
    {
        /*if (sprint == true) {
            move_sp = Orin_move_sp * sprintValue;
            sprint = false;
        } else {
            move_sp = Orin_move_sp; ;
            sprint = false;
        }*/
        /*
        if (!grounded){
            moveVec = moveVec.normalized * move_sp * 2 * Time.deltaTime; 
            Vector3 airMove = new(moveVec.x, 0f, moveVec.z);
                if (Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z) < move_sp * Time.deltaTime)
                {
                    rb.AddForce(airMove, ForceMode.Acceleration);
                }
            return;
        }*/
        moveVec = moveVec.normalized * move_sp  * Time.deltaTime; 
        moveVecWg = new Vector3(moveVec.x, rb.velocity.y, moveVec.z);
        rb.velocity = moveVecWg;
    }

    /*
    void Jump()
    {
        rb.AddForce(Vector3.up * jump_F, ForceMode.Impulse);
    }
    */


}