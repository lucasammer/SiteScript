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
                    Arg detected = new Arg(args[i], args[i+1]);
                    found.Add(detected);
                }
            }
            return found.ToArray();
        }

        public static void log(string msg, LogLevel level){

        }

        public static void Main(string[] args){
            Arg[] arguements = collectArgs(args);
            log(arguements[0].value, LogLevel.debug);
        }
    }
}