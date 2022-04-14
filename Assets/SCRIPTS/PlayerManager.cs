using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Floor
{
    Bottom = 0,
    Right,
    Top,
    Left,
};


public class PlayerManager : MonoBehaviour
{
    private float speed = 15f;
    private bool changingState = false;
    public Floor CurrentFloor;
    private float flipRate = 0.3f;


    //tunel borders
    private float UPPER_LIMIT = 6.5F;
    private float DOWN_LIMIT = 1.5F;
    private float RIGHT_LIMIT = 3.5F;
    private float LEFT_LIMIT = -3.5F;

    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    private bool cancellingGrounded;
    private float maxSlopeAngle = 35f;
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private CameraController CC;
    private Rigidbody rb;
    public GameObject cam;

    void Start()
    {
        changingState = false;
        CurrentFloor = Floor.Bottom;
        CC = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody>();
        cam =  GameObject.FindGameObjectWithTag("MainCamera");

    }

    void Update()
    {
        Movement();

    }

    private void Movement(){

        //sets tunnel borders and locks movement
        Vector3 pos = gameObject.transform.position;
        if (pos.x < LEFT_LIMIT) transform.Translate(LEFT_LIMIT - pos.x, 0.0f, 0.0f, Space.World);
        if (pos.x > RIGHT_LIMIT) transform.Translate(RIGHT_LIMIT - pos.x, 0.0f, 0.0f, Space.World);
        if(pos.y < DOWN_LIMIT) transform.Translate(0.0f, DOWN_LIMIT - pos.y, 0.0f, Space.World);
        if(pos.y > UPPER_LIMIT) transform.Translate(0.0f, UPPER_LIMIT - pos.y, 0.0f, Space.World);


        //reads input and moves player
        if (Input.GetAxisRaw("Horizontal") == -1) //left
        {
            transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetAxisRaw("Horizontal") == 1) //right
        {
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
        }

        if(Input.GetAxisRaw("Vertical") == 0)
        {
            changingState = false;
        }

        if (Input.GetAxisRaw("Vertical") == 1 && !changingState)
        {
            changingState = true;
            Vector3 target;
            switch (CurrentFloor)
            {
                case Floor.Bottom:
                    CurrentFloor = Floor.Top;
                    CC.currentFloor = CurrentFloor;
                    target = transform.position;
                    target.y = UPPER_LIMIT; //for player
                    CC.unchild(); //to prevent the camera rotation
                    gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
                    CC.child(gameObject.transform);
                    StartCoroutine(SmoothFlipCoroutine(target, 5f, flipRate));

                    target.y = 2.25f; //for camera
                    CC.SmoothFlip(target, 5f, flipRate);

                    break;

                case Floor.Top:
                    CurrentFloor = Floor.Bottom;
                    CC.currentFloor = CurrentFloor;
                    target = transform.position;
                    target.y = DOWN_LIMIT; //for player
                    CC.unchild();
                    gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
                    CC.child(gameObject.transform);
                    StartCoroutine(SmoothFlipCoroutine(target, 5f, flipRate));

                    target.y = 1.5f; //for camera
                    CC.SmoothFlip(target, 5f, flipRate);

                    break;


                case Floor.Right:
                    CurrentFloor = Floor.Left;
                    CC.currentFloor = CurrentFloor;
                    target = transform.position;
                    target.x = LEFT_LIMIT;
                    CC.unchild();
                    gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
                    CC.child(gameObject.transform);
                    StartCoroutine(SmoothFlipCoroutine(target, 7f, flipRate));

                    target.x = 0.0f;
                    CC.SmoothFlip(target, 7f, flipRate);

                    break;

                case Floor.Left:
                    CurrentFloor = Floor.Right;
                    CC.currentFloor = CurrentFloor;
                    target = transform.position;
                    target.x = RIGHT_LIMIT;
                    CC.unchild();
                    gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
                    CC.child(gameObject.transform);
                    StartCoroutine(SmoothFlipCoroutine(target, 7f, flipRate));

                    target.x = 0.0f;
                    CC.SmoothFlip(target, 7f, flipRate);

                    break;

                default:
                    break;
            }
        }
    }

    IEnumerator SmoothFlipCoroutine(Vector3 target, float distance, float rate)
    {
        float currentDistance = 0;

        while (Mathf.Abs(distance) > Mathf.Abs(currentDistance))
        {
            transform.position = Vector3.MoveTowards(transform.position, target, rate);
            currentDistance += rate;
            yield return null;
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other) {
        if (!changingState)
        {
            if(other.CompareTag("RightFloor") && CurrentFloor != Floor.Right ){
                if(CurrentFloor == Floor.Bottom){
                    transform.Rotate(0f, 0f, 90f);
                }
                else {
                    transform.Rotate(0f, 0f, -90f);
                }
                CurrentFloor = Floor.Right;
                CC.SmoothRotate(CurrentFloor);
            }

            else if(other.CompareTag("LeftFloor") && CurrentFloor != Floor.Left ){
                if(CurrentFloor == Floor.Top){
                    transform.Rotate(0f, 0f, 90f);
                }
                else {
                    transform.Rotate(0f, 0f, -90f);
                }
                CurrentFloor = Floor.Left;
                CC.SmoothRotate(CurrentFloor);
            }

            else if(other.CompareTag("TopFloor") && CurrentFloor != Floor.Top ){
                if(CurrentFloor == Floor.Right){
                    transform.Rotate(0f, 0f, 90f);
                }
                else{
                    transform.Rotate(0f, 0f, -90f);
                }
                CurrentFloor = Floor.Top;
                CC.SmoothRotate(CurrentFloor);
            }

            else if(other.CompareTag("BottomFloor") && CurrentFloor != Floor.Bottom ){
                if(CurrentFloor == Floor.Left){
                    transform.Rotate(0f, 0f, 90f);
                }
                else{
                    transform.Rotate(0f, 0f, -90f);
                }
                CurrentFloor = Floor.Bottom;
                CC.SmoothRotate(CurrentFloor);
            }
        }
    }

}
