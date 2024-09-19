using System;
using Tomlyn;
using Tomlyn.Model;
using Tomlyn.Syntax;

using Utilities;

public static class Config
{
    // clearance
    public static readonly double HEAD_CLEARANCE;
    public static readonly double BODY_CLEARANCE;

    // rules
    public static readonly double MIN_LATERAL_LENGTH;
    public static readonly double MIN_VERTICAL_HEIGHT;

    // process
    public static readonly double ITERATION_STEP;
    public static readonly int SLEEP;


    static Config() 
    {
        try {
            // ensure config file exists
            string base_dir = AppContext.BaseDirectory;
            string filepath = Path.Combine(base_dir, "config.toml");

            if (File.Exists(filepath))
            {
                string toml_string = File.ReadAllText(filepath);
                DocumentSyntax toml_parse = Toml.Parse(toml_string);

                // check parsing errors
                if (toml_parse.HasErrors)
                {
                    foreach (DiagnosticMessage error in toml_parse.Diagnostics)
                    {
                        Console.WriteLine($"Error parsing config: {error}");
                    }

                    throw new BadTomlParseException("Error parsing config.toml");
                }

                // find all necessary key-value pairs
                TomlTable toml = toml_parse.ToModel();

                TomlTable? clearance = toml["clearance"] as TomlTable;

                if (clearance != null)
                {
                    HEAD_CLEARANCE = (double)clearance["head"];
                    BODY_CLEARANCE = (double)clearance["body"];
                }
                else
                {
                    throw new MissingTomlKeyException("clearance table not found in config.toml");
                }

                TomlTable? rules = toml["rules"] as TomlTable;

                if (rules != null)
                {
                    MIN_LATERAL_LENGTH = (double)rules["lateral"];
                    MIN_VERTICAL_HEIGHT = (double)rules["vertical"];
                }
                else
                {
                    throw new MissingTomlKeyException("rules table not found in config.toml");
                }

                TomlTable? process = toml["process"] as TomlTable;

                if (process != null)
                {
                    ITERATION_STEP = (double)process["step"];
                    SLEEP = (int)process["sleep"];
                }
                else
                {
                    throw new MissingTomlKeyException("process table not found in config.toml");
                }
            }
            else
            {
                throw new FileNotFoundException("No config.toml was found. Ensure the configuration file meets the requirements (consult the README) and is located in the same directory as the program.");
            }
        }
        catch (BadTomlParseException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red, true);
            Environment.Exit(11);
        }
        catch (MissingTomlKeyException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red, true);
            Environment.Exit(12);
        }
        
        catch (FileNotFoundException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red, true);
            Environment.Exit(10);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(1);           // general error
        }
        
    }
}