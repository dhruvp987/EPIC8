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
     * Run the processor execution loop.
     *
     * Parameter:
     *   phl: The peripherals used when executing instructions
     */
    public void Run(Peripherals phl) {
	while (true) {
            var instFirstByte = phl.Mem.Get(regs[PC_INDEX]);
	    var instSecByte = phl.Mem.Get(regs[PC_INDEX] + 1);
	    // Merge the 2 bytes into one 16-bit instruction.
	    var inst = (0x0000 | (int) instFirstByte << 8) | instSecByte;

	    regs[PC_INDEX] += 2;

	    // Set to false if the instruction doesn't get matched in
	    // a switch statement.
            var executed = true;
	    
	    switch (inst) {
                case 0x00E0:
		    OpClearScreen(phl.Gpu, phl.Disp);
		    break;

		default:
		    executed = false;
		    break;
	    }
	    if (executed) {
                continue;
	    }

	    var instFirstNibbles = SplitIntoNibbles(instFirstByte);
	    var instSecNibbles = SplitIntoNibbles(instSecByte);

	    // May be needed by some instructions to store a memory address,
	    // so it is declared here for clarity.
            short addr = 0;

            switch (instFirstNibbles[0]) {
                case 0x1:
		    addr = (short) ((0x000 | (short) instFirstNibbles[1] << 8) | instSecByte);
		    OpJump(addr);
		    break;

		case 0x6:
		    OpSet(instFirstNibbles[1], instSecByte);
		    break;

		case 0x7:
		    OpAdd(instFirstNibbles[1], instSecByte);
		    break;

		case 0xA:
		    addr = (short) ((0x000 | (short) instFirstNibbles[1] << 8) | instSecByte);
		    OpSetIndex(addr);
		    break;

		case 0xD:
		    OpDisplay(
		        instFirstNibbles[1],
			instSecNibbles[0],
			instSecNibbles[1],
			phl.Mem,
			phl.Gpu,
			phl.Disp);
		    break;

		default:
		    executed = false;
		    break;
	    }
	}
    }

    /*
     * The operation to clear the screen's contents.
     *
     * Parameters:
     *   gpu: The GPU whose framebuffer to display
     *   disp: The display to update with the new framebuffer
     */
    public void OpClearScreen(GPU gpu, IDisplay disp) {
        gpu.RunKernel((x, y, isOn) => false);
	gpu.Display(disp);
    }

    /*
     * The operation to set the program counter to a given memory address.
     *
     * Parameter:
     *   addr: The address to set the program counter to
     */
    public void OpJump(short addr) {
        regs[PC_INDEX] = addr;
    }

    /*
     * The operation to set a register to a given value.
     *
     * Parameters:
     *   regIndex: The register to set
     *   val: The value to set in the register
     */
    public void OpSet(int regIndex, byte val) {
        regs[regIndex] = val;
    }

    /*
     * The operation to add a value to a register's current value.
     *
     * Parameters:
     *   regIndex: The register to add to
     *   val: The value to add
     */
    public void OpAdd(int regIndex, byte val) {
        regs[regIndex] += val;
    }

    /*
     * The operation to set the index register to a given value.
     *
     * Parameter:
     *   val: The value to set in the index register
     */
    public void OpSetIndex(short val) {
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
    public void OpDisplay(int xReg, int yReg, int spriteHeight, Memory mem, GPU gpu, IDisplay disp) {
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

    byte[] SplitIntoNibbles(byte b) {
        var nibbles = new byte[2];
        nibbles[0] = (byte) (b >> 4);
	nibbles[1] = (byte) ((b << 4) >> 4);
        return nibbles;
    }
}
