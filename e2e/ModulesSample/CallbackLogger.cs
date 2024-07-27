// Temp disabled
//  - Prism.Logging has been deprecated and Prism.Logging.Serilog is out of date.
//  - https://github.com/augustoproiete/prism-logging-serilog/issues/3
/*
using System;
using System.Collections.Generic;
using Prism.Logging;

namespace ModulesSample
{
    /// <summary>
    /// A logger that holds on to log entries until a callback delegate is set, then plays back log
    /// entries and sends new log entries
    /// </summary>
    public class CallbackLogger : ILoggerFacade
    {
        #region ILoggerFacade members

        private readonly Queue<Tuple<string, Category, Priority>> savedLogs =
            new Queue<Tuple<string, Category, Priority>>();

        /// <summary>
        /// Gets or sets the callback to receive logs
        /// </summary>
        /// <value>An Action&lt;string, Category&gt; callback</value>
        public Action<string, Category, Priority> Callback { private get; set; }

        /// <summary>
        /// Write a new log entry with the specified category and priority
        /// </summary>
        /// <param name="message">Message body to log</param>
        /// <param name="category">Category of the log message</param>
        /// <param name="priority">The priority of the entry</param>
        public void Log(string message, Category category, Priority priority)
        {
            if (this.Callback != null)
            {
                this.Callback(message, category, priority);
            }
            else
            {
                savedLogs.Enqueue(new Tuple<string, Category, Priority>(message, category, priority));
            }
        }

        /// <summary>
        /// Replays the saved logs if the Callback has been set
        /// </summary>
        public void ReplaySavedLogs()
        {
            if (this.Callback != null)
            {
                while (this.savedLogs.Count > 0)
                {
                    var log = this.savedLogs.Dequeue();
                    this.Callback(log.Item1, log.Item2, log.Item3);
                }
            }
        }

        #endregion
    }
}
*/
