using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]
	public static AudioManager instance;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
        }
	}


	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

        s.source.pitch = s.pitch + UnityEngine.Random.Range(-s.pitchVariance, s.pitchVariance);
        s.source.volume = s.volume + UnityEngine.Random.Range(-s.volumeVariance, s.volumeVariance);

        if ((!s.source.isPlaying && s.dontRepeat) || !s.dontRepeat) s.source.Play();
	}

    public void PlayAtPoint(string sound, Vector3 pos = default(Vector3))
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        AudioSource.PlayClipAtPoint(s.clip, pos);
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.source.Stop();
    }
}
