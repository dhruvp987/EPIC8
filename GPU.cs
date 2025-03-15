/*
 * Provide the emulator's graphical capabilities.
 */
public class GPU {
    FrameBuffer buf;

    public int FrameBufWidth  { get => buf.Width;  }
    public int FrameBufHeight { get => buf.Height; }

    /*
     * Create a new GPU.
     *
     * Parameters:
     *   resWidth: The width of the display resolution
     *   resHeight: The height of the display resolution
     */
    public GPU(int resWidth, int resHeight) {
        buf = new FrameBuffer(resWidth, resHeight);
    }

    /*
     * Set a pixel on (true) or off (false).
     *
     * Parameters:
     *   x: The pixel's x index
     *   y: The pixel's y index
     *   isOn: Whether the pixel is on or off
     */
    public void Set(int x, int y, bool isOn) {
        buf.Set(x, y, isOn);
    }

    /*
     * Run a kernel on the GPU's frame buffer.
     *
     * Parameter:
     *   fun: The kernel to run on the frame buffer. It takes
     *        three args: x-cor, y-cor, and the on/off state of that
     *        pixel. It returns the new on/off state of the pixel
     */
    public void RunKernel(Func<int, int, bool, bool> fun) {
        for (int y = 0; y < buf.Height; y++) {
            for (int x = 0; x < buf.Width; x++) {
                buf.Set(x, y, fun(x, y, buf.Get(x, y)));
	    }
	}
    }

    /*
     * Display the current framebuffer to the board's display
     * component.
     *
     * Parameter:
     *   disp: The display to update
     */
    public void Display(IDisplay disp) {
        disp.Display(buf);
    }
}
