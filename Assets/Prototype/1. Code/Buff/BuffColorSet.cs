using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Color", menuName = "ScriptableObject/Buff/Buff Color")]
public class BuffColorSet : ScriptableObject
{
    [field: SerializeField] public Color normalColor { get; private set; }
    [field: SerializeField] public Color highlightedColor { get; private set; }
    [field: SerializeField] public Color pressedColor { get; private set; }
    [field: SerializeField] public Color textColor { get; private set; }
}
