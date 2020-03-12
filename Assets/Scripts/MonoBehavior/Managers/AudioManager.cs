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
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AudioManager>();

            return instance;
        }
    }

    public Sound[] musics;
    public Sound[] sounds;

    public string currentMusic;
    public string currentSound;

    public Button soundButton;
    public Button musicButton;

    public bool isPlayingSound
    {
        get
        {
            return currentSound != "" && soundDictionary[currentSound].source.isPlaying;
        }
    }

    public bool isPlayingMusic
    {
        get
        {
            return currentMusic != "" && musicDictionary[currentMusic].source.isPlaying;
        }
    }

    public double currentSoundDuration
    {
        get
        {
            if (currentSound == "")
                return 0;
            return (double)soundDictionary[currentSound].source.clip.samples /
                soundDictionary[currentSound].source.clip.frequency;
        }
    }

    bool soundOn;

    bool musicOn;

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
    Dictionary<string, Sound> musicDictionary = new Dictionary<string, Sound>();

    private void Awake()
    {
        soundOn = PlayerPrefs.GetInt("soundOn", 1) == 0 ? false : true;
        if (soundOn)
            soundButton.image.sprite = soundOnSprite;
        else
            soundButton.image.sprite = soundOffSprite;

        musicOn = PlayerPrefs.GetInt("musicOn", 1) == 0 ? false : true;
        if (musicOn)
            musicButton.image.sprite = musicOnSprite;
        else
            musicButton.image.sprite = musicOffSprite;


        //-------------------------------------------------
        foreach (Sound m in musics)
        {

            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;

            musicDictionary.Add(m.name, m);
        }


        //---------------------------------------

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            soundDictionary.Add(s.name, s);
        }
    }
    public void OnMusicClick()
    {
        Debug.Log("Music  = " + musicOn);

        if (musicOn)
        {
            musicButton.image.sprite = musicOffSprite;
            musicOn = false;
            PauseCurrentMusic();
        }
        else
        {
            musicButton.image.sprite = musicOnSprite;
            musicOn = true;
            UnpauseCurrentMusic();
        }

        PlayerPrefs.SetInt("musicOn", musicOn ? 1 : 0);
    }
    public void OnSoundClick()
    {

        Debug.Log("Sound  = " + soundOn);

        if (soundOn)
        {
            soundButton.image.sprite = soundOffSprite;
            soundOn = false;
        }
        else
        {
            soundButton.image.sprite = soundOnSprite;

            soundOn = true;
        }

        PlayerPrefs.SetInt("soundOn", soundOn ? 1 : 0);
    }

    public void PlaySound(string name)
    {
        Sound newSound;
        bool foundSound = soundDictionary.TryGetValue(name, out newSound);

        if (foundSound)
        {
            if (soundOn)
                newSound.source.Play();
            currentSound = name;
            return;
        }
        Debug.LogWarning("Sound: " + name + " not found!");
    }

    public void PlayMusic(string name)
    {
        Sound newMusic;
        bool foundMusic = musicDictionary.TryGetValue(name, out newMusic);

        if (foundMusic)
        {
            if (musicOn)
            {
                StopCurrentMusic();
                newMusic.source.Play();
            }
            currentMusic = name;
            return;
        }
        Debug.LogWarning("Music: " + name + " not found!");
    }

    public void StopCurrentMusic()
    {
        if (currentMusic != "")
            musicDictionary[currentMusic].source.Stop();
    }

    public void PauseCurrentMusic()
    {
        if (currentMusic != "")
            musicDictionary[currentMusic].source.Pause();
    }

    public void UnpauseCurrentMusic()
    {
        if (currentMusic != "")
            musicDictionary[currentMusic].source.Play();
    }
}
