using System.Collections.Generic;
using UnityEngine;
using EchoGrid.Events;

namespace EchoGrid.EchoSystem
{
    // A system that lives in the game world (or maybe on the player) 
    // waiting to hear that a recording has successfully finished.
    public class EchoPlayback : MonoBehaviour
    {
        [Header("Echo Clone Settings")]
        [Tooltip("Assign the Echo Clone prefab (e.g. A ghostly robot) from the project here")]
        [SerializeField] private GameObject echoClonePrefab;

        private void OnEnable()
        {
            // Subscribe to listen anytime an Echo is correctly finished recording
            EventBus.OnEchoRecorded += SpawnAndPlayEcho;
        }

        private void OnDisable()
        {
            EventBus.OnEchoRecorded -= SpawnAndPlayEcho;
        }

        private void SpawnAndPlayEcho(List<FrameInput> recordedData)
        {
            if (recordedData == null || recordedData.Count == 0)
            {
                Debug.LogWarning("Playback failed: Recording was empty.");
                return;
            }

            if (echoClonePrefab == null)
            {
                Debug.LogError("Playback failed: Did you forget to assign the Echo Clone Prefab in the Inspector?");
                return;
            }

            // Spawn the clone exactly where the player WAS when they first hit spacebar
            GameObject cloneObj = Instantiate(echoClonePrefab, recordedData[0].position, recordedData[0].rotation);
            EchoClone cloneScript = cloneObj.GetComponent<EchoClone>();
            
            if (cloneScript != null)
            {
                cloneScript.Initialize(recordedData);
            }
        }
    }
}
