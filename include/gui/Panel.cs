public class Panel
{
    public bool isDisplayed = true; // Flag to determine if the panel is currently being displayed

    protected List<Component> components = new List<Component>(); // List of components in the panel
    protected int selectedLine = 0; // Index of the currently selected line (component)
    public static Panel? displayedPanel; // Static reference to the currently displayed panel
    public string title; // Title of the panel

    // Property for getting and setting the selected line
    public int SelectedLine
    {
        get { return selectedLine; }
        set { selectedLine = value; }
    }

    // Constructor to initialize the panel with an optional title
    public Panel(string title = "")
    {
        this.title = title;
    }

    // Method to add a component to the panel
    public void Add(Component component)
    {
        component.LineIndex = components.Count; // Set the line index of the component
        components.Add(component); // Add the component to the list
    }

    // Method to remove a component from the panel by its line index
    public void Remove(int line)
    {
        components.RemoveAt(line); // Remove the component at the specified index
    }

    // Method to display the panel
    public void Display()
    {
        Console.Clear(); // Clear the console
        Console.CursorVisible = false; // Hide the cursor
        ScrollUp(); // Scroll up to ensure the correct selected line is displayed
        ScrollDown(); // Scroll down to ensure the correct selected line is displayed
        Console.WriteLine(title + "\n"); // Print the title of the panel
        foreach (Component component in components) // Iterate through each component
        {
            Label label = (Label)component; // Cast the component to a Label
            if (label.Type != ComponentType.BUTTON)
            {
                // Display non-button components without an arrow
                Console.WriteLine("   " + label.Text);
            }
            else
            {
                // Display button components with an arrow if selected
                Console.WriteLine((label.LineIndex == selectedLine ? "-> " : "   ") + label.Text);
            }
        }
    }

    // Virtual method to update the panel based on user input
    public virtual void Update()
    {
        ConsoleKey key = Console.ReadKey().Key; // Get the key pressed by the user

        switch (key) // Handle different key inputs
        {
            case ConsoleKey.UpArrow:
                ScrollUp(); // Scroll up if the up arrow key is pressed
                break;
            case ConsoleKey.DownArrow:
                ScrollDown(); // Scroll down if the down arrow key is pressed
                break;
            case ConsoleKey.Enter:
                if (components[selectedLine].Type == ComponentType.BUTTON)
                {
                    // Perform the button's action if the enter key is pressed on a button
                    Button button = (Button)components[selectedLine];
                    button.ActionPerformed(button, (int)ConsoleKey.Enter);
                }
                break;
        }
    }

    // Method to scroll up through the components
    private void ScrollUp()
    {
        // Check if there are any buttons in the components list
        if (!components.Any(c => c.Type == ComponentType.BUTTON)) return;
        selectedLine = (selectedLine + components.Count - 1) % components.Count; // Move the selected line up
        if (components[selectedLine].Type != ComponentType.BUTTON) ScrollUp(); // Skip non-button components
    }

    // Method to scroll down through the components
    private void ScrollDown()
    {
        // Check if there are any buttons in the components list
        if (!components.Any(c => c.Type == ComponentType.BUTTON)) return;
        selectedLine = (selectedLine + 1) % components.Count; // Move the selected line down
        if (components[selectedLine].Type != ComponentType.BUTTON) ScrollDown(); // Skip non-button components
    }

    // Method to run the panel display and update loop
    public void Run()
    {
        while (isDisplayed) // Continue running while the panel is displayed
        {
            Display(); // Display the panel
            Update(); // Update the panel based on user input
        }
    }

    // Static method to display a specified panel
    public static void Display(Panel panel)
    {
        if (displayedPanel != null) displayedPanel.isDisplayed = false; // Hide the currently displayed panel
        displayedPanel = panel; // Set the new panel as the displayed panel
        panel.Run(); // Run the new panel
    }
}