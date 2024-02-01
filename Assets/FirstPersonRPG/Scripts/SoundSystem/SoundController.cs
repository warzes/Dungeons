using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public GameObject soundObjectPrefab;
    [Range(0f, 1f)]
    public float masterVolume = 1;

    public static SoundObject Play(string id)
    {
        SoundObject sound = Instance.PlaySound(id);
        return sound;
    }

    public static SoundObject Play(string id, Vector3 pos)
    {
        SoundObject sound = Instance.PlaySound(id);
        if (sound == null) return null;

        sound.transform.position = pos;
        return sound;
    }

    public static SoundObject Play(string id, Transform tran)
    {
        SoundObject sound = Instance.PlaySound(id);
        if (sound == null) return null;

        sound.transform.parent = tran;
        sound.transform.localPosition = Vector3.zero;
        return sound;
    }

    private SoundObject PlaySound(string id)
    {
        SoundItem item = SoundsLibrary.Instance.GetItemById(id);
        if (item == null) return null;
        SoundObject sound = ((GameObject)Instantiate(soundObjectPrefab)).GetComponent<SoundObject>();
        sound.SetClip(item);
        return sound;
    }
}
