/*
 * A display that uses System.Console to output a frame.
 */
public class ConsoleDisplay : IDisplay {
    /*
     * Display a frame.
     *
     * Parameter:
     *   fbuf: The frame buffer to display.
     */
    public void Display(FrameBuffer fbuf) {
        fbuf.Traverse((x, y, isOn) => {
            if (isOn) Console.Write('#');
	    else Console.Write(' ');
	    if (x == fbuf.Width - 1) Console.WriteLine();
	});
	Console.WriteLine();
    }
}
