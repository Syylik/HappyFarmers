using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

[Serializable]
public class WorkerManager : ITickable
{
    private Queue<ICollectable> _queueToCollect = new Queue<ICollectable>();
    private List<Worker> _workers = new List<Worker>();

    public void AddNewWorker(Worker worker) => _workers.Add(worker);

    public void AddTaskToCollect(ICollectable collectable) => _queueToCollect.Enqueue(collectable);

    public void Tick()
    {
        if(_queueToCollect.Count <= 0 || _workers.Count <= 0) return;

        foreach(var worker in _workers)
        {
            if(!worker.isBusy)
            {
                worker.StartNewTask(_queueToCollect.Dequeue());
            }
        }
    }
}