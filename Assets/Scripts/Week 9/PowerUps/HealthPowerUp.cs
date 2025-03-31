using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPowerUp : PowerUpBase
{
    public override void Heal(float heal)
    {
        health += heal;
    }
    public override void OnCollisionEnter(Collision collision)
    {
       //setactive false
    }
}
