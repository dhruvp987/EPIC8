/*
 * A processor that can execute CHIP-8 instructions.
 */
public class Processor {
    const int NUM_GEN_REGS = 16;
    const int NUM_SPEC_REGS = 2;
    const int PC_INDEX = NUM_GEN_REGS;
    const int I_REG_INDEX = PC_INDEX + 1;

    int[] regs;

    Timer delayTimer;
    Timer soundTimer;

    /*
     * Create a new processor.
     */
    public Processor() {
	// Create the general registers plus the program counter
	// and index register.
        regs = new int[NUM_GEN_REGS + NUM_SPEC_REGS];

	delayTimer = new Timer();
	soundTimer = new Timer();
    }

    /*
     * The operation to clear the screen's contents.
     *
     * Parameters:
     *   gpu: The GPU whose framebuffer to display
     *   disp: The display to update with the new framebuffer
     */
    void OpClearScreen(GPU gpu, IDisplay disp) {
        gpu.RunKernel((x, y, isOn) => false);
	gpu.Display(disp);
    }

    /*
     * The operation to set the program counter to a given memory address.
     *
     * Parameter:
     *   addr: The address to set the program counter to
     */
    void OpJump(short addr) {
        regs[PC_INDEX] = addr;
    }

    /*
     * The operation to set a register to a given value.
     *
     * Parameters:
     *   regIndex: The register to set
     *   val: The value to set in the register
     */
    void OpSet(int regIndex, byte val) {
        regs[regIndex] = val;
    }

    /*
     * The operation to add a value to a register's current value.
     *
     * Parameters:
     *   regIndex: The register to add to
     *   val: The value to add
     */
    void OpAdd(int regIndex, byte val) {
        regs[regIndex] += val;
    }

    /*
     * The operation to set the index register to a given value.
     *
     * Parameter:
     *   val: The value to set in the index register
     */
    void OpSetIndex(short val) {
        regs[I_REG_INDEX] = val;
    }

    /*
     * The operation to display a sprite located at the memory address
     * specified by the index register to an x, y coordinate.
     *
     * Parameters:
     *   xReg: The register index where the x coordinate is
     *   yReg: The register index where the y coordinate is
     *   spriteHeight: How tall the sprite is, in number of pixels
     *   mem: The memory component the sprite is stored in
     *   gpu: The gpu whose frame buffer will be updated
     *   disp: The display to draw to
     */
    void OpDisplay(int xReg, int yReg, int spriteHeight, Memory mem, GPU gpu, IDisplay disp) {
	var spriteAddr = regs[I_REG_INDEX];

	var frameWidth = gpu.FrameBufWidth;
	var frameHeight = gpu.FrameBufHeight;

	var x = regs[xReg] % frameWidth;
	var y = regs[yReg] % frameHeight;

	for (var i = 0; i < spriteHeight; i++) {
	    if (y + i >= frameHeight) break;

	    // Each bit represents the on/off state of a pixel.
            var spriteRow = mem.Get(spriteAddr + i);
	   
	    // Iterate over a byte.
	    for (var j = 0; j < 8; j++) {
                if (x + j >= frameWidth) break;

		// Grab the jth bit, starting from the leftmost one.
                var bit = (byte) ((spriteRow << j) >> 7);
                
		if (bit == 1) {
                    gpu.Set(x + j, y + i, true);
		}
		else {
                    gpu.Set(x + j, y + i, false);
		}
	    }
	}

	gpu.Display(disp);
    }
}
