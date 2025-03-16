/*
 * Stores the on/off state for each pixel on a 2D grid.
 */
public class FrameBuffer {
    int    width;
    int    height;
    int[]  buffer;

    // Frame buffer's width
    public int Width  { get => width;  }
    // Frame buffer's height
    public int Height { get => height; }

    /*
     * Constructs the frame buffer.
     *
     * Parameters:
     *   width: The frame buffer's width.
     *   height: The frame buffer's height.
     */
    public FrameBuffer(int width, int height) {
        this.width = width;
	this.height = height;
	buffer = new int[width * height];
    }

    /*
     * Sets a pixel on (1) or off (0).
     *
     * Parameters:
     *   x: The pixel's x index.
     *   y: The pixel's y index.
     *   isOn: Whether the pixel is on (1) or off (0).
     */
    public void Set(int x, int y, int isOn) {
        buffer[width * y + x] = isOn;
    }

    /*
     * Gets the on/off state of the requested pixel.
     *
     * Parameters:
     *   x: The pixel's x index.
     *   y: The pixel's y index.
     *
     * Returns: Whether the pixel is on (1) or off (0).
     */
    public int Get(int x, int y) {
        return buffer[width * y + x];
    }

    /*
     * Performs a given action for each pixel.
     *
     * Parameters:
     *   act: A void function takes the x index, y index, and
     *        on (true)/off (false) state of a pixel.
     */
    public void Traverse(Action<int, int, int> act) {
        for (var i = 0; i < height; i++) {
            for (var j = 0; j < width; j++) {
                act(j, i, buffer[width * i + j]);
	    }
	}
    }
}
