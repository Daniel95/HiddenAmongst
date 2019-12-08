using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class ThiefPlayerThirdPerson : Mirror.NetworkBehaviour
{
    [SerializeField] private GameObject thiefCameraPrefab;
    [SerializeField] private Transform cameraPivot;

    private ThiefMovement thiefMovement; // A reference to the ThirdPersonCharacter on the object
    private Transform cameraTransform;                  // A reference to the main camera in the scenes transform
    private Vector3 cameraForward;             // The current forward direction of the camera
    private Vector3 move;
    private bool jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

    private void OnDestroy()
    {
        if(cameraTransform != null)
        {
            Destroy(cameraTransform.gameObject);
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        cameraTransform = Instantiate(thiefCameraPrefab).transform;
        cameraTransform.gameObject.SetActive(true);
        cameraTransform.GetComponent<ThirdPersonCamera>().target = cameraPivot;

        thiefMovement = GetComponent<ThiefMovement>();
    }

    private void Update()
    {
        if (!isLocalPlayer) { return; }

        if (!jump)
        {
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }

        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (cameraTransform != null)
        {
            // calculate camera relative direction to move:
            cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            move = v * cameraForward + h * cameraTransform.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            move = v * Vector3.forward + h * Vector3.right;
        }

        // pass all parameters to the character control script
        thiefMovement.Move(move, crouch, jump);
        jump = false;
    }
}
