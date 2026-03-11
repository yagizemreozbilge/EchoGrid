using System.Collections.Generic;
using UnityEngine;

namespace EchoGrid.EchoSystem
{
    // Attached to the actual visual "clone" ghost that will spawn in the world
    public class EchoClone : MonoBehaviour
    {
        private List<FrameInput> _frames = new List<FrameInput>();
        private bool _isPlaying = false;
        private float _playbackTimer = 0f;
        private int _currentFrameIndex = 0;

        public void Initialize(List<FrameInput> recordedFrames)
        {
            _frames = recordedFrames;
            _isPlaying = true;
            _playbackTimer = 0f;
            _currentFrameIndex = 0;
            Debug.Log("Echo Clone started playing!");
        }

        private void FixedUpdate()
        {
            if (!_isPlaying || _frames.Count == 0) return;

            _playbackTimer += Time.fixedDeltaTime;

            // Advance frame index accurately according to the time recorded
            while (_currentFrameIndex < _frames.Count - 1 && 
                   _frames[_currentFrameIndex + 1].timestamp <= _playbackTimer)
            {
                _currentFrameIndex++;
            }

            // Apply position and rotation directly
            transform.position = _frames[_currentFrameIndex].position;
            transform.rotation = _frames[_currentFrameIndex].rotation;

            if (_currentFrameIndex >= _frames.Count - 1)
            {
                StopPlayback();
            }
        }

        private void StopPlayback()
        {
            _isPlaying = false;
            Debug.Log("Echo Clone finished playing and disappeared.");
            
            // The clone has lived its recorded memory entirely, so it vanishes.
            // (If the clone was standing on a pressure switch, removing it here will trigger Door closing).
            Destroy(gameObject);
        }
    }
}
