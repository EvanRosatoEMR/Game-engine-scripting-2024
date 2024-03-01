using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    //private AudioSource m_AudioSource;

    [SerializeField]
    [Tooltip("Assign prefab that is supposed to play sound effects")]
    private GameObject m_SoundEffectPrefab;

    [SerializeField] private AudioClip m_Attack;
    [SerializeField] private AudioClip m_Damage;

    public enum SoundType
    {
        Attack = 1,
        Damage = 2
    }

    private void Awake()
    {
        //m_AudioSource = GetComponent<AudioSource>();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static SoundEffect GetSound(SoundType sound)
    {
        return instance.privGetSound(sound);
    }

    private SoundEffect privGetSound(SoundType sound)
    {
        AudioClip clip = null;
        /*switch (sound)
        {
            case SoundType.Attack: m_AudioSource.clip = m_Attack; break;
            case SoundType.Damage: m_AudioSource.clip = m_Damage; break;
        }

        m_AudioSource.Play();*/

        /*switch (sound)
        {
            case SoundType.Attack: m_AudioSource.PlayOneShot(m_Attack); break;
            case SoundType.Damage: m_AudioSource.PlayOneShot(m_Damage); break;
        }*/

        switch (sound)
        {
            case SoundType.Attack: clip = m_Attack; break;
            case SoundType.Damage: clip = m_Damage; break;
        }

        GameObject newSoundEffectObject = Instantiate(m_SoundEffectPrefab);
        SoundEffect newSoundEffect = newSoundEffectObject.GetComponent<SoundEffect>();
        newSoundEffect.Init(clip);

        return newSoundEffect;
    }
}
