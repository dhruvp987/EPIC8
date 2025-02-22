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

    Board? board;

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
     * Attach the processor to a board if it isn't already attached.
     * The processor will then be able to access the components on
     * that board.
     *
     * Parameter:
     *   board: The board to attach to
     */
    public void Attach(Board board) {
        if (this.board == null) {
            this.board = board;
	}
    }
}
