using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour
{

    private void Awake()
    {
        Robot robot = new Robot();
    }
}

public class Robot
{
    private Battery includedBattery;
    public Robot()
    {
        Debug.Log("New Robot has been created!");
        includedBattery = new Battery();    
    }
}

public class Battery
{
    public float health = 100f;

    public Battery()
        {
             Debug.Log("New Battery has been created!");
        }
}
