using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : PlayerRPG //to access variables to change
{
    public bool playerHasObject = false;
    public bool powerUpActive = true;
    public virtual void OnCollisionEnter(Collision collision)
    {
        playerHasObject = true;
        powerUpActive = false;
    }
}
