//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerTankControl"",
            ""id"": ""bc3800f6-a34e-45ae-abbe-011b2a439d88"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Value"",
                    ""id"": ""4412e1be-975d-4396-b96f-7e7f8dd035bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""fd979ce2-078b-4c48-b907-4e2db2a28816"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""7bd4280e-f75f-47b0-bae7-ea75ec4f53bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b0f01ff0-a53f-44dd-b171-e9c821a5d8ce"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""158a6028-0db4-4940-8b12-b124208a0dfb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bab77c79-803d-49d8-90ac-e2cb2853c0d8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ba3017a2-ab15-4050-ba1b-25e61f4a4152"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""46243d84-7869-4de0-9f7f-833b4c4210bc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6ee35adc-1dd4-44a1-a4c7-4d39ad8a491e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6ea8abe2-e74c-4221-964f-70337263a97c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PauseMenu"",
            ""id"": ""24e11c60-9afc-409e-9bf1-097f1c946ff6"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""ed550914-bf7b-4a4b-85db-530d976edc5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ExitPause"",
                    ""type"": ""Button"",
                    ""id"": ""b62c5aa9-c541-4fc1-b89d-337afc720ddd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bd5e00bc-695b-4f3a-9fe6-059a233c0a85"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdc70067-6924-481d-b2a1-67c5bb3c9c40"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitPause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69be52b3-ff94-421a-af05-c3294370e6e4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitPause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerTankControl
        m_PlayerTankControl = asset.FindActionMap("PlayerTankControl", throwIfNotFound: true);
        m_PlayerTankControl_Fire = m_PlayerTankControl.FindAction("Fire", throwIfNotFound: true);
        m_PlayerTankControl_Movement = m_PlayerTankControl.FindAction("Movement", throwIfNotFound: true);
        m_PlayerTankControl_Pause = m_PlayerTankControl.FindAction("Pause", throwIfNotFound: true);
        // PauseMenu
        m_PauseMenu = asset.FindActionMap("PauseMenu", throwIfNotFound: true);
        m_PauseMenu_Newaction = m_PauseMenu.FindAction("New action", throwIfNotFound: true);
        m_PauseMenu_ExitPause = m_PauseMenu.FindAction("ExitPause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerTankControl
    private readonly InputActionMap m_PlayerTankControl;
    private IPlayerTankControlActions m_PlayerTankControlActionsCallbackInterface;
    private readonly InputAction m_PlayerTankControl_Fire;
    private readonly InputAction m_PlayerTankControl_Movement;
    private readonly InputAction m_PlayerTankControl_Pause;
    public struct PlayerTankControlActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerTankControlActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_PlayerTankControl_Fire;
        public InputAction @Movement => m_Wrapper.m_PlayerTankControl_Movement;
        public InputAction @Pause => m_Wrapper.m_PlayerTankControl_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerTankControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerTankControlActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerTankControlActions instance)
        {
            if (m_Wrapper.m_PlayerTankControlActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnFire;
                @Movement.started -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnMovement;
                @Pause.started -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerTankControlActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerTankControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerTankControlActions @PlayerTankControl => new PlayerTankControlActions(this);

    // PauseMenu
    private readonly InputActionMap m_PauseMenu;
    private IPauseMenuActions m_PauseMenuActionsCallbackInterface;
    private readonly InputAction m_PauseMenu_Newaction;
    private readonly InputAction m_PauseMenu_ExitPause;
    public struct PauseMenuActions
    {
        private @PlayerInputActions m_Wrapper;
        public PauseMenuActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_PauseMenu_Newaction;
        public InputAction @ExitPause => m_Wrapper.m_PauseMenu_ExitPause;
        public InputActionMap Get() { return m_Wrapper.m_PauseMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseMenuActions set) { return set.Get(); }
        public void SetCallbacks(IPauseMenuActions instance)
        {
            if (m_Wrapper.m_PauseMenuActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnNewaction;
                @ExitPause.started -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnExitPause;
                @ExitPause.performed -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnExitPause;
                @ExitPause.canceled -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnExitPause;
            }
            m_Wrapper.m_PauseMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @ExitPause.started += instance.OnExitPause;
                @ExitPause.performed += instance.OnExitPause;
                @ExitPause.canceled += instance.OnExitPause;
            }
        }
    }
    public PauseMenuActions @PauseMenu => new PauseMenuActions(this);
    public interface IPlayerTankControlActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IPauseMenuActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnExitPause(InputAction.CallbackContext context);
    }
}
