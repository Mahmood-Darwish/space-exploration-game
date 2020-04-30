using UnityEngine;

public class GameManager : MonoBehaviour
{
    //  Variables affecting the whole game.
    public const float G = 100f;
    public static int stateOfPlayer = 1; 
    // = 1 means the player is out of the ship.
    // = 2 means ship is grounded and the player is in it.
    // = 3 means player in ship and in space.

    //  Save the sounds that we have in game. Sound is a custom class we made.
    public Sound[] sounds;

    //  Which sound is currently playing.
    int playingSound = 0;

    //  Create audio sources for each one of our sounds. Populate the sounds array in the inspector.
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

    //  Function to help play a certian sound.
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

    //  Function to help close a certian sound.
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

    //  Function to help us know if a sound is playing.
    public bool isPlaying(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                return s.source.isPlaying;
            }
        }
        return false;
    }


    //  Check which sound should be playing and manage the sounds accordingly.
    private void Update()
    {
        if(stateOfPlayer == 1 && playingSound != 1)
        {
            gameObject.GetComponent<GameManager>().Close("Space Theme");
            gameObject.GetComponent<GameManager>().Close("LiftOff");
            gameObject.GetComponent<GameManager>().Play("Planet Theme");
            playingSound = 1;
        }        
        if (SpaceShip.liftOff && playingSound != 2)
        {
            gameObject.GetComponent<GameManager>().Close("Space Theme");
            gameObject.GetComponent<GameManager>().Close("Planet Theme");
            gameObject.GetComponent<GameManager>().Play("LiftOff");
            playingSound = 2;
        }
        if (stateOfPlayer == 3 && playingSound != 3)
        {
            gameObject.GetComponent<GameManager>().Close("Planet Theme");
            gameObject.GetComponent<GameManager>().Close("LiftOff");
            gameObject.GetComponent<GameManager>().Play("Space Theme");
            playingSound = 3;
        }
        if (!SpaceShip.liftOff && gameObject.GetComponent<GameManager>().isPlaying("LiftOff"))
        {
            gameObject.GetComponent<GameManager>().Close("LiftOff");
            playingSound = 0;
        }
    }
}
