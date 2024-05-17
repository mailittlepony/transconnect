// Enumeration for different types of components
public enum ComponentType
{
    VOID,        
    LABEL,       
    BUTTON,      
    TEXTINPUT,   
}

// Base class for UI components
public class Component
{
    protected string id = "";          // Unique identifier for the component
    protected int lineIndex = 0;       // Index of the component's line in the panel
    protected bool selectable;         // Indicates if the component can be selected
    protected ComponentType type = ComponentType.VOID; // Type of the component

    // Property for getting and setting the component's ID
    public string Id 
    { 
        get { return id; } 
        set { id = value; } 
    }

    // Property for checking if the component is selectable
    public bool Selectable 
    { 
        get { return selectable; } 
    }

    // Property for getting the component's type
    public ComponentType Type
    { 
        get { return type; } 
    }

    // Property for getting and setting the component's line index
    public int LineIndex
    { 
        get { return lineIndex; } 
        set { lineIndex = value; } 
    }

    // Default constructor
    public Component() 
    {
        
    }

    // Constructor that initializes the component with a specific ID
    public Component(string id)
    {
        this.id = id; // Set the component's ID
    } 
}
