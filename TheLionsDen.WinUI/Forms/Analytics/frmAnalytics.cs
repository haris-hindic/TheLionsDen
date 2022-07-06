using System.Data;
using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Analytics
{
    public partial class frmAnalytics : Form
    {
        private AnalyticsAPI analyticsAPI;
        public frmAnalytics()
        {
            InitializeComponent();
            analyticsAPI = new AnalyticsAPI();
            formsPlot1.Plot.Style(ScottPlot.Style.Seaborn);
            formsPlot2.Plot.Style(ScottPlot.Style.Seaborn);
        }

        private void frmAnalytics_Load(object sender, EventArgs e)
        {
            cmbChart.DataSource = cmbHelper.charts;

            loadEmployeePerJobType();
        }

        private async void loadEmployeePerJobType()
        {
            formsPlot2.Hide();
            formsPlot1.Show();
            formsPlot1.Plot.Clear();
            var response = await analyticsAPI.GetEmployeesPerRoomType();

            var labels = Enumerable.Range(0, response.Count)
                                   .Select(i => $"{response[i].Label}\n({response[i].Value})")
                                   .ToArray();

            var pie = formsPlot1.Plot.AddPie(response.Select(x => (double)x.Value).ToArray());

            pie.SliceLabels = labels;
            pie.ShowLabels = true;
            formsPlot1.Plot.Legend();
            formsPlot1.Refresh();

            formsPlot1.Plot.YLabel("");
            formsPlot1.Plot.XLabel("");
            groupBox2.Text = "Employees per Job Type";
        }

        private async void loadRevenuePerRoomType()
        {
            formsPlot2.Hide();
            formsPlot1.Show();
            formsPlot1.Plot.Clear();
            var response = await analyticsAPI.RevenuePerRoomType();

            var labels = Enumerable.Range(0, response.Count)
                                   .Select(i => $"{response[i].Label}\n({response[i].Value}) $")
                                   .ToArray();

            var pie = formsPlot1.Plot.AddPie(response.Select(x => (double)x.Value).ToArray());

            pie.SliceLabels = labels;
            pie.ShowValues = true;
            formsPlot1.Plot.Legend();

            formsPlot1.Plot.YLabel("");
            formsPlot1.Plot.XLabel("");
            formsPlot1.Refresh();
            groupBox2.Text = "Revenue per room type ($)";
        }

        private async void loadRoomsPerRoomType()
        {
            formsPlot2.Hide();
            formsPlot1.Show();
            formsPlot1.Plot.Clear();
            var response = await analyticsAPI.RoomsPerRoomType();


            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Aurora;
            double[] values = response.Select(x => (double)x.Value).ToArray();

            var gauges = formsPlot1.Plot.AddRadialGauge(values);
            gauges.Labels = Enumerable.Range(0, response.Count)
                                   .Select(i => $"{response[i].Label}\n({response[i].Value})")
                                   .ToArray();
            formsPlot1.Plot.Legend(true);
            gauges.CircularBackground = false;
            gauges.StartingAngle = 180;
            gauges.Font.Color = Color.Black;

            formsPlot1.Plot.YLabel("");
            formsPlot1.Plot.XLabel("");


            formsPlot1.Refresh();
            groupBox2.Text = "Rooms per room type";
        }

        private async void loadYearlyRevenue()
        {
            formsPlot1.Hide();
            formsPlot2.Show();
            formsPlot2.Plot.Clear();
            var response = await analyticsAPI.YearlyRevenue();

            double[] revenue = response.Select(x => x.xValue).ToArray();
            double[] years = response.Select(x => x.yValue).ToArray();

            double[] positions = GeneratePositions(revenue.Length);
            formsPlot2.Plot.AddScatter(positions, years);

            string[] labels = revenue.Select(x => x.ToString()).ToArray();
            formsPlot2.Plot.XAxis.ManualTickPositions(positions, labels);
            formsPlot2.Plot.XAxis.TickLabelStyle(rotation: 45);
            formsPlot2.Plot.XAxis.SetSizeLimit(min: 50); 

            formsPlot2.Plot.YLabel("Revenue ($)");
            formsPlot2.Plot.XLabel("Timespan");

            

            formsPlot2.Refresh();
            groupBox2.Text = "Last 5 years revenue";
        }

        private double[] GeneratePositions(int length)
        {
            var result = new List<double>();

            for (int i = 0; i < length; i++)
            {
                result.Add(i + 1);
            }

            return result.ToArray();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var selectedValue = cmbChart.SelectedValue as string;
            if (!String.IsNullOrEmpty(selectedValue))
            { 
                switch (selectedValue)
                {
                    case "Display Employees per Job Type":
                        loadEmployeePerJobType();
                        break;
                    case "Display rooms per room type":
                        loadRoomsPerRoomType();
                        break;
                    case "Display last 5 years revenue":
                        loadYearlyRevenue();
                        break;
                    case "Display revenue per room type":
                        loadRevenuePerRoomType();
                        break;
                }
            }
        }
    }
}
