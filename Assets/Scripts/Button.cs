using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool allow;
    GameManager manager;
    bool canPress = false;
    public float timeToWait;
    AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        StartCoroutine(EnablePress(timeToWait));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(canPress)
        {
            source.PlayOneShot(clip);
            GetComponent<Animator>().SetTrigger("presssed");
            manager.ButtonPressed(allow);
            canPress = false;
            StartCoroutine(EnablePress(timeToWait));
        }
        
    }

    public IEnumerator EnablePress (float t)
    {
        yield return new WaitForSeconds(t);
        canPress = true;
    }
}
