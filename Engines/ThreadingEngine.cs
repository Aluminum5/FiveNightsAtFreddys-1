using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FNAF.Engines
{
    public abstract class ThreadBase
    {
        public Thread Thread;
        public string Name
        {
            get { return this.Thread.Name; }
            set { this.Thread.Name = value; }
        }

        public ThreadBase(string name)
        {
            Initialize(name);  
        }

        public void Start()
        {
            this.Thread.Start();
        }

        public void Stop()
        {
            this.Thread.Abort();
        }

        protected abstract void Start(object param);

        private void Initialize(string name)
        {
            ParameterizedThreadStart startInfo = new ParameterizedThreadStart(this.Start);
            this.Thread = new Thread(startInfo);
            this.Name = name;
        }
    }
    public static class ThreadingEngine
    {
        private static List<ThreadBase> _threads = new List<ThreadBase>();

        public static void AddThread(ThreadBase thread)
        {
            foreach (ThreadBase existingThread in _threads)
            {
                if (existingThread.Name == thread.Name)
                {
                    throw new ArgumentException("Thread name already exists in the thread engine.");
                }
            }

            _threads.Add(thread);
        }

        public static void RemoveThread(ThreadBase thread)
        {
            foreach (ThreadBase existingThread in _threads)
            {
                if (existingThread.Name == thread.Name)
                {
                    _threads.Remove(thread);
                }
            }

            throw new ArgumentException("Thread name did not exists in the thread engine.");
        }

        public static T GetThread<T>() where T : ThreadBase
        {
            Type threadType = typeof(T);

            foreach (ThreadBase thread in _threads)
            {
                if (thread.GetType() == threadType)
                {
                    return (T)thread;
                }
            }

            throw new InvalidOperationException("A thread of the type provided in the Generic typing was not found.");
        }

        public static void StartThreads()
        {
            foreach (ThreadBase thread in _threads)
            {
                StartThread(thread);
            }
        }

        public static void StartThread(ThreadBase thread)
        {
            if (
                    thread.Thread.ThreadState == ThreadState.Unstarted ||
                    thread.Thread.ThreadState == ThreadState.Stopped
                )
            {
                thread.Start();
            }
        }

        public static void StopThreads()
        {
            foreach (ThreadBase thread in _threads)
            {
                StopThread(thread);
            }
        }

        public static void StopThread(ThreadBase thread)
        {
            if (
                    thread.Thread.ThreadState == ThreadState.Running ||
                    thread.Thread.ThreadState == ThreadState.Suspended ||
                    thread.Thread.ThreadState == ThreadState.Running
                )
            {
                thread.Stop();
            }
        }

        public static void Dispose()
        {
            StopThreads();
            _threads.Clear();
        }
    }
}
