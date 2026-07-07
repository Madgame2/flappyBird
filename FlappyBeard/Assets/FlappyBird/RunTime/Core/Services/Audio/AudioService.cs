using UnityEngine;

namespace FlappyBird.RunTime.Core.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly AudioSource _musicSource;
        private readonly AudioSource _sfxSource;

        public AudioService(AudioSource musicSource, AudioSource sfxSource)
        {
            _musicSource = musicSource;
            _sfxSource = sfxSource;
        }

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (_musicSource.clip == clip && _musicSource.isPlaying)
                return;

            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
            {
                _sfxSource.PlayOneShot(clip);
            }
        }

        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = Mathf.Clamp01(volume);
        }

        public void SetSFXVolume(float volume)
        {
            _sfxSource.volume = Mathf.Clamp01(volume);
        }
    }
}
