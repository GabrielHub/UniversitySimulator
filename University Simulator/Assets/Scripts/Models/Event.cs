public struct Event {
    public string text;
    public Resources modifiers;

    public Event(string text, Resources modifiers = null) {
        this.text = text;
        this.modifiers = new Resources();
    }
}