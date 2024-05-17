// Class representing a text input component, derived from the Button class
public class TextInput : Button
{
    private string defaultValue = ""; 
    private string output = "";      
    private string name = "";         

    // Property for getting and setting the default value
    public string DefaultValue
    {
        get { return defaultValue; }
        set { defaultValue = value; }
    }

    // Property for getting and setting the name of the text input
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Property for getting the output value
    public string Output 
    {
        get { return output; }
    }

    // Default constructor
    public TextInput()
    {
        type = ComponentType.BUTTON; // Set the component type to BUTTON
    }

    // Constructor that initializes the text input with specific values
    public TextInput(string name, string defaultValue, IActionListener actionListener, string id = "") 
        : base(name + defaultValue, actionListener, id)
    {
        this.name = name; // Set the name of the text input
        this.defaultValue = defaultValue; // Set the default value
        type = ComponentType.BUTTON; // Set the component type to BUTTON
    }

    // Override method to handle the action performed on the text input
    override public void ActionPerformed(Button button, int key)
    {
        text = name; // Reset the text to the name
        Console.Clear(); // Clear the console
        if (Panel.displayedPanel != null) Panel.displayedPanel.Display(); // Redisplay the panel

        Console.CursorVisible = true; // Make the cursor visible
        int x = Console.GetCursorPosition().Left; // Get the current cursor position
        int y = Console.GetCursorPosition().Top;
        x += text.Length + 3; // Adjust the x position for input
        y = lineIndex + 2; // Adjust the y position based on the line index
        Console.SetCursorPosition(x, y); // Set the cursor position for input
        string? str = Console.ReadLine(); // Read input from the user
        output = str == null ? "" : str; // Set the output value
        text = name + output; // Update the text with the output value
        Console.CursorVisible = false; // Hide the cursor

        actionlistener.ActionPerformed(button, key); // Perform the action listener's action
    }
}
