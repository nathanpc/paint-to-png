using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseToObjective : MonoBehaviour
{
    //[SerializeField] private GameObject prefab;
    [SerializeField] private float ClosingDistance = 15f;
    private Transform player;
    [SerializeField] private GameObject Cillinder;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    // Update is called once per frame
    void Update() // It deactives the gameobject when approaching a certain distance from the objective
    {
        distance = Vector3.Distance(player.position, this.gameObject.transform.position);
        if (distance <= ClosingDistance)
        {
            Cillinder.SetActive(false);
        }
        else
        {
            Cillinder.SetActive(true);
        }
    }
}
