using Aspose.Cells;

namespace RESAFIGSMS.Web.Helpers
{
	public static class FileHelpers
	{
		public static List<string> getListNumber(string fileName)
		{
			var list = new List<string>();

			Workbook wb = new Workbook(fileName);

			Worksheet ws = wb.Worksheets[0];

			int rows = ws.Cells.MaxDataRow;
			int cols = ws.Cells.MaxDataColumn;

			for (int i = 1; i <= rows; i++)
			{
				var item = ws.Cells[i, 1].Value == null ? "" : ws.Cells[i, 1].Value.ToString();

				if (item != null)
				{
					if (!item.StartsWith("225"))
						item = "225" + item.Replace(" ", "");


					list.Add(item);
				}
			}



			return list;
		}
	}

}
