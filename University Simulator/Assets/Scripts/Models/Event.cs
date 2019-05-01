public struct Event {
    public string text;
    public Resources modifiers;

    public Event(string text, Resources modifiers = new Resources()) {
        this.text = text;
        this.modifiers = modifiers;
    }
}
