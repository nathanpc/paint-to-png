using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Activation : MonoBehaviour
{
    public bool activateUI = false;

    [SerializeField] private GameObject UI_Component;
    [SerializeField] private GameObject cap;
    [SerializeField] private GameObject capRegulator;
    [SerializeField] private GameObject colorPicker;
    [SerializeField] private GameObject AIModeButton;
    [SerializeField] private GameObject Lip;
    [SerializeField] private Color CanColor;

    [SerializeField] private Color[] Colors = {Color.black, Color.white ,Color.grey,  Color.blue, Color.cyan, Color.green, Color.yellow, Color.magenta};
    private int colorCount = 0;

    private float regulatorScale = 1f;


    // Start is called before the first frame update
    void Start()
    {
        activateUI = false;
        UI_Component.SetActive(false);
        cap.GetComponent<Outline>().enabled = false;
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

    private void startActivation()
    {
        UI_Component.SetActive(true);

        UI_Component.GetComponent<Animator>().SetBool("Active", true);

    }
    private void deactivateUI()
    {
        UI_Component.GetComponent<Animator>().SetBool("Active", false);
       
    }
    private void activateAIMode()
    {
        cap.GetComponent<Outline>().enabled = true;
    }
    private void SelectColorLeft()
    {
        colorCount -= 1;
        countWithinBounds(colorCount);
    }
    private void SelectColorRight()
    {

    }
    private void countWithinBounds(int count)
    {
        if (count >= Colors.Length)
        {
            count = 0;
        }
        else if (count < 0)
        {
            count = Colors.Length - 1;
        }
    }
    private void updateCanColor()
    {
        Lip.GetComponent<Material>().color = Colors[colorCount];
    }
}
