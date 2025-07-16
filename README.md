
# TransConnect

> A C# console application for managing your society.

## Install

**Requierements**
- .NET Framework (version > 8.0)

```bash
git clone https://github.com/mailittlepony/transconnect.git
cd transconnect
dotnet run
```

## Code description

### GUI library

First, we need to write a library for Graphic User Interface. We were inspired by Java JWT library.

We have two base class, Panel and Component. Panel has a list of Component.

Components can be a:

- Label: *simple text (inherits from Component)*
- Button: *start an action on click (inherits from Label)*
- TextInput: *wait for a user input and start an aciton (inherits from Button)*

#### Label

Label is just a class with a string as a field.

#### Button

Button has an object ActionListener that is just a class with a overridable method "ActionPerformed" passable as constructor argument.

When a button is pushed, the method of its ActionListener is called.

The panel that instantiate a button can inherits from IActionListener and can override "ActionPerformed" in the class. Then, the panel can be passed as ActionListener into the button.

All buttons from the panels lauch the same function "ActionPerformed", so in order to recognize the source button, this last one is passed throught the method and then we could for exemple write a condition on the button id.

#### TextInput

TextInput is just a button with a default callback called before the method passed into ActionListener.

This default callback just move the cursor next to the button, get the user input, and then launch "ActionPerformed" to execute some action with the specified input.

#### Panel

The main goal of Panel is to display all the components line by line and handle user key input (scroll and select).

### TransConnect

#### Clients module

A panel that allow to add or edit clients.

#### Employees module

A panel that allow to add or edit employees.

Moreover, it is possible to display the organigram of all the employees of the society.

#### Orders module

A panel that allow to make or edit orders.

An order need to have a departure and an destination. Then, the shortest road had to be found in order to calculate the transport fees. We use Dijkstra algorithm coded with the help of C# PriorityQueue.

### Statistics module

A panel that display some pertinents Statistics about the society.

