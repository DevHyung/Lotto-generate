using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lotto
{
    public partial class statForm : Form
    {
        private DataTable gameDataTable;

        private DataTable statSearchTable;

        private DataTable unShownNumTable;
        private DataTable numFreqTable;
        private int freqMax;
        private DataTable LDigitTable;
        private int LDigitMax;
        private DataTable sumTable;
        private int sumMax;
        private DataTable Mod5Table;
        private int mod5Max;

        private DataTable highLowTable;
        private int highLowMax;
        private DataTable oddEvenTable;
        private int oddEvenMax;
        private DataTable consTable;
        private int consMax;
        private DataTable frontTable;
        private int frontMax;
        private DataTable rearTable;
        private int rearMax;
        private DataTable MSumTable;
        private int MSumMax;
        private DataTable LSumTable;
        private int LSumMax;
        private DataTable distTable;
        private int distMax;
        private DataTable firstLastTable;
        private int flMax;
        private List<lottoStat> stats = new List<lottoStat>();

        private bool parseWithBonus = false;

        public statForm(ref DataTable gameData)
        {
            InitializeComponent();
            gameDataTable = gameData;
            SearchFrom.Maximum = SearchTo.Maximum = gameData.Rows.Count;
            SearchTo.Value = SearchTo.Maximum;
            SearchFrom.Value = SearchTo.Value - 15;
            LastPeriod.Value = 15;


            initTables();
            defaultSearch();
            
        }
        private void initTables()
        {
            statSearchTable = getTable(new string[] { "회차", "당첨 번호", "끝수", "같은 끝수" },
               new string[] { "System.UInt16", "System.String", "System.String", "System.String" });

            String[] strstr = new string[] { "System.String", "System.String" };

            String[] conts;

            unShownNumTable = getTable(new String[] { "구간 구분", "미출현 번호" },
               strstr);
            conts = new String[5] { "1(1~10)번대", "10(11~20)번대", "20(21~30)번대", "30(31~40)번대", "40(41~45)번대" };
            setTable(ref unShownNumTable, conts);
            UnShownGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UnShownGridView.DataSource = unShownNumTable;
            

            numFreqTable = getTable(new String[] { "번호", "출현 누적 횟수" },
                new String[] { "System.Int16", "System.String" });
            conts = new string[45];
            for (int i = 0; i < 45; i++)
                conts[i] = (i + 1).ToString();
            setTable(ref numFreqTable, conts);
            NumFreqView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            NumFreqView.DataSource = numFreqTable;

            LDigitTable = getTable(new String[] { "끝수", "출현 누적 횟수" },
                new String[] { "System.Int16", "System.String" });
            conts = new string[10];
            for (int i = 0; i < 10; i++)
                conts[i] = i.ToString();
            setTable(ref LDigitTable, conts);
            LDigitView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LDigitView.DataSource = LDigitTable;

            sumTable = getTable(new String[] { "총합", "출현 횟수" },
                strstr);
            conts = new string[11];
            conts[0] = "21 ~ 50";
            for (int i = 1; i <= 9; i++)
                conts[i] = (31 + i * 20).ToString() + " ~ " + (50 + i * 20).ToString();
            conts[10] = "231 ~ 255";
            setTable(ref sumTable, conts);
            SumFreqView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            SumFreqView.DataSource = sumTable;


            Mod5Table = getTable(new String[] { "5번대 구간", "출현 횟수" },
               strstr);
            conts = new string[9];
            for (int i = 0; i < 9; i++)
                conts[i] = (1 + i * 5).ToString() + " ~ " + (5 + i * 5).ToString();
            setTable(ref Mod5Table, conts);
            Mod5FreqView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Mod5FreqView.DataSource = Mod5Table;


            highLowTable = getTable(new String[] { "저고율", "출현 횟수" },
           strstr);
            conts = new string[7];
            for (int i = 0; i < 7; i++)
                conts[i] = "저" + i.ToString() + " : 고" + (6 - i).ToString();
            setTable(ref highLowTable, conts);
            HighLowView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            HighLowView.DataSource = highLowTable;

            oddEvenTable = getTable(new String[] { "홀짝율", "출현 횟수" },
            strstr);
            conts = new string[7];
            for (int i = 0; i < 7; i++)
                conts[i] = "홀" + i.ToString() + " : 짝" + (6 - i).ToString();
            setTable(ref oddEvenTable, conts);
            OddEvenView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            OddEvenView.DataSource = oddEvenTable;


            consTable = getTable(new String[] { "연번", "출현 횟수" },
            strstr);
            conts = new string[6];
            conts[0] = "없음";
            for (int i = 1; i < 6; i++)
                conts[i] = i.ToString() + "쌍";
            setTable(ref consTable, conts);
            ConsView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ConsView.DataSource = consTable;

            frontTable = getTable(new String[] { "123합", "출현 횟수" },
            strstr);
            conts = new String[7];
            for (int i = 0; i < 6; i++)
                conts[i] = (6 + i * 17).ToString() + " ~ " + (22 + i * 17).ToString();
            conts[6] = "108 ~ 123";
            setTable(ref frontTable, conts);
            FrontSumView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            FrontSumView.DataSource = frontTable;


            rearTable = getTable(new String[] { "456합", "출현 횟수" },
            strstr);
            conts = new string[7];
            for (int i = 0; i < 6; i++)
                conts[i] = (15 + i * 17).ToString() + " ~ " + (31 + i * 17).ToString();
            conts[6] = "117 ~ 132";
            setTable(ref rearTable, conts);
            RearView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            RearView.DataSource = rearTable;

            MSumTable = getTable(new String[] { "첫수합", "출현 횟수" },
            strstr);
            conts = new string[8];
            for (int i = 0; i < 8; i++)
                conts[i] = (i * 5).ToString() + " ~ " + (4 + i * 5).ToString();
            setTable(ref MSumTable, conts);
            MSumView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            MSumView.DataSource = MSumTable;


            LSumTable = getTable(new String[] { "끝수합", "출현 횟수" },
             strstr);
            setTable(ref LSumTable,
                 new string[5] { "2 ~ 10", "11 ~ 20", "21 ~ 30", "31 ~ 40", "41 ~ 52" });
            LSumView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LSumView.DataSource = LSumTable;

            distTable = getTable(new String[] { "간격합", "출현 횟수" },
             strstr);
            conts = new string[7];
            conts[0] = "5 ~ 15";
            for (int i = 1; i < 6; i++)
                conts[i] = (11 + i * 5).ToString() + " ~ " + (15 + i * 5).ToString();
            conts[6] = "41 ~ 44";
            setTable(ref distTable, conts);
            DistView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DistView.DataSource = distTable;

            firstLastTable = getTable(new String[] { "고저합", "출현 횟수" },
             strstr);
            conts = new String[7];
            for (int i = 0; i < 7; i++)
                conts[i] = (7 + i * 11).ToString() + " ~ " + (17 + i * 11).ToString();
            setTable(ref firstLastTable, conts);
            FirstLastView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            FirstLastView.DataSource = firstLastTable;
            
            collect();

        }
        private DataTable getTable(String[] columnName, String[] columnType)
        {
            DataTable table = new DataTable();
            DataColumn column;

            for (int i = 0; i < columnName.Length; i++)
            {
                column = new DataColumn(columnName[i]);
                column.AllowDBNull = false;
                column.DataType = System.Type.GetType(columnType[i]);
                table.Columns.Add(column);
            }
            collect();
            return table;
        }
        private void setTable(ref DataTable table, String[] conts)
        {
            DataRow row;
            foreach (var cont in conts)
            {
                row = table.NewRow();
                row[0] = cont;
                row[1] = "";
                table.Rows.Add(row);
            }
            collect();
        }
        private void collect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private Byte[] ToWinNum(String winNumStr, bool withBonus)
        {
            Byte[] winNum;
            List<String> buffer = winNumStr.Split().ToList();
            buffer.RemoveAll(x => x.Length == 0);

            if (withBonus)
                winNum = new Byte[7];
            else
                winNum = new Byte[6];

            int i = 0;
            foreach (var buff in buffer)
            {
                if (i >= winNum.Length)
                    break;
                if (Byte.TryParse(buff, out winNum[i]))
                    i++;
                else
                    continue;
            }
            if (withBonus)
                winNum[6] = Byte.Parse(buffer[6].Substring(1, buffer[6].Length-2));

            return winNum;
        }
        delegate void tableSetter(ref int[] arr, ref int max, ref DataTable table);
        delegate void viewSetter(ref DataGridView view);
        private void setByStats()
        {
            dataGridView1.DataSource = statSearchTable;

            freqMax = 0;
            int[] valFreq = new int[45];
            int[] lDigitFreq = new int[10];
            int[] sumFreq = new int[11];
            int[] Mod5Freq = new int[9];
            int[] highLowFreq = new int[7];
            int[] oddEvenFreq = new int[7];
            int[] consFreq = new int[6];
            int[] frontFreq = new int[7];
            int[] rearFreq = new int[7];
            int[] MSumFreq = new int[8];
            int[] LSumFreq = new int[5];
            int[] distFreq = new int[7];
            int[] firstLastFreq = new int[7];

            foreach (var stat in stats)
            {
                foreach (var num in stat.winNum)
                {
                    valFreq[num - 1]++;    // index is num, value is frequency
                    Mod5Freq[(num-1) / 5]++;
                }
                foreach (var num in stat.LDigit)
                    lDigitFreq[num]++;

                int pos = (stat.totalSum - 31) / 20;
                sumFreq[(pos < 0 ? 0 : (pos > 10 ? 10 : pos))]++;

                highLowFreq[stat.highLowRate]++;
                oddEvenFreq[stat.oddEvenRate]++;
                consFreq[stat.consCnt]++;

                frontFreq[(stat.frontSum - 6) / 17]++;
                rearFreq[(stat.rearSum - 15)/17]++;
                MSumFreq[stat.MDigitSum / 5]++;
                LSumFreq[(stat.LDigitSum-1) / 10]++;
                if (stat.distSum <= 15)
                    distFreq[0]++;
                else
                    distFreq[(stat.distSum - 11) / 5]++;
                firstLastFreq[(stat.firstLastSum - 7) / 11]++;
            }

            for (int i = 0; i < valFreq.Length; i++)
            {
                if (valFreq[i] == 0)
                    unShownNumTable.Rows[i / 10]["미출현 번호"] += (i + 1).ToString().PadLeft(4);
                numFreqTable.Rows[i]["출현 누적 횟수"] = valFreq[i].ToString() + "회";
                if (freqMax < valFreq[i])
                    freqMax = valFreq[i];
            }

            tableSetter l = (ref int[] arr, ref int max, ref DataTable table) =>
            {
                max = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    table.Rows[i][1] = arr[i].ToString() + "회";
                    if (max < arr[i])
                        max = arr[i];
                }
            };
            l(ref lDigitFreq, ref LDigitMax, ref LDigitTable);
            l(ref sumFreq, ref sumMax, ref sumTable);
            l(ref Mod5Freq, ref mod5Max, ref Mod5Table);
            l(ref highLowFreq, ref highLowMax, ref highLowTable);
            l(ref oddEvenFreq, ref oddEvenMax, ref oddEvenTable);
            l(ref consFreq, ref consMax, ref consTable);
            l(ref frontFreq, ref frontMax, ref frontTable);
            l(ref rearFreq, ref rearMax, ref rearTable);
            l(ref MSumFreq, ref MSumMax, ref MSumTable);
            l(ref LSumFreq, ref LSumMax, ref LSumTable);
            l(ref distFreq, ref distMax, ref distTable);
            l(ref firstLastFreq, ref flMax, ref firstLastTable);

            viewSetter sort = (ref DataGridView view) => { view.Sort(view.Columns[1], ListSortDirection.Descending); };
            sort(ref NumFreqView);
            sort(ref LDigitView);
            sort(ref SumFreqView);
            
            collect();
        }
        private void defaultSearch()
        {
            searchLast();
        }

        private void clear()
        {
            stats.Clear();
            statSearchTable.Rows.Clear();

            foreach (DataRow row in unShownNumTable.Rows)
                row["미출현 번호"] = "";

            foreach (DataRow row in numFreqTable.Rows)
                row[1] = "";
            foreach (DataRow row in LDigitTable.Rows)
                row[1] = "";
            foreach (DataRow row in sumTable.Rows)
                row[1] = "";
            foreach (DataRow row in Mod5Table.Rows)
                row[1] = "";
            foreach (DataRow row in highLowTable.Rows)
                row[1] = "";
            foreach (DataRow row in oddEvenTable.Rows)
                row[1] = "";
            foreach (DataRow row in consTable.Rows)
                row[1] = "";
            foreach (DataRow row in frontTable.Rows)
                row[1] = "";
            foreach (DataRow row in rearTable.Rows)
                row[1] = "";
            foreach (DataRow row in MSumTable.Rows)
                row[1] = "";
            foreach (DataRow row in LSumTable.Rows)
                row[1] = "";
            foreach (DataRow row in distTable.Rows)
                row[1] = "";
            foreach (DataRow row in firstLastTable.Rows)
                row[1] = "";
        }
        private void searchLast()
        {
            clear();

            for (int i = 0; i < LastPeriod.Value && i < gameDataTable.Rows.Count; i++)
            {
                var row = gameDataTable.Rows[i];
                var addRow = statSearchTable.NewRow();
                addRow["회차"] = row["회차"];
                addRow["당첨 번호"] = row["당첨 번호"];


                Byte[] winNum = ToWinNum(addRow["당첨 번호"].ToString(), parseWithBonus);
                var stat = new lottoStat(winNum);


                String buffer = "";
                for (int j = 0; j < 5; j++)
                    buffer += stat.LDigit[j].ToString() + " - ";
                buffer += stat.LDigit[5].ToString();
                addRow["끝수"] = buffer;

                Byte[] lCount = new byte[10];
                foreach (var lDigit in stat.LDigit)
                    lCount[lDigit]++;

                buffer = "";
                for (int j = 0; j < 10; j++)
                {
                    if (lCount[j] > 1)
                    {
                        buffer += "( ";
                        foreach (var num in stat.winNum)
                        {
                            if (num % 10 == j)
                                buffer += num.ToString() + ", ";
                        }

                        buffer = buffer.Remove(buffer.Length - 2);
                        buffer += " ) ";
                    }
                }
                addRow["같은 끝수"] = buffer;

                statSearchTable.Rows.Add(addRow);
                stats.Add(new lottoStat(winNum));

            }
            setByStats();
            collect();
        }

        private void statForm_Load(object sender, EventArgs e)
        {
        }
        private void paintCell(ref DataGridViewCellPaintingEventArgs e, double max, Brush brush)
        {
            if (e.ColumnIndex > 0 && e.RowIndex > -1)
            {
                String num = ((String)e.Value);
                num = num.Remove(num.IndexOf("회"));
                double rate = int.Parse(num) / max;
                Rectangle rect = new Rectangle(e.CellBounds.Left + 5, e.CellBounds.Top + 5,
                    (int)((e.CellBounds.Width - 10) * rate), e.CellBounds.Height - 10);
                e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
                e.Graphics.FillRectangle(brush, rect);
                e.Handled = true;
                e.PaintContent(e.CellBounds);
            }
        }
        private void searchByPeriod()
        {
            clear();

            foreach (DataRow row in gameDataTable.Rows)
            {
                if ((UInt16)row[0] < SearchFrom.Value || (UInt16)row[0] > SearchTo.Value)
                    continue;

                var addRow = statSearchTable.NewRow();
                addRow["회차"] = row["회차"];
                addRow["당첨 번호"] = row["당첨 번호"];


                Byte[] winNum = ToWinNum(addRow["당첨 번호"].ToString(), parseWithBonus);
                var stat = new lottoStat(winNum);


                String buffer = "";
                for (int i = 0; i < 5; i++)
                    buffer += stat.LDigit[i].ToString() + " - ";
                buffer += stat.LDigit[5].ToString();
                addRow["끝수"] = buffer;

                Byte[] lCount = new byte[10];
                foreach (var lDigit in stat.LDigit)
                    lCount[lDigit]++;

                buffer = "";
                for (int j = 0; j < 10; j++)
                {
                    if (lCount[j] > 1)
                    {
                        buffer += "( ";
                        foreach (var num in stat.winNum)
                        {
                            if(num % 10 == j)
                            buffer += num.ToString() + ", ";
                        }
                        buffer = buffer.Remove(buffer.Length - 2);
                        buffer += " ) ";
                    }
                }
                addRow["같은 끝수"] = buffer;

                statSearchTable.Rows.Add(addRow);
                stats.Add(new lottoStat(winNum));

            }
            setByStats();
            collect();
        }

        private void SearchByPeriodButton_Click(object sender, EventArgs e)
        {
            searchByPeriod();
        }



        private void NumFreqView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)freqMax, Brushes.LightSkyBlue);
        }

        private void LDigitView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)LDigitMax, Brushes.Aquamarine);
        }

        private void SumFreqView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)sumMax, Brushes.LightSeaGreen);

        }

        private void HighLowView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)highLowMax, Brushes.LightCoral);

        }

        private void OddEvenView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)oddEvenMax, Brushes.LightPink);

        }

        private void ConsView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)consMax, Brushes.Yellow);

        }

        private void Mod5FreqView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)mod5Max, Brushes.LightSlateGray);

        }

        private void FrontSumView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)frontMax, Brushes.LightSalmon);

        }

        private void DistView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)distMax, Brushes.LightSteelBlue);

        }

        private void RearView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)rearMax, Brushes.Violet);

        }

        private void FirstLastView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)flMax, Brushes.PeachPuff);

        }

        private void MSumView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)MSumMax, Brushes.LightSeaGreen);

        }

        private void LSumView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCell(ref e, (double)LSumMax, Brushes.LightCoral);

        }
    }

    public class lottoStat
    {
        public Byte[] winNum;
        public Byte[] LDigit;

        public Byte[] tenthSectionCnt = new Byte[5];
        /*  0 : 1 ~ 10, .., 4 : 41 ~ 45
         * */
        public Byte[] fifthSectionCnt = new Byte[9];
        /* 0 : 1 ~ 5, 1 : 6 ~ 10, ..., 8 : 41 ~ 45
         * */

        public int totalSum;
        public int highLowRate;
        public int oddEvenRate;
        public int consCnt;
        public int frontSum;
        public int rearSum;

        public int MDigitSum;
        public int LDigitSum;
        public int firstLastSum;
        public int distSum;

        public lottoStat(Byte[] nums)
        {
            winNum = nums;
            LDigit = new Byte[winNum.Length];
            for (int i = 0; i < winNum.Length; i++)
            {
                LDigit[i] = (Byte)(winNum[i] % 10);
            }
            for (int i = 0; i < 5; i++)
            {
                if (winNum[i] + 1 == winNum[i + 1])
                    consCnt++;
            }
            

            foreach (var num in winNum)
            {
                tenthSectionCnt[(num - 1) / 10]++;
                fifthSectionCnt[(num - 1) / 5]++;
            }
            totalSum = winNum.Sum(x => x);
            highLowRate = winNum.Sum(x => x < 23 ? 1 : 0);
            oddEvenRate = winNum.Sum(x => x % 2);
            frontSum = winNum[0] + winNum[1] + winNum[2];
            rearSum = winNum[3] + winNum[4] + winNum[5];
            MDigitSum = winNum.Sum(x => x / 10 == 0 ? x : x / 10);
            LDigitSum = LDigit.Sum(x => x);
            firstLastSum = winNum[0] + winNum[5];
            distSum = winNum[5] - winNum[0];
        }
    }
}
