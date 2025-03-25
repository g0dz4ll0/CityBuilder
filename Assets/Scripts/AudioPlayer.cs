using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip placementSound;
    [SerializeField] AudioSource audioSource;

    public static AudioPlayer instance;

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
