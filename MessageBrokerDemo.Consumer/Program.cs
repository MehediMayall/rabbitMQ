using static System.Console;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Dumpify;
using System.Text.Json;




// Create a connection factory
var factory = new ConnectionFactory(){
    HostName = "localhost",
    UserName = "mehedi",
    Password = "mehedi007",
    VirtualHost = "booking"
};

// Create a connection
using var conn = factory.CreateConnection();

// Create a channel to recieve messages from a queue
using var channel = conn.CreateModel();

// Declare a queue to connect that queue to receive messages
channel.QueueDeclare("MessageTest", durable: true, exclusive: false, autoDelete: true, null);

// Create a consumer
var consumer = new EventingBasicConsumer(channel);

// Attach a callback function to a channel
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    message.Dump();
    var user = JsonSerializer.Deserialize<User>(message);
    user.Dump();
};

channel.BasicConsume("MessageTest", autoAck: true, consumer);

WriteLine("Press enter to exit...");

ReadLine();



class User {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedOn { get; set; }
}