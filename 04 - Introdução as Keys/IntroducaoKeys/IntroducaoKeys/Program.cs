using StackExchange.Redis;

// Iniciando a conexão com o Redis
var redis = ConnectionMultiplexer.Connect("localhost:7009,password=Passw0rd");

// Obtendo o database do Redis
var database = redis.GetDatabase();

// Definindo a Key Name no Redis
var resultSet = database.StringSet("Name", "Thiago");

// Obtendo e imprimindo o valor da Key obtida
var name = database.StringGet("Name");
Console.WriteLine(name);

// Excluindo a Key do Redis
var deleteResult = database.KeyDelete("Name");

// Varificando se a Key existe no Banco
var isKeyExists = database.KeyExists("Name");

// Definindo uma key com Tempo de vida utilizando o método StringSet
database.StringSet("City", "Rio de Janeiro", TimeSpan.FromSeconds(3));

// Obtendo o Tempo de Vida da Key e iterando até o fim de vida da Key
var ttl = database.KeyTimeToLive("City");

Console.WriteLine($"TimeToLive Inicial: {ttl}");

while (ttl != null)
{
    ttl = database.KeyTimeToLive("City");
    Console.WriteLine(ttl);
}

Console.WriteLine($"TimeToLive final: {ttl}");

Console.WriteLine($"Chave City existe: {database.KeyExists("City")}");

// Definindo o tempo de vida de uma Key utilizando o KeyExpire
database.StringSet("Name", "Thiago");

Console.WriteLine($"TimeTolive Key Name:{database.KeyTimeToLive("Name")}");

database.KeyExpire("Name", TimeSpan.FromSeconds(60));

Console.WriteLine($"TimeTolive Key Name:{database.KeyTimeToLive("Name")}");

// Removendo o tempo de vida definido para uma Key
database.KeyPersist("Name");

Console.WriteLine($"TimeTolive Key Name:{database.KeyTimeToLive("Name")}");

Console.ReadKey();