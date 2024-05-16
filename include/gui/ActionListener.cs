public interface IActionListener
{
    public void ActionPerformed(Button button, int key);
}

public class ActionListener : IActionListener
{
    public delegate void Action(Button button, int key);

    private Action action;

    public ActionListener()
    {
        action = (button, key) => { };
    }

    public ActionListener(Action action)
    {
        this.action = action;
    }

    public void ActionPerformed(Button button, int key) 
    {
        if (action != null) action(button, key);
    }
}