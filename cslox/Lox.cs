using System;
using System.IO;

namespace cslox
{
    class Lox
    {
        static bool hadError = false;
        static void Main(string[] args)
        {
            if (args.Length > 1){
                Console.WriteLine("Usage: cslox [script[");
            } else if (args.Length == 1){
                runFile(args[0]);
            } else if (args.Length == 0){
                runPrompt();
            }
        }

        private static void runFile(String path){
            // Byte[] bytes = File.ReadAllBytes(Path.GetFullPath(path));
            // why not just read it as a string from file??
            // run(new String(bytes));

            String txt = File.ReadAllText(Path.GetFullPath(path));
            run(txt);
            if (hadError) {
                // set non-zero exit code if we had an error while running the script
                Environment.ExitCode = 65;
            }
        }

        private static void runPrompt(){
            while (true){
                Console.Write("> ");
                String line = Console.ReadLine();
                if (line == null){
                    break;
                }
                run(line);
                // reset hadError to keep the REPL alive
                hadError = false;
            }
        }

        private static void run(String source){
            Console.WriteLine(source);
        }

        // private static void run2(String source){

        //     // not yet implemented
        //     Scanner scanner = new Scanner(source);
        //     List<Token> tokens = scanner.scanTokens();

        //     // print tokens while getting started
        //     foreach (Token token in tokens){
        //         Console.WriteLine(token);
        //     }
        // }

        static void error(int line, String message){
            report(line, "", message);
        }

        static void report(int line, String where, String message){
            Console.WriteLine($"[Line {line}] Error {where}: {message}");
            hadError = true;
        }
    }   
}
