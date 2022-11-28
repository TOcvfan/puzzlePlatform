using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed, mouseSensitivity;

    //CharacterControllers are a component Unity uses to do a handful of functions
    [SerializeField]
    private CharacterController charControl;

    //Transforms determine an objects position, rotation and scale.
    //The variable also comes with different functions we can call if we need them.
    [SerializeField]
    private Transform mainCamera;

    private float axisVertical;
    private float axisHorizontal;
    private float mouseInputX;
    private float mouseInputY;

    [SerializeField]
    private float jumpHeight, fallSpeed;
    
    //Vector3 are 3 numbers used to represent position, rotation and scale, Vector3(1, 1 ,1).
    //In this case we need moveDir to represent a direction for the player to move
    private Vector3 moveDir;

    private float xRotation;

    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {

        //Getting the players Transform
        myTransform = transform;
        
        //Locking the Cursor so it won't wander
        Cursor.lockState = CursorLockMode.Locked;

        //Getting the CharacterController on our player
        charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Resetting moveDir so our player won't continuously move forever
        moveDir = new Vector3(0, moveDir.y, 0);

        /*Saving mouse movement in easy to call variables. GetAxisRaw gets the value -1, 0 or 1
         * Depending on whether or not an input has been detected example: left = -1, none = 0, right = 1
         * We use a variable (mouseSensitivity) to modify this value until it feels right in Unity
         * Time.deltaTime is a function in unity that gets the time in between frames, and are used to make certain
         * That Movement isn't different depending on the framerate */
        mouseInputX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseInputY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        /* Reverting mouseInput so it fits with gameplay, and clamp the rotation, so we can't
         * Look more than 90 degrees up or down */
        xRotation -= mouseInputY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        /* Here we rotate the Camera up and down and the player left and right.
         * We don't want the player object to rotate up and down, but we do want him to turn left and right
         * So that we can make him walk forward easier */
        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        myTransform.Rotate(Vector3.up * mouseInputX);

        //Horizontal and Vertical are mapped through Unity to 'A' and 'D' but also the analogue stick on a controller
        //so we save these values. Since we don't use GetAxisRaw here but GetAxis, it follows the same rules, but
        //can be any value inbetween -1 and 1, with 0 being no input.
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");

        //In case the player uses a controller, we make certain do have a small deadzone where the character
        //wont move. That way if the player has a bad controller with a bit of drift, it shouldn't affect the game.
        if (axisVertical > .1f || axisVertical < -.1f)
            moveDir += myTransform.forward * axisVertical;
        
        if (axisHorizontal > .1f || axisHorizontal < -.1f)
            moveDir += myTransform.right * axisHorizontal;
        
        //As with the mouse we modify the speed, with a value we can set in Unity
        moveDir.x *= moveSpeed * Time.deltaTime;
        moveDir.z *= moveSpeed * Time.deltaTime;

        //Move is a function for the CharacterController component, that makes the character move in the direction given.
        charControl.Move(moveDir);

        //We now want our player to be able to jump, so the first thing we do is check if he is on the ground
        if (charControl.isGrounded)
        {
            //If he is, then we want to check if the player is clicking the jump button
            if (Input.GetButtonDown("Jump"))
                //If he does then we set the movedirection upwards to a positive value
                moveDir.y = jumpHeight;
        }
        //If he isn't grounded
        else
            //Then we want to check if he is falling with the top speed, defined in the if-statement
            if (moveDir.y > -1)
            //If he isn't falling at top speed, then we want to increase his speed downwards, until he is
                moveDir.y -= fallSpeed * Time.deltaTime;
    }
}
