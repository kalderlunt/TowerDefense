using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class AudioManager : MonoBehaviour 
    {
        public static AudioManager instance;
        public UnityEvent onPlaySound;
        public UnityEvent onStopSound;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound()
        {
            // Logic to play sound
            onPlaySound?.Invoke();
        }

        public void StopSound()
        {
            // Logic to stop sound
            onStopSound?.Invoke();
        }
    }
}