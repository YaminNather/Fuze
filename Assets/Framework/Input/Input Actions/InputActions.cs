// GENERATED AUTOMATICALLY FROM 'Assets/Framework/Input/Input Actions/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""DefActionMap"",
            ""id"": ""d1cc2b25-1be7-4022-a44f-a9af0661a995"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""cb892dc0-cf95-4820-9f0f-a8f777e06872"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClickPos"",
                    ""type"": ""Value"",
                    ""id"": ""459f3792-2a06-462f-974e-38fa1ff534b8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3d6025a2-f7e5-42c1-8eaa-ebdd6a1efa1e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3143409-e270-4ec7-9a3b-abd740517223"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92fcb4e0-9ed9-4795-89a7-90f190dd8aaf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e49fa72f-eb6a-4a91-b25a-d0cb728cc7bd"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DefActionMap
        m_DefActionMap = asset.FindActionMap("DefActionMap", throwIfNotFound: true);
        m_DefActionMap_Click = m_DefActionMap.FindAction("Click", throwIfNotFound: true);
        m_DefActionMap_ClickPos = m_DefActionMap.FindAction("ClickPos", throwIfNotFound: true);
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

    // DefActionMap
    private readonly InputActionMap m_DefActionMap;
    private IDefActionMapActions m_DefActionMapActionsCallbackInterface;
    private readonly InputAction m_DefActionMap_Click;
    private readonly InputAction m_DefActionMap_ClickPos;
    public struct DefActionMapActions
    {
        private @InputActions m_Wrapper;
        public DefActionMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_DefActionMap_Click;
        public InputAction @ClickPos => m_Wrapper.m_DefActionMap_ClickPos;
        public InputActionMap Get() { return m_Wrapper.m_DefActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IDefActionMapActions instance)
        {
            if (m_Wrapper.m_DefActionMapActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_DefActionMapActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_DefActionMapActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_DefActionMapActionsCallbackInterface.OnClick;
                @ClickPos.started -= m_Wrapper.m_DefActionMapActionsCallbackInterface.OnClickPos;
                @ClickPos.performed -= m_Wrapper.m_DefActionMapActionsCallbackInterface.OnClickPos;
                @ClickPos.canceled -= m_Wrapper.m_DefActionMapActionsCallbackInterface.OnClickPos;
            }
            m_Wrapper.m_DefActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ClickPos.started += instance.OnClickPos;
                @ClickPos.performed += instance.OnClickPos;
                @ClickPos.canceled += instance.OnClickPos;
            }
        }
    }
    public DefActionMapActions @DefActionMap => new DefActionMapActions(this);
    public interface IDefActionMapActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnClickPos(InputAction.CallbackContext context);
    }
}
