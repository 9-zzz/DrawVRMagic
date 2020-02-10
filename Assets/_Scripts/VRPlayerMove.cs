using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerMove: MonoBehaviour
{
    public Vector3 moveDirection;
    public float moveSpeed = 20.0f;
    public float yMove = 0.0f;
    public float yMoveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.JoystickButton0))
            yMove = -yMoveSpeed;
        else if (Input.GetKey(KeyCode.JoystickButton1) == true)
            yMove = yMoveSpeed;
        else
            yMove = 0.0f;

        // horiz, y, forw
        moveDirection = new Vector3(Input.GetAxis("Axis 1"), yMove, -Input.GetAxis("Axis 2"));
        moveDirection *= moveSpeed;

        transform.Translate(moveDirection * Time.deltaTime);
    }
}
