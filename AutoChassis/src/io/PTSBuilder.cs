using Utilities;

namespace IO
{
    public static class PTSBuilder
    {
        public static void BuildPTSFile(Point[] points, string? path = null)
        {
            if (path == null)
            {
                path = OUTPUT_PATH;
            }

            foreach (Point p in points)
            {
                string line = string.Format("{0} {1} {2}", p.x, p.y, p.z);
                File.AppendAllText(path, line + "\n");
            }
        }

        public static void BuildPTSFile(SortedDictionary<string, Point> points, string? path = null)
        {
            if (path == null)
            {
                path = OUTPUT_PATH;
            }

            foreach(KeyValuePair<string, Point> entry in points)
            {
                string name = entry.Key;
                File.AppendAllText(path, name + "\n");

                string line = string.Format("{0} {1} {2}", entry.Value.x, entry.Value.y, entry.Value.z);
                File.AppendAllText(path, line + "\n");
            }
        }
    }
}