
public class Label : Component
{
    protected string text = "";

    public string Text 
    {
        get { return text; }
        set { text = value; }
    }

    public Label() : base()
    {
        type = ComponentType.LABEL;
    }

    public Label(string text, string id = "") : base(id.Length == 0 ? text : id)
    {
        this.text = text;
        type = ComponentType.LABEL;
    }
}