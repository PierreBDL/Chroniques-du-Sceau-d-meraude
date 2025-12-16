using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundVolume (float volume)
    {
        audioMixer.SetFloat("Sound", volume);
    }
}
