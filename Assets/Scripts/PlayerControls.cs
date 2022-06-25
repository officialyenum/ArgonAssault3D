using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast rig moves up and down")] 
    [SerializeField] float moveSpeed = 10f;
    [Tooltip("How fast ship moves horizontally")] 
    [SerializeField] float xRange = 15f;
    [Tooltip("How fast ship moves vertically")] 
    [SerializeField] float yRange = 10f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;


    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;



    [Header("Player input based tuning")]
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;


    [Header("Player input control settings")]
    [SerializeField] InputAction movementAction;
    [SerializeField] InputAction fire;
    // Start is called before the first frame update
    float xMove, yMove;

    void OnEnable()
    {
        movementAction.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movementAction.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xMove = movementAction.ReadValue<Vector2>().x;
        yMove = movementAction.ReadValue<Vector2>().y;

        float xOffset = xMove * moveSpeed * Time.deltaTime;
        float yOffset = yMove * moveSpeed * Time.deltaTime;

        float newXpos = transform.localPosition.x + xOffset;
        float newYpos = transform.localPosition.y + yOffset;

        float clampedX = Mathf.Clamp(newXpos, -xRange, xRange);
        float clampedY = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);

        // Debug.Log(xMove);
        // Debug.Log(yMove);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yMove * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float rollDueToControl = xMove * controlRollFactor;
        float pitch = pitchDueToPosition + pitchDueToControl; //Rotate on XAxis
        float yaw = yawDueToPosition; //Rotate on YAxis
        float roll = rollDueToControl; //Rotate on ZAXis
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (fire.IsPressed())
        {
            SetLasersActivate(true);
            // Debug.Log("I'm shooting");
        }
        else
        {
            SetLasersActivate(false);
            // Debug.Log("Not Shooting");
        }
    }

    void SetLasersActivate(bool isActive)
    {
            foreach (GameObject laser in lasers)
            {
                var emissionModule = laser.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isActive;
            }
    }
}
