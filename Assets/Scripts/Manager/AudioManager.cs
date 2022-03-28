using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach(Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if(sound == null) {
            Debug.LogWarning("Did not found sound file: " + name);
            return;
        }
        sound.source.Play();
    }

    public void Mute(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if(sound == null) {
            Debug.LogWarning("Did not found sound file: " + name);
            return;
        }
        sound.source.volume = 0f;
    }

    public void Unmute(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if(sound == null) {
            Debug.LogWarning("Did not found sound file: " + name);
            return;
        }
        sound.source.volume = 1f;
    }

    public static void ToggleAllSFX(bool disable) {
        if(disable){
            instance.Mute(SoundConst.BUTTON_CLICK);
            instance.Mute(SoundConst.BUTTON_CLOSE);
        } else {
            instance.Unmute(SoundConst.BUTTON_CLICK);
            instance.Unmute(SoundConst.BUTTON_CLOSE);
        }
    }

    public static void ToggleMusic(bool disable) {
        if(disable){
            instance.Mute(SoundConst.THEME1);
            instance.Mute(SoundConst.THEME2);
            instance.Mute(SoundConst.THEME3);
            instance.Mute(SoundConst.THEME4);
            instance.Mute(SoundConst.THEME5);
        } else {
            instance.Unmute(SoundConst.THEME1);
            instance.Unmute(SoundConst.THEME2);
            instance.Unmute(SoundConst.THEME3);
            instance.Unmute(SoundConst.THEME4);
            instance.Unmute(SoundConst.THEME5);
        }
    }
}
