using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    public float move_sp = 500f;
    public float smoothVal = 0.3f;
    Vector3 moveVec;
    Vector3 moveVecWg;

    [Header("Rotation")]
    public float mouse_sp = 5f;
    float xRotation, yRotation;
    private readonly float AngleLimit = 60f;

    private Camera cam;


    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main;
    }

    void Update(){
        Rotate();
        MoveInput();
    }

    void FixedUpdate(){
        Move(); 
    }

    void Rotate() {   
        yRotation = Input.GetAxis("Mouse X") * mouse_sp;
        xRotation += -Input.GetAxis("Mouse Y") * mouse_sp;
        xRotation = Mathf.Clamp(xRotation, -AngleLimit, AngleLimit);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, yRotation, 0);
        }

    void MoveInput() {
        moveVec = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal"));
        }


    void Move(){
        moveVec = moveVec.normalized * move_sp  * Time.deltaTime; 
        moveVecWg = new Vector3(moveVec.x, rb.velocity.y, moveVec.z);
        float smoothness;
        if (moveVec == Vector3.zero){
            smoothness= smoothVal*5;
        } else{ smoothness = smoothVal;}
        rb.velocity = Vector3.Lerp(rb.velocity, moveVecWg, smoothness) ;
    }


}