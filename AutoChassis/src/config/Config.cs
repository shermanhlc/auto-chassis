using Tomlyn;
using Tomlyn.Model;
using Tomlyn.Syntax;

using IO;

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
    public static readonly double SLEEP;
    public static readonly string OUTPUT_PATH;


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


                if (toml["clearance"] is TomlTable clearance)
                {
                    HEAD_CLEARANCE = (double)clearance["head"];
                    BODY_CLEARANCE = (double)clearance["body"];
                }
                else
                {
                    throw new MissingTomlKeyException("clearance table not found in config.toml");
                }


                if (toml["rules"] is TomlTable rules)
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
                    ITERATION_STEP = (double)process["iteration_step"];
                }
                else
                {
                    throw new MissingTomlKeyException("process table not found in config.toml");
                }

                TomlTable? output_path = toml["output"] as TomlTable;

                if (output_path != null)
                {
                    OUTPUT_PATH = (string)output_path["path"];
                }
                else
                {
                    throw new MissingTomlKeyException("output table not found in config.toml");
                }
            }
            else
            {
                throw new FileNotFoundException("No config.toml was found. Ensure the configuration file meets the requirements (consult the README) and is located in the same directory as the program.");
            }
        }
        catch (FileNotFoundException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red);
            Environment.Exit(10);
        }
        catch (BadTomlParseException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red);
            Environment.Exit(11);
        }
        catch (MissingTomlKeyException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red);
            Environment.Exit(12);
        }
        catch (InvalidCastException e)
        {
            Printer.PrintSingleLineColor(e.Message, ConsoleColor.Red);
            Printer.PrintSingleLineColor("Could not cast config value, this is most likely due to a number not being a floating point value.", ConsoleColor.White);
            Environment.Exit(13);
        }
        catch (Exception e)
        {
            Console.WriteLine($"General configuration error: {e.Message}");
            Environment.Exit(1);           // general error
        }
        
    }
}