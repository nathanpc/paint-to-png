using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the spray behaviour and aspects.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class SprayController : MonoBehaviour
{
	public InputAction sprayAction;

    void Awake()
	{
		// Setup the spray action.
		sprayAction.performed += OnSpray;
		sprayAction.Enable();
	}

    // Start is called before the first frame update
    void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	/// <summary>
	/// Actually performs the spraying action.
	/// </summary>
	/// <param name="context">Context of the input action.</param>
    public void OnSpray(InputAction.CallbackContext context)
	{
		Debug.Log("Spraying");
	}
}
