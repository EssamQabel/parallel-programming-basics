using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void HelloConsoleWithParam(object msg)
        {
            Console.WriteLine("Hello, {0}!!", msg);
        }
        static int ReturnAnInt(object param)
        {
            Thread.Sleep(1000);
            return Convert.ToInt32(param);
        }
        static void workload()
        {
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine("Iteration {0}", i);
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            /* PART 1, 2: How to create a Task with a parameter, if no parameters needed, we can simplye remove it.
            
            Task task1 = new Task(new Action<object>(HelloConsoleWithParam), "Task1");
            Task task2 = new Task(delegate(object msg) 
                {
                    HelloConsoleWithParam(msg);
                }, "Task2");
            Task task3 = new Task((msg) => HelloConsoleWithParam(msg), "Task3");

            Console.WriteLine("Startring the program");
            task1.Start();
            task2.Start();
            task3.Start();
            Console.WriteLine("Finished the program");
            Console.ReadKey();
            */

            /* PART 3: returning a value form a task, NOTE that using its return, will wait until it finishes its
            work.
            
            int value = 10;
            Task<int> returnIntTask = new Task<int>((pValue) => ReturnAnInt(pValue), value);
            Console.WriteLine("Startring the task.");
            returnIntTask.Start();
            Console.WriteLine("Ths is the returned number: {0}", returnIntTask.Result);
            Console.ReadKey();
            */

            /* PART 4: canceling a task using CancellationTokenSource.

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task cancellationTask = new Task(() => {
                for (int i = 0; i < 100000; ++i)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancel() was called.");
                        return;
                    }
                    Console.WriteLine("Loop value {0}", i);
                }
            }, token);

            Console.WriteLine("Press any key to start the task.");
            Console.WriteLine("Then press any key again to cancel the task.");

            Console.ReadKey();
            cancellationTask.Start();

            Console.ReadKey();
            cancellationTokenSource.Cancel();

            Console.ReadKey();
            */

            /* PART 5: wating for one task to complete.
            Task task1 = new Task(new Action(workload));
            task1.Start();

            Console.WriteLine("Waiting for task to complete.");
            task1.Wait();
            Console.WriteLine("Task1 has completed.");

            Task task2 = new Task(new Action(workload));
            task2.Start();
            task2.Wait(1000);
            Console.WriteLine("Task2 has started.");
            Thread.Sleep(1000);
            Console.WriteLine("Wait 2 seconds.");

            Console.ReadKey();
            */

            /* PART 6: Waiting for multiple tasks.
            Task task1 = new Task(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Task 1 - iteration {0}", i); 
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Task 1 complete");
            });
            Task task2 = new Task(() =>
            {
                Console.WriteLine("Task 2 complete");
            });
            task1.Start();
            task2.Start();

            Console.WriteLine("Waiting for tasks to complete.");
            Task.WaitAll(task1, task2);
            Console.WriteLine("Tasks Completed.");

            Console.WriteLine("Main method complete. Press any key to finish.");
            Console.ReadKey(); 
            */

            /* PART 7: Parallel programming exception handling.*/

            var task1 = Task.Run(() => { throw new Exception("This exception is expected!"); });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    // Handle the custom exception.
                    if (e is Exception)
                    {
                        Console.WriteLine(e.Message);
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw;
                    }
                }
            }
            finally
            {
                Console.WriteLine("Catched all exceptions.");
            }
            Console.ReadKey();
        }
    }
}
