/*
 * A collection of components that a processor uses to operate the emulator.
 */
public class Peripherals {
    Memory mem;
    IDisplay disp;
    EStack stack;
    GPU gpu;

    /*
     * Create a new Peripherals collection and add components to it.
     *
     * Parameters:
     *   mem: The object to store RAM data
     *   disp: The object to use for display output
     *   stack: The stack to use for CHIP-8's subroutine calls
     *   gpu: The GPU to provide graphical capabilites
     */
    public Peripherals(Memory mem,
		 IDisplay disp,
		 EStack stack,
		 GPU gpu) {
	this.mem = mem;
	this.disp = disp;
	this.stack = stack;
	this.gpu = gpu;
    }

    public Memory Mem { get => mem; }

    public IDisplay Disp { get => disp; }

    public EStack Stack { get => stack; }

    public GPU Gpu { get => gpu; }
}
