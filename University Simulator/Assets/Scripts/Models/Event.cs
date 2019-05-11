public struct Event {
    public string text;
    public Resources modifiers;
    public string type;

    public Event(string text, string type, Resources modifiers = null) {
        this.text = text;
        this.type = type; //5 types of events: Random, Feature, GameState, Narrative, Notification
        this.modifiers = new Resources();
    }
}