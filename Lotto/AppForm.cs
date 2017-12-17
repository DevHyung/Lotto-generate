using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lotto
{
    public partial class AppForm : Form
    {

        private bool[] search_highLowFlags = new bool[7];
        private bool[] search_oddEvenFlags = new bool[7];
        private bool[] search_consFlags = new bool[6];

        private bool[] gen_highLowFlags = new bool[7];
        private bool[] gen_oddEvenFlags = new bool[7];
        private bool[] gen_consFlags = new bool[6];
        private bool[] gen_fixed = new bool[45];
        private bool[] gen_exclude = new bool[45];
        
        private Image lottoDef = Properties.Resources.lotto;
        private Image check = (Properties.Resources.check);
        Random random = new Random();
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

        private DataTable gameDataTable;

        private DataTable gameSearchTable;
        private DataTable genDataTable;
        public AppForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            gameSearchTable = getGameDataTable();
            gameDataTable = getGameDataTable();
            genDataTable = getGenDataTable();
            initGameData();

            gameSearchTable = gameDataTable;
            GridView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridView_Data.DataSource = gameSearchTable;

            GridView_Gen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridView_Gen.DataSource = genDataTable;
            Label_SearchRowCount.Text = "항목 개수 : " + gameSearchTable.Rows.Count.ToString();

            clearGenData();
            
        }

        private DataTable getGameDataTable()
        {
            /* column data of gameDataTable *
             *    0     1           2           3           4              5           6           7           8           9       10          11       12
             * "time" "winNum" "totalSum" "hIghLowRate" "oddEvenRate" "consNum" "mDigitSum" "lDigitSum" "firstLastSum" "distSum" "frontSum" "rearSum" "gameDate"
             *  Uint16 String   Uint16      String       String         Byte        Byte        Byte        Byte            Byte       Byte     Byte    String
             *  749  12 14 24 26 34 45 [41] 155 저:2 고:4 홀:1 짝:5    0            13          25          57          33          50          105     2017-04-08
             */
            DataTable table = new DataTable();

            DataColumn time = new DataColumn("회차");
            time.DataType = System.Type.GetType("System.UInt16");
            time.AllowDBNull = false;
            table.Columns.Add(time);

            DataColumn winNum = new DataColumn("당첨 번호");
            winNum.DataType = System.Type.GetType("System.String");
            winNum.AllowDBNull = false;
            table.Columns.Add(winNum);

            DataColumn totalSum = new DataColumn("총합");
            totalSum.DataType = System.Type.GetType("System.UInt16");
            totalSum.AllowDBNull = false;
            table.Columns.Add(totalSum);

            DataColumn highLowRate = new DataColumn("저고율");
            highLowRate.DataType = System.Type.GetType("System.String");
            highLowRate.AllowDBNull = false;
            table.Columns.Add(highLowRate);

            DataColumn oddEvenRate = new DataColumn("홀짝율");
            oddEvenRate.DataType = System.Type.GetType("System.String");
            oddEvenRate.AllowDBNull = false;
            table.Columns.Add(oddEvenRate);

            DataColumn consNum = new DataColumn("연번");
            consNum.DataType = System.Type.GetType("System.Byte");
            consNum.AllowDBNull = false;
            table.Columns.Add(consNum);

            DataColumn mDigitSum = new DataColumn("첫수합");
            mDigitSum.DataType = System.Type.GetType("System.Byte");
            mDigitSum.AllowDBNull = false;
            table.Columns.Add(mDigitSum);

            DataColumn lDigitSum = new DataColumn("끝수합");
            lDigitSum.DataType = System.Type.GetType("System.Byte");
            lDigitSum.AllowDBNull = false;
            table.Columns.Add(lDigitSum);

            DataColumn firstLastSum = new DataColumn("고저합");
            firstLastSum.DataType = System.Type.GetType("System.Byte");
            firstLastSum.AllowDBNull = false;
            table.Columns.Add(firstLastSum);

            DataColumn distSum = new DataColumn("간격합");
            distSum.DataType = System.Type.GetType("System.Byte");
            distSum.AllowDBNull = false;
            table.Columns.Add(distSum);

            DataColumn frontSum = new DataColumn("123합");
            frontSum.DataType = System.Type.GetType("System.Byte");
            frontSum.AllowDBNull = false;
            table.Columns.Add(frontSum);

            DataColumn rearSum = new DataColumn("456합");
            rearSum.DataType = System.Type.GetType("System.Byte");
            rearSum.AllowDBNull = false;
            table.Columns.Add(rearSum);

            DataColumn gameDate = new DataColumn("추첨일");
            gameDate.DataType = System.Type.GetType("System.String");
            gameDate.AllowDBNull = false;
            table.Columns.Add(gameDate);

            return table;
        }
        private void initGameData()
        {
            /* column data of gameDataTable *
             *    0     1           2           3           4              5           6           7           8           9       10          11       12
             * "time" "winNum" "totalSum" "hIghLowRate" "oddEvenRate" "consNum" "mDigitSum" "lDigitSum" "firstLastSum" "distSum" "frontSum" "rearSum" "gameDate"
             *  Uint16 String   Uint16      String       String         Byte        Byte        Byte        Byte            Byte       Byte     Byte    String
             *  749  12 14 24 26 34 45 [41] 155 저:2 고:4 홀:1 짝:5    0            13          25          57          33          50          105     2017-04-08
             */
            string filePath = "data.txt";

            if (!File.Exists(filePath))
                throw new System.Exception("no_data_file");

            using (StreamReader fsRead = new StreamReader(filePath))
            {

                while (!fsRead.EndOfStream)
                {
                    string[] buffer = fsRead.ReadLine().Split();
                    var row = gameDataTable.NewRow();
                    row["회차"] = UInt16.Parse(buffer[0]);
                    String temp = "";
                    for (int i = 1; i <= 6; i++)
                        temp += (buffer[i] + (int.Parse(buffer[i]) % 2 == 1 ? " : 홀수 " : " : 짝수 ")).PadRight(8);
                    row["당첨 번호"] = (temp + buffer[7].PadRight(5));
                    row["추첨일"] = buffer[8];

                    setDataRow(ref row);
                    gameDataTable.Rows.Add(row);
                }
            }
        }
        private Byte[] ToWinNum(String str)
        {
            String[] elems = str.Split();
            Byte[] nums = new Byte[6];
            int i = 0;
            foreach (var elem in elems)
            {
                Byte buff;
                if (Byte.TryParse(elem, out buff))
                    nums[i++] = buff;
                else
                    continue;
            }
            return nums;
        }
        private void setDataRow(ref DataRow row)
        {
            Byte[] winNum = ToWinNum(row[1 /* 당첨번호 or 생성번호 */].ToString());
            Byte temp = 0;
            row["총합"] = 0;
            for (int i = 0; i < 6; i++) row["총합"] = (UInt16)row["총합"] + winNum[i];

            int count = 0;
            for (int i = 0; i < 6; i++) { if (winNum[i] < 23) count++; else break; }
            row["저고율"] = "저:" + count.ToString() + " 고:" + (6 - count).ToString();

            count = 0;
            for (int i = 0; i < 6; i++) if (winNum[i] % 2 == 1) count++;
            row["홀짝율"] = "홀:" + count.ToString() + " 짝:" + (6 - count).ToString();

            count = 0;
            for (int i = 0; i < 5; i++) if (winNum[i] + 1 == winNum[i + 1]) count++;
            row["연번"] = count;

            for (int i = 0; i < 6; i++)
            {
                if (winNum[i] / 10 == 0)
                    temp += winNum[i];
                else
                    temp += (Byte)(winNum[i] / 10);
            }
            row["첫수합"] = temp;

            temp = 0;
            for (int i = 0; i < 6; i++)
                temp += (Byte)(winNum[i] % 10);
            row["끝수합"] = temp;

            row["고저합"] = winNum[0] + winNum[5];
            row["간격합"] = winNum[5] - winNum[0];
            row["123합"] = winNum[0] + winNum[1] + winNum[2];
            row["456합"] = winNum[3] + winNum[4] + winNum[5];
        }
        private DataTable getGenDataTable()
        {

            /* column data of gameDataTable *
             *    0     1           2           3           4              5           6           7           8           9       10          11       12
             * "order" "genNum" "totalSum" "hIghLowRate" "oddEvenRate" "consNum" "mDigitSum" "lDigitSum" "firstLastSum" "distSum" "frontSum" "rearSum" "gameDate"
             *  Uint16 String   Uint16      String       String         Byte        Byte        Byte        Byte            Byte       Byte     Byte    String
             *  749  12 14 24 26 34 45 [41] 155 저:2 고:4 홀:1 짝:5    0            13          25          57          33          50          105     2017-04-08
             */
            DataTable table = new DataTable();

            DataColumn order = new DataColumn("순서");
            order.DataType = System.Type.GetType("System.UInt16");
            order.AllowDBNull = false;
            table.Columns.Add(order);

            DataColumn genNum = new DataColumn("생성 번호");
            genNum.DataType = System.Type.GetType("System.String");
            genNum.AllowDBNull = false;
            table.Columns.Add(genNum);

            DataColumn totalSum = new DataColumn("총합");
            totalSum.DataType = System.Type.GetType("System.UInt16");
            totalSum.AllowDBNull = false;
            table.Columns.Add(totalSum);

            DataColumn highLowRate = new DataColumn("저고율");
            highLowRate.DataType = System.Type.GetType("System.String");
            highLowRate.AllowDBNull = false;
            table.Columns.Add(highLowRate);

            DataColumn oddEvenRate = new DataColumn("홀짝율");
            oddEvenRate.DataType = System.Type.GetType("System.String");
            oddEvenRate.AllowDBNull = false;
            table.Columns.Add(oddEvenRate);

            DataColumn consNum = new DataColumn("연번");
            consNum.DataType = System.Type.GetType("System.Byte");
            consNum.AllowDBNull = false;
            table.Columns.Add(consNum);

            DataColumn mDigitSum = new DataColumn("첫수합");
            mDigitSum.DataType = System.Type.GetType("System.Byte");
            mDigitSum.AllowDBNull = false;
            table.Columns.Add(mDigitSum);

            DataColumn lDigitSum = new DataColumn("끝수합");
            lDigitSum.DataType = System.Type.GetType("System.Byte");
            lDigitSum.AllowDBNull = false;
            table.Columns.Add(lDigitSum);

            DataColumn firstLastSum = new DataColumn("고저합");
            firstLastSum.DataType = System.Type.GetType("System.Byte");
            firstLastSum.AllowDBNull = false;
            table.Columns.Add(firstLastSum);

            DataColumn distSum = new DataColumn("간격합");
            distSum.DataType = System.Type.GetType("System.Byte");
            distSum.AllowDBNull = false;
            table.Columns.Add(distSum);

            DataColumn frontSum = new DataColumn("123합");
            frontSum.DataType = System.Type.GetType("System.Byte");
            frontSum.AllowDBNull = false;
            table.Columns.Add(frontSum);

            DataColumn rearSum = new DataColumn("456합");
            rearSum.DataType = System.Type.GetType("System.Byte");
            rearSum.AllowDBNull = false;
            table.Columns.Add(rearSum);

            DataColumn genDate = new DataColumn("생성일");
            genDate.DataType = System.Type.GetType("System.String");
            genDate.AllowDBNull = false;
            table.Columns.Add(genDate);

            return table;
        }
        private void Button_Open_Click(object sender, EventArgs e)
        {
            loadGenData();
        }
        private void loadGenData()
        {
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "txt files (*.txt)|*.txt| xlsx files (*.xlsx)|*.xlsx";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (dlg.FilterIndex)
                {
                    case 1:
                        loadGenDataFromTxt(dlg.FileName);
                        break;
                    case 2:
                        loadGenDataFromExcel(dlg.FileName);
                        break;
                }
                Label_GenRowCount.Text = "항목 개수 : " + gameSearchTable.Rows.Count.ToString();

            }
        }
        private void loadGenDataFromTxt(string filePath)
        {
            using (StreamReader fsRead = new StreamReader(filePath))
            {
                while (!fsRead.EndOfStream)
                {
                    List<String> buffer = fsRead.ReadLine().Split().ToList();
                    buffer.RemoveAll((String x) => x.Length == 0);
                    var row = genDataTable.NewRow();
                    row["순서"] = genDataTable.Rows.Count + 1; 
                    String temp = "";
                    for (int i = 1; i <= 6; i++)
                        temp += (buffer[i] + (int.Parse(buffer[i]) % 2 == 1 ? " : 홀수" : " : 짝수")).PadRight(8);
                    row["생성 번호"] = temp;
                    row["생성일"] = buffer[buffer.Count-1];

                    setDataRow(ref row);
                    genDataTable.Rows.Add(row);
                }
            }
            collect();
        }
        private void collect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void loadGenDataFromExcel(string filePath)
        {
            var app = new Excel.Application();
            Excel.Workbook workBook = app.Workbooks.Open(filePath);
            Excel._Worksheet workSheet = workBook.Sheets[1];
            Excel.Range range = workSheet.UsedRange;
            object[,] data = range.Value;
            for (int i = 2; i <= range.Rows.Count; i++)
            {
                var row = genDataTable.NewRow();

                String str = data[i, 2].ToString();
                List<String> buffer = str.Split().ToList();
                buffer.RemoveAll((String x) => x.Length == 0);
                row["순서"] = genDataTable.Rows.Count + 1;
                String temp = "";
                for (int j = 0; j < 6; j++)
                    temp += (buffer[j] + (int.Parse(buffer[j]) % 2 == 1 ? " : 홀수 " : " : 짝수 ")).PadRight(8);
                row["생성 번호"] = temp;
                row["생성일"] = DateTime.Parse(data[i, 13].ToString()).ToShortDateString();
                setDataRow(ref row);
                genDataTable.Rows.Add(row);
            }
            workBook.Close();
            app.Quit();
            collect();

        }
        private void Button_Search_Data_Click(object sender, EventArgs e)
        {

            gameSearchTable.Dispose();
            gameSearchTable = getGameDataTable();
            search();
            GridView_Data.DataSource = gameSearchTable;
            Label_SearchRowCount.Text = "항목 개수 : " + gameSearchTable.Rows.Count.ToString();

        }
        private void Button_RevSearch_Data_Click(object sender, EventArgs e)
        {
            gameSearchTable.Dispose();
            gameSearchTable = getGameDataTable();
            reverseSearch();
            GridView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridView_Data.DataSource = gameSearchTable;
            GridView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Label_SearchRowCount.Text = "항목 개수 : " + gameSearchTable.Rows.Count.ToString();
        }
        private void clearSearchConditions()
        {
            TotalSumCheck_Search.Checked = false;
            TotalSumMin_Search.Value = TotalSumMin_Search.Minimum;
            TotalSumMax_Search.Value = TotalSumMax_Search.Maximum;

            FirstLastCheck_Search.Checked = false;
            FirstLastMin_Search.Value = FirstLastMin_Search.Minimum;
            FirstLastMax_Search.Value = FirstLastMax_Search.Maximum;

            LSumCheck_Search.Checked = false;
            LSumMin_Search.Value = LSumMin_Search.Minimum;
            LSumMax_Search.Value = LSumMax_Search.Maximum;

            MSumCheck_Search.Checked = false;
            MSumMin_Search.Value = MSumMin_Search.Minimum;
            MSumMax_Search.Value = MSumMax_Search.Maximum;

            DistSumCheck_Search.Checked = false;
            DistSumMin_Search.Value = DistSumMin_Search.Minimum;
            DistSumMax_Search.Value = DistSumMax_Search.Maximum;

            HighLowCheck_Search.Checked = false;
            HighLow_Search_0.Checked = HighLow_Search_1.Checked =
            HighLow_Search_2.Checked = HighLow_Search_3.Checked =
            HighLow_Search_4.Checked = HighLow_Search_5.Checked =
            HighLow_Search_6.Checked = false;

            OddEvenCheck_Search.Checked = false;
            OddEven_Search_0.Checked = OddEven_Search_1.Checked =
            OddEven_Search_2.Checked = OddEven_Search_3.Checked =
            OddEven_Search_4.Checked = OddEven_Search_5.Checked =
            OddEven_Search_6.Checked = false;

            ConsCheck_Search.Checked = false;
            Cons_Search_0.Checked = Cons_Search_1.Checked =
            Cons_Search_2.Checked = Cons_Search_3.Checked =
            Cons_Search_4.Checked = Cons_Search_5.Checked = false;

            FrontSumCheck_Search.Checked = false;
            FrontSumMin_Search.Value = FrontSumMin_Search.Minimum;
            FrontSumMax_Search.Value = FrontSumMax_Search.Maximum;

            RearSumCheck_Search.Checked = false;
            RearSumMin_Search.Value = RearSumMin_Search.Minimum;
            RearSumMax_Search.Value = RearSumMax_Search.Maximum;

            DateCheck_Search.Checked = false;
            StartDate_Search.Value = EndDate_Search.Value = DateTime.Now;

            search_consFlags.Initialize();
            search_highLowFlags.Initialize();
            search_oddEvenFlags.Initialize();
        }
        private void clearGenConditions()
        {
            TotalSumCheck_Gen.Checked = false;
            TotalSumMin_Gen.Value = TotalSumMin_Gen.Minimum;
            TotalSumMax_Gen.Value = TotalSumMax_Gen.Maximum;

            FirstLastCheck_Gen.Checked = false;
            FirstLastMin_Gen.Value = FirstLastMin_Gen.Minimum;
            FirstLastMax_Gen.Value = FirstLastMax_Gen.Maximum;

            LSumCheck_Gen.Checked = false;
            LSumMin_Gen.Value = LSumMin_Gen.Minimum;
            LSumMax_Gen.Value = LSumMax_Gen.Maximum;

            MSumCheck_Gen.Checked = false;
            MSumMin_Gen.Value = MSumMin_Gen.Minimum;
            MSumMax_Gen.Value = MSumMax_Gen.Maximum;

            DistSumCheck_Gen.Checked = false;
            DistSumMin_Gen.Value = DistSumMin_Gen.Minimum;
            DistSumMax_Gen.Value = DistSumMax_Gen.Maximum;

            HighLowCheck_Gen.Checked = false;
            HighLow_Gen_0.Checked = HighLow_Gen_1.Checked =
            HighLow_Gen_2.Checked = HighLow_Gen_3.Checked =
            HighLow_Gen_4.Checked = HighLow_Gen_5.Checked =
            HighLow_Gen_6.Checked = false;

            OddEvenCheck_Gen.Checked = false;
            OddEven_Gen_0.Checked = OddEven_Gen_1.Checked =
            OddEven_Gen_2.Checked = OddEven_Gen_3.Checked =
            OddEven_Gen_4.Checked = OddEven_Gen_5.Checked =
            OddEven_Gen_6.Checked = false;

            ConsCheck_Gen.Checked = false;
            Cons_Gen_0.Checked = Cons_Gen_1.Checked =
            Cons_Gen_2.Checked = Cons_Gen_3.Checked =
            Cons_Gen_4.Checked = Cons_Gen_5.Checked = false;

            FrontSumCheck_Gen.Checked = false;
            FrontSumMin_Gen.Value = FrontSumMin_Gen.Minimum;
            FrontSumMax_Gen.Value = FrontSumMax_Gen.Maximum;

            RearSumCheck_Gen.Checked = false;
            RearSumMin_Gen.Value = RearSumMin_Gen.Minimum;
            RearSumMax_Gen.Value = RearSumMax_Gen.Maximum;

            gen_consFlags.Initialize();
            gen_highLowFlags.Initialize();
            gen_oddEvenFlags.Initialize();
        }
        private void Button_ClearCondition_Data_Click(object sender, EventArgs e)
        {
            clearSearchConditions();
        }
        private void search()
        {
            bool[] searchConditions = new bool[11];

            foreach (DataRow row in gameDataTable.Rows)
            {
                searchConditions[0] = TotalSumCheck_Search.Checked &&
                    !checkTotalSum((UInt16)row["총합"], decimal.ToUInt16(TotalSumMin_Search.Value), decimal.ToUInt16(TotalSumMax_Search.Value));
                searchConditions[1] = FirstLastCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["고저합"], decimal.ToByte(FirstLastMin_Search.Value), decimal.ToByte(FirstLastMax_Search.Value));
                searchConditions[2] = MSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["첫수합"], decimal.ToByte(MSumMin_Search.Value), decimal.ToByte(MSumMax_Search.Value));
                searchConditions[3] = LSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["끝수합"], decimal.ToByte(LSumMin_Search.Value), decimal.ToByte(LSumMax_Search.Value));
                searchConditions[4] = DistSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["간격합"], decimal.ToByte(DistSumMin_Search.Value), decimal.ToByte(DistSumMax_Search.Value));

                searchConditions[5] = HighLowCheck_Search.Checked &&
                    !(search_highLowFlags[ToRate((String)row["저고율"])]);

                searchConditions[6] = OddEvenCheck_Search.Checked &&
                    !(search_oddEvenFlags[ToRate((String)row["홀짝율"])]);

                searchConditions[7] = ConsCheck_Search.Checked &&
                    !(search_consFlags[(Byte)row["연번"]]);

                searchConditions[8] = FrontSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["123합"], decimal.ToByte(FrontSumMin_Search.Value), decimal.ToByte(FrontSumMax_Search.Value));

                searchConditions[9] = RearSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["456합"], decimal.ToByte(RearSumMin_Search.Value), decimal.ToByte(RearSumMax_Search.Value));

                searchConditions[10] = DateCheck_Search.Checked &&
                    !checkDateBorder(DateTime.Parse((String)row["추첨일"]), StartDate_Search.Value, EndDate_Search.Value);

                bool matchWithCond = true;
                foreach (var cond in searchConditions)
                    matchWithCond = matchWithCond && !cond;

                if (matchWithCond)
                {
                    var temp = gameSearchTable.NewRow();
                    for (int i = 0; i < gameSearchTable.Columns.Count; i++)
                        temp[i] = row[i];
                    gameSearchTable.Rows.Add(temp);
                }
            }
        }
        private void reverseSearch()
        {
            bool[] searchConditions = new bool[11];

            foreach (DataRow row in gameDataTable.Rows)
            {
                searchConditions[0] = TotalSumCheck_Search.Checked &&
                    !checkTotalSum((UInt16)row["총합"], decimal.ToUInt16(TotalSumMin_Search.Value), decimal.ToUInt16(TotalSumMax_Search.Value));
                searchConditions[1] = FirstLastCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["고저합"], decimal.ToByte(FirstLastMin_Search.Value), decimal.ToByte(FirstLastMax_Search.Value));
                searchConditions[2] = MSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["첫수합"], decimal.ToByte(MSumMin_Search.Value), decimal.ToByte(MSumMax_Search.Value));
                searchConditions[3] = LSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["끝수합"], decimal.ToByte(LSumMin_Search.Value), decimal.ToByte(LSumMax_Search.Value));
                searchConditions[4] = DistSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["간격합"], decimal.ToByte(DistSumMin_Search.Value), decimal.ToByte(DistSumMax_Search.Value));

                searchConditions[5] = HighLowCheck_Search.Checked &&
                    !(search_highLowFlags[ToRate((String)row["저고율"])]);

                searchConditions[6] = OddEvenCheck_Search.Checked &&
                    !(search_oddEvenFlags[ToRate((String)row["홀짝율"])]);

                searchConditions[7] = ConsCheck_Search.Checked &&
                    !(search_consFlags[(Byte)row["연번"]]);

                searchConditions[8] = FrontSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["123합"], decimal.ToByte(FrontSumMin_Search.Value), decimal.ToByte(FrontSumMax_Search.Value));

                searchConditions[9] = RearSumCheck_Search.Checked &&
                    !checkByteBorder((Byte)row["456합"], decimal.ToByte(RearSumMin_Search.Value), decimal.ToByte(RearSumMax_Search.Value));

                searchConditions[10] = DateCheck_Search.Checked &&
                    !checkDateBorder(DateTime.Parse((String)row["추첨일"]), StartDate_Search.Value, EndDate_Search.Value);

                bool matchWithCond = true;
                foreach (var cond in searchConditions)
                    matchWithCond = matchWithCond && !cond;

                if (!matchWithCond)
                {
                    var temp = gameSearchTable.NewRow();
                    for (int i = 0; i < gameSearchTable.Columns.Count; i++)
                        temp[i] = row[i];
                    gameSearchTable.Rows.Add(temp);
                }
            }
        }
        private int ToRate(String str)
        {
            return str[str.IndexOf(':') + 1] - '0';
        }
        private bool checkDateBorder(DateTime time, DateTime start, DateTime end)
        {
            return (time >= start) && (time <= end);
        }
        private bool checkTotalSum(UInt16 totalSum, UInt16 min, UInt16 max)
        {
            return (totalSum >= min) && (totalSum <= max);
        }
        private bool checkByteBorder(Byte num, Byte min, Byte max)
        {
            return (num >= min) && (num <= max);
        }

        private void saveSearchAsText()
        {
            var dlg = new SaveFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "txt files (*.txt)|*.txt";
            dlg.Title = "검색 결과를 텍스트로 저장";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.ShowDialog();
            
            if (dlg.FileName != "")
            {
                FileStream fs = (FileStream)dlg.OpenFile();
                foreach (DataRow row in gameSearchTable.Rows)
                {
                    String str = "";
                    Byte[] buffer;
                    foreach (var item in row.ItemArray)
                    {
                        String temp = item.ToString();
                        if (temp.Contains("홀수") || temp.Contains("짝수"))
                        {
                            temp.Replace(" : 홀수 ", "\t");
                            temp.Replace(" : 짝수 ", "\t");
                        }
                        str += temp + "\t";
                    }
                    str += System.Environment.NewLine;
                    buffer = UTF8Encoding.UTF8.GetBytes(str);
                    fs.Write(buffer, 0, buffer.Length);
                }

                fs.Close();
            }
        }

        private void Button_SaveAsTxt_Data_Click(object sender, EventArgs e)
        {
            saveSearchAsText();
        }

        private void saveSearchCondition()
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.InitialDirectory = Application.StartupPath;
                dlg.Filter = "lsCond Files (*.lsc)|*.lsc";
                dlg.Title = "검색 조건 저장";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.ShowDialog();
                
                if (dlg.FileName != "")
                {
                    using (FileStream fs = (FileStream)dlg.OpenFile())
                    {
                        Byte[] buffer = new Byte[1024];
                        buffer.Initialize();
                        /* binary structure of lsc file *
                        * LSC - signature (ASCII) 3bytes, stands for lotto search condition
                        * totalSum, firstLast, MSum, LSum, distSum, highLow, oddEven, cons flags : 1byte
                        * frontSum, rearSum, date, 0,0,0,0,0 : 1byte
                        * totalMin, totalMax 2byte
                        * ...
                        * highLow_0, 1, ......, 0 1byte
                        * ...
                        * date
                        * 
                        *                               */
                        buffer[0] = (Byte)'L';
                        buffer[1] = (Byte)'S';
                        buffer[2] = (Byte)'C';

                        if (TotalSumCheck_Search.Checked)
                            buffer[3] |= 0x80;
                        if (FirstLastCheck_Search.Checked)
                            buffer[3] |= 0x40;
                        if (MSumCheck_Search.Checked)
                            buffer[3] |= 0x20;
                        if (LSumCheck_Search.Checked)
                            buffer[3] |= 0x10;
                        if (DistSumCheck_Search.Checked)
                            buffer[3] |= 0x08;
                        if (HighLowCheck_Search.Checked)
                            buffer[3] |= 0x04;
                        if (OddEvenCheck_Search.Checked)
                            buffer[3] |= 0x02;
                        if (ConsCheck_Search.Checked)
                            buffer[3] |= 0x01;

                        if (FrontSumCheck_Search.Checked)
                            buffer[4] |= 0x80;
                        if (RearSumCheck_Search.Checked)
                            buffer[4] |= 0x40;
                        if (DateCheck_Search.Checked)
                            buffer[4] |= 0x20;

                        buffer[5] = decimal.ToByte(TotalSumMin_Search.Value);
                        buffer[6] = decimal.ToByte(TotalSumMax_Search.Value);
                        buffer[7] = decimal.ToByte(FirstLastMin_Search.Value);
                        buffer[8] = decimal.ToByte(FirstLastMax_Search.Value);
                        buffer[9] = decimal.ToByte(MSumMin_Search.Value);
                        buffer[10] = decimal.ToByte(MSumMax_Search.Value);
                        buffer[11] = decimal.ToByte(LSumMin_Search.Value);
                        buffer[12] = decimal.ToByte(LSumMax_Search.Value);
                        buffer[13] = decimal.ToByte(DistSumMin_Search.Value);
                        buffer[14] = decimal.ToByte(DistSumMax_Search.Value);


                        for (int i = 0; i < search_highLowFlags.Length; i++)
                        {
                            if (search_highLowFlags[i])
                                buffer[15] |= (Byte)(0x80 >> i);
                        }
                        for (int i = 0; i < search_oddEvenFlags.Length; i++)
                        {
                            if (search_oddEvenFlags[i])
                                buffer[16] |= (Byte)(0x80 >> i);
                        }
                        for (int i = 0; i < search_consFlags.Length; i++)
                        {
                            if (search_consFlags[i])
                                buffer[17] |= (Byte)(0x80 >> i);
                        }

                        Byte[] dateTemp = UTF8Encoding.UTF8.GetBytes(StartDate_Search.Value.ToString() + '&' + EndDate_Search.Value.ToString());
                        buffer[18] = (Byte)dateTemp.Length;
                        (dateTemp).CopyTo(buffer, 19);
                        fs.Write(buffer, 0, 19 + dateTemp.Length);

                        fs.Close();
                    }
                }
            }
        }
        private void loadSearchCondition()
        {
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "lsCond Files (*.lsc)|*.lsc|lgCond Files (*.lgc)|*.lgc";
            dlg.Title = "검색 조건 불러오기";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (FileStream read = new FileStream(dlg.FileName, FileMode.Open))
                {
                    Byte[] buffer = new Byte[1024];
                    read.Read(buffer, 0, (int)read.Length);
                    read.Close();

                    TotalSumCheck_Search.Checked = (buffer[3] & 0x80) != 0x00;
                    FirstLastCheck_Search.Checked = (buffer[3] & 0x40) != 0x00;
                    MSumCheck_Search.Checked = (buffer[3] & 0x20) != 0x00;
                    LSumCheck_Search.Checked = (buffer[3] & 0x10) != 0x00;
                    DistSumCheck_Search.Checked = (buffer[3] & 0x08) != 0x00;
                    HighLowCheck_Search.Checked = (buffer[3] & 0x04) != 0x00;
                    OddEvenCheck_Search.Checked = (buffer[3] & 0x02) != 0x00;
                    ConsCheck_Search.Checked = (buffer[3] & 0x01) != 0x00;

                    FrontSumCheck_Search.Checked = (buffer[4] & 0x80) != 0x00;
                    RearSumCheck_Search.Checked = (buffer[4] & 0x40) != 0x00;

                    TotalSumMin_Search.Value = buffer[5];
                    TotalSumMax_Search.Value = buffer[6];
                    FirstLastMin_Search.Value = buffer[7];
                    FirstLastMax_Search.Value = buffer[8];
                    MSumMin_Search.Value = buffer[9];
                    MSumMax_Search.Value = buffer[10];
                    LSumMin_Search.Value = buffer[11];
                    LSumMax_Search.Value = buffer[12];
                    DistSumMin_Search.Value = buffer[13];
                    DistSumMax_Search.Value = buffer[14];

                    for (int i = 0; i < search_highLowFlags.Length; i++)
                        search_highLowFlags[i] = (buffer[15] & (0x80 >> i)) != 0x00;

                    bool[] flag = new bool[7];

                    search_highLowFlags.CopyTo(flag, 0);
                    HighLow_Search_0.Checked = search_highLowFlags[0];
                    HighLow_Search_1.Checked = search_highLowFlags[1];
                    HighLow_Search_2.Checked = search_highLowFlags[2];
                    HighLow_Search_3.Checked = search_highLowFlags[3];
                    HighLow_Search_4.Checked = search_highLowFlags[4];
                    HighLow_Search_5.Checked = search_highLowFlags[5];
                    HighLow_Search_6.Checked = search_highLowFlags[6];
                    flag.CopyTo(search_highLowFlags, 0);

                    for (int i = 0; i < search_oddEvenFlags.Length; i++)
                        search_oddEvenFlags[i] = (buffer[16] & (0x80 >> i)) != 0x00;

                    search_oddEvenFlags.CopyTo(flag, 0);
                    OddEven_Search_0.Checked = search_oddEvenFlags[0];
                    OddEven_Search_1.Checked = search_oddEvenFlags[1];
                    OddEven_Search_2.Checked = search_oddEvenFlags[2];
                    OddEven_Search_3.Checked = search_oddEvenFlags[3];
                    OddEven_Search_4.Checked = search_oddEvenFlags[4];
                    OddEven_Search_5.Checked = search_oddEvenFlags[5];
                    OddEven_Search_6.Checked = search_oddEvenFlags[6];
                    flag.CopyTo(search_oddEvenFlags, 0);


                    flag = new bool[6];

                    for (int i = 0; i < search_consFlags.Length; i++)
                        search_consFlags[i] = (buffer[17] & (0x80 >> i)) != 0x00;
                    search_consFlags.CopyTo(flag, 0);
                    Cons_Search_0.Checked = search_consFlags[0];
                    Cons_Search_1.Checked = search_consFlags[1];
                    Cons_Search_2.Checked = search_consFlags[2];
                    Cons_Search_3.Checked = search_consFlags[3];
                    Cons_Search_4.Checked = search_consFlags[4];
                    Cons_Search_5.Checked = search_consFlags[5];
                    flag.CopyTo(search_consFlags, 0);

                    if (dlg.FilterIndex == 1)
                    {
                        DateCheck_Search.Checked = (buffer[4] & 0x20) != 0x00;
                        Byte[] temp = new byte[buffer[18]];
                        for (int i = 0; i < buffer[18]; i++)
                        {
                            temp[i] = buffer[19 + i];
                        }

                        String[] dates = UTF8Encoding.UTF8.GetString(temp).Split('&');
                        StartDate_Search.Value = DateTime.Parse(dates[0]);
                        EndDate_Search.Value = DateTime.Parse(dates[1]);
                    }
                }
            }
        }
        private void Button_LoadCondition_Data_Click(object sender, EventArgs e)
        {
            loadSearchCondition();
        }
        private void Button_SaveCondition_Data_Click(object sender, EventArgs e)
        {
            saveSearchCondition();
        }

        private void clearGenData()
        {
            if (genDataTable != null)
                genDataTable.Dispose();
            genDataTable = getGenDataTable();
            //GridView_Gen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridView_Gen.DataSource = genDataTable;
            //GridView_Gen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Label_GenRowCount.Text = "항목 개수 : " + genDataTable.Rows.Count.ToString();
            collect();
        }
        private void saveGenAsText(string filter, string dlgName)
        {
            var dlg = new SaveFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = filter;
            dlg.Title = dlgName;
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (dlg.FileName != "")
            {
                FileStream fs = (FileStream)dlg.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                foreach (DataRow row in genDataTable.Rows)
                {
                    String str = "";
                    Byte[] buffer;
                    foreach (var item in row.ItemArray)
                    {
                        String temp = item.ToString();
                        if (temp.Contains("홀수") || temp.Contains("짝수"))
                        {
                            temp = temp.Replace(" : 홀수 ", "\t");
                            temp = temp.Replace(" : 짝수 ", "\t");
                        }
                        str += temp + "\t";
                    }
                    str += System.Environment.NewLine;
                    buffer = UTF8Encoding.UTF8.GetBytes(str);
                    fs.Write(buffer, 0, buffer.Length);
                }

                fs.Close();
            }
        }

        private void Button_ClearGen_Click(object sender, EventArgs e)
        {
            clearGenData();
        }
        private void Button_SaveAsTxt_Gen_Click(object sender, EventArgs e)
        {
            saveGenAsText("txt files (*.txt)|*.txt", "생성 결과를 텍스트로 저장");

        }
        private void Button_ClearCondition_Gen_Click(object sender, EventArgs e)
        {
            clearGenConditions();
        }
        private void Shuffle<T>(ref List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void Button_Generate_Click(object sender, EventArgs e)
        {
            if (genDataTable.Rows.Count >= 10000)
            {
                MessageBox.Show("만개 이상 생성할 수 없습니다!");
                return;
            }

            var genList = generate();
            foreach (var nums in genList)
            {
                if (genDataTable.Rows.Count >= 10000)
                {
                    MessageBox.Show("만개 이상 생성할 수 없습니다!");
                    break;;
                }
                var row = genDataTable.NewRow();
                String temp = "";
                for (int j = 0; j < 6; j++)
                    temp += (nums[j].ToString() + (int.Parse(nums[j].ToString()) % 2 == 1 ? " : 홀수 " : " : 짝수 ")).PadRight(8);
                row["순서"] = genDataTable.Rows.Count + 1;
                row["생성 번호"] = temp;
                row["생성일"] = DateTime.Now.ToShortDateString();
                setDataRow(ref row);
                genDataTable.Rows.Add(row);
            }
            Label_GenRowCount.Text = "항목 개수 : " + genDataTable.Rows.Count.ToString();
            collect();

        }
        private void checkTotalSum(ref List<Byte> list, Byte[] bytes)
        {
            /* complete */

            int totalSum = bytes.Sum(x => x);
            int totalMax, totalMin;

            totalMax = (int)TotalSumMax_Gen.Value;
            totalMin = (int)TotalSumMin_Gen.Value;
            // 총합수 max를 무조건 넘겨버리는 값을 지운다
            for (int i = 0; i < list.Count; i++)   // i loop for 0 ~ cnt + p -5
            {
                // 어떤 수에 대해 가능한 최소합은 sum + n + n+1 ..
                int minSum = 0;
                for (int j = 0; j < 6 - bytes.Length; j++)   // j loop for 0 ~ 4-pos
                {
                    minSum += list[i] + j;
                }

                if (totalSum + minSum > totalMax)
                {
                    list.RemoveRange(i, list.Count - i);
                    break;
                }
            }
            if (list.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    int maxSum = 0;
                    maxSum += list[i];
                    for (int j = 0; j < 5 - bytes.Length; j++)
                    {
                        maxSum += 45 - j;
                    }
                    if (totalSum + maxSum < totalMin)
                    {
                        list.RemoveRange(0, i + 1);
                        break;
                    }
                }
            }
        }
        private void checkFrontSum(ref List<Byte> list, Byte[] bytes)
        {
            /* completed */
            int pos = bytes.Length;
            if (pos > 2)   // pos == 0, 1, 2(마지막)
                return;
            // 4~6수의 내용은 관련이 없다
            int frontSum = bytes.Sum(x => x);

            for (int i = 0; i < list.Count; i++)
            {
                int minSum = list[i];
                for (int j = 1; j < 3 - pos; j++)
                {
                    minSum += list[i] + j;
                }
                if (frontSum + minSum > FrontSumMax_Gen.Value)
                {
                    list.RemoveRange(i, list.Count - i);
                    break;
                }
            }
            if (list.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    int maxSum = list[i];
                    for (int j = 1; j < 3 - pos; j++)
                    {
                        maxSum += 43 - j;
                    }
                    if (frontSum + maxSum < FrontSumMin_Gen.Value)
                    {
                        list.RemoveRange(0, i + 1);
                        break;

                    }
                }
            }
        }
        private void checkRearSum(ref List<Byte> list, Byte[] bytes)
        {
            /* completed */
            int pos = bytes.Length; // pos == 3, 4, 5
            if (pos < 3)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int min = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        min += list[i] + 3 - pos + j;
                    }
                    if (min > RearSumMax_Gen.Value)
                    {
                        list.RemoveRange(i, list.Count - i);
                        break;
                    }
                }
                return;
            }

            int rearSum = 0;
            for (int i = 3; i < pos; i++)
            {
                rearSum += bytes[i];
            }
            // pos == 3, 4, 5
            for (int i = 0; i < list.Count; i++)
            {
                int min = list[i];
                for (int j = 1; j < 6 - pos; j++)
                {
                    min += list[i] + j;
                }
                if (rearSum + min > RearSumMax_Gen.Value)
                {
                    list.RemoveRange(i, list.Count - i);
                    break;
                }
            }
            if (list.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    int maxSum = list[i];
                    for (int j = 1; j < 6 - pos; j++)
                    {
                        maxSum += 46 - j;
                    }
                    if (rearSum + maxSum < RearSumMin_Gen.Value)
                    {
                        list.RemoveRange(0, i + 1);
                        break;

                    }
                }
            }
        }
        private void checkFirstLast(ref List<Byte> list, Byte[] bytes)
        {
            /* complete */
            int pos = bytes.Length;
            int first = (pos == 0 ? 0 : bytes[0]);
            // 이번에 list에 남길 수는 bytes[pos]에 올수있는 수들이다
            // 이번에 올 수가 list[i]라 하면
            // 6수가 될 가능성이 있는 수는 list[i] + 5 - pos ~ 45

            for (int i = 0; i < list.Count; i++)
            {
                if (first + list[i] + 5 - pos > FirstLastMax_Gen.Value)
                {
                    list.RemoveRange(i, list.Count - i);
                    break;
                }
            }
            if (pos == 5)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (first + list[i] < FirstLastMin_Gen.Value)
                    {
                        list.RemoveRange(0, i + 1);
                        break;
                    }
                }
            }
        }
        private void checkMSum(ref List<Byte> list, Byte[] bytes)
        {
            /* complete */

            /* 첫수합은 1, 10 ~ 19는 1
            * 2, 20~29는 2
            * 3, 30~39는 3
            * 4, 40~45는 4
            * 5~9는 5~9
            * 최소는 6, 최대는 39(4(40, 41, .. , 45) 5 6 7 8 9)
            * */
            int pos = bytes.Length;
            List<Byte> deleteList = new List<Byte>();
            int mSum = bytes.Sum(x => (x / 10 == 0 ? x : x / 10));

            foreach (var num in list)
            {
                int sum = mSum + ((num / 10) == 0 ? num : num / 10);
                List<int> tempList = new List<int>(Enumerable.Range(num + 1, 45 - num));

                if (tempList.Count >= 5 - pos)
                {
                    int min = 0;
                    for (int i = 0; i < 5 - pos; i++)
                    {
                        for (int j = 1; j <= 9; j++)
                        {
                            int minIndex = tempList.FindIndex(x => ((x / 10 == 0 ? x : x / 10) == j));
                            if (minIndex == -1)
                                continue;
                            min += tempList[minIndex] / 10 == 0 ? tempList[minIndex] : tempList[minIndex] / 10;
                            tempList.RemoveAt(minIndex);
                            break;
                        }
                    }
                    if (sum + min > MSumMax_Gen.Value)
                        deleteList.Add(num);
                }
                else
                    deleteList.Add(num);
            }
            foreach (var num in deleteList)
            {
                list.Remove(num);
            }
            deleteList.Clear();
            foreach (var num in list)
            {
                int sum = mSum + ((num / 10) == 0 ? num : num / 10);
                List<int> tempList = new List<int>(Enumerable.Range(num + 1, 45 - num));

                if (tempList.Count >= 5 - pos)
                {
                    int max = 0;
                    for (int i = 0; i < 5 - pos; i++)
                    {
                        for (int j = 9; j >= 1; j--)
                        {
                            int maxIndex = tempList.FindIndex(x => ((x / 10 == 0 ? x : x / 10) == j));
                            if (maxIndex == -1)
                                continue;
                            max += (tempList[maxIndex] / 10 == 0 ? tempList[maxIndex] : tempList[maxIndex] / 10);
                            tempList.RemoveAt(maxIndex);
                            break;
                        }
                    }
                    if (sum + max < MSumMin_Gen.Value)
                        deleteList.Add(num);
                }
                else
                    deleteList.Add(num);
            }
            foreach (var num in deleteList)
            {
                list.Remove(num);
            }
        }
        private void checkLSum(ref List<Byte> list, Byte[] bytes)
        {
            /* complete */
            /* 끝수는
             * 10, 20, .., 40 : 0 (4)
             * 1, 11, .., 41 : 1 (5)
             * 2, 12, ...,42 : 2 (5)
             * 3, 13, ..., 43 : 3 (5)
             * 4, 14, ..., 44 : 4 (5)
             * 5, 15, ..., 45 : 5 (5)
             * 6, 16, ..., 36 : 6 (4)
             * ...
             * 9, 19, ..., 39 : 9 (4)
             * 최소는 2 , 최대는 52
             * */
            int pos = bytes.Length;
            List<Byte> deleteList = new List<Byte>();
            int lSum = bytes.Sum(x => x % 10);

            foreach (var num in list)
            {
                int sum = lSum + (num % 10);
                List<int> tempList = new List<int>(Enumerable.Range(num + 1, 45 - num));

                if (tempList.Count >= 5 - pos)
                {
                    int min = 0;
                    for (int i = 0; i < 5 - pos; i++)
                    {
                        for (int j = 0; j <= 9; j++)
                        {
                            int minIndex = tempList.FindIndex(x => ((x % 10) == j));
                            if (minIndex == -1)
                                continue;
                            tempList.RemoveAt(minIndex);
                            break;
                        }
                    }
                    if (sum + min > LSumMax_Gen.Value)
                        deleteList.Add(num);
                }
                else
                    deleteList.Add(num);
            }
            foreach (var num in deleteList)
            {
                list.Remove(num);
            }
            deleteList.Clear();
            foreach (var num in list)
            {
                int sum = lSum + (num % 10);
                List<int> tempList = new List<int>(Enumerable.Range(num + 1, 45 - num));

                if (tempList.Count >= 5 - pos)
                {
                    int max = 0;
                    for (int i = 0; i < 5 - pos; i++)
                    {
                        for (int j = 9; j >= 0; j--)
                        {
                            int maxIndex = tempList.FindIndex(x => ((x % 10) == j));
                            if (maxIndex == -1)
                                continue;
                            max += tempList[maxIndex];
                            tempList.RemoveAt(maxIndex);
                            break;
                        }
                    }
                    if (sum + max < LSumMin_Gen.Value)
                        deleteList.Add(num);
                }
                else
                    deleteList.Add(num);
            }
            foreach (var num in deleteList)
            {
                list.Remove(num);
            }
        }
        private void checkHighLow(ref List<Byte> list, Byte[] bytes)
        {
            /* completed */
            /* 저수는 0~ 6개 가능하다
             * gen_high
             * */

            int lowCount = bytes.Sum(x => x <23 ? 1 : 0);  // 현재 저수 개수

            bool moreLowPossible = false;
            for (int i = lowCount + 1; i < 7; i++)
            {
                moreLowPossible |= gen_highLowFlags[i];
            }

            if (!gen_highLowFlags[lowCount])    // 현재 저수는 조건 만족 안함
            {
                if (!moreLowPossible)   // 더 나와서도 안되면
                {
                    list.Clear();   // 불가능한 분기
                }
                else // 더 나와야 가능하면
                {
                    list.RemoveAll(x => x >= 23);   // 고수 제거
                }
            }
            else // 현재 저수는 조건 만족
            {
                if (!moreLowPossible)   // 더이상 저수가 나오면 안되면
                {
                    list.RemoveAll(x => x < 23);    // 저수를 모두 제거
                }
            }
        }
        private void checkOddEven(ref List<Byte> list, Byte[] bytes)
        {
            /* completed */
            int oddCount = bytes.Sum(x => x % 2);  // 현재 홀수 개수
            int leftNum = 6 - bytes.Length; // 앞으로 leftNum개 숫자가 올 수 있다
            bool moreOddPossible = false;
            for (int i = oddCount + 1; i < 7; i++)
            {
                moreOddPossible |= gen_oddEvenFlags[i];
            }

            if (moreOddPossible)    // 더 나올수 있다
            {
                if(!gen_oddEvenFlags[oddCount]) // 더 나와야만 하면
                {
                    int minOdd = 0;
                    for (int i = 1; i < 7 - oddCount; i++)  // i : 1 ~ 6 - oC
                    {
                        if (gen_oddEvenFlags[oddCount + i])    // [oddCount + 1] ~ [6]
                        {
                            minOdd = i;
                            break;
                        }
                    }
                    if (leftNum < minOdd)   // 앞으로 다 홀수라도 안되면
                    {
                        list.Clear();   // 불가능한 분기
                    }
                    if (leftNum == minOdd)
                    {
                        list.RemoveAll(x => x % 2 == 0);    // 짝수 제거
                    }
                }
            }
            else    // 더 나오면 안된다
            {
                if (gen_oddEvenFlags[oddCount]) // 현재 조건 만족
                {
                    list.RemoveAll(x => x % 2 == 1);    // 홀수 제거
                }
                else
                    list.Clear();   // 불가능한 분기
            }
            
        }
        private void checkCons(ref List<Byte> list, Byte[] bytes)
        {
            /* completed */
            int pos = bytes.Length;
            if (pos == 0)
                return;

            int consLeft = 6 - pos;    // 앞으로 최대 가능한 연번 쌍
            Byte last = bytes[pos - 1];

            int consCount = 0;  // 현재 연번쌍 개수
            for (int i = 0; i < pos - 1; i++)
            {
                if (bytes[i] + 1 == bytes[i + 1])
                    consCount++;
            }
            bool moreConsPossible = false;
            for (int i = consCount + 1; i < 6; i++)
            {
                moreConsPossible |= gen_consFlags[i];
            }


            if (gen_consFlags[consCount])   // 현재 조건 만족
            {
                if (!moreConsPossible)   // 더 나오면 안된다
                {
                    list.Remove((Byte)(last + 1));
                }
            }
            else   // 연번쌍이 더 나와야 함
            {
                int minCons = 0;
                for (int i = 1; i < 6 - consCount; i++) // i : 1 ~ 5 - cC
                {
                    if (gen_consFlags[consCount + i])   // [cC + 1] ~ [5]
                        minCons = i;
                }

                if (consLeft < minCons) // 다 연번이어도 안되면
                {
                    list.Clear();   // 불가능한 분기
                }
                else if (consLeft == minCons)   // 다 연번이어야 한다면
                {
                    int consIndex = list.FindIndex(x => x == last + 1); 
                    if (consIndex == -1)// 연번일 수 없으면
                        list.Clear();   // 불가능한 분기
                    else
                    {
                        int cons = list[consIndex];
                        list.RemoveAll(x => x != cons); // 연번만 남기고 다 지운다
                    }
                }
            }
        }
        private void checkDist(ref List<Byte> list, Byte[] bytes)
        {
            /* 간격합은 6수 - 1수
             * 
             * */
            int pos = bytes.Length;
            if (pos == 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int first = list[i];
                    int lastMax = 45;
                    if (lastMax - first < DistSumMin_Gen.Value)
                    {
                        list.RemoveRange(i, list.Count-i);
                        break;
                    }
                }
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                int first = bytes[0];
                int lastMin = list[i] + 5 - pos;
                if (lastMin - first > DistSumMax_Gen.Value)
                {
                    list.RemoveRange(i, list.Count - i);
                    break;
                }
            }
            if (pos == 5)
            {
                var first = bytes[0];
                list.RemoveAll(x => x - first > DistSumMax_Gen.Value || x - first < DistSumMin_Gen.Value);
            }
        }
      

        private void checkFixedNum(ref List<Byte> list, Byte[] bytes)
        {
            int alreadyFixed = bytes.Sum(x => gen_fixed[x-1] ? 1 : 0);
            int fixCount = gen_fixed.Sum(x => x ? 1 : 0);

            // moreFix개 만큼 고정수가 남아있다
            if (alreadyFixed >= fixCount)
                return;

            int moreToFix = fixCount - alreadyFixed;
            if (moreToFix > 6 - bytes.Length)
                list.Clear();

            else
            {
                int nextFix = 0;
                for (int i = 0; i < gen_fixed.Length; i++)
                {
                    if (gen_fixed[i] && !bytes.Contains((Byte)(i + 1)))
                    {
                        nextFix = i + 1;
                        break;
                    }
                }
                if (moreToFix == 6 - bytes.Length)
                    list.RemoveAll(x => x != nextFix);

                else
                    list.RemoveAll(x => x > nextFix);

            }
        }
        private Byte possibleNextByte(Byte[] bytes)
        {
            /* 1수부터 5수까지, 주어진 배열 bytes와 조건을 고려해
             * 다음에 올 수 있는 수를 임의로 하나 반환
             * 
             *********************/
            int byteLastVal = bytes.Length == 0 ? 0 : bytes[bytes.Length - 1];
            int fixNumCount = gen_fixed.Sum(x => x ? 1 : 0);
            List<Byte> list = new List<Byte>();
            foreach (var i in Enumerable.Range(byteLastVal + 1, 40 + bytes.Length - byteLastVal))
            {
                list.Add((Byte)i);
            }
            for (int i = 0; i < gen_exclude.Length; i++)
            {
                if (gen_exclude[i])
                    list.Remove((Byte)(i + 1));
            }
            // 다음에 올 수는 기본적으로 (마지막 수 + 1) ~ 40 + pos

            if (fixNumCount > 0) checkFixedNum(ref list, bytes);
            if (TotalSumCheck_Gen.Checked) checkTotalSum(ref list, bytes);
            if (FrontSumCheck_Gen.Checked) checkFrontSum(ref list, bytes);
            if (RearSumCheck_Gen.Checked) checkRearSum(ref list, bytes);
            if (FirstLastCheck_Gen.Checked) checkFirstLast(ref list, bytes);
            if (MSumCheck_Gen.Checked) checkMSum(ref list, bytes);
            if (LSumCheck_Gen.Checked) checkLSum(ref list, bytes);
            if (HighLowCheck_Gen.Checked) checkHighLow(ref list, bytes);
            if (OddEvenCheck_Gen.Checked) checkOddEven(ref list, bytes);
            if (ConsCheck_Gen.Checked) checkCons(ref list, bytes);
            if (DistSumCheck_Gen.Checked) checkDist(ref list, bytes);
            //Shuffle(list);
            collect();

            if (list.Count > 0)
                return list[random.Next(0, list.Count - 1)];
            else return 0;
        }
        private List<Byte> possibleNextBytes(Byte[] bytes)
        {
            /* 1수부터 5수까지, 주어진 배열 bytes와 조건을 고려해
             * 다음에 올 수 있는 수 들을 리스트로 반환
             * 
             *********************/
            int byteLastVal = bytes.Length == 0 ? 0 : bytes[bytes.Length - 1];
            int fixNumCount = gen_fixed.Sum(x => x ? 1 : 0);
            List<Byte> list = new List<Byte>();
            foreach(var i in Enumerable.Range(byteLastVal + 1, 40 + bytes.Length - byteLastVal))
            {
                list.Add((Byte)i);
            }
            for (int i = 0; i < gen_exclude.Length; i++)
            {
                if (gen_exclude[i])
                    list.Remove((Byte)(i + 1));
            }
            // 다음에 올 수는 기본적으로 (마지막 수 + 1) ~ 40 + pos

            if (fixNumCount > 0) checkFixedNum(ref list, bytes);
            if(TotalSumCheck_Gen.Checked) checkTotalSum(ref list, bytes);
            if(FrontSumCheck_Gen.Checked) checkFrontSum(ref list, bytes);
            if (RearSumCheck_Gen.Checked) checkRearSum(ref list, bytes);
            if (FirstLastCheck_Gen.Checked)checkFirstLast(ref list, bytes);
            if (MSumCheck_Gen.Checked ) checkMSum(ref list, bytes);
            if (LSumCheck_Gen.Checked ) checkLSum(ref list, bytes);
            if (HighLowCheck_Gen.Checked ) checkHighLow(ref list, bytes);
            if (OddEvenCheck_Gen.Checked ) checkOddEven(ref list, bytes);
            if (ConsCheck_Gen.Checked )checkCons(ref list, bytes);
            if (DistSumCheck_Gen.Checked ) checkDist(ref list, bytes);
            Shuffle(ref list);
            collect();

            return list;
        }
        private List<Byte[]> generate()
        {
            List<Byte[]> list = new List<byte[]>(); // 생성한 경우의 수 
            List<Byte[]> retList = new List<byte[]>();  // 임의로 선택해서 반환

            var watch = System.Diagnostics.Stopwatch.StartNew();
            gen(ref list, watch);   // 3초 경과할 때 까지 개별 생성

            if (list.Count < GenNum.Value && MessageBox.Show("임의 생성에 실패했습니다. 전체 경우의 수를 탐색할까요?","생성 실패", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();
                list = genAll(watch);  // 7초 경과할 때 까지 전체 탐색
            }
            if (list.Count <= 0)
                MessageBox.Show("경우의 수를 탐색할 수 없습니다..");
            else
            {
                for (int i = 0; i < GenNum.Value; i++)
                {
                    retList.Add(list[random.Next(0, list.Count - 1)]);
                }
                list.Clear();
            }
            collect();

            return retList;
        }
        private bool checkFinal(Byte[] bytes)
        {
            int fixNumCount = gen_fixed.Sum(x => x ? 1 : 0);
            if (fixNumCount > 0)
            {
                for (int i = 0; i < gen_fixed.Length; i++)
                {
                    if (gen_fixed[i] && !bytes.Contains((Byte)(i + 1)))
                        return false;
                }
            }
            foreach (Byte foo in bytes)
                if (gen_exclude[foo - 1])
                    return false;
            if (TotalSumCheck_Gen.Checked)
            {
                int sum = bytes.Sum(x => x);
                if (!(sum >= TotalSumMin_Gen.Value && sum <= TotalSumMax_Gen.Value))
                    return false;
            }
            if (FrontSumCheck_Gen.Checked)
            {
                int frontSum = bytes[0] + bytes[1] + bytes[2];
                if (!(frontSum >= FrontSumMin_Gen.Value && frontSum <= FrontSumMax_Gen.Value))
                    return false;
            }
            if (RearSumCheck_Gen.Checked)
            {
                int rearSum = bytes[3] + bytes[4] + bytes[5];
                if (!(rearSum >= RearSumMin_Gen.Value && rearSum <= RearSumMax_Gen.Value))
                    return false;
            }
            if (FirstLastCheck_Gen.Checked)
            {
                int flSum = bytes[0] + bytes[5];
                if (!(flSum >= FirstLastMin_Gen.Value && flSum <= FirstLastMax_Gen.Value))
                    return false;
            }
            if (MSumCheck_Gen.Checked)
            {
                int mSum = bytes.Sum(x => x / 10 == 0 ? x : x / 10);
                if (!(mSum >= MSumMin_Gen.Value && mSum <= MSumMax_Gen.Value))
                    return false;
            }
            if (LSumCheck_Gen.Checked)
            {
                int lSum = bytes.Sum(x => x % 10);
                if (!(lSum >= LSumMin_Gen.Value && lSum <= LSumMax_Gen.Value))
                    return false;
            }
            if (HighLowCheck_Gen.Checked)
            {
                int lowCount = bytes.Sum(x => x < 23 ? 1 : 0);
                if (!gen_highLowFlags[lowCount])
                    return false;
            }
            if (OddEvenCheck_Gen.Checked)
            {
                int oddCount = bytes.Sum(x => x % 2);
                if (!gen_oddEvenFlags[oddCount])
                    return false;
            }
            if (ConsCheck_Gen.Checked)
            {
                int consCount = 0;
                for (int i = 0; i < 5; i++)
                    if (bytes[i] + 1 == bytes[i + 1])
                        consCount++;

                if (!gen_consFlags[consCount])
                    return false;
            }
            if (DistSumCheck_Gen.Checked)
            {
                int distSum = bytes[5] - bytes[1];
                if (!(distSum >= DistSumMin_Gen.Value && distSum <= DistSumMax_Gen.Value))
                    return false;
            }


            return true;
        }
        private void gen(ref List<Byte[]> list, System.Diagnostics.Stopwatch watch)
        {
            List<Byte> nums = new List<byte>();
            for (int i = 0; i < 23; i++)
                nums.Add((Byte)(1 + i * 2));
            for (int i = 1; i < 23; i++)
                nums.Add((Byte)(i * 2));
            Shuffle(ref nums);


            while (list.Count < GenNum.Value && watch.ElapsedMilliseconds < 5000)   
            {
                Shuffle(ref nums);
                Byte[] num = new byte[6];

                for (int i = 0; i < 6; i++)
                {
                    num[i] = nums[i];
                    nums[i] = nums[44 - i];
                    nums[44 - i] = num[i];
                }
                num = num.OrderBy(x => x).ToArray();
                if (checkFinal(num))
                    list.Add(num);
            }
            collect();
        }
        private List<Byte[]> genAll(System.Diagnostics.Stopwatch watch)
        {
            List<Byte[]> list = new List<byte[]>();
            List<Byte> firstNums = possibleNextBytes(new Byte[0]);
            List<Thread> threadList = new List<Thread>();
            
            foreach (Byte num in firstNums)
            {
                var thread = new Thread(() =>
               {
                   List<Byte[]> left = new List<byte[]>();
                   left.Add(new Byte[1] { num });
                   while (left.Count > 0)
                   {
                       int pos = random.Next(0, left.Count - 1);

                       Byte[] bytes = left[pos];
                       left.RemoveAt(pos);

                       if (bytes.Length == 6)
                       {
                           list.Add(bytes);
                       }
                        else
                        {
                            var nextList = possibleNextBytes(bytes);
                          
                           foreach (var next in nextList)
                           {
                               Byte[] temp = new Byte[bytes.Length + 1];
                               bytes.CopyTo(temp, 0);
                               temp[bytes.Length] = next;
                               left.Add(temp);
                           }
                           nextList.Clear();

                        }
                    }
               }
                );
                threadList.Add(thread);
            }
               
            foreach (var th in threadList)
                th.Start();

            while (watch.ElapsedMilliseconds < 5000)
            { }
            if (list.Count <= 0 && 
                DialogResult.Yes != MessageBox.Show("전체 경우의 수 탐색에 시간이 소요됩니다.. 계속할까요?", "전체 경우의 수 탐색", 
                MessageBoxButtons.YesNo))
                {
                    foreach (var th in threadList)
                        th.Abort();
                }
            
            foreach (var th in threadList)
                th.Join();

            collect();
            return list;
        }

        private void HighLow_Gen_0_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[0] = !gen_highLowFlags[0];
        }

        private void HighLow_Gen_1_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[1] = !gen_highLowFlags[1];

        }

        private void HighLow_Gen_2_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[2] = !gen_highLowFlags[2];

        }

        private void HighLow_Gen_3_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[3] = !gen_highLowFlags[3];

        }

        private void HighLow_Gen_4_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[4] = !gen_highLowFlags[4];

        }

        private void HighLow_Gen_5_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[5] = !gen_highLowFlags[5];

        }

        private void HighLow_Gen_6_CheckedChanged(object sender, EventArgs e)
        {
            gen_highLowFlags[6] = !gen_highLowFlags[6];

        }

        private void OddEven_Gen_6_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[6] = !gen_oddEvenFlags[6];

        }

        private void OddEven_Gen_5_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[5] = !gen_oddEvenFlags[5];

        }

        private void OddEven_Gen_4_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[4] = !gen_oddEvenFlags[4];

        }

        private void OddEven_Gen_3_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[3] = !gen_oddEvenFlags[3];

        }

        private void OddEven_Gen_2_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[2] = !gen_oddEvenFlags[2];

        }

        private void OddEven_Gen_1_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[1] = !gen_oddEvenFlags[1];

        }

        private void OddEven_Gen_0_CheckedChanged(object sender, EventArgs e)
        {
            gen_oddEvenFlags[0] = !gen_oddEvenFlags[0];

        }

        private void Cons_Gen_0_CheckedChanged(object sender, EventArgs e)
        {
            gen_consFlags[0] = !gen_consFlags[0];

        }

        private void Cons_Gen_1_CheckedChanged(object sender, EventArgs e)
        {
            gen_consFlags[1] = !gen_consFlags[1];

        }

        private void Cons_Gen_2_CheckedChanged(object sender, EventArgs e)
        {
            gen_consFlags[2] = !gen_consFlags[2];

        }

        private void Cons_Gen_3_CheckedChanged(object sender, EventArgs e)
        {
            gen_consFlags[3] = !gen_consFlags[3];

        }

        private void Cons_Gen_4_CheckedChanged(object sender, EventArgs e)
        {
            gen_consFlags[4] = !gen_consFlags[4];

        }

        private void Cons_Gen_5_CheckedChanged(object sender, EventArgs e)
        {
            gen_consFlags[5] = !gen_consFlags[5];

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_SearchRowCount.Text = "항목 개수 : " + gameSearchTable.Rows.Count.ToString();
            Label_GenRowCount.Text = "항목 개수 : " + genDataTable.Rows.Count.ToString();

        }

        private void HighLow_Search_0_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[0] = !search_highLowFlags[0];
        }

        private void HighLow_Search_1_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[1] = !search_highLowFlags[1];

        }

        private void HighLow_Search_2_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[2] = !search_highLowFlags[2];
        }

        private void HighLow_Search_3_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[3] = !search_highLowFlags[3];

        }

        private void HighLow_Search_4_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[4] = !search_highLowFlags[4];

        }

        private void HighLow_Search_5_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[5] = !search_highLowFlags[5];

        }

        private void HighLow_Search_6_CheckedChanged(object sender, EventArgs e)
        {
            search_highLowFlags[6] = !search_highLowFlags[6];

        }

        private void OddEven_Search_0_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[0] = !search_oddEvenFlags[0];

        }

        private void OddEven_Search_1_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[1] = !search_oddEvenFlags[1];

        }

        private void OddEven_Search_2_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[2] = !search_oddEvenFlags[2];

        }

        private void OddEven_Search_3_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[3] = !search_oddEvenFlags[3];

        }

        private void OddEven_Search_4_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[4] = !search_oddEvenFlags[4];

        }

        private void OddEven_Search_5_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[5] = !search_oddEvenFlags[5];

        }

        private void OddEven_Search_6_CheckedChanged(object sender, EventArgs e)
        {
            search_oddEvenFlags[6] = !search_oddEvenFlags[6];

        }

        private void Cons_Search_0_CheckedChanged(object sender, EventArgs e)
        {
            search_consFlags[0] = !search_consFlags[0];

        }

        private void Cons_Search_1_CheckedChanged(object sender, EventArgs e)
        {
            search_consFlags[1] = !search_consFlags[1];

        }

        private void Cons_Search_2_CheckedChanged(object sender, EventArgs e)
        {
            search_consFlags[2] = !search_consFlags[2];

        }

        private void Cons_Search_3_CheckedChanged(object sender, EventArgs e)
        {
            search_consFlags[3] = !search_consFlags[3];

        }

        private void Cons_Search_4_CheckedChanged(object sender, EventArgs e)
        {
            search_consFlags[4] = !search_consFlags[4];

        }

        private void Cons_Search_5_CheckedChanged(object sender, EventArgs e)
        {
            search_consFlags[5] = !search_consFlags[5];

        }
      
        private Point ToMiddle(int x, int y)
        {
            return new Point(x * 23 - 7, y * 32 - 12);
        }
        private Point ToLeftTop(int x, int y)
        {
            return new Point(x * 23 - 13, y * 32 - 22);
        }
        private void genRowClick(int rowIndex)
        {
            if (rowIndex == -1)
                return;

            var row = genDataTable.Rows[rowIndex];
            var nums = ToWinNum(row[1].ToString());

            Image temp = new Bitmap(lottoDef.Width, lottoDef.Height);
            using (Pen pen = new Pen(Brushes.Blue))
            {
                using (Graphics grfx = Graphics.FromImage(temp))
                {
                    grfx.DrawImage(lottoDef, 0, 0);
                }

                for (int i = 0; i < nums.Length - 1; i++)
                {
                    // draw line for i~i+1;
                    using (Graphics grfx = Graphics.FromImage(temp))
                    {
                        Point from = ToMiddle( (nums[i]-1) % 7 + 1, (nums[i]-1) / 7 + 1);
                        Point to = ToMiddle((nums[i+1] - 1) % 7 + 1, (nums[i+1] - 1) / 7 + 1);

                        grfx.DrawLine(pen, from, to);
                    }
                }
            }
            foreach (var num in nums)
            {
                using (Graphics grfx = Graphics.FromImage(temp))
                {
                    grfx.DrawImage(check, ToLeftTop((num - 1) % 7 + 1, (num - 1) / 7 + 1));
                }
            }
            Picture_Gen.Image = temp;
        }
        private void searchRowClick(int rowIndex)
        {
            if (rowIndex == -1)
                return;

            var row = gameSearchTable.Rows[rowIndex];
            var nums = ToWinNum(row[1].ToString());

            Image temp = new Bitmap(lottoDef.Width, lottoDef.Height);
            using (Pen pen = new Pen(Brushes.Blue))
            {
                using (Graphics grfx = Graphics.FromImage(temp))
                {
                    grfx.DrawImage(lottoDef, 0, 0);
                }

                for (int i = 0; i < nums.Length - 1; i++)
                {
                    // draw line for i~i+1;
                    using (Graphics grfx = Graphics.FromImage(temp))
                    {
                        Point from = ToMiddle((nums[i] - 1) % 7 + 1, (nums[i] - 1) / 7 + 1);
                        Point to = ToMiddle((nums[i + 1] - 1) % 7 + 1, (nums[i + 1] - 1) / 7 + 1);

                        grfx.DrawLine(pen, from, to);
                    }
                }
            }
            foreach (var num in nums)
            {
                using (Graphics grfx = Graphics.FromImage(temp))
                {
                    grfx.DrawImage(check, ToLeftTop((num - 1) % 7 + 1, (num - 1) / 7 + 1));
                }
            }
            Picture_Data.Image = temp;
        }
        private void GridView_Gen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            genRowClick(e.RowIndex);
        }

        private void GridView_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            searchRowClick(e.RowIndex);
        }

        private void GenByInput_Click(object sender, EventArgs e)
        {
            GenerateByInput();
        }
        private void GenerateByInput()
        {
            using (var form = new GetInputForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var bytes = form.bytes;
                    var row = genDataTable.NewRow();
                    String temp = "";
                    for (int j = 0; j < 6; j++)
                        temp += (bytes[j].ToString() + (int.Parse(bytes[j].ToString()) % 2 == 1 ? " : 홀수 " : " : 짝수 ")).PadRight(8);
                    row["순서"] = genDataTable.Rows.Count + 1;
                    row["생성 번호"] = temp;
                    
                    row["생성일"] = DateTime.Now.ToShortDateString();
                    setDataRow(ref row);
                    genDataTable.Rows.Add(row);
                }
                Label_GenRowCount.Text = "항목 개수 : " + genDataTable.Rows.Count.ToString();

            }
        }
        private void saveGenAsExcel()
        {
            String filePath = "";
            using (var dlg = new SaveFileDialog())
            {
                dlg.InitialDirectory = Application.StartupPath;
                dlg.Filter = "xlsx files (*.xlsx)|*.xlsx";
                dlg.Title = "생성 번호 저장";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filePath = dlg.FileName;
                    var app = new Excel.Application();
                    Excel.Workbook workBook = app.Workbooks.Add("");
                    Excel._Worksheet workSheet = workBook.ActiveSheet;
                    for (int i = 0; i < genDataTable.Columns.Count; i++)
                    {
                        workSheet.Cells[1, i + 1] = genDataTable.Columns[i].ColumnName;
                    }
                    for (int i = 0; i < genDataTable.Rows.Count; i++)
                    {
                        var row = genDataTable.Rows[i];
                        for (int j = 0; j < genDataTable.Columns.Count; j++)
                        {
                            String temp = row[j].ToString();
                            if (temp.Contains("홀수") || temp.Contains("짝수"))
                            {
                                temp = temp.Replace(" : 홀수 ", "   ");
                                temp = temp.Replace(" : 짝수 ", "   ");
                            }
                            workSheet.Cells[i + 2, j + 1] = temp;
                        }
                    }
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    workBook.SaveAs(filePath);
                    workBook.Close();
                    app.Quit();


                }
            }
            collect();

        }
        private void Button_SaveAsExcel_Gen_Click(object sender, EventArgs e)
        {
            saveGenAsExcel();
        }
        private void saveGenConditions()
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.InitialDirectory = Application.StartupPath;
                dlg.Filter = "lgCond Files (*.lgc)|*.lgc";
                dlg.Title = "생성 조건 저장";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (dlg.FileName != "")
                {
                    using (FileStream fs = (FileStream)dlg.OpenFile())
                    {
                        Byte[] buffer = new Byte[1024];
                        buffer.Initialize();
                        /* binary structure of lsc file *
                        * LSC - signature (ASCII) 3bytes, stands for lotto search condition
                        * totalSum, firstLast, MSum, LSum, distSum, highLow, oddEven, cons flags : 1byte
                        * frontSum, rearSum, date, 0,0,0,0,0 : 1byte
                        * totalMin, totalMax 2byte
                        * ...
                        * highLow_0, 1, ......, 0 1byte
                        * ...
                        * date
                        * 
                        *                               */
                        buffer[0] = (Byte)'L';
                        buffer[1] = (Byte)'G';
                        buffer[2] = (Byte)'C';

                        if (TotalSumCheck_Gen.Checked)
                            buffer[3] |= 0x80;
                        if (FirstLastCheck_Gen.Checked)
                            buffer[3] |= 0x40;
                        if (MSumCheck_Gen.Checked)
                            buffer[3] |= 0x20;
                        if (LSumCheck_Gen.Checked)
                            buffer[3] |= 0x10;
                        if (DistSumCheck_Gen.Checked)
                            buffer[3] |= 0x08;
                        if (HighLowCheck_Gen.Checked)
                            buffer[3] |= 0x04;
                        if (OddEvenCheck_Gen.Checked)
                            buffer[3] |= 0x02;
                        if (ConsCheck_Gen.Checked)
                            buffer[3] |= 0x01;

                        if (FrontSumCheck_Gen.Checked)
                            buffer[4] |= 0x80;
                        if (RearSumCheck_Gen.Checked)
                            buffer[4] |= 0x40;

                        buffer[5] = decimal.ToByte(TotalSumMin_Gen.Value);
                        buffer[6] = decimal.ToByte(TotalSumMax_Gen.Value);
                        buffer[7] = decimal.ToByte(FirstLastMin_Gen.Value);
                        buffer[8] = decimal.ToByte(FirstLastMax_Gen.Value);
                        buffer[9] = decimal.ToByte(MSumMin_Gen.Value);
                        buffer[10] = decimal.ToByte(MSumMax_Gen.Value);
                        buffer[11] = decimal.ToByte(LSumMin_Gen.Value);
                        buffer[12] = decimal.ToByte(LSumMax_Gen.Value);
                        buffer[13] = decimal.ToByte(DistSumMin_Gen.Value);
                        buffer[14] = decimal.ToByte(DistSumMax_Gen.Value);


                        for (int i = 0; i < gen_highLowFlags.Length; i++)
                        {
                            if (gen_highLowFlags[i])
                                buffer[15] |= (Byte)(0x80 >> i);
                        }
                        for (int i = 0; i < gen_oddEvenFlags.Length; i++)
                        {
                            if (gen_oddEvenFlags[i])
                                buffer[16] |= (Byte)(0x80 >> i);
                        }
                        for (int i = 0; i < gen_consFlags.Length; i++)
                        {
                            if (gen_consFlags[i])
                                buffer[17] |= (Byte)(0x80 >> i);
                        }
                        
                        fs.Write(buffer, 0, 18);
                        fs.Close();
                    }
                }
            }
        }
        
        private void loadGenConditions()
        {
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "lgc Files (*.lgc)|*.lgc|lsc Files (*.lsc)|*.lsc";
            dlg.Title = "생성 조건 불러오기";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (FileStream read = new FileStream(dlg.FileName, FileMode.Open))
                {
                    Byte[] buffer = new Byte[1024];
                    read.Read(buffer, 0, (int)read.Length);
                    read.Close();

                    TotalSumCheck_Gen.Checked = (buffer[3] & 0x80) != 0x00;
                    FirstLastCheck_Gen.Checked = (buffer[3] & 0x40) != 0x00;
                    MSumCheck_Gen.Checked = (buffer[3] & 0x20) != 0x00;
                    LSumCheck_Gen.Checked = (buffer[3] & 0x10) != 0x00;
                    DistSumCheck_Gen.Checked = (buffer[3] & 0x08) != 0x00;
                    HighLowCheck_Gen.Checked = (buffer[3] & 0x04) != 0x00;
                    OddEvenCheck_Gen.Checked = (buffer[3] & 0x02) != 0x00;
                    ConsCheck_Gen.Checked = (buffer[3] & 0x01) != 0x00;

                    FrontSumCheck_Gen.Checked = (buffer[4] & 0x80) != 0x00;
                    RearSumCheck_Gen.Checked = (buffer[4] & 0x40) != 0x00;

                    TotalSumMin_Gen.Value = buffer[5];
                    TotalSumMax_Gen.Value = buffer[6];
                    FirstLastMin_Gen.Value = buffer[7];
                    FirstLastMax_Gen.Value = buffer[8];
                    MSumMin_Gen.Value = buffer[9];
                    MSumMax_Gen.Value = buffer[10];
                    LSumMin_Gen.Value = buffer[11];
                    LSumMax_Gen.Value = buffer[12];
                    DistSumMin_Gen.Value = buffer[13];
                    DistSumMax_Gen.Value = buffer[14];

                    for (int i = 0; i < gen_highLowFlags.Length; i++)
                        gen_highLowFlags[i] = (buffer[15] & (0x80 >> i)) != 0x00;

                    bool[] flag = new bool[7];

                    gen_highLowFlags.CopyTo(flag, 0);
                    HighLow_Gen_0.Checked = gen_highLowFlags[0];
                    HighLow_Gen_1.Checked = gen_highLowFlags[1];
                    HighLow_Gen_2.Checked = gen_highLowFlags[2];
                    HighLow_Gen_3.Checked = gen_highLowFlags[3];
                    HighLow_Gen_4.Checked = gen_highLowFlags[4];
                    HighLow_Gen_5.Checked = gen_highLowFlags[5];
                    HighLow_Gen_6.Checked = gen_highLowFlags[6];
                    flag.CopyTo(gen_highLowFlags, 0);

                    for (int i = 0; i < gen_oddEvenFlags.Length; i++)
                        gen_oddEvenFlags[i] = (buffer[16] & (0x80 >> i)) != 0x00;

                    gen_oddEvenFlags.CopyTo(flag, 0);
                    OddEven_Gen_0.Checked = gen_oddEvenFlags[0];
                    OddEven_Gen_1.Checked = gen_oddEvenFlags[1];
                    OddEven_Gen_2.Checked = gen_oddEvenFlags[2];
                    OddEven_Gen_3.Checked = gen_oddEvenFlags[3];
                    OddEven_Gen_4.Checked = gen_oddEvenFlags[4];
                    OddEven_Gen_5.Checked = gen_oddEvenFlags[5];
                    OddEven_Gen_6.Checked = gen_oddEvenFlags[6];
                    flag.CopyTo(gen_oddEvenFlags, 0);


                    flag = new bool[6];

                    for (int i = 0; i < gen_consFlags.Length; i++)
                        gen_consFlags[i] = (buffer[17] & (0x80 >> i)) != 0x00;

                    gen_consFlags.CopyTo(flag, 0);
                    Cons_Gen_0.Checked = gen_consFlags[0];
                    Cons_Gen_1.Checked = gen_consFlags[1];
                    Cons_Gen_2.Checked = gen_consFlags[2];
                    Cons_Gen_3.Checked = gen_consFlags[3];
                    Cons_Gen_4.Checked = gen_consFlags[4];
                    Cons_Gen_5.Checked = gen_consFlags[5];
                    flag.CopyTo(gen_consFlags, 0);

                }
            }
        }
        private void Button_SaveCondition_Gen_Click(object sender, EventArgs e)
        {
            saveGenConditions();
        }
        private void Button_LoadCondition_Gen_Click(object sender, EventArgs e)
        {
            loadGenConditions();
        }
        private void saveSearchAsExcel()
        {
            String filePath = "";
            using (var dlg = new SaveFileDialog())
            {
                dlg.InitialDirectory = Application.StartupPath;
                dlg.Filter = "xlsx files (*.xlsx)|*.xlsx";
                dlg.Title = "검색 결과 엑셀로 저장";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.ShowDialog();

                if (dlg.FileName != "")
                {
                    filePath = dlg.FileName;
                }
            }


            var app = new Excel.Application();
            Excel.Workbook workBook = app.Workbooks.Add("");
            Excel._Worksheet workSheet = workBook.ActiveSheet;
            
            for (int i = 0; i < gameSearchTable.Columns.Count; i++)
            {
                workSheet.Cells[1, i + 1] = gameSearchTable.Columns[i].ColumnName;
            }
            for (int i = 0; i < gameSearchTable.Rows.Count; i++)
            {
                var row = gameSearchTable.Rows[i];
                for (int j = 0; j < gameSearchTable.Columns.Count; j++)
                {
                    String temp = row[j].ToString();
                    if (temp.Contains("홀수") || temp.Contains("짝수"))
                    {
                        temp = temp.Replace(" : 홀수 ", "   ");
                        temp = temp.Replace(" : 짝수 ", "   ");
                    }
                    workSheet.Cells[i + 2, j + 1] = temp;
                }
            }
            if (File.Exists(filePath))
                File.Delete(filePath);
            workBook.SaveAs(filePath);
            workBook.Close();
            app.Quit();
            collect();

        }
        private void Button_SaveAsExcel_Data_Click(object sender, EventArgs e)
        {
            saveSearchAsExcel();
        }

        private void setFixNum()
        {
            using (var dlg = new NumFixForm(gen_fixed, gen_exclude))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.Fixed.CopyTo(gen_fixed, 0);
                    dlg.Excluded.CopyTo(gen_exclude, 0);
                }
            }
        }
        private void FixedNum_Click(object sender, EventArgs e)
        {
            setFixNum();   
        }
        private void showStats()
        {
            using (var form = new statForm(ref gameDataTable))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        private void Button_Stats_Click(object sender, EventArgs e)
        {
            showStats();
        }
        private void clearDuplicated()
        {
            List<int> toDelete = new List<int>();
            for (int i = 0; i < genDataTable.Rows.Count-1; i++)
            {
                if (toDelete.Contains(i))
                    continue;
                for (int j = i + 1; j < genDataTable.Rows.Count; j++)
                {
                    
                    Byte[] numA = ToWinNum((String)genDataTable.Rows[i][1]);
                    Byte[] numB = ToWinNum((String)genDataTable.Rows[j][1]);

                    for (int k = 0; k < 6; k++)
                    {
                        if (numA[k] != numB[k])
                            break;
                        if (k == 5)
                            toDelete.Add(j);
                    }
                }
            }

            for (int i = toDelete.Count - 1; i >= 0; i--)
                genDataTable.Rows.RemoveAt(toDelete[i]);

        }
        private void Button_DupClear_Click(object sender, EventArgs e)
        {
            clearDuplicated();
        }
    }
}
