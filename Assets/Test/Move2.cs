using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public const int MaxThrottle = 10;
    public const float SmoothMovement = 0.5f;
    public const float SmoothTurning = 2f;

    private float leftThrottleValue = 0f;
    private float rightThrottleValue = 0f;
    private Rigidbody tankRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        tankRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leftThrottleValue += Input.GetAxis("VerticalLeft") / 10;
        leftThrottleValue = (leftThrottleValue > MaxThrottle) ? MaxThrottle : leftThrottleValue;
        float roundedLeftThrottle = Mathf.Round(leftThrottleValue);
        if (Mathf.Abs(roundedLeftThrottle - leftThrottleValue) < 0.02f)
        {
            leftThrottleValue = roundedLeftThrottle;
        }
        else if (roundedLeftThrottle > leftThrottleValue)
        {
            leftThrottleValue += 0.01f;
        }
        else if (roundedLeftThrottle < leftThrottleValue)
        {
            leftThrottleValue -= 0.01f;
        }

        rightThrottleValue += Input.GetAxis("VerticalRight") / 10;
        rightThrottleValue = (rightThrottleValue > MaxThrottle) ? MaxThrottle : rightThrottleValue;
        float roundedRightThrottle = Mathf.Round(rightThrottleValue);
        if (Mathf.Abs(roundedRightThrottle - rightThrottleValue) < 0.02f)
        {
            rightThrottleValue = roundedRightThrottle;
        }
        else if (roundedRightThrottle > rightThrottleValue)
        {
            rightThrottleValue += 0.01f;
        }
        else if (roundedRightThrottle < rightThrottleValue)
        {
            rightThrottleValue -= 0.01f;
        }

        // Move the tank.
        Vector3 movement = transform.forward * ((leftThrottleValue + rightThrottleValue) / 2f) * SmoothMovement * Time.deltaTime;
        tankRigidbody.MovePosition(tankRigidbody.position + movement);

        // Turn the tank.
        float turn = (leftThrottleValue - rightThrottleValue) * SmoothTurning * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        tankRigidbody.MoveRotation(tankRigidbody.rotation * turnRotation);
    }
}