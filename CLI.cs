namespace SiteScript
{
    class CLIcommand{
        public string command;
        public Arg[] args;

        public CLIcommand(string command){
            string[] splitted = command.Split(" ");
            this.command = splitted[0];
            args = Program.collectArgs(splitted);
        }
    }

    public static class CLI{
        public static void Open(){
            Program.log("CLI initialising...", LogLevel.info);
            while (true)
            {
                string input = Console.ReadLine();
                CLIcommand command = new(input);
                Program.log($"Recieved command {command.command}", LogLevel.trace);
            }
        }
    }
}