using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource m_AudioSource;

    bool m_Initialized = false;
    bool m_Playing = false;
    float m_Delay;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void Init(AudioClip clip)
    {
        if (m_Initialized) return;

        m_AudioSource.clip = clip;
        m_Initialized = true;
    }

    public void Play(float delay)
    {
        m_Delay = delay;        
        StartCoroutine(PlayAndWaitForFinish(m_Delay));
    }

    public void SetLoop(bool state)
    {
        m_AudioSource.loop = state;
    }

    public void Pause()
    {
        StopCoroutine(PlayAndWaitForFinish(m_Delay));
    }

    // Update is called once per frame
    /*void Update()
    {
        if (m_Playing == false) return;
        if (m_AudioSource.isPlaying == false) Destroy(gameObject);        
    }*/

    IEnumerator PlayAndWaitForFinish(float delay)
    {
        yield return new WaitForSeconds(delay);

        m_AudioSource.Play();

        while(m_AudioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
