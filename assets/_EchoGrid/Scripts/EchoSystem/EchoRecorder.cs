using System.Collections.Generic;
using UnityEngine;
using EchoGrid.Player;
using EchoGrid.Events;

namespace EchoGrid.EchoSystem
{
    [RequireComponent(typeof(InputHandler))]
    public class EchoRecorder : MonoBehaviour
    {
        [Header("Recording Settings")]
        [SerializeField] private float maxRecordTime = 5f;
        
        public bool IsRecording { get; private set; }
        
        private List<FrameInput> _recordedFrames = new List<FrameInput>();
        private float _currentRecordTime = 0f;
        private InputHandler _inputHandler;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            // Only start recording if Space is pressed, we are not already recording
            if (_inputHandler.RecordPressed && !IsRecording)
            {
                StartRecording();
            }
        }

        private void FixedUpdate()
        {
            if (IsRecording)
            {
                RecordFrame();
                _currentRecordTime += Time.fixedDeltaTime;

                if (_currentRecordTime >= maxRecordTime)
                {
                    StopRecording();
                }
            }
        }

        private void StartRecording()
        {
            IsRecording = true;
            _currentRecordTime = 0f;
            _recordedFrames.Clear();
            Debug.Log($"Started Recording Echo... (Max duration: {maxRecordTime}s)");
        }

        private void RecordFrame()
        {
            _recordedFrames.Add(new FrameInput
            {
                position = transform.position,
                rotation = transform.rotation,
                timestamp = _currentRecordTime
            });
        }

        private void StopRecording()
        {
            IsRecording = false;
            Debug.Log($"Stopped Recording. Total frames: {_recordedFrames.Count}");
            
            // Notify other systems (like EchoPlayback) that our recording is finished
            EventBus.TriggerEchoRecorded(new List<FrameInput>(_recordedFrames));
        }
    }
}
