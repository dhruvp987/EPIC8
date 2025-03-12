/*
 * The component that allows other components to access each other.
 * This faciliates communication between the emulator's various pieces.
 */
public class Board {
    Memory mem;
    IDisplay disp;
    EStack stack;
    GPU gpu;
    Processor psr;

    /*
     * Create a new Board and add components to it.
     *
     * Parameters:
     *   mem: The object to store RAM data
     *   dis: The object to use for display output
     *   stack: The stack to use for CHIP-8's subroutine calls
     *   gpu: The GPU to provide graphical capabilites
     *   psr: The processor to execute CHIP-8 instructions
     */
    public Board(Memory mem,
		 IDisplay disp,
		 EStack stack,
		 GPU gpu,
		 Processor psr) {
	this.mem = mem;
	this.disp = disp;
	this.stack = stack;
	this.gpu = gpu;
	this.psr = psr;

	psr.Attach(this);
    }

    // The board's memory component.
    public Memory Mem { get => mem; }

    // The board's display component.
    public IDisplay Disp { get => disp; }

    // The board's stack component.
    public EStack Stack { get => stack; }

    // The board's GPU component.
    public GPU Gpu { get => gpu; }

    // The board's processor component.
    public Processor Psr { get => psr; }
}
