using UnityEngine;

namespace FlappyBird.RunTime.Core.Services.Audio
{
    public interface IAudioService
    {
        void PlayMusic(AudioClip clip, bool loop = true);
        void StopMusic();
        void PlaySFX(AudioClip clip);
        void SetMusicVolume(float volume);
        void SetSFXVolume(float volume);
    }
}