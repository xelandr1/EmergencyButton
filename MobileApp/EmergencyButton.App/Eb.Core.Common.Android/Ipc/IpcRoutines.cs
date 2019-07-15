using System;
using Android.OS;
using Eb.Core.Common.Common;
using Eb.Core.Common.ComponentModel;
using EmergencyButton.App.Service;
using EmergencyButton.Core.Common.Common;
using Newtonsoft.Json;

namespace Eb.Core.Common.Droid.Ipc
{
    public static class IpcRoutines
    {
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public static void SetData<T>(T data, Message message, Messenger answerMessenger, ServiceOperation operation)
        {
            if (data != null) message.Data.PutString("json", Serialize(data));

            message.Obj = answerMessenger;
            message.What = (int) operation;
        }

        public static T GetData<T>(Message message)
        {
            if (message.Data.ContainsKey("json"))
                return Deserialize<T>(message.Data.GetString("json"));
            return default(T);
        }

        public static Messenger GetAnswerMessenger(Message msg)
        {
            return (Messenger) msg.Obj;
        }

        public static void SendData<T>(T data, Messenger msgr, Messenger answerMessenger, ServiceOperation operation)
        {
            if (msgr == null) return;

            var message = Message.Obtain();
            SetData(data, message, answerMessenger, operation);
            msgr.Send(message);
        }

        public static void SendData(Messenger msgr, Messenger answerMessenger, ServiceOperation operation)
        {
            if (msgr == null) return;

            var message = Message.Obtain();
            message.What = (int) operation;
            message.Obj = answerMessenger;
            msgr.Send(message);
        }

        public static void RaiseEvent<T>(T data, Messenger msgr, Messenger answerMessenger, ServiceOperation operation)
        {
            if (msgr == null) return;

            var message = Message.Obtain();
            SetData(data, message, answerMessenger, operation);
            msgr.Send(message);
        }

        public static void RaiseEvent(Messenger msgr, Messenger answerMessenger, ServiceOperation operation)
        {
            RaiseEvent<object>(null, msgr, answerMessenger, operation);
        }

        public static void DoAfter(Action action, int timeoutMs)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            Singleton.GetService<ISystemUtils>().StartTimer(t =>
                {
                    action();
                    t.Cancel();
                },
                () => timeoutMs,
                false);
        }
    }
}