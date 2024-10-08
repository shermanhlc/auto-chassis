using System.Diagnostics;

namespace AutoChassis.Tests
{
    public class Integration
    {
        [Fact]
        public void SystemTest()
        {
            string input_path = Path.Combine(Directory.GetCurrentDirectory(), "tests/integration/inputs");

            string test_directory = AppDomain.CurrentDomain.BaseDirectory;
            string relative_path = Path.Combine(
                test_directory
                , "..", "..", "..", ".."
                , "AutoChassis"
                , "bin"
                , "Debug"
                , "net8.0"
                , "AutoChassis"
            );
            string binary_path = Path.GetFullPath(relative_path);

            // Console.WriteLine($"Binary Path: {binary_path}");


            ProcessStartInfo start_info = new ProcessStartInfo
            {
                FileName = binary_path,
                Arguments = "-t",
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Console.WriteLine(start_info.FileName);

            using (Process process = new Process { StartInfo = start_info })
            {
                process.Start();


                using (StreamWriter writer = process.StandardInput)
                using (StreamReader reader = new StreamReader(input_path))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(line);
                        writer.Flush();
                    }

                    string final_output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    Console.Write(final_output);
                    Assert.Contains("finished calculations for firewall", final_output);
                }
            }

            // Assert.True(false);
        }
    }
}
