public struct Event {
    public enum Type { Random, Feature, GameState, Narrative, Notification }

    public string text;
    public Resources modifiers;
    public Type type;

    public Event(string text, Type type, Resources modifiers = null) {
        this.text = text;
        this.type = type; //5 types of events: Random, Feature, GameState, Narrative, Notification
        this.modifiers = new Resources();
    }
}
