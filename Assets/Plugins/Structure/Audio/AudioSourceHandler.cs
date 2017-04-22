using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class AudioSourceHandler : MonoBehaviour
{
	[SerializeField] AudioClip testClip;
	[SerializeField] AudioSource sourcePrefab;
	List<AudioSource> sources = new List<AudioSource>();

	void Awake()
	{
		if(testClip == null) {
			return;
		}
		var source = Add(testClip);
		source.Play();
		source.FadeIn(5f);
	}

	public int Count
	{
		get { return sources.Count; }
	}

	public AudioSource Add(AudioClip clip)
	{
		AudioSource source = Instantiate(sourcePrefab, transform, true);
		source.playOnAwake = false;
		source.clip = clip;
		sources.Add(source);
		return source;
	}

	public List<AudioSource> AddRange(IEnumerable<AudioClip> clips)
	{
		List<AudioSource> newSources = new List<AudioSource>(clips.Count());
		foreach(AudioClip clip in clips) {
			newSources.Add(Add(clip));
		}
		return newSources;
	}

	public void Remove(string name)
	{
		AudioSource source = FindByString(name);
		sources.Remove(source);
		Destroy(source.gameObject);
	}

	public void Remove(AudioClip clip)
	{
		AudioSource source = FindByClip(clip);
		sources.Remove(source);
		Destroy(source.gameObject);
	}

	public bool Exists(string name)
	{
		return sources.Exists(s => s.clip.name == name);
	}

	public bool Exists(AudioClip clip)
	{
		return sources.Exists(s => s.clip == clip);
	}

	public void Play(string name)
	{
		FindByString(name).Play();
	}

	public void Play(AudioClip clip)
	{
		FindByClip(clip).Play();
	}

	public void Pause(string name)
	{
		FindByString(name).Pause();
	}

	public void Pause(AudioClip clip)
	{
		FindByClip(clip).Pause();
	}

	public void UnPause(string name)
	{
		FindByString(name).UnPause();
	}

	public void UnPause(AudioClip clip)
	{
		FindByClip(clip).UnPause();
	}

	public void Stop(string name)
	{
		FindByString(name).Stop();
	}

	public void Stop(AudioClip clip)
	{
		FindByClip(clip).Stop();
	}

	AudioSource FindByClip(AudioClip clip)
	{
		AudioSource source = sources.Find(s => s.clip == clip);
		Assert.IsNotNull(source, string.Format("AudioSource: {0} not playing", clip.name));
		return source;
	}

	AudioSource FindByString(string name)
	{
		AudioSource source = sources.Find(s => s.clip.name == name);
		Assert.IsNotNull(source, string.Format("AudioSource: {0} not playing", name));
		return source;
	}
}
