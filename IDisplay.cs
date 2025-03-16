/*
 * An interface for displays that outputs frames.
 */
public interface IDisplay {
    public const int DISPLAY_WIDTH = 64;
    public const int DISPLAY_HEIGHT = 32;

    /*
     * Display a frame.
     *
     * Parameter:
     *   fbuf: The frame buffer to display.
     */
    public void Display(FrameBuffer fbuf);
}
