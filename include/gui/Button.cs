// Class representing a button component, derived from the Label class and implementing IActionListener
public class Button : Label, IActionListener
{
   protected IActionListener actionlistener = new ActionListener(); // Instance of the action listener

   // Property for getting and setting the action listener
   public IActionListener Actionlistener
   {
      get { return actionlistener; }
      set { actionlistener = value; }
   }

   // Default constructor
   public Button()
   {
      type = ComponentType.BUTTON; // Set the component type to BUTTON
   }

   // Constructor that initializes the button with specific text, action listener, and optional ID
   public Button(string text, IActionListener actionListener, string id = "") : base(text, id)
   {
      this.actionlistener = actionListener; // Set the action listener
      type = ComponentType.BUTTON; // Set the component type to BUTTON
   }

   // Method to handle the action performed on the button
   public virtual void ActionPerformed(Button button, int key)
   {
      actionlistener.ActionPerformed(button, key); // Delegate the action to the action listener
   }
}
