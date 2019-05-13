using UnityEditor;

[CustomPropertyDrawer(typeof(GameStateDictionary))]
public class GameStateDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(EventTypeColorDictionary))]
public class EventTypeColorDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
