public class TextInput : Button
{
    private string defaultValue = "";
    private string output = "";
    private string name = "";

    public string DefaultValue
    {
        get { return defaultValue; }
        set { defaultValue = value; }
    }

    public string Output 
    {
        get { return output; }
    }

    public TextInput()
    {
        type = ComponentType.BUTTON;
    }

    public TextInput(string name, string defaultValue, IActionListener actionListener, string id = "") : base (name + defaultValue, actionListener, id)
    {
        this.name = name;
        this.defaultValue = defaultValue;
        type = ComponentType.BUTTON;
    }

    override public void ActionPerformed(Button button, int key)
    {
        text = name;
        Console.Clear();
        if (Panel.displayedPanel != null) Panel.displayedPanel.Display();

        int x = Console.GetCursorPosition().Left;
        int y = Console.GetCursorPosition().Top;
        x+= text.Length + 3;
        y = lineIndex + 2;
        Console.SetCursorPosition(x,y);
        string? str = Console.ReadLine();
        output = str == null ? "" : str;
        text = name + output;

        actionlistener.ActionPerformed(button, key);
    }
}