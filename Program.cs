using Microsoft.Extensions.DependencyInjection;
using VirtualMachine.interfaces;
using VirtualMachine.Services;
using Microsoft.Extensions.Configuration;
using VirtualMachine.Managers;

class Program
{
    public static  IConfiguration? configuration;
    private  IVMManager _vmManager;

    public Program(IVMManager vMManager)
    {
        _vmManager = vMManager;
    }
    static void Main()
    {
        //setup appsettings
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");
        configuration = builder.Build();

        //initialize services
        var serviceProvider = new ServiceCollection().
            AddSingleton<IConfiguration>(configuration).
            AddTransient<IFileHandler, FileHandler>().
            AddTransient<IParseService, ParseService>().
            AddTransient<IVMManager,VMManager>().
            AddTransient<ICommandDictionary, CommandDictionary>().
            BuildServiceProvider();
        var program = ActivatorUtilities.CreateInstance<Program>(serviceProvider);

        


     

        
        program.RunProgram();

    }

    


    //run program
   public void RunProgram()
    {
        var Manager = _vmManager;
        Manager.CompileCode();
    }
}