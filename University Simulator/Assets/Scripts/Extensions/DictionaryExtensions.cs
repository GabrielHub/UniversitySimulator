using System.Collections.Generic;
using System;
using UnityEngine;

public static class DictionaryExtensions {
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
        TValue value;
        return dictionary.TryGetValue(key, out value) ? value : defaultValue;
    }

    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueProvider) {
        TValue value;
        return dictionary.TryGetValue(key, out value) ? value : defaultValueProvider();
    }
}


[Serializable]
public class GameStateDictionary: SerializableDictionary<GameState.State, bool> { }

[Serializable]
public class EventTypeColorDictionary: SerializableDictionary<Event.Type, Color> { }
