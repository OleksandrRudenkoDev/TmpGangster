using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Databases/AudioDatabase")]
    public class AudioDatabase : ScriptableObject
    {
        public List<AudioData> DataList = new List<AudioData>();

        private void OnValidate()
        {
            foreach(AudioData data in DataList)
            {
                data.LengthPercent = Mathf.Clamp(data.LengthPercent, 0, 100);
            }
        }
    }
    [Serializable]
    public class AudioData
    {
        public AudioId Id;
        [AudioTag]
        public string [] Tags;
        public float LengthPercent = 100f;
        public AudioClip [] Clips;
    }

    public enum AudioId
    {
        None = 0
    }
}