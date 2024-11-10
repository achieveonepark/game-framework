// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Audio;
//
// namespace GameFramework.Sound
// {
//
//     public enum SOUND_TYPE
//     {
//         BGM,
//         SOUND
//     }
//
//     public class Sound
//     {
//         [SerializeField] AudioSource soundEffect;
//         [SerializeField] AudioSource backgroundMusic;
//         [SerializeField] AudioClip[] audios;
//
//         public static void Open()
//         {
//             if (PlayerPrefs.HasKey("ertjndtfgn") == false)
//             {
//                 Option.SFXSliderValue = 100;
//                 Option.BGMSliderValue = 100;
//
//                 Option.Bgm = 1;
//                 Option.SFX = 1;
//             }
//         }
//
//         public static void Active(bool isActive)
//         {
//             if (isActive)
//             {
//                 SetVolume(SOUND_TYPE.BGM, Option.Bgm);
//                 SetVolume(SOUND_TYPE.SOUND, Option.SFX);
//             }
//             else
//             {
//                 SetVolume(SOUND_TYPE.BGM, 0);
//                 SetVolume(SOUND_TYPE.SOUND, 0);
//             }
//         }
//
//         public static void Play(SOUND_TYPE type, string name)
//         {
//             switch (type)
//             {
//                 case SOUND_TYPE.BGM:
//                     Instance.PlayBackgroundMusic(name);
//                     break;
//                 case SOUND_TYPE.SOUND:
//                     Instance.PlaySound(name);
//                     break;
//             }
//         }
//
//         public static void SetVolume(SOUND_TYPE type, float volume)
//         {
//             string parameter = string.Empty;
//
//             switch (type)
//             {
//                 case SOUND_TYPE.BGM:
//                     Instance.SetBackgroundMusicVolume(volume);
//                     break;
//                 case SOUND_TYPE.SOUND:
//                     Instance.SetSoundEffectVolume(volume);
//                     break;
//             }
//
//         }
//
//         public void PlaySound(string name)
//         {
//             soundEffect.PlayOneShot(GetClip(name));
//         }
//
//         public void PlayBackgroundMusic(string name)
//         {
//             backgroundMusic.clip = GetClip(name);
//             backgroundMusic.Play();
//         }
//
//         public void SetSoundEffectVolume(float volume)
//         {
//             soundEffect.volume = volume;
//         }
//
//         public void SetBackgroundMusicVolume(float volume)
//         {
//             backgroundMusic.volume = volume;
//         }
//
//         AudioClip GetClip(string name)
//         {
//             AudioClip clip = null;
//
//             for (int i = 0; i < audios.Length; i++)
//             {
//                 if (audios[i].name == name)
//                 {
//                     clip = audios[i];
//                     break;
//                 }
//             }
//
//             return clip;
//         }
//     }
// }