using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
    private readonly IMongoCollection<Todo> _todos;

    public TodoService(IConfiguration config)
    {
        var client = new MongoClient(
            config["MongoDbSettings:ConnectionString"]);

        var database = client.GetDatabase(
            config["MongoDbSettings:DatabaseName"]);

        _todos = database.GetCollection<Todo>(
            config["MongoDbSettings:CollectionName"]);
    }

    public List<Todo> Get() =>
        _todos.Find(todo => true).ToList();

    public Todo Create(Todo todo)
    {
        _todos.InsertOne(todo);
        return todo;
    }

    public void Delete(string id)
    {
        _todos.DeleteOne(todo => todo.Id == id);
    }
}