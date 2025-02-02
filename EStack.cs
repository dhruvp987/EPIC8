using System.Collections.Generic;

/*
 * A stack for the emulator to store memory addresses for subroutines.
 */
public class EStack {
    Stack<ushort> stack;

    public EStack() {
        stack = new Stack<ushort>();
    }

    /*
     * Push the given data onto the top of the stack.
     *
     * Parameter:
     *   data: The data to push.
     */
    public void Push(ushort data) {
        stack.Push(data);
    }

    /*
     * Pop and return the topmost data in the stack.
     */
    public ushort Pop() {
        return stack.Pop();
    }
}
