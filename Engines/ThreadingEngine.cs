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
            ThreadStop();
            this.Thread.Join();
        }

        protected abstract void ThreadStart();
        protected abstract void ThreadStop();

        private void Initialize(string name)
        {
            this.Thread = new Thread(new ThreadStart(this.ThreadStart));
            this.Name = name;
        }
    }
    public static class ThreadingEngine
    {
        private static List<ThreadBase> _threads = new List<ThreadBase>();

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

        public static void StartThread(ThreadBase thread)
        {
            if (
                    thread.Thread.ThreadState == ThreadState.Unstarted ||
                    thread.Thread.ThreadState == ThreadState.Stopped
                )
            {
                AddThread(thread);
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
                    thread.Thread.ThreadState != ThreadState.Stopped &&
                    thread.Thread.ThreadState != ThreadState.StopRequested &&
                    thread.Thread.ThreadState != ThreadState.AbortRequested
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

        private static void AddThread(ThreadBase thread)
        {
            foreach (ThreadBase existingThread in _threads)
            {
                if (existingThread.Name == thread.Name)
                {
                    return;
                }
            }

            _threads.Add(thread);
        }
    }
}
