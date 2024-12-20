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
	[Header("Spray Behaviour")]
    public int brushDiameter = 20;
	public Color sprayColor = new Color(1, 1, 1, 1);

	[Header("Internal Stuff")]
    public float sprayDistance = 2.0f;
	public string sprayableLayer = "Sprayable";
	public InputAction sprayAction;

	private LayerMask sprayableLayerMask;
	private bool isSpraying = false;
	private ParticleSystem[] particleSystems = null;

	void Awake()
	{
		// Setup the spray action.
		sprayAction.performed += OnSprayPerformed;
		sprayAction.canceled += OnSprayCanceled;
		sprayAction.Enable();

		// Change the color of the particle systems.
		particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystems)
		{
			ParticleSystem.MainModule psMain = particleSystem.main;
			psMain.startColor = sprayColor;
        }
	}

	// Start is called before the first frame update
	void Start()
	{
		sprayableLayerMask = LayerMask.GetMask(sprayableLayer);
		if (sprayableLayerMask == -1)
		{
			throw new System.Exception("Sprayable layer mask wasn't found");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isSpraying)
		{
			RaycastHit hit;
			Debug.DrawRay(transform.position,
				transform.TransformDirection(Vector3.forward) * sprayDistance,
				Color.green);

			if (Physics.Raycast(transform.position,
					transform.TransformDirection(Vector3.forward), out hit,
					sprayDistance, sprayableLayerMask))
			{
				Debug.DrawRay(transform.position,
					transform.TransformDirection(Vector3.forward) * hit.distance,
					Color.red);
				hit.collider.GetComponent<SprayableSurface>()?.PaintSurface(
					hit, sprayColor, brushDiameter);
			}
		}
	}

	/// <summary>
	/// Actually performs the spraying action.
	/// </summary>
	/// <param name="context">Context of the input action.</param>
	public void OnSprayPerformed(InputAction.CallbackContext context)
	{
		Debug.Log("Started spraying");
		isSpraying = true;
		particleSystems[0].Play();
	}

	/// <summary>
	/// Stops the spraying action.
	/// </summary>
	/// <param name="context">Context of the input action.</param>
	public void OnSprayCanceled(InputAction.CallbackContext context)
	{
		Debug.Log("Stopped spraying");
		isSpraying = false;
        particleSystems[0].Stop();
    }
}
