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
    public Sound[] musics;
    public Sound[] sounds;

    public Sound current;

    public  Button soundButton;
    public  Button musicButton;

    bool sound;

    bool Music;

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public static AudioManager instance;

    private void Awake()
    {
        sound = true;
        soundButton.image.sprite = soundOnSprite;
        Music = true;
        musicButton.image.sprite = musicOnSprite;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        //-------------------------------------------------
        foreach (Sound s in musics)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


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
    public void OnMusicClick()
    {
        Debug.Log("Music  = " + Music);

        if (Music)
        {
            musicButton.image.sprite = musicOffSprite;
            Music = false;
        }
        else
        {
            musicButton.image.sprite = musicOnSprite;
            Music = true;
        }
    }
    public void OnSoundClick() {

        Debug.Log("Sound  = " + sound);

        if (sound)
        {
            soundButton.image.sprite = soundOffSprite;
            sound = false;
         
        }
        else
        {
            soundButton.image.sprite = soundOnSprite;

            sound = true;
        }
}
       
    public void PlaySound(string name)
    {
        current = Array.Find(sounds, sound => sound.name == name);

        if (current == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (sound)
        {
            current.source.Play();
        }
        if (!sound)
        {
            current.source.Pause();
        }
    }

    public void PlayMusic(string name)
    {
        current = Array.Find(musics, music => music.name == name);

        if (current == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        if (Music)
        {
            current.source.Play();
            Debug.Log("********************"+current);
        }
        if (!Music)
        {
            current.source.Pause();
        }
    }

    public void HoldMusic(String name)
    {
        current = Array.Find(musics, music => music.name == name);

        if (current == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        current.source.Pause();
    }

    //public void StopMusic()
    //{
    //    foreach (Sound s in musics)
    //    {
    //        if (musicToggle.isOn == false)
    //        {
    //            s.source.Pause();
    //        }
    //        if (musicToggle.isOn == true)
    //        {
    //            s.source.Play();
    //        }
    //    }
    //}


}
