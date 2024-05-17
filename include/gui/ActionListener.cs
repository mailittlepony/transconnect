// Interface for handling action events
public interface IActionListener
{
    // Method to be called when an action is performed on a button
    public void ActionPerformed(Button button, int key);
}

// Implementation of the IActionListener interface
public class ActionListener : IActionListener
{
    // Delegate type for handling actions
    public delegate void Action(Button button, int key);

    // Instance of the delegate to store the action
    private Action action;

    // Default constructor initializing the action with an empty delegate
    public ActionListener()
    {
        action = (button, key) => { }; // Default action does nothing
    }

    // Constructor that accepts a specific action delegate
    public ActionListener(Action action)
    {
        this.action = action; // Set the action to the provided delegate
    }

    // Method to perform the action when an event occurs
    public void ActionPerformed(Button button, int key) 
    {
        if (action != null) action(button, key); // Invoke the action if it's not null
    }
}
