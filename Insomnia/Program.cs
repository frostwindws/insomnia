using Insomnia;

using (InsomniaService.PreventSleep())
{
    Console.WriteLine("Press any key to stop insomnia...");
    Console.ReadKey();
}
