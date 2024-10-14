using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMap : MonoBehaviour
{
    public GameObject menu;   // Assign the menu GameObject in the Inspector
    public GameObject ship;   // Assign the ship GameObject in the Inspector
    public GameObject harbor; // Assign the harbor GameObject in the Inspector
    public GameObject church; // Assign the church GameObject in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, you can hide or deactivate all the objects initially
        // ship.SetActive(false);
        // harbor.SetActive(false);
        // church.SetActive(false);
    }


    public void ShowMapMenu()
    {
        // Check if the menu is active
        if (!menu.activeSelf)  // If the menu is not active
        {
            menu.SetActive(true);   // Activate the menu
        }
        else
        {
            menu.SetActive(false);   // Activate ship
        }
    }




    // This method will activate the ship and deactivate the others
    public void ShowShip()
    {
        ship.SetActive(true);   // Activate ship
        harbor.SetActive(false); // Deactivate harbor
        church.SetActive(false); // Deactivate church
    }

    // This method will activate the harbor and deactivate the others
    public void ShowHarbor()
    {
        ship.SetActive(false);   // Deactivate ship
        harbor.SetActive(true);  // Activate harbor
        church.SetActive(false); // Deactivate church
    }

    // This method will activate the church and deactivate the others
    public void ShowChurch()
    {
        ship.SetActive(false);   // Deactivate ship
        harbor.SetActive(false); // Deactivate harbor
        church.SetActive(true);  // Activate church
    }

    
}
