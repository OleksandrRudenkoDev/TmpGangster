using Base.Editor.Selectors;
using UnityEditor;

namespace Base.AudioSystem.Editor
{
    [CustomPropertyDrawer(typeof(AudioTagAttribute))]
    public class AudioTagDrawer : BasePropertyDrawer<AudioTag>
    {}
}