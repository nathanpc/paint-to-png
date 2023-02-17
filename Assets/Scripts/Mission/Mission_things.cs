using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission_things
{
    [SerializeField]
    public string Type;
    [SerializeField]
    public string Name;
    [SerializeField]
    public GameObject[] Objects;
    [SerializeField]
    public GameObject Place;
    public bool isCompleted;
}
public class Paint_mission
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public GameObject[] Objects;
}
//public class missionTopic
//{
//    [SerializeField]
//    public string Topic;

    
//}