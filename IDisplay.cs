/*
 * An interface for displays that outputs frames.
 */
public interface IDisplay {
    /*
     * Display a frame.
     *
     * Parameter:
     *   fbuf: The frame buffer to display.
     */
    public void Display(FrameBuffer fbuf);
}
