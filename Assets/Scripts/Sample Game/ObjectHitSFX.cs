using UnityEngine;

public class ObjectHitSFX : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip SFX;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        if (!_source)
            _source = gameObject.AddComponent<AudioSource>();

        _source.spatialBlend = 1;
        _source.volume = 0.75f;
        _source.rolloffMode = AudioRolloffMode.Linear;
    }

    public void PlaySFX()
    {
        _source.PlayOneShot(SFX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlaySFX();
    }
}
