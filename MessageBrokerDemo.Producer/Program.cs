using System.Text;
using System.Text.Json;
using RabbitMQ.Client;


// First create a Connection Factory
var factory = new ConnectionFactory(){
    HostName = "localhost",
    UserName = "mehedi",
    Password = "mehedi007",
    VirtualHost = "booking",
};

// Create a connection using the factory
using var conn = factory.CreateConnection();

// Create a channel to send message
using var channel = conn.CreateModel();

// Declare a queue
channel.QueueDeclare(queue: "MessageTest", durable: true, exclusive: false, autoDelete: true, null);

// Create a message
// string message = "Here is the message for the comsumer to consume from a message broker.";
var user = new User{
    UserId = 1,
    FirstName = "Mehedi",
    LastName = "Hasan",
    UserName = "mehedimayall",
    Email = "mehedimayall@gmail.com",
    Password = "A#C@021",
    CreatedOn = DateTime.Now,
};


// Create a message body
var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));

// Now, send the message to a Queue
channel.BasicPublish(exchange:"", routingKey: "MessageTest", null, body);


Console.WriteLine("A message has been sent to the message booker to publish.");




class User {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedOn { get; set; }
}