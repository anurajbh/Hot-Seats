using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    //public parameters


    //whether someone is sitting on it or not
    public bool isOccupied = false;

    //which chair is this one?
    public int chairNumber;

    //private parameters


    //color of player sitting on it
    Color occupiedColor;



    //methods
    //method that handles sitting
    public void hasSat()
    {
        //early return if already occupied
        if(isOccupied)
        {
            print("Someone is already sitting here");
            //send info to Game Manager to prevent player from attempting to sit
            return;
        }
        //set bool to indicate that someone has set on chair
        isOccupied = true;
    }
    
    //method that handles scoring
    public void scorePlayer()
    {
        //use colour to score player accordingly in the Game Manager
    }
}
