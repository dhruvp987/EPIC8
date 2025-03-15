using System.IO;

/*
 * Load various data from the outside world into the emulated memory.
 */
public class MemLoader {
    public const int PROGRAM_LOAD_LOC = 0x200;
    public const int FONT_LOAD_LOC = 0x50;

    /*
     * Load an outside binary into emulated memory.
     *
     * Parameters:
     *   binReader: The binary's stream to read from
     *   mem: The emulated memory to load to
     *   addr: Where in the emulated memory to start loading to (inclusive)
     */
    public static void LoadBinary(BinaryReader binReader, Memory mem, int addr) {
        try {
            while (true) {
                var nextByte = binReader.ReadByte();
		mem.Set(addr, nextByte);
		addr++;
	    }
	}
	catch (EndOfStreamException) {
            binReader.Close();
	}
    }
}
