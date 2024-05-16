
public enum ComponentType
{
    VOID,
    LABEL,
    BUTTON,
    TEXTINPUT,
}
public class Component
{
    protected string id = "";
    protected int lineIndex = 0;
    protected bool selectable;
    protected ComponentType type = ComponentType.VOID;

    public string Id 
    { 
        get { return id; } 
        set { id = value; } 
    }

    public bool Selectable 
    { 
        get { return selectable; } 
    }

    public ComponentType Type
    { 
        get { return type; } 
    }

    public int LineIndex
    { 
        get { return lineIndex; } 
        set { lineIndex = value; } 
    }

    public Component() 
    {
        
    }
    public Component(string id)
    {
        this.id = id;
    } 
}