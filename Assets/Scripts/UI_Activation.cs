using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Activation : MonoBehaviour
{
    public bool activateUI = false;

    [SerializeField] private GameObject UI_Component;
    [SerializeField] private SprayController sprayController;

    [SerializeField] private GameObject cap;
    [SerializeField] private GameObject capRegulator;
    [SerializeField] private GameObject colorPicker;
    [SerializeField] private GameObject LeftArrow;
    [SerializeField] private GameObject RightArrow;

    [SerializeField] private GameObject AIModeButton;
    [SerializeField] private Material ButtonColor;

    [SerializeField] private Material LipMaterial;
    [SerializeField] private Color CanColor;

    [SerializeField] private Color[] Colors = {Color.black, Color.white ,Color.grey,  Color.blue, Color.cyan, Color.green, Color.yellow, Color.magenta};
    private int colorCount = 0;

    private float regulatorScale = 1f;

    public InputAction NextColorAction;
    public InputAction PreviousColorAction;

    public InputAction AIModeAction;

    // Start is called before the first frame update
    void Awake()
    {
        // Setup the spray action.
        NextColorAction.performed += SelectColorRight;
        NextColorAction.Enable();

        PreviousColorAction.performed += SelectColorLeft;
        PreviousColorAction.Enable();

        AIModeAction.performed += activateAIMode;
        AIModeAction.Enable();
    }
    void Start()
    {
        activateUI = false;
        UI_Component.SetActive(false);
        cap.GetComponent<Outline>().enabled = false;
        updateCanColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (activateUI)
        {
            startActivation();
        }
        else
        {
            deactivateUI();
        }
    }
    //UI
    //Activation
    private void startActivation()
    {
        UI_Component.SetActive(true);

        UI_Component.GetComponent<Animator>().SetBool("Active", true);

    }
    // Deactivation
    private void deactivateUI()
    {
        UI_Component.GetComponent<Animator>().SetBool("Active", false);
       
    }

    //AI MODE
    //Activation
    public void activateAIMode(InputAction.CallbackContext context)
    {
        cap.GetComponent<Outline>().enabled = true;
        ButtonColor.color = Color.blue;
    }
    // Deactivation
    public void deactivateAIMode()
    {
        cap.GetComponent<Outline>().enabled = false;
        ButtonColor.color = Color.red;
    }

    //Previous Color
    public void SelectColorLeft(InputAction.CallbackContext context)
    {
        colorCount -= 1;
        colorCount = countWithinBounds(colorCount);
        updateCanColor();
    }

    //Next Color
    public void SelectColorRight(InputAction.CallbackContext context)
    {
        colorCount += 1;
        colorCount = countWithinBounds(colorCount);
        updateCanColor();
    }

    
    private int countWithinBounds(int count)
    {
        //Make the count stay within bounds, 0 -> (Lenght - 1) 
        if (count >= Colors.Length)
        {
            count = 0;
        }
        else if (count < 0)
        {
            count = Colors.Length - 1;
        }
        return count;
    }
    private void updateCanColor()
    {
        //Change color in the frame and sprayScript
        Color colour = Colors[colorCount];
        LipMaterial.color = colour;
        sprayController.sprayColor = colour;

        //Apply colors to the arrows
        LeftArrow.GetComponent<Outline>().OutlineColor = Colors[countWithinBounds(colorCount - 1)];
        RightArrow.GetComponent<Outline>().OutlineColor = Colors[countWithinBounds(colorCount + 1)];
    }
}
