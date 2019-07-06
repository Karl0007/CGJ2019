using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAudio : MonoBehaviour,IPointerClickHandler
{
    public AudioClip clip;

    public bool playOnEnable = true;

    public bool playOnClick = false;

    private void OnEnable()
    {
        if(playOnEnable)
        {
            AudioSource audioSources = this.GetComponent<AudioSource>();
            if (audioSources == null)
            {
                audioSources = this.gameObject.AddComponent<AudioSource>();

            }
            audioSources.clip = clip;
            audioSources.Play();
        }
        

    }


    public static void Play(GameObject go,AudioClip clip)
    {
        AudioSource audioSources = go.GetComponent<AudioSource>();
        if (audioSources == null)
        {
            audioSources = go.gameObject.AddComponent<AudioSource>();

        }
        audioSources.clip = clip;
        audioSources.Play();
    }

    public static void Play(GameObject go, string sourcePath)
    {
        Play(go, Resources.Load<AudioClip>(sourcePath));
    }

    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSource audioSources = this.GetComponent<AudioSource>();
        if (audioSources == null)
        {
            audioSources = this.gameObject.AddComponent<AudioSource>();

        }
        audioSources.clip = clip;
        audioSources.Play();
    }
}
