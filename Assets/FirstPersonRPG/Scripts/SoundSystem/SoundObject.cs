using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private AudioSource source;

    public void SetClip(SoundItem sound)
    {
        if (source == null) source = GetComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume * SoundController.Instance.masterVolume;
        source.spatialBlend = sound.is2d ? 0 : 1;
        source.Play();
        Destroy(this.gameObject, sound.clip.length + 0.5f);
    }
}
