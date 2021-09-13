using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;

public class TaskTesting : MonoBehaviour
{
    string newGoName = "lalalal";
    public Action<object> calcFunction;
    Task t;
    public List<int> array;
    void Start()
    {
         t = ZZ();
        StartRotate();

        Debug.Log("Start: " +
      Thread.CurrentThread.ManagedThreadId);


        Action<object> action = (object obj) =>
        {
            Debug.Log("Task=");
            try
            {
                Debug.Log("Task= " + Task.CurrentId + ", obj= " + obj + ", Thread=" +
         Thread.CurrentThread.ManagedThreadId);
                Task.Delay(2000);

                array[2] = 5;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

        };

        Task t1 = new Task(action, 3);
        t1.Start();
            Debug.Log("Finished");
    }

    private void Update()
    {
        /*if(t != null)
            Debug.Log(t.Status);*/

        Debug.Log(newGoName);

    }
    async Task ZZ()
    {
        await Task.Delay(1000);
        Debug.Log("bisiler" + Thread.CurrentThread.ManagedThreadId);
    }


    async void StartRotate()
    {
      


        var cts = new CancellationTokenSource();
        this.Rotate(cts.Token);
       
        await Task.Delay(1000);

        Debug.Log("StartRotate: " +
    Thread.CurrentThread.ManagedThreadId);
        cts.Cancel();

    }

    async void Rotate(CancellationToken ct)
    {

        Debug.Log("Rotate: " +
         Thread.CurrentThread.ManagedThreadId);
        while (!ct.IsCancellationRequested)
        {
            this.transform.Rotate(Vector3.forward, 1.0f);
            await Task.Yield();
        }
    }
  

    void HowToCreateTask()
    {
        Action<object> action = (object obj) =>
        {
            Debug.Log("Task= " + Task.CurrentId + ", obj= " + obj + ", Thread=" +
            Thread.CurrentThread.ManagedThreadId);
        };

        Task t1 = new Task(action, "alpha");

        // Construct a started task
        Task t2 = Task.Factory.StartNew(action, "beta");
        // Block the main thread to demonstrate that t2 is executing
        t2.Wait();

        // Launch t1 
        t1.Start();
        Debug.Log("t1 has been launched. (Main Thread= " +
                          Thread.CurrentThread.ManagedThreadId);
        // Wait for the task to finish.
        t1.Wait();

        // Construct a started task using Task.Run.
        String taskData = "delta";
        Task t3 = Task.Run(() => {
            Debug.Log("Task= " + Task.CurrentId + ", obj= " + taskData + ", Thread=" +
           Thread.CurrentThread.ManagedThreadId);
        });
        // Wait for the task to finish.
        t3.Wait();

        // Construct an unstarted task
        Task t4 = new Task(action, "gamma");
        // Run it synchronously
        t4.RunSynchronously();
        // Although the task was run synchronously, it is a good practice
        // to wait for it in the event exceptions were thrown by the task.
        t4.Wait();
    }

}
