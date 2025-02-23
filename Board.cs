/*
 * The component that connects all of the other components with each other.
 * This faciliates communication and cooperation between the emulator's
 * various pieces.
 */
public class Board {
    Memory mem;
    IDisplay dis;
    EStack stack;
    Processor psr;

    /*
     * Create a new Board and add components to it.
     *
     * Parameters:
     *   mem: The object to store RAM data
     *   dis: The object to use for display output
     *   stack: The stack to use for CHIP-8's subroutine calls
     *   psr: The processor to execute CHIP-8 instructions
     */
    public Board(Memory mem,
		 IDisplay dis,
		 EStack stack,
		 Processor psr) {
	this.mem = mem;
	this.dis = dis;
	this.stack = stack;
	this.psr = psr;

	// Attach the processor to the board so it can access
	// the rest of the components.
	psr.Attach(this);
    }

    /*
     * Set a memory address to a given byte in the memory component.
     *
     * Parameters:
     *   addr: The memory address to set to
     *   data: The byte to set
     */
    public void SetMem(uint addr, byte data) {
        mem.Set(addr, data);
    }

    /*
     * Get the byte at a memory address in the memory component.
     *
     * Parameter:
     *   addr: The memory address to get from
     *
     * Returns: The byte at the memory address.
     */
    public byte GetMem(uint addr) {
        return mem.Get(addr);
    }

    /*
     * Display a frame on the display component.
     *
     * Parameter:
     *   fbuf: The frame buffer to display.
     */
    public void Display(FrameBuffer fbuf) {
        dis.Display(fbuf);
    }
}
