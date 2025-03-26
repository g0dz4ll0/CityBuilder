using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip placementSound;
    [SerializeField] AudioSource audioSource;

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

    public void PlayPlacementSound()
    {
        audioSource.PlayOneShot(placementSound);
    }
}
