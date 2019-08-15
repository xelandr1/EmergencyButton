﻿using Android.OS;
using EmergencyButton.App.Service;
using Newtonsoft.Json;
using System;
using EmergencyButton.Core.ComponentModel;

namespace EmergencyButton.Core.Common.Droid.Ipc
{
    //public static class IpcRoutines
    //{
    //    public static string Serialize<T>(T obj)
    //    {
    //        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
    //        {
    //            TypeNameHandling = TypeNameHandling.Auto
    //        });
    //    }

    //    public static T Deserialize<T>(string data)
    //    {
    //        return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
    //        {
    //            TypeNameHandling = TypeNameHandling.Auto
    //        });
    //    }

    //    public static void SetData<T>(T data, Message message, Messenger answerMessenger, ServiceCommand operation)
    //    {
    //        if (data != null) message.Data.PutString("json", Serialize(data));

    //        message.Obj = answerMessenger;
    //        message.What = (int) operation;
    //    }

    //    public static T GetData<T>(Message message)
    //    {
    //        if (message.Data.ContainsKey("json"))
    //            return Deserialize<T>(message.Data.GetString("json"));
    //        return default(T);
    //    }

    //    public static Messenger GetAnswerMessenger(Message msg)
    //    {
    //        return (Messenger) msg.Obj;
    //    }

    //    public static void SendData<T>(T data, Messenger msgr, Messenger answerMessenger, ServiceCommand operation)
    //    {
    //        if (msgr == null) return;

    //        var message = Message.Obtain();
    //        SetData(data, message, answerMessenger, operation);
    //        msgr.Send(message);
    //    }

    //    public static void SendData(Messenger msgr, Messenger answerMessenger, ServiceCommand operation)
    //    {
    //        if (msgr == null) return;

    //        var message = Message.Obtain();
    //        message.What = (int) operation;
    //        message.Obj = answerMessenger;
    //        msgr.Send(message);
    //    }

    //    public static void RaiseEvent<T>(T data, Messenger msgr, Messenger answerMessenger, ServiceCommand operation)
    //    {
    //        if (msgr == null) return;

    //        var message = Message.Obtain();
    //        SetData(data, message, answerMessenger, operation);
    //        msgr.Send(message);
    //    }

    //    public static void RaiseEvent(Messenger msgr, Messenger answerMessenger, ServiceCommand operation)
    //    {
    //        RaiseEvent<object>(null, msgr, answerMessenger, operation);
    //    }

    //    public static void DoAfter(Action action, int timeoutMs)
    //    {
    //        if (action == null) throw new ArgumentNullException(nameof(action));

    //        Singleton.GetService<ISystemUtils>().StartTimer(t =>
    //            {
    //                action();
    //                t.Cancel();
    //            },
    //            () => timeoutMs,
    //            false);
    //    }
    //}
}