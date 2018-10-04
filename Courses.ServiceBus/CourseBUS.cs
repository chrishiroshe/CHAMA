using Courses.Domain.Entities;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Core;


namespace Courses.ServiceBus
{
    public class CourseBUS
    {
        //TODO: PUT CONFIGURATION AT APPSETTINGS
        readonly string _connectionBus = "Endpoint=sb://christeste.servicebus.windows.net/;SharedAccessKeyName=todo;SharedAccessKey=xxxxxxxxx";
        private readonly string _queueName = "coursequeue"; 
        QueueClient _client;
        MessageReceiver _messageReceiver;// = new MessageReceiver(SBConnString, QueueName, ReceiveMode.PeekLock);

        public CourseBUS()
        {
            _client = new QueueClient(_connectionBus, _queueName, ReceiveMode.PeekLock);
            _messageReceiver = new MessageReceiver(_connectionBus, _queueName, ReceiveMode.PeekLock);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        /// <summary>
        /// Put Course Subscription at queue service bus
        /// </summary>
        /// <param name="course">course with student</param>
        /// <returns>task async</returns>
        public async Task PutCourseSubscriptionQueue(Course course)
        {
            string messageBody = JsonConvert.SerializeObject(course);
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await _client.SendAsync(message);
        }
        
        /// <summary>
        /// Get a message from bus
        /// </summary>
        /// <returns>Message from bus</returns>
        public async Task<Message> ProcessCourseSubscriptionQueue()
        {
            Course course = new Course();
            Message message = await _messageReceiver.ReceiveAsync();
            return message; 
        }

    }
   
}
