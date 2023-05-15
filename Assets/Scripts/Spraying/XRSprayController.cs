using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSprayController : XRGrabInteractable
{
    public SprayController sprayController;

    // Start is called before the first frame update
    void Start()
    {
		activated.AddListener(OnSprayActivated);
        deactivated.AddListener(OnSprayDeactivated);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSprayActivated(ActivateEventArgs args)
	{
        sprayController.OnSprayPerformed(
            new UnityEngine.InputSystem.InputAction.CallbackContext());
    }

    void OnSprayDeactivated(DeactivateEventArgs args)
    {
        sprayController.OnSprayCanceled(
            new UnityEngine.InputSystem.InputAction.CallbackContext());
    }
}
