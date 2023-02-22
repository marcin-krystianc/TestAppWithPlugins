using System;
using System.Linq;
using MyPlugins;
using ParquetSharp;

namespace Plugin1001
{
    public class Plugin1001 : IMyPlugin
    {
      private void WriteFile(string path)
        {
            int n = 5;
            var dt = new DateTime(2023, 1, 1);
            var timestamps = Enumerable.Range(0, n).Select(x => dt.AddDays(n)).ToArray();
            var objectIds = Enumerable.Range(0, n).Select(x => x).ToArray();
            var values = Enumerable.Range(0, n).Select(x => (float)x).ToArray();

            var columns = new Column[]
            {
                new Column<DateTime>("Timestamp"),
                new Column<int>("ObjectId"),
                new Column<float>("Value")
            };

            using var file = new ParquetFileWriter(path, columns);
            using var rowGroup = file.AppendRowGroup();

            using (var timestampWriter = rowGroup.NextColumn().LogicalWriter<DateTime>())
            {
                timestampWriter.WriteBatch(timestamps);
            }
            using (var objectIdWriter = rowGroup.NextColumn().LogicalWriter<int>())
            {
                objectIdWriter.WriteBatch(objectIds);
            }
            using (var valueWriter = rowGroup.NextColumn().LogicalWriter<float>())
            {
                valueWriter.WriteBatch(values);
            }

            file.Close();
        }

        private int ReadFile(string path)
        {
            var result = 0;
            using var file = new ParquetFileReader(path);

            for (int rowGroup = 0; rowGroup < file.FileMetaData.NumRowGroups; ++rowGroup) {
                using var rowGroupReader = file.RowGroup(rowGroup);
                var groupNumRows = checked((int) rowGroupReader.MetaData.NumRows);

                var groupTimestamps = rowGroupReader.Column(0).LogicalReader<DateTime>().ReadAll(groupNumRows);
                var groupObjectIds = rowGroupReader.Column(1).LogicalReader<int>().ReadAll(groupNumRows);
                var groupValues = rowGroupReader.Column(2).LogicalReader<float>().ReadAll(groupNumRows);

                result = groupTimestamps.Length + groupObjectIds.Length + groupValues.Length;
            }

            file.Close();
            return result;
        }

        public string GetResult(string path)
        {
            WriteFile(path);
            var result = ReadFile(path);
            return $"Hello from 1001 n={result}";
        }
    }
}