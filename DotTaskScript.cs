using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    List<int> dots_picked = new();

    GameObject Dot1;
    GameObject Dot2;
    GameObject Dot3;
    GameObject Dot4;
    GameObject Dot5;
    GameObject Dot6;
    GameObject FixationCross;
    public GameObject TextNumber;

    // Start is called before the first frame update
    void Start()
    {
        Dot1 = GameObject.Find("Dot 1");
        Dot2 = GameObject.Find("Dot 2");
        Dot3 = GameObject.Find("Dot 3");
        Dot4 = GameObject.Find("Dot 4");
        Dot5 = GameObject.Find("Dot 5");
        Dot6 = GameObject.Find("Dot 6");

        FixationCross = GameObject.Find("Fixation Cross");

        TextNumber = GameObject.Find("Text");

        FixationCross.SetActive(false);

        StartCoroutine(Trials());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Trials()
    {
        for (int j = 1; j < TotalBlocks; j++)
        {
            for (int i = 1; i < TotalTrials; i++)
            {
                TextNumber.SetActive(true);

                TextNumber.GetComponent<TextMeshPro>().text = Random.Range(0, 4).ToString();

                TotalTrialsAvailable.Add(i);
                ranNumber = Random.Range(0, TotalTrialsAvailable.Count); //selects a random value between 0 and total values in the list.
                TrialsRan = TotalTrialsAvailable[ranNumber];   //Selects the corresponding value from the list. For example if the random number is 3, it takes the 3rd number from the list.
                TotalTrialsAvailable.RemoveAt(ranNumber);//in that list remove random number

                Dot1.SetActive(false);
                Dot2.SetActive(false);
                Dot3.SetActive(false);
                Dot4.SetActive(false);
                Dot5.SetActive(false);
                Dot6.SetActive(false);

                yield return new WaitForSeconds(2f);

                TextNumber.SetActive(false);

                if (TrialsRan >= 1 && TrialsRan < 13)
                {
                    SelfConsistent();                       
                }
                else if(TrialsRan >= 13 && TrialsRan < 25)
                {
                    OtherConsistent();                     
                }
                else if (TrialsRan >= 25 && TrialsRan < 37)
                {
                    SelfInconsistent();              
                }
                else if (TrialsRan >= 37 && TrialsRan < 49)
                {
                    OtherInconsistent();             
                }
                else if (TrialsRan >= 49 && TrialsRan < 53)
                {
                    Debug.Log("Random Trial");
                    RandomTrial();
                    
                }

                yield return new WaitForSeconds(1f);

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

    public List<int> DotPicker(int dot_number_1, int dot_number_2, int dot_number_3, int niter)
    {
        dots_picked.Clear();
        List<int> picker = new () { dot_number_1, dot_number_2, dot_number_3 };
        List<int> picked = new ();

        for(int i = 0; i < niter; i++)
        {
            int dot = Random.Range(0, picker.Count);
            picked.Add(picker[dot]);
            picker.Remove(picker[dot]);
        }

        return picked;
    }

    public void ConDotDisplay(int niter)
    {
        for (int i = 0; i < niter; i++)
        {
            if (dots_picked[i] == 1)
            {
                Dot1.SetActive(true);
            }
            else if (dots_picked[i] == 2)
            {
                Dot2.SetActive(true);
            }
            else if (dots_picked[i] == 3)
            {
                Dot3.SetActive(true);
            }
        }
    }

    public void InconDotDisplay(int niter)
    {
        for (int i = 0; i < niter; i++)
        {
            if (dots_picked[i] == 4)
            {
                Dot4.SetActive(true);
            }
            else if (dots_picked[i] == 5)
            {
                Dot5.SetActive(true);
            }
            else if (dots_picked[i] == 6)
            {
                Dot6.SetActive(true);
            }
        }
    }

    public void SelfConsistent()//is the self, looking same way as dots
    {
        //show 0 with 0 dot - filler
        Debug.Log("Self-Consistent");
        int dots = Random.Range(1, 4);
        if (dots == 1)
        {
            dots_picked = DotPicker(1, 2, 3, 1);
            ConDotDisplay(dots_picked.Count);
        }
        else if (dots == 2)
        {
            dots_picked = DotPicker(1, 2, 3, 2);
            ConDotDisplay(dots_picked.Count);
        }
        else if (dots == 3)
        {
            Dot1.SetActive(true);
            Dot2.SetActive(true);
            Dot3.SetActive(true);
        }


        TrialsRan++;//next iteration of enumerator

       
    }
    public void OtherConsistent()////is the other, looking same way as dots
    {
        //show 1 with 1 dot
        //show 2 with 2 dot
        //show 3 with 3 dot
        Debug.Log("Other-Consistent");
        int dots = Random.Range(1, 4);
        if (dots == 1)
        {
            dots_picked = DotPicker(1, 2, 3, 1);
            ConDotDisplay(dots_picked.Count);
        }
        else if (dots == 2)
        {
            dots_picked = DotPicker(1, 2, 3, 2);
            ConDotDisplay(dots_picked.Count);
        }
        else if (dots == 3)
        {
            Dot1.SetActive(true);
            Dot2.SetActive(true);
            Dot3.SetActive(true);
        }
    }

    //inconsistent is looking at opposite way of dots.
    //does the opposite wall they are not looking at have same number of dots as prompt
    public void SelfInconsistent()//is the self, looking opposite way to dots
    {
        Debug.Log("Self-Inconsistent");
        int dots = Random.Range(1, 4);
        if (dots == 1)
        {
            dots_picked = DotPicker(4, 5, 6, 1);
            InconDotDisplay(dots_picked.Count);
        }
        else if (dots == 2)
        {
            dots_picked = DotPicker(4, 5, 6, 2);
            InconDotDisplay(dots_picked.Count);
        }
        else if (dots == 3)
        {
            Dot4.SetActive(true);
            Dot5.SetActive(true);
            Dot6.SetActive(true);
        }
    }
    public void OtherInconsistent()//is the other, looking opposite way to dots
    {
        Debug.Log("Other-Inconsistent");
        int dots = Random.Range(1, 4);
        if (dots == 1)
        {
            dots_picked = DotPicker(4, 5, 6, 1);
            InconDotDisplay(dots_picked.Count);
        }
        else if (dots == 2)
        {
            dots_picked = DotPicker(4, 5, 6, 2);
            InconDotDisplay(dots_picked.Count);
        }
        else if (dots == 3)
        {
            Dot4.SetActive(true);
            Dot5.SetActive(true);
            Dot6.SetActive(true);
        }
    }
    public void RandomTrial()
    {
        int trial = Random.Range(0, 4);

        if (trial == 0)
        {
            SelfConsistent();
        }
        else if (trial == 1)
        {
            SelfInconsistent();
        }
        else if (trial == 2)
        {
            OtherConsistent();
        }
        else if (trial == 3)
        {
            OtherInconsistent();
        }
    }
}
