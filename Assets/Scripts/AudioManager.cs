using UnityEngine;

/// <summary>
/// Handles the audio in the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Tooltip("The sound to play when placing a structure")]
    [SerializeField] AudioClip placementSound;
    [SerializeField] AudioSource audioSource;

    // Singleton pattern to make sure there is only one instance of the AudioManager
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays the placement sound
    /// </summary>
    public void PlayPlacementSound()
    {
        audioSource.PlayOneShot(placementSound);
    }
}
