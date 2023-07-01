using Code.Gameplay.Configuration.Animation;
using UnityEditor;

[CustomEditor(typeof(DirectionAnimationConfig))]
public class DirectionAnimationConfigEditor : Editor
{
    private DirectionAnimationConfig _config;

    private void OnEnable()
    {
        _config = (DirectionAnimationConfig)target;
        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        _config.Type = (DirectionAnimationType)EditorGUILayout.EnumPopup(
            "Type",
            _config.Type);
        
        switch (_config.Type)
        {
            case DirectionAnimationType.OneSide:
                _config.Anim1 = EditorGUILayout.TextField("Anim", _config.Anim1);
                break;
            case DirectionAnimationType.TwoSide:
                _config.Anim1 = EditorGUILayout.TextField("Positive Anim", _config.Anim1);
                _config.Anim2 = EditorGUILayout.TextField("Negative Anim", _config.Anim2);
                break;
            case DirectionAnimationType.FourSide:
                _config.Anim1 = EditorGUILayout.TextField("Up Anim", _config.Anim1);
                _config.Anim2 = EditorGUILayout.TextField("Down Anim", _config.Anim2);
                _config.Anim3 = EditorGUILayout.TextField("Left Anim", _config.Anim3);
                _config.Anim4 = EditorGUILayout.TextField("Right Anim", _config.Anim4);
                break;
        }
        
    }
}

