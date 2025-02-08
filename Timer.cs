using System.Timers;

/*
 * This represents a CHIP-8 timer register. It contains a register 
 * that holds a value, which is decremented every time the timer elapses
 * and the value is above 0. CHIP-8 programs may depend on this information.
 *
 * For a CHIP-8 delay and sound timer, the timer interval should be 1/60 of a second, 
 * the max register value should be 255 (the max value for one byte), and the
 * decrement amount should be 1.
 */
public class Timer {
    // 1/60 seconds converted to miliseconds (what the System timer uses)
    // is about 17
    public const int DEFAULT_TIMRINTRVL = 17;
    public const int DEFAULT_MXRGVL = 255;
    public const int DEFAULT_DCRAMT = 1;

    int register;
    int maxRegVal;
    int decrAmt;

    System.Timers.Timer timer;

    /*
     * Create the CHIP-8 timer register.
     *
     * Parameters:
     *   timerInterval (optional): The time the timer waits before elapsing
     *   maxRegVal (optional): The max value the register can store
     *   decrAmt (optional): The amount to decrement the register everytime
     *                       the timer elapses
     */
    public Timer(int timerInterval = DEFAULT_TIMRINTRVL,
		 int maxRegVal = DEFAULT_MXRGVL,
		 int decrAmt = DEFAULT_DCRAMT) {
	// Prepare the register
	this.maxRegVal = maxRegVal;
        register = maxRegVal;
	this.decrAmt = decrAmt;

	// Prepare the timer
	timer = new System.Timers.Timer(timerInterval);
	timer.Elapsed += OnElapsedEvent;
	timer.AutoReset = true;

	Start();
    }
	
    /*
     * Stop decrementing the register.
     */
    public void Stop() {
        timer.Stop();
    }

    /*
     * Start decrementing the register.
     */
    public void Start() {
	timer.Start();
    }

    /*
     * Stop the timer and clean up resources. The timer should
     * not be used after this is called.
     */
    public void StopAndClean() {
        timer.Stop();
	timer.Dispose();
    }

    /*
     * This is the procedure that the timer runs when it has elasped.
     * It gets attached to the timer's Elapsed event handler.
     */
    private void OnElapsedEvent(Object? source, ElapsedEventArgs args) {
	if (register > 0) {
            register -= decrAmt;
	}
	else {
            register = maxRegVal;
	}
    }

    /*
     * The finalizer cleans up the timer's resources when the object
     * is being collected by the garbage collector.
     */
    ~Timer() {
        StopAndClean();
    }
}
