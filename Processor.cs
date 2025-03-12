/*
 * A processor that can execute CHIP-8 instructions.
 */
public class Processor {
    const int NUM_GEN_REGS = 16;
    const int PC_INDEX = NUM_GEN_REGS;
    const int I_REG_INDEX = PC_INDEX + 1;

    Registers regs;

    Timer delayTimer;
    Timer soundTimer;

    /*
     * Create a new processor.
     */
    public Processor() {
	// Create the general registers plus the program counter
	// and index register.
        regs = new Registers(NUM_GEN_REGS + 2);

	delayTimer = new Timer();
	soundTimer = new Timer();
    }

    /*
     * The operation to clear the screen's contents.
     */
    void OpClearScreen(GPU gpu, IDisplay disp) {
        gpu.RunKernel((x, y, isOn) => false);
	gpu.Display(disp);
    }
}
