using Editor.Selectors;
using UnityEditor;

namespace AudioSystem.Editor
{
    [CustomPropertyDrawer(typeof(AudioTagAttribute))]
    public class AudioTagDrawer : BasePropertyDrawer<AudioTag>
    {}
}