using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int stateOfPlayer = 1;
    public Sound[] sounds;
    int playingSound = 0;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
                return;
            }
        }
    }

    public void Close(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
                return;
            }
        }
    }


    private void Update()
    {
        if(stateOfPlayer == 1 && playingSound != 1)
        {
            gameObject.GetComponent<GameManager>().Close("Space Theme");
            gameObject.GetComponent<GameManager>().Play("Planet Theme");
            playingSound = 1;
        }
        if (stateOfPlayer == 2 && playingSound != 2)
        {
            gameObject.GetComponent<GameManager>().Close("Planet Theme");
            gameObject.GetComponent<GameManager>().Play("Space Theme");
            playingSound = 2;
        }
    }
}
