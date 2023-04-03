using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public List<AudioClip>sounds;

	private void Awake()
	{
		instance = this;
	}

	public AudioClip GetClipName(string soundName)
	{
		for (int i = 0; i < sounds.Count; i++)
		{
			if (sounds[i].name == soundName)
			{
				return sounds[i];
			}
		}
		return null;
	}
	public void PlayClipAtPoint(AudioClip clip ,Vector3 pos)
	{
		GameObject source = new GameObject("source");
		AudioSource audioSource = source.AddComponent<AudioSource>();
		audioSource.playOnAwake = false;
		audioSource.loop = false;
		audioSource.clip = clip;
		audioSource.pitch = Random.Range(0.9f, 1.1f);
		audioSource.Play();
		Destroy(source, clip.length);
	}

	private void Start()
	{
		Food.OnFoodEat += Food_OnFoodEat;
	}
	private void OnDestroy()
	{
		Food.OnFoodEat -= Food_OnFoodEat;
	}
	private void Food_OnFoodEat()
	{
		PlayClipAtPoint(GetClipName("ham"), transform.position);
	}
}
