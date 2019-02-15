/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound Music;
    public Sound[] sounds;

    public GameObject _MusicToggle;
    public GameObject _SoundToggle;
    Toggle musicToggle;
    Toggle soundToggle;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicToggle = _MusicToggle.GetComponent<Toggle>();
        soundToggle = _SoundToggle.GetComponent<Toggle>();

        //-------------------------------------------------
        Music.source = gameObject.AddComponent<AudioSource>();
        Music.source.clip = Music.clip;
        Music.source.volume = Music.volume;
        Music.source.pitch = Music.pitch;
        Music.source.loop = Music.loop;

        //---------------------------------------

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
       // PlaySound("TempSoundTrack");
        Music.source.Play();
    }

    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (soundToggle.isOn == true)
        {
            s.source.Play();
        }
        if (soundToggle.isOn == false)
        {
            s.source.Pause();
        }

    }


 


    public void stopMusic()
    {
        if (musicToggle.isOn == false)
        {
          
            Music.source.Pause();

        }
        if (musicToggle.isOn == true)
        {
            Music.source.Play();
        }
       
    }

}
