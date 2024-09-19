namespace Utilities
{
    public static class PTSBuilder
    {
        public const string default_path = "chassis.pts";
        public static void BuildPTSFile(Point[] points, string path = default_path)
        {
            foreach (Point p in points)
            {
                string line = string.Format("{0} {1} {2}", p.x, p.y, p.z);
                File.AppendAllText(path, line + "\n");
            }
        }
    }
}