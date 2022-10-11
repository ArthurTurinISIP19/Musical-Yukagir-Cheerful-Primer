using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _charactersAudioClipArray;
    [SerializeField] private AudioClip[] _wordsAudioClipArray;
    [SerializeField] private AudioSource source;

    public void SoundCharacter(int soundNum)
    {
        if (soundNum < _charactersAudioClipArray.Length)
        {
            source.clip = _charactersAudioClipArray[soundNum];
            source.PlayOneShot(source.clip);
            //source.Play();
        }
    }

    public void SoundFullWord(int currentWordNumber)
    {
        source.clip = _wordsAudioClipArray[currentWordNumber];
        source.PlayOneShot(source.clip);
        source.Play();
    }
}

