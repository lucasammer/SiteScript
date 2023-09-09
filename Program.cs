using System.Linq;

namespace SiteScript
{
    public enum LogLevel{
        off = 0,
        fatal = 1,
        error=2,
        warn=3,
        info=4,
        debug=5,
        trace=6,
        all=7
    }

    public class Arg{
        public string name;
        public string value;

        public Arg(string name, string value) {
            this.value = value;
            this.name = name;
        }

    }

    public static class Program{
        public static LogLevel CurrentLogLevel = LogLevel.warn;
        public static Arg[] collectArgs(string[] args){
            List<Arg> found = new List<Arg>();
            for (int i = 0; i < args.Length; i++)
            {
                if(args[i].StartsWith("-")){
                    if(i == args.Length - 1){
                        log($"Invalid parameter for aguement {args[i]}", LogLevel.fatal);
                        Environment.Exit(1);
                    }
                    Arg detected = new Arg(args[i], args[i+1]);
                    found.Add(detected);
                }
            }
            return found.ToArray();
        }

        public static void log(string msg, LogLevel level){
            Console.BackgroundColor = ConsoleColor.Black;
            if(CurrentLogLevel < level){
                return;
            }
            switch (level)
            {
                case LogLevel.all:
                    Console.WriteLine($"{msg}");
                    break;
                case LogLevel.trace:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"TRACE | {msg}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.debug:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"DEBUG | {msg}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"INFO | {msg}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.fatal:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"FATAL | {msg}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.error:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR | {msg}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case LogLevel.warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"WARN | {msg}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.WriteLine($"UNKNOWN | {msg}");
                    break;
            }
        }

        public static void Main(string[] args){
            Arg[] arguements = collectArgs(args);
            Arg[] argsLL = arguements.Where(a => a.name.ToLower() == "-loglevel").ToArray();
            if(argsLL.Length > 0){
                CurrentLogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), argsLL[0].value);
                log($"Set log level to {argsLL[0].value}", LogLevel.debug);
            }
            argsLL = arguements.Where(a => a.name.ToLower() == "-mode").ToArray();
            if(argsLL.Length > 0){
                if(argsLL[0].value.ToLower() == "cli"){
                    CLI.Open();
                    Environment.Exit(0);
                }
            }
        }
    }
}