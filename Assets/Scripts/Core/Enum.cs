using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class EnumFlags: PropertyAttribute{}

[CustomPropertyDrawer(typeof(EnumFlags))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
    }
}
#endif

/// <summary>
/// 人物状态
/// </summary>
public enum ECharacterStatus
{
    Dizzy=1,
    MoveSpeedUp=2,
    MoveSpeedDown=4,
    AtkUp=8,
    AtkDown=16,
    AtkSpeedUp=32,
    AtkSpeedDown=64,
    Cured=128,
    Burned=256
    
    //VulnerableMagical=32,
    //VulnerablePhysical=64,

}

public enum ETowerStatus
{
    Dizzy=1,
    RadiusUp=2,
    RadiusDown=4,
    AtkUp=8,
    AtkDown=16,
    AtkSpeedUp=32,
    AtkSpeedDown=64,
}

public enum EDamageType
{
    Physical,
    Magic,
    True
}

public enum EEnemyType
{
    Summon,
    Minion,
    Elite,
    Boss
}
