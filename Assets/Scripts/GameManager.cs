using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{

    //public parameters
    public float roundTimeLength;
    public float negotiationLength;
    //public float roundTimer;
    //list of players
    public List<Player> players = new List<Player>();

    //private parameters
    [SerializeField] bool isRoundRunning = false;
    [SerializeField] bool isNegotiationOccurring = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            BeginTalk();
        }
/*        if(Input.GetKeyDown(KeyCode.R))
        {
            BeginRound();
        }  */  
    }
    //round starting method
    public void BeginRound()
    {
        if(isRoundRunning)
        {
            print("Round is already running");
            return;
        }
        if(isNegotiationOccurring) 
        {
            print("Can't start round when negotiation is on!");
            return;
        }
        isRoundRunning = true;
        StartCoroutine(RunRound());
    }
    //round ending method
    public void EndRound()
    {
        print("Stop! Round is over");
        //disable player control
        isRoundRunning = false;
    }
    IEnumerator RunRound()
    {
        //enable player control
        print("Move now!");
        yield return new WaitForSeconds(roundTimeLength);
        EndRound();
    }
    //negotiation begins
    public void BeginTalk()
    {
        if (isNegotiationOccurring)
        {
            print("Talk is already running");
            return;
        }
        if (isRoundRunning)
        {
            print("Round is already running");
            return;
        }
        isNegotiationOccurring = true;
        StartCoroutine(RunTalk());
    }
    //negotiation ends
    public void EndTalk()
    {
        print("Stop! Talk is over");
        //disable player talk
        isNegotiationOccurring = false;
        print("Begin moving!");
        BeginRound();
    }
    IEnumerator RunTalk()
    {
        //enable player talk
        print("Talk now!");
        yield return new WaitForSeconds(negotiationLength);
        EndTalk();
    }
}
