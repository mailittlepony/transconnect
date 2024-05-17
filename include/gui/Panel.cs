using System.Security.Cryptography.X509Certificates;

public class Panel
{
    public bool isDisplayed = true;

    protected List<Component> components = new List<Component>();
    protected int selectedLine = 0;
    public static Panel? displayedPanel;
    public string title;

    public int SelectedLine
    {
        get { return selectedLine; }
        set { selectedLine = value; }
    }

    public Panel(string title = "")
    {
        this.title = title;
    }

    public void Add(Component component)
    {
        component.LineIndex = components.Count;
        components.Add(component);
    }

    public void Remove(int line)
    {
        components.RemoveAt(line);
    }

    public void Display()
    {
        Console.Clear();
        Console.CursorVisible = false;
        ScrollUp();
        ScrollDown();
        Console.WriteLine(title + "\n");
        foreach ( Component component in components )
        {
            Label label = (Label)component;
            if (label.Type != ComponentType.BUTTON)
            {
                Console.WriteLine( "   " + label.Text);
            }
            else
            {
                Console.WriteLine((label.LineIndex == selectedLine ? "-> " : "   ") + label.Text);
            }
        }
    }

    public virtual void Update()
    {
        ConsoleKey key = Console.ReadKey().Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                ScrollUp();
                break;
            case ConsoleKey.DownArrow:
                ScrollDown();
                break;
            case ConsoleKey.Enter:
                if (components[selectedLine].Type == ComponentType.BUTTON)
                {
                    Button button = (Button)components[selectedLine];
                    button.ActionPerformed(button, (int)ConsoleKey.Enter);
                }
                break;
        }
    }

    private void ScrollUp()
    {
        if (!components.Any( c => { return c.Type == ComponentType.BUTTON; })) return;
        selectedLine = (selectedLine + components.Count - 1) % components.Count;
        if (components[selectedLine].Type != ComponentType.BUTTON) ScrollUp();
    }

    private void ScrollDown()
    {
        if (!components.Any( c => { return c.Type == ComponentType.BUTTON; })) return;
        selectedLine = (selectedLine + 1) % components.Count;
        if (components[selectedLine].Type != ComponentType.BUTTON) ScrollDown();
    }

    public void Run()
    {
        while(isDisplayed)
        {
            Display();
            Update();
        }
    }


    public static void Display(Panel panel)
    {
        if (displayedPanel != null) displayedPanel.isDisplayed = false;
        displayedPanel = panel;
        panel.Run();
    }
}
