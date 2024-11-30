using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ID : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;
    public TextMeshProUGUI idText;
    GameManager manager;
    AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void DisplayId(string id)
    {
        idText.text = id;
    }

    private void OnMouseDown()
    {
        if(!manager.isNoteOpened)
        {
            audioSource.PlayOneShot(audioClip);
            isOpen = !isOpen;
            animator.SetBool("open", isOpen);
        }
        
    }
}
