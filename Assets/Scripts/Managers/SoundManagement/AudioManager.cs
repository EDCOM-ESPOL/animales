using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.SoundManagerNamespace
{
    public class AudioManager : UnitySingleton<AudioManager>
    {

        public List<AudioSource> MusicAudioSources;
        public List<AudioSource> SFXAudioSources;
        public List<AudioSource> VoiceAudioSources;
        
        
        void Start()
        {
            foreach (Transform child in GameObject.Find("MusicSources").gameObject.transform)
            {
                MusicAudioSources.Add(child.GetComponent<AudioSource>());
            }

            foreach (Transform child in GameObject.Find("SFXSources").gameObject.transform)
            {
                SFXAudioSources.Add(child.GetComponent<AudioSource>());
            }

            foreach (Transform child in GameObject.Find("VoiceSources").gameObject.transform)
            {
                Debug.Log(child.GetComponent<AudioSource>());
                VoiceAudioSources.Add(child.GetComponent<AudioSource>());
            }

        }


        public void PlaySFX(string Clip)
        {
            foreach (AudioSource source in SFXAudioSources)
            {
                if (source.name == Clip)
                {
                    source.Play();
                }
            }

        }

        public void PlayVoice(string Clip)
        {
            StopAllVoices();
            foreach (AudioSource source in VoiceAudioSources)
            {
                if (source.name == Clip)
                {
                    source.Play();
                }
            }

        }

        public void StopAllVoices()
        {
            foreach (AudioSource source in VoiceAudioSources)
            {
                source.Stop();
            }
        }

        public void PlayMusic(string Clip)
        {
            foreach (AudioSource source in MusicAudioSources)
            {
                if (source.name == Clip)
                {
                    source.PlayLoopingMusicManaged(1.0f, 2.0f, false);
                }
            }

        }

        public void StopMusic(string Clip)
        {
            foreach (AudioSource source in MusicAudioSources)
            {
                if (source.name == Clip)
                {
                    source.Stop();
                    source.StopLoopingMusicManaged();
                }
            }

        }
      
        

    }
}
