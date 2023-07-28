using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    // button configuration variables

    public KeyCode steerLeftButton;
    public KeyCode steerRightButton;

    // end of button configuration variables



    // variables for relative rotation of player to camera

    /*
    * The player ship should rotate relative to the camera when the player steers
    * (see F-Zero GX steering for reference).
    * There is a maximal rotation angle for both horizontal and vertical rotation.
    * If the player does not hold down any steer button 
    * the ship slowly rotates back to default rotation (0).
    * This happens at a probably different, most commonly slower, speed.
    */

    public float horizontalRelToCamRotationSpeed;
    public float maxHorizontalRelToCamRotation;
    public float horizontalRelToCamRotationGoBackSpeed;

    public float verticalRelToCamRotationSpeed;
    public float maxVerticalRelToCamRotation;
    public float verticalRelToCamRotationGoBackSpeed;

    // end of variables for relative rotation of player to camera



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerRotationRelativeToCamera();
    }

    /*
    * Updates the rotation of the player spaceship relative to the camera.
    * Note that Unity stores rotations as Quaternions (can be imagined as 4-dim. vectors of complex numbers, for simplicity) internally,
    * but displays them as Euler angles (3-dim. vectors of real numbers, degrees of rotation around the respective axes).
    */
    void UpdatePlayerRotationRelativeToCamera() {
        // if any steer button is held: rotate player ship,
        if(SteerRightButtonHeld()) {
            // compute new player rotation
            float newPlayerRotation = transform.rotation.eulerAngles.y + horizontalRelToCamRotationSpeed * Time.deltaTime;

            // clamp at certain threshold
            if(newPlayerRotation > maxHorizontalRelToCamRotation) {
                newPlayerRotation = maxHorizontalRelToCamRotation;
            }

            // update player rotation in transform component
            transform.rotation = Quaternion.Euler(0, newPlayerRotation, 0);
        }
        if(SteerLeftButtonHeld()) {
            // compute new player rotation
            float newPlayerRotation = transform.rotation.eulerAngles.y - horizontalRelToCamRotationSpeed * Time.deltaTime;

            // clamp at certain threshold
            if(newPlayerRotation < -maxHorizontalRelToCamRotation) {
                newPlayerRotation = -maxHorizontalRelToCamRotation;
            }

            // update player rotation in transform component
            transform.rotation = Quaternion.Euler(0, newPlayerRotation, 0);
        }

        // else: rotate player back to 0, 
        if(!SteerLeftButtonHeld() && !SteerRightButtonHeld()) {
            // retrieve current player y rotation for readability
            float playerRotation = transform.rotation.eulerAngles.y;

            // declaration with dummy init value
            float newPlayerRotation = 0;

            if(playerRotation > 0) {
                newPlayerRotation = playerRotation - horizontalRelToCamRotationGoBackSpeed * Time.deltaTime;

                // clamp at 0 if less
                if(newPlayerRotation < 0) {
                    newPlayerRotation = 0;
                }
            }
            if(playerRotation < 0) {
                newPlayerRotation = playerRotation + horizontalRelToCamRotationGoBackSpeed * Time.deltaTime;

                // clamp at 0 if less
                if(newPlayerRotation > 0) {
                    newPlayerRotation = 0;
                }
            }

            // update player rotation in the transform component
            transform.rotation = Quaternion.Euler(0, newPlayerRotation, 0);
        }
    }



    // player input methods

    /*
    * Boolean methods to capsulate detection of certain actions 
    * needing to be executed upon player input.
    * This makes it easy to change the button configuration freely, 
    * (especially when adding multiple alternative buttons)
    * without minimal necessity to change code.
    */

    public bool SteerLeftButtonHeld() {
        return Input.GetKey(steerLeftButton);
    }

    public bool SteerLeftButtonPressed() {
        return Input.GetKeyDown(steerLeftButton);
    }

    public bool SteerRightButtonHeld() {
        return Input.GetKey(steerRightButton);
    }

    public bool SteerRightButtonPressed() {
        return Input.GetKeyDown(steerRightButton);
    }

    // end of player input methods
}
