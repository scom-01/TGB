//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Scripts/Character/Player/Input/PlayerInputActions.inputactions
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
            ""name"": ""GamePlay"",
            ""id"": ""65a79c32-6023-4a67-bfbb-ac4f9873d158"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""340bcd39-51ba-4642-8dc8-d16fd87d4144"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""77de3ba5-221f-4ef0-9272-612ca69c3ed2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""5a6fa1af-c214-4172-a847-0b11cbcdaabf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""6990ccc3-08b3-4a66-b619-60a27835fe6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""63c5d98c-0c1f-4677-805c-b6b3d797ab7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""0424d634-4392-4de5-8503-6a949fd7575f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""b5e1f0d3-8a1c-4c72-929f-053003735d4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""62f09e07-e18f-45bf-9d3a-9e0cb0df973d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Change"",
                    ""type"": ""Button"",
                    ""id"": ""33cc7a68-30a5-4d89-8ae1-ca0c430395b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""c9536ce7-8a8e-4d61-91e3-9d2d49ac2761"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""5ef6d7c4-9f8e-4d95-a822-89ab03cfecbe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""2f43fc99-450b-49c0-a447-aa51acaa84b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""5d9a02df-459b-4940-bacd-1d8df81c5f9b"",
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
                    ""id"": ""9c70a508-17fb-427b-8f4b-8d1794ad65a5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fb81fb62-5ed7-425c-9f2b-67b1a72b29c1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""67cbfe48-4947-46c7-8a4e-8e9493793d62"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5a4daa0a-d7ec-4ffe-a9af-f6a4f64cda20"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e82263b5-9976-4a3e-8f4b-6c667a18b5be"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2239b71d-e5f5-4aaf-a576-01dc5beba898"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8b88acf-52b1-43d1-805c-ca293720c521"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89981c79-993f-478b-8ca2-2bbd464a8336"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0f069bf-50e4-432e-a803-5fc6ba97669e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""994e0bc5-3c8e-4344-a143-ad8910edece0"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d564be0-fd03-4c82-9427-5085594a5b46"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b73af24f-cd4b-4c73-999f-4fa44ec15d9e"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6c81528-58d7-4546-92cc-eb810926d95a"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""381151c3-f86c-48b7-975d-5b70041ec302"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7158b675-5dc8-4ca0-8cba-72f8034b2798"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""c3e760cb-3d6b-4dbd-a2ce-61849351d6cc"",
            ""actions"": [
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""b6792d25-2dfd-4bde-896d-9ac950f27bf8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""44e1b40c-d23c-4b89-ba4c-8ac8d9676769"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""a57f4db8-9671-4fee-be89-bd0560bd42fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55310409-d664-4513-a361-cc097e73ad5b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24739e07-aa29-4746-8a72-b97068f90252"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54a58f2c-6c36-4ea4-b96e-a31b6329990b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Movement = m_GamePlay.FindAction("Movement", throwIfNotFound: true);
        m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
        m_GamePlay_Grab = m_GamePlay.FindAction("Grab", throwIfNotFound: true);
        m_GamePlay_Dash = m_GamePlay.FindAction("Dash", throwIfNotFound: true);
        m_GamePlay_Skill1 = m_GamePlay.FindAction("Skill1", throwIfNotFound: true);
        m_GamePlay_Skill2 = m_GamePlay.FindAction("Skill2", throwIfNotFound: true);
        m_GamePlay_Primary = m_GamePlay.FindAction("Primary", throwIfNotFound: true);
        m_GamePlay_Secondary = m_GamePlay.FindAction("Secondary", throwIfNotFound: true);
        m_GamePlay_Change = m_GamePlay.FindAction("Change", throwIfNotFound: true);
        m_GamePlay_Tap = m_GamePlay.FindAction("Tap", throwIfNotFound: true);
        m_GamePlay_ESC = m_GamePlay.FindAction("ESC", throwIfNotFound: true);
        m_GamePlay_Interaction = m_GamePlay.FindAction("Interaction", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Interaction = m_UI.FindAction("Interaction", throwIfNotFound: true);
        m_UI_Tap = m_UI.FindAction("Tap", throwIfNotFound: true);
        m_UI_ESC = m_UI.FindAction("ESC", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Movement;
    private readonly InputAction m_GamePlay_Jump;
    private readonly InputAction m_GamePlay_Grab;
    private readonly InputAction m_GamePlay_Dash;
    private readonly InputAction m_GamePlay_Skill1;
    private readonly InputAction m_GamePlay_Skill2;
    private readonly InputAction m_GamePlay_Primary;
    private readonly InputAction m_GamePlay_Secondary;
    private readonly InputAction m_GamePlay_Change;
    private readonly InputAction m_GamePlay_Tap;
    private readonly InputAction m_GamePlay_ESC;
    private readonly InputAction m_GamePlay_Interaction;
    public struct GamePlayActions
    {
        private @PlayerInputActions m_Wrapper;
        public GamePlayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_GamePlay_Movement;
        public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
        public InputAction @Grab => m_Wrapper.m_GamePlay_Grab;
        public InputAction @Dash => m_Wrapper.m_GamePlay_Dash;
        public InputAction @Skill1 => m_Wrapper.m_GamePlay_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_GamePlay_Skill2;
        public InputAction @Primary => m_Wrapper.m_GamePlay_Primary;
        public InputAction @Secondary => m_Wrapper.m_GamePlay_Secondary;
        public InputAction @Change => m_Wrapper.m_GamePlay_Change;
        public InputAction @Tap => m_Wrapper.m_GamePlay_Tap;
        public InputAction @ESC => m_Wrapper.m_GamePlay_ESC;
        public InputAction @Interaction => m_Wrapper.m_GamePlay_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Grab.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGrab;
                @Dash.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDash;
                @Skill1.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSkill2;
                @Primary.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPrimary;
                @Secondary.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSecondary;
                @Secondary.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSecondary;
                @Secondary.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnSecondary;
                @Change.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChange;
                @Change.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChange;
                @Change.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChange;
                @Tap.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTap;
                @ESC.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnESC;
                @Interaction.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteraction;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
                @Secondary.started += instance.OnSecondary;
                @Secondary.performed += instance.OnSecondary;
                @Secondary.canceled += instance.OnSecondary;
                @Change.started += instance.OnChange;
                @Change.performed += instance.OnChange;
                @Change.canceled += instance.OnChange;
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Interaction;
    private readonly InputAction m_UI_Tap;
    private readonly InputAction m_UI_ESC;
    public struct UIActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interaction => m_Wrapper.m_UI_Interaction;
        public InputAction @Tap => m_Wrapper.m_UI_Tap;
        public InputAction @ESC => m_Wrapper.m_UI_ESC;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Interaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInteraction;
                @Tap.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTap;
                @ESC.started -= m_Wrapper.m_UIActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnESC;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnChange(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnInteraction(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
    }
}
