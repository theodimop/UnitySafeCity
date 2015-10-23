using UnityEngine;
using UnityEngine.UI;
using System.Collections;



    class AudioManager  : MonoBehaviour
    {
      public  AudioSource backgroundMusic;
      bool isPlaying = true;


      public void toggleOnClick()
      {
          if (isPlaying)
          {
              isPlaying = false;
              backgroundMusic.mute = true;
          }
          else
          {
              isPlaying = true;
              backgroundMusic.Play();
              backgroundMusic.mute = false;

          }
      }
    }

