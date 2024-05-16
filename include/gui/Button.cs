public class Button : Label, IActionListener
{
   protected IActionListener actionlistener = new ActionListener();

   public IActionListener Actionlistener
   {
      get { return actionlistener; }
      set { actionlistener = value; }
   }
   public Button()
   {
      type = ComponentType.BUTTON;
   }

   public Button(string text, IActionListener actionListener, string id = "") : base(text, id)
   {
      this.actionlistener = actionListener;
      type = ComponentType.BUTTON;
   }

   public virtual void ActionPerformed(Button button, int key)
   {
      actionlistener.ActionPerformed(button, key);
   }
}