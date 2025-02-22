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
    }
}
