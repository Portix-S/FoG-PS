using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField] AudioClip[] soundtracks;
    private int i;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ChangeMusic());
    }

    IEnumerator ChangeMusic()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        i++;
        if (i >= soundtracks.Length)
            i = 0;
        audioSource.clip = soundtracks[i];
        audioSource.Play();
        StartCoroutine(ChangeMusic());
    }
}
