/*
 * A class representing the collection of registers CHIP-8 uses to
 * execute instructions.
 */
public class Registers {
    int[] regs;
   
    /*
     * Create the collection of registers.
     *
     * Parameter:
     *   numRegs: The number of registers to create
     */
    public Registers(int numRegs) {
        regs = new int[numRegs];
    }

    /*
     * Get and set the contents of a register using the indexer syntax.
     *
     * Parameter:
     *   index: The index of the register
     */
    public int this[int index] {
        get => regs[index];
	set => regs[index] = value;
    }
}
