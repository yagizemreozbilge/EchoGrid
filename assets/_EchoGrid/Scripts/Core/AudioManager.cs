using UnityEngine;
using System.Collections.Generic;

namespace EchoGrid.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Clips")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip switchSound;
        [SerializeField] private AudioClip echoSpawnSound;
        [SerializeField] private AudioClip laserHitSound;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySwitchSound() => sfxSource.PlayOneShot(switchSound);
        public void PlayEchoSound() => sfxSource.PlayOneShot(echoSpawnSound);
        public void PlayLaserHitSound() => sfxSource.PlayOneShot(laserHitSound);
    }
}
