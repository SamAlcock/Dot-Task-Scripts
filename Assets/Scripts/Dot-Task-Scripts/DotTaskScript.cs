using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    bool YesClicked = false;
    bool NoClicked = false;

    List<int> dots_picked = new();

    int number;
    int correct;

    GameObject Dot1;
    GameObject Dot2;
    GameObject Dot3;
    GameObject Dot4;
    GameObject Dot5;
    GameObject Dot6;
    GameObject FixationCross;
    public GameObject TextNumber;
    GameObject TextPerspective;
    GameObject YesButton;
    GameObject NoButton;

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
        TextPerspective = GameObject.Find("Perspective Text");

        YesButton = GameObject.Find("Yes Button");
        NoButton = GameObject.Find("No Button");

        Button btn_y = YesButton.GetComponent<Button>();
        Button btn_n = NoButton.GetComponent<Button>();

        // Declares which functions will run when each button is clicked
        btn_y.onClick.AddListener(YesClick);
        btn_n.onClick.AddListener(NoClick);

        

        StartCoroutine(Trials());
    }
    void YesClick() // What to do when 'Yes' button is clicked
    {
        YesClicked = true;
        YesButton.SetActive(false);
        NoButton.SetActive(false);
    }
    void NoClick() // What to do when 'No' button is clicked
    {
        NoClicked = true;
        YesButton.SetActive(false);
        NoButton.SetActive(false);
    }
    void CheckCorrect(int dots)
    {
        Debug.Log("Dots: " + dots + ", Number: " + number);
        if(YesClicked && NoClicked) // May be pointless now as buttons dissappear on click, need to put it at bottom of if statement
        {
            Debug.Log("Both buttons clicked, user is incorrect");
        }
        else if(YesClicked && dots == number) // if yes is clicked and dots displayed equals the number shown
        {
            correct++; // Add one to correct
        }
        else if(NoClicked && dots != number) // if no is clicked and dots displyed doesn't equal the number shown
        {
            correct++; // Add one to correct 
        }
        YesClicked = false;
        NoClicked = false;
        Debug.Log("You have got " + correct + " correct!");
    }
    public IEnumerator Trials()
    {
        // Code to show/hide fixation cross
        FixationCross.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        FixationCross.SetActive(false);


        for (int j = 1; j < TotalBlocks; j++)
        {
            for (int i = 1; i < TotalTrials; i++)
            {

                TotalTrialsAvailable.Add(i);
                ranNumber = Random.Range(0, TotalTrialsAvailable.Count); //selects a random value between 0 and total values in the list.
                TrialsRan = TotalTrialsAvailable[ranNumber];   //Selects the corresponding value from the list. For example if the random number is 3, it takes the 3rd number from the list.
                TotalTrialsAvailable.RemoveAt(ranNumber);//in that list remove random number

                yield return new WaitForSeconds(2f);

                // Hide dots and buttons
                Dot1.SetActive(false);
                Dot2.SetActive(false);
                Dot3.SetActive(false);
                Dot4.SetActive(false);
                Dot5.SetActive(false);
                Dot6.SetActive(false);

                YesButton.SetActive(false);
                NoButton.SetActive(false);

                // Code to decide which measure to run - trial number decides what measure is used (except RandomTrial())
                if (TrialsRan >= 1 && TrialsRan < 13) 
                {
                    if (TrialsRan == 1) // Temporary code for console
                    {
                        Debug.Log("Self Consistent");
                    }

                    SelfConsistent();                       
                }
                else if(TrialsRan >= 13 && TrialsRan < 25) 
                {
                    if (TrialsRan == 13) // Temporary code for console
                    {
                        Debug.Log("Other Consistent");
                    }
                    OtherConsistent();                     
                }
                else if (TrialsRan >= 25 && TrialsRan < 37) 
                {
                    if (TrialsRan == 25) // Temporary code for console
                    {
                        Debug.Log("Self Inconsistent");
                    }
                    SelfInconsistent();              
                }
                else if (TrialsRan >= 37 && TrialsRan < 49) 
                {
                    if (TrialsRan == 37) // Temporary code for console
                    {
                        Debug.Log("Other Inconsistent");
                    }
                    OtherInconsistent();             
                }
                else if (TrialsRan >= 49 && TrialsRan < 53)
                {
                    Debug.Log("Random Trial");
                    RandomTrial();
                    
                }

                yield return new WaitForSeconds(1f);

                YesButton.SetActive(true);
                NoButton.SetActive(true);

                yield return new WaitForSeconds(2f);

            }
        } 
    }

    IEnumerator PickNumber(bool consistent, int dots) // Decides what number to display
    {
        if (consistent) // if trial is consistent, display correct number
        {
            number = dots;
            TextNumber.GetComponent<TextMeshPro>().text = dots.ToString();
        }
        else // else trial is inconsistent, display a random number
        {
            number = Random.Range(0, 4);
            TextNumber.GetComponent<TextMeshPro>().text = number.ToString();
        }
        yield return new WaitForSeconds(0.95f);
        TextNumber.SetActive(true); // Display number, wait, then hide it
        yield return new WaitForSeconds(0.75f);
        TextNumber.SetActive(false);

    }

    IEnumerator DisplayPerspective(bool self) // Displays perspective by changing text on screen
    {
        if (self) // if self boolean is true
        {
            TextPerspective.GetComponent<TextMeshPro>().text = "Self";
        }
        else // if self isn't true then it must be false, so perspective is 'Other'
        {
            TextPerspective.GetComponent<TextMeshPro>().text = "Other";
        }
        // Code to show/hide text
        TextPerspective.SetActive(true); 
        yield return new WaitForSeconds(0.75f);
        TextPerspective.SetActive(false);

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
        List<int> picker = new () { dot_number_1, dot_number_2, dot_number_3 }; // Creates a list out of the dots input into the function
        List<int> picked = new ();

        for(int i = 0; i < niter; i++)
        {
            int dot = Random.Range(0, picker.Count); // Picks a random location in the list
            picked.Add(picker[dot]); // Adds the picked item to the picked list
            picker.Remove(picker[dot]); // Removes the picked item to avoid it being picked twice
        }

        return picked; // returns the picked list to display later
    }

    // 'niter' sets the number of iterations for the for loop. It is set as the length of the list created in DotPicker()
    public void ConDotDisplay(int niter) // Displays dots from list that was created in DotPicker()
    {
        for (int i = 0; i < niter; i++) // Loops through list
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

    public void InconDotDisplay(int niter) // Does the same as ConDotDisplay(), but for the other wall
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
        bool consistent = true;
        bool self = true;
        int dots = Random.Range(1, 4);
        StartCoroutine(DisplayPerspective(self));
        StartCoroutine(PickNumber(consistent, dots));

        if (dots == 1)
        {
            dots_picked = DotPicker(1, 2, 3, 1); // Pick 1 dot out of dots 1, 2 and 3
            ConDotDisplay(dots_picked.Count);
        }
        else if (dots == 2)
        {
            dots_picked = DotPicker(1, 2, 3, 2); // Pick 2 dots out of dots 1, 2, and 3
            ConDotDisplay(dots_picked.Count);
        }
        else if (dots == 3)
        {
            Dot1.SetActive(true);
            Dot2.SetActive(true);
            Dot3.SetActive(true);
        }
        CheckCorrect(dots);

        TrialsRan++;//next iteration of enumerator

       
    }
    public void OtherConsistent()////is the other, looking same way as dots
    {
        //show 1 with 1 dot
        //show 2 with 2 dot
        //show 3 with 3 dot
        bool consistent = true;
        bool self = false;

        int dots = Random.Range(1, 4);
        StartCoroutine(DisplayPerspective(self));
        StartCoroutine(PickNumber(consistent, dots));
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
        CheckCorrect(dots);
    }

    //inconsistent is looking at opposite way of dots.
    //does the opposite wall they are not looking at have same number of dots as prompt
    public void SelfInconsistent()//is the self, looking opposite way to dots
    {
        bool consistent = false;
        bool self = true;
        int dots = Random.Range(1, 4);
        StartCoroutine(DisplayPerspective(self));
        StartCoroutine(PickNumber(consistent, dots));
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
        CheckCorrect(dots);
    }
    public void OtherInconsistent()//is the other, looking opposite way to dots
    {
        bool consistent = false;
        bool self = false;
        int dots = Random.Range(1, 4);
        StartCoroutine(DisplayPerspective(self));
        StartCoroutine(PickNumber(consistent, dots));
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
        CheckCorrect(dots);
    }
    public void RandomTrial()
    {
        int trial = Random.Range(0, 4); // Sets trial integer to a random number - the number picked corresponds to a certain measure

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
