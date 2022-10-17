using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotTaskScript : MonoBehaviour
{
    /////////////////////general trial procedure (also found in word document)//////////////////////

    //Instructions
    //fixation cross for 750 ms 
    //Self or other text for 750ms 
    //200ms interval
    //Number of Dots (0-3) 750ms - number
    //Part with avatar facing dots  2000ms - if over 2000ms next trial
    //yes or no button

    /////////////////////Measures//////////////////////
    //consistent =  dots on left wall
    //inconsistent = dots on right wall

    //Self- consistent
    //self- inconsistent

    //other-consistent
    //other-inconsistent

    //random - 0 dots - ignore for time being need to work this out fully 

    ///////////////////mismatching and matching explanation////////////////////////

    //need 6 matching, 6 mismatching per meeasure (e.g. self-consistent/other-consistent)

    //matching - number of dots in shown as a number is the same as the nunber of dots on the wall
    //mismatching - number of dots shown as a number is NOT the same as the number of dots on a wall


    //shows the number one, next slide has 3 dots. 
    //mismatching//////

    //show 1 with 3 dot
    //show 1 with 2 dot
    //show 3 with 1 dot
    //show 2 with 1 dot
    //show 3 with 2 dot
    //show 2 with 3 dot


    //matching/////////
    //show 1 with 1 dot
    //show 2 with 2 dot
    //show 3 with 3 dot
    //show 1 with 1 dot
    //show 2 with 2 dot
    //show 3 with 3 dot


    ///////////////////Counterbalancing////////////////////////

    //can ignore for the time being.




    public float TotalTrials = 104f;
    public float TotalBlocks = 2f;

    public int ranNumber = 0;
    public int TrialsRan = 0;
    public List<int> TotalTrialsAvailable = new List<int>(); //create list



    // Start is called before the first frame update
    void Start()
    {
        Trials();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Trials() // changed IEnumerator to void (same with all trials)
    {
        for (int j = 1; j < TotalBlocks; j++)
        {
            for (int i = 1; i < TotalTrials; i++)
            {
                TotalTrialsAvailable.Add(i);
                ranNumber = Random.Range(0, TotalTrialsAvailable.Count); //selects a random value between 0 and total values in the list.
                TrialsRan = TotalTrialsAvailable[ranNumber];   //Selects the corresponding value from the list. For example if the random number is 3, it takes the 3rd number from the list.
                TotalTrialsAvailable.RemoveAt(ranNumber);//in that list rmemove random number
                {
                    if (TrialsRan >= 1 || TrialsRan < 13)
                    {
                        SelfConsistent();
                        Debug.Log("Self-Consistent");
                    }
                    else if(TrialsRan >= 13 && TrialsRan < 25)
                    {
                        OtherConsistent();
                        Debug.Log("Other-Consistent");
                    }
                    else if (TrialsRan >= 25 && TrialsRan < 37)
                    {
                        SelfInconsistent();
                        Debug.Log("Self-Inconsistent");
                    }
                    else if (TrialsRan >= 37 && TrialsRan < 49)
                    {
                        OtherInconsistent();
                        Debug.Log("Self-Inconsistent");
                    }
                    else if (TrialsRan >= 49 && TrialsRan < 53)
                    {
                        RandomTrial();
                        Debug.Log("Random Trial");
                    }
                }
            }
        } 
    }


    //response = is the number of dots in the prompt the same the agent can see?
    //feedback correct, incorrect, no response. 
    //same, different or no response

    //need place with dots game object 0, 1, 3. (use all 3 in each trial)
    //Location A (L) and B (R) 

    //number of dots from self
    //number of dots from other

    //consistent is looking same way
    //inconsistent is looking other way

    //filler involves the number 0

    //order of trials within a block psuedo randomised, no more than 3 consecutive trials as same type.
    //self and other equally proceeded by no shift, or shift.



    //block x 2
    //2 x 2 contingency design (4 conditions)
    //104 trials in total / 2 (block) 
    // 52 trial block/ 4 (measures)
    // 13 - 1 = 12 (total trials - filler trials)
    //12 / 2 = 6 (mismatch and match)



    //counterbalance
    //no more than 3 measure repeat
    //self-other/ other-self 50% of time, self-self/other-other 50% of time.
    //order of block presentation counterbalanced


    //50% matching
    //50% mismatching
    //only matching in analysis


    public void SelfConsistent()//is the self, looking same way as dots
    {




        //show 0 with 0 dot - filler


        TrialsRan++;//next iteration of enumerator

       
    }
    public void OtherConsistent()////is the other, looking same way as dots
    {
        //show 1 with 1 dot
        //show 2 with 2 dot
        //show 3 with 3 dot
    }

    //inconsistent is looking at opposite way of dots.
    //does the opposite wall they are not looking at have same number of dots as prompt
    public void SelfInconsistent()//is the self, looking opposite way to dots
    {

    }
    public void OtherInconsistent()//is the other, looking opposite way to dots
    {

    }
    public void RandomTrial()
    {

    }
}
