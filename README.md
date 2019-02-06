# SharpClipboard
**SharpClipboard** is a clipboard-monitoring library for .NET that listens to the system's clipboard entries,
allowing developers to tap into the rich capabilities of determining the clipboard's contents at runtime.

Here's a screenshot of the library's features:

![sc-preview](/Assets/sharpclipboard-preview-01.png)

# Installation
To install, simply [download](https://github.com/Willy-Kimura/SharpClipboard/releases/download/v1.0.3/SharpClipboard.dll) the library and add it to Visual Studio's Toolbox.

*Nuget version coming soon :)*

# Features
Here's a comprehensive list of the features available:
- Built as a component making it accessible in Design Mode.
- Silently monitors the system clipboard uninterrupted; can also be disabled while running.
- Ability to detect clipboard content in various formats, namely **text**, **images**, and **files**.
- Option to control the type of content to be monitored, e.g. **text** only, **text** and **images** only.
- Ability to capture the background application's details from where the clipboard's contents were cut/copied.
  *For example:*
```c#
private void ClipboardChanged(Object sender, SharpClipboard.ClipboardChangedEventArgs e)
{
    // Gets the application's executable name.
    Debug.WriteLine(e.SourceApplication.Name);
    // Gets the application's window title.
    Debug.WriteLine(e.SourceApplication.Title);
    // Gets the application's process ID.
    Debug.WriteLine(e.SourceApplication.ID.ToString());
    // Gets the application's executable path.
    Debug.WriteLine(e.SourceApplication.Path);
}
```
