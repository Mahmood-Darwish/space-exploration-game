using UnityEngine;

/// <summary>
/// Our custom class to save certain properties about our audio clips. We use it in game manager.
/// </summary>
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [SerializeField]
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
