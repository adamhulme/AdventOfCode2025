using ScottPlot;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Create a ScottPlot control
            var formsPlot = new ScottPlot.WinForms.FormsPlot
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(formsPlot);

            var filename = "D:\\projects\\adventofcode2025\\day9\\in.txt";
            var lines = File.ReadAllLines(filename);
            double[] xs = new double[lines.Length];
            double[] ys = new double[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var split = lines[i].Split(',');
                xs[i] = double.Parse(split[0]);
                ys[i] = -double.Parse(split[1]);
            }

            formsPlot.Plot.Add.Scatter(xs, ys);

            formsPlot.Plot.Title("Scatter Plot Example");
            formsPlot.Plot.XLabel("X Axis");
            formsPlot.Plot.YLabel("Y Axis");

            formsPlot.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
