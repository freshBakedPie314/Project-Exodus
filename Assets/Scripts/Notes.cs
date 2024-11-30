using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public int rand;
    public int min = 0;
    public int max = 5;
    public int dayToGo;
    GameManager manager;
    bool isOpen = false;
    AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        rand = Random.Range(min, max + 1);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(manager.currentDay ==  dayToGo && manager.currentPerson == rand)
        {
            GetComponent<Animator>().SetTrigger("Go");
        }

        
    }

    private void OnMouseDown()
    {
        if(!isOpen)
        {
            audioSource.PlayOneShot(audioClip);
            isOpen = true;
            GetComponent<Animator>().SetTrigger("Open");
            manager.isNoteOpened = true;
        }
        else
        {
            manager.isNoteOpened = false;
            GetComponent<Animator>().SetTrigger("Close");
        }
    }
    public void Desimate()
    {
        Destroy(gameObject);
    }

    public void Check()
    {
        if (manager.currentDay == dayToGo && manager.currentPerson == rand)
        {
            manager.isNoteOpened = true;
            manager.makeBad = true;
        }
        else
        {
            manager.isNoteOpened |= false;
            manager.makeBad |= false;
        }
    }
}
