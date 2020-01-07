using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataCommon
{
    public class RedisHelper : IDisposable
    {

        private static ConnectionMultiplexer redis = null;
        private static bool connected = false;
        private IDatabase db = null;
        private int current = 0;
        public static bool IsConnected
        {
            get
            {
                Open();
                return redis.IsConnected;
            }
        }
        public static bool Test()
        {
            bool r = true;
            try
            {
                RedisHelper.Using(rs => { rs.Use(0); });
            }
            catch (Exception e)
            {
                //记录日志
                r = false;
            }
            return r;
        }
        private static int Open()
        {
            if (connected) return 1;
            redis = ConnectionMultiplexer.Connect("localhost:6379,password=123456,abortConnect = false");
            //redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            connected = true;
            return 1;
        }
        public static void Using(Action<RedisHelper> func)
        {
            using (var red = new RedisHelper())
            {
                func(red);
            }
        }
        public RedisHelper Use(int i)
        {
            Open();
            current = i;
            db = redis.GetDatabase(i);

            //Log.Logs($"RedisDB Conntet State: {redis.IsConnected}");
            var t = db.Ping();
            //Log.Logs($"RedisDB Select {i}, Ping.{t.TotalMilliseconds}ms");
            return this;
        }

        public void Set(string key, string val, TimeSpan? ts = null)
        {
            db.StringSet(key, val, ts);
        }

        public string Get(string key)
        {
            return db.StringGet(key);
        }

        public void Remove(string key)
        {
            db.KeyDelete(key, CommandFlags.HighPriority);
        }

        public bool Exists(string key)
        {
            return db.KeyExists(key);
        }

        public void Dispose()
        {
            db = null;
        }



        public delegate void RedisDeletegate(string str);
        public event RedisDeletegate RedisSubMessageEvent;

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="subChannel"></param>
        public void RedisSub(string subChannel)
        {

            redis.GetSubscriber().Subscribe(subChannel, (channel, message) =>
            {
                RedisSubMessageEvent?.Invoke(message); //触发事件

            });

        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long RedisPub<T>(string channel, T msg)
        {

            return redis.GetSubscriber().Publish(channel, JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public void Unsubscribe(string channel)
        {
            redis.GetSubscriber().Unsubscribe(channel);
        }

        /// <summary>
        /// 取消全部订阅
        /// </summary>
        public void UnsubscribeAll()
        {
            redis.GetSubscriber().UnsubscribeAll();

        }
    }
}
