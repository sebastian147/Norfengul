using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Timers class provides a utility function to manage fixed-time events.
/// This can be used to trigger an action after a certain amount of time has passed, using Unity's fixed time step.
/// </summary>
public class Timers
{
    /// <summary>
    /// Increments the provided time by Time.fixedDeltaTime and checks if the specified timeMax has been reached.
    /// If the timer exceeds timeMax, it resets to 0 and returns true. 
    /// The timer only runs when the 'start' parameter is set to true.
    /// </summary>
    /// <param name="time">A reference to the current timer value.</param>
    /// <param name="timeMax">The maximum time before the timer resets.</param>
    /// <param name="start">Determines if the timer should run or not.</param>
    /// <returns>Returns true if timeMax is reached, false otherwise. If not started, returns true.</returns>
    public bool TimePassFixed(ref float time, float timeMax, bool start)
    {
        if(start) // If the timer is started
        {
            time += Time.fixedDeltaTime; // Increment time by the fixed time step

            if(time >= timeMax) // Check if the timer has reached or exceeded the maximum time
            {
                time = 0; // Reset the timer
                return true; // Return true to signal that timeMax was reached
            }

            return false; // If timeMax is not reached, return false
        }

        return true; // If the timer is not started, return true to avoid blocking operations
    }
}
