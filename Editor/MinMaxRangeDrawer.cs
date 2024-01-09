// NOTE: Put in a Editor folder. //

// Originally created by GucioDevs
// Modified by Kiltec

using UnityEngine;
using UnityEditor;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
    public class MinMaxRangeDrawer : PropertyDrawer
    {
        float floatVal;
        int intVal;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minMaxAttribute = (MinMaxRangeAttribute)attribute;
            var propertyType = property.propertyType;
            var rect = position;

            if (propertyType == SerializedPropertyType.Vector3 || propertyType == SerializedPropertyType.Vector3Int)
            {
                position = new Rect(position.x, position.y, position.width - 40, position.height);
            }

            if (propertyType == SerializedPropertyType.Vector2 || propertyType == SerializedPropertyType.Vector3)
            {
                label.tooltip = $"Range (x,y): {minMaxAttribute.min:F2}f to {minMaxAttribute.max:F2}f";
                if (propertyType == SerializedPropertyType.Vector3) { label.tooltip += $"\nValue (z): {property.vector3Value.z:F2}f"; }
            }
            else
            {
                label.tooltip = $"Range (x,y): {minMaxAttribute.min} to {minMaxAttribute.max}";
                if (propertyType == SerializedPropertyType.Vector3Int) { label.tooltip += $"\nValue (z): {property.vector3IntValue.z}"; }
            }

            Rect controlRect = EditorGUI.PrefixLabel(position, label);
            Rect[] splittedRect = SplitRect(controlRect, 3);

            if (propertyType == SerializedPropertyType.Vector2 || propertyType == SerializedPropertyType.Vector3)
            {
                EditorGUI.BeginChangeCheck();

                Vector3 vector = Vector3.zero;

                if (propertyType == SerializedPropertyType.Vector2) { vector = (Vector3)property.vector2Value; }
                else if (propertyType == SerializedPropertyType.Vector3)
                {
                    vector = property.vector3Value;
                    floatVal = property.vector3Value.z;
                    floatVal = EditorGUI.FloatField(new Rect(rect.width - 17, rect.y, 35, rect.height), floatVal);
                }

                float minVal = vector.x;
                float maxVal = vector.y;

                minVal = EditorGUI.FloatField(splittedRect[0], float.Parse(minVal.ToString("F2")));
                maxVal = EditorGUI.FloatField(splittedRect[2], float.Parse(maxVal.ToString("F2")));

                EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, minMaxAttribute.min, minMaxAttribute.max);

                if (minVal < minMaxAttribute.min) { minVal = minMaxAttribute.min; }
                if (maxVal > minMaxAttribute.max) { maxVal = minMaxAttribute.max; }

                if (floatVal < minVal) { floatVal = minVal; }
                if (floatVal > maxVal) { floatVal = maxVal; }

                vector = (Vector3)new Vector2(minVal > maxVal ? maxVal : minVal, maxVal);

                if (EditorGUI.EndChangeCheck())
                {
                    if (propertyType == SerializedPropertyType.Vector2) { property.vector2Value = vector; }
                    else if (propertyType == SerializedPropertyType.Vector3) { property.vector3Value = new Vector3(vector.x, vector.y, floatVal); }
                }
            }
            else if (propertyType == SerializedPropertyType.Vector2Int || propertyType == SerializedPropertyType.Vector3Int)
            {
                EditorGUI.BeginChangeCheck();

                Vector3Int vector = Vector3Int.zero;

                if (propertyType == SerializedPropertyType.Vector2Int) { vector = (Vector3Int)property.vector2IntValue; }
                else if (propertyType == SerializedPropertyType.Vector3Int)
                {
                    vector = property.vector3IntValue;
                    intVal = property.vector3IntValue.z;
                    intVal = EditorGUI.IntField(new Rect(rect.width - 17, rect.y, 35, rect.height), intVal);
                }

                float minVal = vector.x;
                float maxVal = vector.y;

                minVal = EditorGUI.FloatField(splittedRect[0], minVal);
                maxVal = EditorGUI.FloatField(splittedRect[2], maxVal);

                EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, minMaxAttribute.min, minMaxAttribute.max);

                if (minVal < minMaxAttribute.min) { maxVal = minMaxAttribute.min; }
                if (minVal > minMaxAttribute.max) { maxVal = minMaxAttribute.max; }

                if (intVal < minVal) { intVal = (int)minVal; }
                if (intVal > maxVal) { intVal = (int)maxVal; }

                vector = (Vector3Int)new Vector2Int(Mathf.FloorToInt(minVal > maxVal ? maxVal : minVal), Mathf.FloorToInt(maxVal));

                if (EditorGUI.EndChangeCheck())
                {
                    if (propertyType == SerializedPropertyType.Vector2Int) { property.vector2IntValue = (Vector2Int)vector; }
                    else if (propertyType == SerializedPropertyType.Vector3Int) { property.vector3IntValue = new Vector3Int(vector.x, vector.y, intVal); }
                }
            }
        }

        Rect[] SplitRect(Rect rectToSplit, int n)
        {
            Rect[] rects = new Rect[n];

            for (int i = 0; i < n; i++)
            {
                rects[i] = new Rect(rectToSplit.position.x + (i * rectToSplit.width / n), rectToSplit.position.y, rectToSplit.width / n, rectToSplit.height);
            }

            int padding = (int)rects[0].width - 40;
            int space = 5;

            rects[0].width -= padding + space;
            rects[2].width -= padding + space;

            rects[1].x -= padding;
            rects[1].width += padding * 2;

            rects[2].x += padding + space;

            return rects;
        }
    }
}