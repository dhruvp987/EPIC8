/*
 * The emulator's volatile memory representing the CHIP-8's RAM.
 */
public class Memory {
    public const int MEM_DEFAULT_SIZE = 4000;

    int numBytes;
    byte[] mem;

    /*
     * The size of the memory, in bytes.
     */
    public int Size { get => numBytes; }

    /*
     * Constructs the memory object.
     *
     * Parameter:
     *   size: The number of bytes that the memory can store.
     */
    public Memory(int size = MEM_DEFAULT_SIZE) {
	numBytes = size;
        mem = new byte[size];
    }

    /*
     * Set a memory address to a given byte.
     *
     * Parameters:
     *   addr: The memory address to set to.
     *   data: The byte to set.
     */
    public void Set(int addr, byte data) {
        mem[addr] = data;
    }

    /*
     * Get the byte at a memory address.
     *
     * Parameter:
     *   addr: The memory address to get from.
     *
     * Returns: The byte at the memory address.
     */
    public byte Get(int addr) {
        return mem[addr];
    }
}
