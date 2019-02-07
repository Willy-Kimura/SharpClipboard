# SharpClipboard
**SharpClipboard** is a clipboard-monitoring library for .NET that listens to the system's clipboard entries,
allowing developers to tap into the rich capabilities of determining the clipboard's contents at runtime.

Here's a screenshot of the library's features:

![sc-preview-01](/Assets/sharpclipboard-preview-01.png)

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
# Usage
If you prefer working with the Designer, simply add the library to Visual Studio's Toolbox and use the
*Properties* window to change its options:

![sc-preview-02](/Assets/sharpclipboard-preview-02.png)

To use it in code, first import `WK.Libraries.SharpClipboardNS` - the code below will then assist you: 
```c#
    var clipboard = new SharpClipboard();

    // Attach your code to the ClipboardChanged event to listen to cuts/copies.
    clipboard.ClipboardChanged += ClipboardChanged;
    
    private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
    {
        // Check if the content copied is of text type...
        if (e.ContentType == SharpClipboard.ContentTypes.Text)
        {
            // Get the cut/copied text.
            // You can also use 'e.Content' though for Images
            // and Files content-types you'll need to cast it.
            Debug.WriteLine(clipboard.ClipboardText);
        }

        // Check if the content copied is of image type...
        else if (e.ContentType == SharpClipboard.ContentTypes.Image)
        {
            // Add a picturebox and uncomment the line below.
            // pictureBox1.Image = clipboard.ClipboardImage;
        }

        // Check if the content copied is of file type...
        else if (e.ContentType == SharpClipboard.ContentTypes.Files)
        {
            Debug.WriteLine(clipboard.ClipboardFiles.ToArray());

            // Or you can also use 'ClipboardFile' to get the
            // first file copied to the clipboard.
            Debug.WriteLine(clipboard.ClipboardFile);
        }
    }
```
