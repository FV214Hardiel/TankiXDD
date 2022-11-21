using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    
    public Cinemachine.CinemachineFreeLook ccamera;

    public List<object> orbitsBase;
    public object obj1;
    public Vector3 minOrbits;
    public Vector3 maxOrbits;

    float multiplier;
    float increment;

    PlayerInputActions inputActions;
    // Start is called before the first frame update
    void Start()
    {
        inputActions = new();

        inputActions.Enable();
        inputActions.Look.Zoom.performed += ChangeOrbits;

        multiplier = 0;
        increment = 0.1f;
        
        for (int i = 0; i <= 2; i++)
        {
            ccamera.m_Orbits[i].m_Radius = minOrbits[i];
        }
        
        
    }

    private void OnDisable()
    {
        inputActions.Look.Zoom.performed -= ChangeOrbits;
    }

    public void ChangeOrbits(InputAction.CallbackContext context)
    {
        float change = context.ReadValue<Vector2>()[1];        
        
        multiplier -= increment * change;
        multiplier = Mathf.Clamp01(multiplier);

        for (int i = 0; i <= 2; i++)
        {
            ccamera.m_Orbits[i].m_Radius = Mathf.Lerp(minOrbits[i], maxOrbits[i], multiplier);

        }
    }
}
