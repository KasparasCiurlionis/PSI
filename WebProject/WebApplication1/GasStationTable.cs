namespace WebApplication1
{
    public class GasStationTable : IGasStationTable
    {
        public List<List<String>> GetTable(int id)
        {
            List<String> lines = GasData.getBrand();
            lines.Insert(0, "All");
            return ProcessData.process(lines[id], new GasStation());
        }
    }
}