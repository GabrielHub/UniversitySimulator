using UnityEditor;

[CustomPropertyDrawer(typeof(GameStateBoolDictionary))]
public class GameStateBoolDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

[CustomPropertyDrawer(typeof(GameStateStringDictionary))]
public class GameStateStringDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

[CustomPropertyDrawer(typeof(EventTypeColorDictionary))]
public class EventTypeColorDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
