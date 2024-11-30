using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public bool isNoteOpened = false;
    public bool makeBad = false;
    public SCALE scale;
    public PC pc;
    public ID idGO;
    Dictionary<string, Vector2> min_max = new Dictionary<string, Vector2>();
    int[] mins = {0,0,0,0,0};
    int[] maxs = {100,100,100,100,100};
    private const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string digits = "0123456789";
    public string id = "";
    public TMP_InputField enteredID;
    public GameObject invalid;
    public GameObject search;
    public GameObject valid;

    bool isAllowed;
    bool onBoard;
    public bool validID = true;
    public bool allowed = false;

    [Header("Game Settings")]
    public int noOfDays;
    public int currentDay;
    public int perseonPerDay;
    public int currentPerson;
    public float nextPersonWaitTime = 1f;
    public float nextPersonWaitTimeDay = 1f;
    public GameObject dayScreen;
    public GameObject revoked;
    public GameObject newspaper;
    public GameObject[] notes;
    public SManager sManager;
    private void Awake()
    {
        Time.timeScale = 1f;
    }


    void Start()
    {
        scale = GameObject.FindGameObjectWithTag("SCALE").GetComponent<SCALE>();
        pc = GameObject.FindGameObjectWithTag("PC").GetComponentInChildren<PC>();
        //idGO = GameObject.FindGameObjectWithTag("ID").GetComponent<ID>();

        
        
        
        
    }

    public void StartGame()
    {
        min_max.Add("LFI", new Vector2(0, 100));
        min_max.Add("NPI", new Vector2(0, 100));
        min_max.Add("PRI", new Vector2(0, 100));
        min_max.Add("GMI", new Vector2(0, 100));
        min_max.Add("PRF", new Vector2(0, 100));
        currentPerson = 1;
         currentDay = 1;
        idGO.gameObject.SetActive(true);
         DecideSCALE();
         DecideFate();
        int[] digitPositions = { 2, 3, 4, 8, 9 };
        id = GenerateRandomString(10, digitPositions);
         idGO.DisplayId(id);
    }
    private void Update()
    {
         
    }
    public int fate;
    public void DecideFate()
    {
        fate = Random.Range(0, 2);
        if(fate == 0 && !makeBad)
        {
            allowed = true;
            isAllowed = true;
            onBoard = false;
            DecideSCALEtoDisplay(true);
        }
        else
        {
            Debug.Log("Called");
            allowed = false;
            int badApple = Random.Range(0, 4);
            if(badApple == 0)
            {
                DecideSCALEtoDisplay(false);
                isAllowed = true;
                onBoard = false;
                
            }
            else if (badApple == 1)
            {
                isAllowed = false;
                onBoard = false;
                DecideSCALEtoDisplay(true);
            }
            else if(badApple == 2)
            {
                isAllowed = true;
                onBoard = true;
                DecideSCALEtoDisplay(true);
            }
            else
            {
                validID = false;
                DecideSCALEtoDisplay(true);
            }
        }
    }

    public void DecideSCALE()
    {
        foreach (var key in min_max.Keys.ToList())
        {
            // Generate a valid min and max value for the key
            int min = Random.Range(0, 31);    // Example min range
            int max = Random.Range(min, 101); // Ensure max is greater than or equal to min

            // Store the generated min and max in the dictionary
            min_max[key] = new Vector2(min, max);
        }
    }



    public void DecideSCALEtoDisplay(bool isCorrect)
    {
        // Initialize the scale values within the valid range
        int LFI = Random.Range((int)min_max["LFI"][0], (int)min_max["LFI"][1] + 1);
        int NPI = Random.Range((int)min_max["NPI"][0], (int)min_max["NPI"][1] + 1);
        int PRI = Random.Range((int)min_max["PRI"][0], (int)min_max["PRI"][1] + 1);
        int GMI = Random.Range((int)min_max["GMI"][0], (int)min_max["GMI"][1] + 1);
        int PRF = Random.Range((int)min_max["PRF"][0], (int)min_max["PRF"][1] + 1);

        if (!isCorrect)
        {
            // Introduce potential errors for each field if the data should be incorrect
            int badApple = Random.Range(0, 5); // Select which field to corrupt
            int corruptMethod = Random.Range(0, 2); // Decide to go above max or below min

            switch (badApple)
            {
                case 0:
                    LFI = (corruptMethod == 0) ? Random.Range(0, (int)min_max["LFI"][0]) : Random.Range((int)min_max["LFI"][1] + 1, 101);
                    break;
                case 1:
                    NPI = (corruptMethod == 0) ? Random.Range(0, (int)min_max["NPI"][0]) : Random.Range((int)min_max["NPI"][1] + 1, 101);
                    break;
                case 2:
                    PRI = (corruptMethod == 0) ? Random.Range(0, (int)min_max["PRI"][0]) : Random.Range((int)min_max["PRI"][1] + 1, 101);
                    break;
                case 3:
                    GMI = (corruptMethod == 0) ? Random.Range(0, (int)min_max["GMI"][0]) : Random.Range((int)min_max["GMI"][1] + 1, 101);
                    break;
                case 4:
                    PRF = (corruptMethod == 0) ? Random.Range(0, (int)min_max["PRF"][0]) : Random.Range((int)min_max["PRF"][1] + 1, 101);
                    break;
            }
        }

        // Assign the values to the scale object
        scale.LFI = LFI;
        scale.NPI = NPI;
        scale.PRI = PRI;
        scale.GMI = GMI;
        scale.PRF = PRF;

        // Update the scale object with the current min_max and status flags
        scale.min_max = min_max;
        scale.isAllowed = isAllowed;
        scale.onBoard = onBoard;

        pc.UpdateUISCALE();
    }




    public static string GenerateRandomString(int length, int[] digitPositions)
    {
        char[] stringChars = new char[length];
        System.Random random = new System.Random();

        // Fill the string with random letters
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = letters[random.Next(letters.Length)];
        }

        // Replace specific positions with random digits
        foreach (int position in digitPositions)
        {
            if (position >= 0 && position < length)
            {
                stringChars[position] = digits[random.Next(digits.Length)];
            }
            else
            {
                Debug.LogWarning("Position " + position + " is out of bounds for string of length " + length);
            }
        }

        return new string(stringChars);
    }

    public void Authenticate()
    {
        if (id.Equals(enteredID.text , System.StringComparison.OrdinalIgnoreCase) && validID)
        {
            valid.SetActive(true);
            search.SetActive(false);
        }
        else
        {
            invalid.SetActive(true);
            search.SetActive(false);
        }
    }

    public IEnumerator Reset(float m_nextPersonWaitTime)
    {
        yield return new WaitForSeconds(m_nextPersonWaitTime);
        idGO.GetComponent<Animator>().SetTrigger("decided");
        validID = true;
        allowed = false;
        makeBad = false;
        isNoteOpened = false;
        foreach(GameObject note in notes)
        {
            if(note!=null)
            note.GetComponent<Notes>().Check();
        }
        DecideSCALE();
        DecideFate();
        
        //DecideSCALEtoDisplay();
        pc.UpdateUISCAN();
        int[] digitPositions = { 2, 3, 4, 8, 9 };
        id = GenerateRandomString(10, digitPositions);
        idGO.DisplayId(id);
        
    }

    public void ButtonPressed(bool decison)
    {
        
        //idGO.GetComponent<Animator>().ResetTrigger("decided");
        if(decison == allowed)
        {
            if (decison)
            {
                scale.peopleAllowed++;
            }

        }
        else
        {
            scale.wrongDesison++;
            if (decison) scale.peopleAllowed++;
        }
        currentPerson++;
        if (currentPerson <= perseonPerDay)
            StartCoroutine(Reset(nextPersonWaitTime));
        else
        {
            currentDay++;
            if (currentDay <= noOfDays)
            {
                if (scale.wrongDesison <= scale.allowedWrongDesicions)
                    StartNewDay();
                else
                    EndGame(1);
            }
                
            else
            {
                if (scale.wrongDesison <= scale.allowedWrongDesicions) EndGame(2);
                else EndGame(1);
            }
                
        }
    }

    public void StartNewDay()
    {
        currentPerson = 1;
        dayScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Day " + currentDay;
        dayScreen.SetActive(true);
        StartCoroutine(Reset(nextPersonWaitTimeDay));
    }

    public void EndGame(int ending)
    {
        if(ending == 1)
        {
            revoked.SetActive(true);
            //Time.timeScale = 0f;
        }
        else if(ending == 2)
        {
            newspaper.SetActive(true);
            //Time.timeScale = 0f;
        }

        sManager.t = true;
    }
}
