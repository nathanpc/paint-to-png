using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;

    private bool _isActivated;
    private InputAction thumbstick;
    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;
        var activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportationActivate;
        var cancel = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        thumbstick = actionAsset.FindActionMap("XRI RightHand").FindAction("Move");
        thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    { 
        if (!_isActivated) return;
        
        if (thumbstick.triggered) return;

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit Hit))
        {
            rayInteractor.enabled = false; 
            _isActivated = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = Hit.point,
            //destinationRotation = Hit.transform.forward,
        };
        provider.QueueTeleportRequest(request);
        
    }
    void OnTeleportationActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActivated = true;
    }
    void OnTeleportationCancel()
    {
        rayInteractor.enabled = false;
        _isActivated = false;
    }
}
