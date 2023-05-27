using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] Vector2 limits;
    private Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        limits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        float movementX = 0f;
        float movementY = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
            movementY = 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            movementY = -1f;
        if (Input.GetKey(KeyCode.RightArrow))
            movementX = 1f;
        if (Input.GetKey(KeyCode.LeftArrow))
            movementX = -1f;
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            movementX = 0f;

        if (movementX == 0f)
        {
            playerAnim.SetBool("IsGoingRight", false);
            playerAnim.SetBool("IsGoingLeft", false);
        }
        else if(movementX > 0f)
        {
            playerAnim.SetBool("IsGoingRight", true);
            playerAnim.SetBool("IsGoingLeft", false);
        }
        else
        {
            playerAnim.SetBool("IsGoingRight", false);
            playerAnim.SetBool("IsGoingLeft", true);
        }

        moveDirection = new Vector3(movementX, movementY).normalized;
    }

    //*
    // Clamp player to screen
    void LateUpdate()
    {
        Vector3 objPos = transform.position;
        objPos.x = Mathf.Clamp(objPos.x, -limits.x+1.35f, limits.x-1.35f);
        objPos.y = Mathf.Clamp(objPos.y, limits.y-19.8f, limits.y-0.6f);
        transform.position = objPos;
    }
    //*/

    private void FixedUpdate()
    {
        // Adds velocity to rigidbody decide how to move
        //if (!isOnMenu)
        rb.velocity = moveDirection * moveSpeed;
    }
}
