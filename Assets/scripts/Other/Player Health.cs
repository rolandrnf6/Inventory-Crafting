using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health =0 ;
    [SerializeField] private int food =0 ;
    [SerializeField] private int water = 0 ;

    public void addHealth(int addedHealth)
    {
        health += addedHealth;
    }
    public void addFood(int addFood)
    {
        food += addFood;
    }
    public void addWater(int addWater)
    {
        water += addWater;
    }
}
