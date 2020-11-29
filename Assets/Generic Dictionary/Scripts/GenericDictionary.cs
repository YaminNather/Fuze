using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

/// <summary>
/// Generic Serializable Dictionary for Unity 2020.1.
/// Simply declare your key/value types and you're good to go - zero boilerplate.
/// </summary>
[Serializable]
public class GenericDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    // Since lists can be serialized natively by unity no custom implementation is needed
    public void OnBeforeSerialize() { }

    // Fill dictionary with list pairs and flag key-collisions.
    public void OnAfterDeserialize()
    {
        dict.Clear();
        indexByKey.Clear();
        keyCollision = false;

        for (int i = 0; i < list.Count; i++)
        {
            var key = list[i].Key;
            if (key != null && !ContainsKey(key))
            {
                dict.Add(key, list[i].Value);
                indexByKey.Add(key, i);
            }
            else
            {
                keyCollision = true;
            }
        }
    }

    // IDictionary
    public TValue this[TKey key]
    {
        get => dict[key];
        set
        {
            dict[key] = value;

            if (indexByKey.ContainsKey(key))
            {
                var index = indexByKey[key];
                list[index] = new KeyValuePair(key, value);
            }
            else
            {
                list.Add(new KeyValuePair(key, value));
                indexByKey.Add(key, list.Count - 1);
            }
        }
    }

    public ICollection<TKey> Keys => dict.Keys;
    public ICollection<TValue> Values => dict.Values;

    public void Add(TKey key, TValue value)
    {
        dict.Add(key, value);
        list.Add(new KeyValuePair(key, value));
        indexByKey.Add(key, list.Count - 1);
    }

    public bool ContainsKey(TKey key) => dict.ContainsKey(key);

    public bool Remove(TKey key)
    {
        if (dict.Remove(key))
        {
            var index = indexByKey[key];
            list.RemoveAt(index);
            indexByKey.Remove(key);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryGetValue(TKey key, out TValue value) => dict.TryGetValue(key, out value);

    // ICollection
    public int Count => dict.Count;
    public bool IsReadOnly { get; set; }

    public void Add(KeyValuePair<TKey, TValue> pair)
    {
        Add(pair.Key, pair.Value);
    }

    public void Clear()
    {
        dict.Clear();
        list.Clear();
        indexByKey.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> pair)
    {
        TValue value;
        if (dict.TryGetValue(pair.Key, out value))
        {
            return EqualityComparer<TValue>.Default.Equals(value, pair.Value);
        }
        else
        {
            return false;
        }
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentException("The array cannot be null.");
        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
        if (array.Length - arrayIndex < dict.Count)
            throw new ArgumentException("The destination array has fewer elements than the collection.");

        foreach (var pair in dict)
        {
            array[arrayIndex] = pair;
            arrayIndex++;
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> pair)
    {
        TValue value;
        if (dict.TryGetValue(pair.Key, out value))
        {
            bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
            if (valueMatch)
            {
                return Remove(pair.Key);
            }
        }
        return false;
    }

    // IEnumerable
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dict.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => dict.GetEnumerator();

    // Internal
    [SerializeField]
    List<KeyValuePair> list = new List<KeyValuePair>();
    [SerializeField]
    Dictionary<TKey, int> indexByKey = new Dictionary<TKey, int>();
    [SerializeField, HideInInspector]
    Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

#pragma warning disable 0414
    [SerializeField, HideInInspector]
    bool keyCollision;
#pragma warning restore 0414

    // Serializable KeyValuePair struct
    [Serializable]
    struct KeyValuePair
    {
        public TKey Key;
        public TValue Value;

        public KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}


/// <summary>
/// Draws the generic dictionary a bit nicer than Unity would natively (not as many expand-arrows
/// and better spacing between KeyValue pairs). Also renders a warning-box if there are duplicate
/// keys in the dictionary.
/// </summary>
///
/// 
[CustomPropertyDrawer(typeof(GenericDictionary<,>))]
public class GenericDictionaryPropertyDrawer : PropertyDrawer
{
    static float lineHeight = EditorGUIUtility.singleLineHeight;
    static float vertSpace = EditorGUIUtility.standardVerticalSpacing;
    static float combinedPadding = lineHeight + vertSpace;
    int valuePropCount;

    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        // Setup variables used for drawing.
        var currentPos = new Rect(lineHeight, pos.y, pos.width, lineHeight);
        bool isExpanded = false;
        var propCopy = property.Copy();
        var enumerator = property.GetEnumerator();

        // Iterate properties and draw them.
        while (enumerator.MoveNext())
        {
            var currentProp = ((SerializedProperty)enumerator.Current);
            if (currentProp.name == "list")
            {
                // Draw list header and expand arrow.
                string fieldName = ObjectNames.NicifyVariableName(fieldInfo.Name);
                EditorGUI.PropertyField(currentPos, currentProp, new GUIContent(fieldName));
                isExpanded = currentProp.isExpanded;
            }
            else if (currentProp.name == "size" && isExpanded)
            {
                // Draw size property.
                EditorGUI.indentLevel++;
                currentPos = new Rect(currentPos.x, currentPos.y + lineHeight, pos.width, lineHeight);
                EditorGUI.PropertyField(currentPos, currentProp, new GUIContent("Size"));
                currentPos.y += vertSpace;
            }
            else if (isExpanded && (currentProp.name == "Key" || currentProp.name == "Value"))
            {
                // Setup position and draw KeyValue-properties.
                var entryPos = new Rect(currentPos.x, currentPos.y + combinedPadding, pos.width, lineHeight);
                if (currentProp.isExpanded)
                {
                    currentPos.y += valuePropCount * combinedPadding;
                }
                EditorGUI.PropertyField(entryPos, currentProp, new GUIContent(currentProp.name), currentProp.isExpanded);

                // Add padding.
                if (currentProp.name == "Value")
                {
                    currentPos.y += combinedPadding + vertSpace;
                }
                else
                {
                    currentPos.y += lineHeight + vertSpace;
                }
            }
        }

        // Draw key collision warning box.
        bool keyCollision = propCopy.FindPropertyRelative("keyCollision").boolValue;
        if (keyCollision)
        {
            var entryPos = new Rect(lineHeight, currentPos.y + combinedPadding, pos.width, lineHeight * 2f);
            EditorGUI.HelpBox(entryPos, "Duplicate keys will not be serialized.", MessageType.Warning);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Make copies of property so we can iterate it more than once
        var listCopy = property.Copy();
        var valueCopy = property.Copy();

        // Count number of data-properties and expanded value-properties.
        int listCount = 0;
        int expandedCount = 0;
        var enumerator = property.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentProp = ((SerializedProperty)enumerator.Current);
            if (currentProp.name == "data")
            {
                listCount++;
            }
            else if (currentProp.isExpanded && currentProp.name == "Value")
            {
                expandedCount++;
            }
        }

        // Count number of sub-properties inside Value-property.
        enumerator = valueCopy.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentProp = ((SerializedProperty)enumerator.Current);
            if (currentProp.name == "Value" && currentProp.isExpanded)
            {
                valuePropCount = currentProp.CountInProperty() - 1;
                break;
            }
        }

        // Accumulate height for all properties.
        float totHeight = 0f;

        // Height of key collision warning.
        bool keyCollision = listCopy.FindPropertyRelative("keyCollision").boolValue;
        if (keyCollision)
        {
            totHeight += lineHeight * 2f + vertSpace;
        }

        // Height of KeyValue list.
        var listProp = listCopy.FindPropertyRelative("list");
        if (listProp.isExpanded)
        {
            var t = EditorGUI.GetPropertyHeight(listProp, false);
            totHeight += lineHeight * 2f + vertSpace;  // list header and size fields
            totHeight += listCount * 2f * combinedPadding + listCount * vertSpace;  // list contents fields
            totHeight += expandedCount * valuePropCount * combinedPadding;  // expanded value fields
            return totHeight;
        }
        else
        {
            return totHeight + lineHeight;
        }
    }
}