using System.IO;

/*
 * Execute programs using the emulator's components.
 */
public class Executor {
    /*
     * Execute a program using the given components.
     *
     * Parameters:
     *   progReader: The program to read and execute
     *   psr: The processor to use to execute instructions
     *   phl: The peripherals the processor will use
     */
    public static void Execute(BinaryReader progReader, Processor psr, Peripherals phl) {

    }

    /*
     * Split a byte into 4 nibbles.
     *
     * Parameter:
     *   b: The byte to split
     *
     * Returns: An array of size 4 containing the nibbles.
     */
    static byte[] SplitIntoNibbles(byte b) {
        var nibbles = new byte[4];

        nibbles[3] = (byte) ((b << 6) >> 6);
	nibbles[2] = (byte) ((b << 4) >> 6);
	nibbles[1] = (byte) ((b << 2) >> 6);
	nibbles[0] = (byte) (b >> 6);

        return nibbles;
    }
}
