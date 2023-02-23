using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public static class Simulator
{
    private static event EventHandler? startUpdate;
    private static event EventHandler? endUpdate;
    public static event EventHandler? endSimulator;

    private static bool active;

    private readonly static IBL bl = Factory.Get()!;
    private readonly static Random rn = new Random();

    public static void Active()
    {
        new Thread(() =>
        {
            active = true;
            while (active)
            {
                int? ID = bl.Order.GetOldestOrder();
                if (ID != null)
                {
                    Order o = bl.Order.Get((int)ID);
                    int delay = rn.Next(10, 15);
                    DateTime tm = DateTime.Now + new TimeSpan(0, 0, 0, 0, delay * 1000);
                    if (startUpdate != null) startUpdate(null, new UpdateStartEventArgs(o, tm));
                    Thread.Sleep(delay * 1000);
                    if (endUpdate != null) endUpdate(null, new());
                    bl.Order.UpdateStatus(o.ID);
                }
                Thread.Sleep(3000);
            }
            if (endSimulator != null ) endSimulator(null, new());
        }).Start();
    }

    public static void Stop() => active = false;
    public static void RegisterEndSimulator(EventHandler handler) => endSimulator += handler;
    public static void RegisterEndUpdate(EventHandler handler) => endUpdate += handler;
    public static void RegisterStartUpdate(EventHandler handler) => startUpdate += handler;

    public static void UnregisterEndSimulator(EventHandler handler) => endSimulator -= handler;
    public static void UnregisterEndUpdate(EventHandler handler) => endUpdate -= handler;
    public static void UnregisterStartUpdate(EventHandler handler) => startUpdate -= handler;
}
public class UpdateStartEventArgs : EventArgs
{
    public Order CurrentOrder { get; set; }
    public DateTime EndTime { get; set; }

    public UpdateStartEventArgs(Order order, DateTime endTime)
    {
        CurrentOrder = order;
        EndTime = endTime;
    }
}